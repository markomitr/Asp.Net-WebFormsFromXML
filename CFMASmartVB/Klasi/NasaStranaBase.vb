Imports System.Web.UI.HtmlControls
Imports System.Threading
Imports System.Globalization
Imports System.IO

Public MustInherit Class NasaStranaBase
    Inherits Page


    Private Shared drzava As String = "MK"

    Private tipStrana As NasEnum.TipPage
    'GLAVNIOT SERVIS
    Dim client As New TestServiceObicen.Service

    'Masina klasata se koristi za vcituvanje,sreduvanje i generiranje na kontrolite
    Dim demoMasina As Masina
    'Masina za MENI
    Dim meniMasina As MasinaMeni
    'Head e delot od stranata kade sto se stavat direkcii i pomosni raboti kako: skripti,metatagovi
    Dim head As HtmlHead
    'Content se koristi kako panel za dodavanje na kontrolte
    Dim contentPC As PlaceHolder
    Dim logoPC As PlaceHolder

    'Kopce za Potvrdi
    Public WithEvents kopcePotvrdi As Button

    'Naslov na reportot
    Dim naslovMaska As String

    'Pateka za vcituvanje za XML
    Dim patekaXML As String = ""

    'Poraka
    Dim msg As String = ""

    'Korisnik Login

    Dim parovIdVrednost As Dictionary(Of String, String)
    Public ReadOnly Property Korisnik As Korisnik
        Get
            If Not Session("Korisnik") Is Nothing Then
                Try
                    Return CType(Session("Korisnik"), Korisnik)
                Catch ex As Exception
                    Return Nothing
                End Try
            Else
                Return Nothing
            End If
        End Get
    End Property
    Public ReadOnly Property ServisNas As TestServiceObicen.Service
        Get
            Return Me.client
        End Get
    End Property
    Public ReadOnly Property ZemiMasina As Masina
        Get
            Return Me.demoMasina
        End Get
    End Property
    Public ReadOnly Property TipNaStranica As NasEnum.TipPage
        Get
            Return Me.tipStrana
        End Get
    End Property
    Public ReadOnly Property ZemiVrednostKontrola(ByVal imeNaKontrola As String)
        Get
            Return Me.vratiVrednostOdKontrola(imeNaKontrola)
        End Get
    End Property
    Public Property NaslovNaMaska As String
        Get
            Dim imeMaska As String = ""
            If Me.TipNaStranica = NasEnum.TipPage.Login Then
                If drzava = "MK" OrElse drzava = "" Then
                    imeMaska = "Логин"
                Else
                    imeMaska = "Login"
                End If

            ElseIf Me.TipNaStranica = NasEnum.TipPage.Meni Then
                If drzava = "MK" OrElse drzava = "" Then
                    imeMaska = "Мени"
                Else
                    imeMaska = "Menu"
                End If
            End If
            Return imeMaska
        End Get
        Set(ByVal value As String)
            naslovMaska = value
        End Set
    End Property
    Public Property PatekaXMLFile As String
        Get
            If Not patekaXML Is Nothing And Not String.IsNullOrEmpty(patekaXML) Then
                Return Me.patekaXML
            Else
                Return Nothing
            End If

        End Get
        Set(ByVal value As String)
            Me.patekaXML = Server.MapPath(String.Empty) & "\XML\" & value
        End Set
    End Property
    Public Property Poraka As String
        Get
            Return Me.msg
        End Get
        Set(ByVal value As String)
            Me.msg = value
        End Set
    End Property
    Public Shared ReadOnly Property KojaDrzava As Drzavi
        Get
            Dim koja As String = ""
            Dim drz As Drzavi = Drzavi.Makedonija
            koja = ZemiParamOdKonfig("Drzava")
            koja = koja.Trim
            koja = koja.ToUpper
            If koja.Trim = "" Then
                koja = "MK"
            End If
            If koja = "MK" Then
                drz = Drzavi.Makedonija
            ElseIf koja = "KS" Then
                drz = Drzavi.Kosovo
            ElseIf koja = "SR" Then
                drz = Drzavi.Srbija
            ElseIf koja = "IT" Then
                drz = Drzavi.Italija
            End If
            Return drz
        End Get
    End Property
    Public Sub New(ByVal patekaXML As String, ByVal stoEStranata As NasEnum.TipPage)
        Me.PatekaXMLFile = patekaXML
        Me.tipStrana = stoEStranata

        parovIdVrednost = New Dictionary(Of String, String)
        kopcePotvrdi = New Button()


    End Sub
    Protected Overrides Sub OnPreInit(ByVal e As System.EventArgs)
        MyBase.OnPreInit(e)

            ' Za logout
            If Not Request.QueryString("lout") Is Nothing Then
                Me.IscistiKorisnikVoSesija()
                'Response.Redirect("Login.aspx")
            End If

            'Proverka dali korisnikot e logiran, ako ne e se vraka na Login page
        If Not Me.TipNaStranica = NasEnum.TipPage.Login Then
            inicLogo() ' se crta kontrolata za Logo
            If Korisnik Is Nothing Then
                Response.Redirect("Login.aspx")
            End If
        End If

            If Me.TipNaStranica = NasEnum.TipPage.Maska Then
                incijalizirajContent() 'Incijalizacija na GlavniHolder na strana
                inicMasina() 'Inicijalizacija za masina koja gi generira kontrolite - Spojuva XML-BAZA i generira Kontroli - ALAT
                inicKopceZaPotvrda() ' Kopce koe se potvrduva
                NacrtajKontroli() ' Generiranje na kontrolite
            ElseIf Me.TipNaStranica = NasEnum.TipPage.Meni Then
                incijalizirajContent()
                inicMasinaMeni()
            NacrtajMeniZaMaski()

            ElseIf Me.TipNaStranica = NasEnum.TipPage.Login Then
            'LOGIRANJE

            End If

    End Sub
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        inicIDodadiLinkojVoHead()

        If Me.TipNaStranica = NasEnum.TipPage.Maska Then
            'Response.BufferOutput = True
            Try

                Dim nFile As New NasFile
                If Not Request.QueryString Is Nothing Then
                    If Not IsPostBack Then

                        If Not Request.QueryString("Zemi") Is Nothing And Not String.IsNullOrEmpty(Request.QueryString("Zemi")) Then
                            If Not nFile.ZemiFileLokalno(Server.UrlDecode(Request.QueryString("zemi").ToString()), Server.UrlDecode(Request.QueryString("zemi")), Me.Response, Me.Poraka) Then
                                Me.DodadiInformacija(Poraka)
                            End If

                            'Try
                            '    Dim pateka As String = Server.UrlDecode(Request.QueryString("Zemi").ToString())
                            '    Dim gol As Integer = 0
                            '    Dim imeFile As String = Path.GetFileName(pateka)
                            '    Dim fileb As Byte() = ServisNas.GetPDF(pateka, gol, Poraka)
                            '    If Not fileb Is Nothing AndAlso fileb.Length > 0 AndAlso fileb.Length = gol Then
                            '        If Not nFile.PratiFileOdByte(fileb, imeFile, Me.Response, Me.Poraka) Then
                            '            Me.DodadiInformacija(Poraka)
                            '        End If
                            '    End If
                            'Catch ex As Exception

                            'End Try
                           
                          
                        End If
                    End If
                End If

            Catch ex As Exception
                Me.DodadiInformacija(ex.Message)
            End Try

        End If
    End Sub
    Private Sub inicMasina()
        demoMasina = New Masina(Me)
    End Sub
    Private Sub inicMasinaMeni()
        meniMasina = New MasinaMeni(contentPC)
    End Sub
    Private Sub incijalizirajContent()
        'PlaceHolder vo koj se stavaat Kontrolite
        contentPC = New PlaceHolder
        contentPC.ID = "Content"

        If Not Me.Page.FindControl("form1") Is Nothing Then
            Me.Page.FindControl("form1").Controls.Add(contentPC)
        End If
        'CType(Me.Page.FindControl("form1"), HtmlForm).Attributes.Add("onsubmit", "proveriSUBMIT()")
    End Sub
    Private Sub inicLogo()
        logoPC = New PlaceHolder()
        logoPC.ID = "LogoHeader"
        If Not Me.Page.FindControl("form1") Is Nothing Then
            Me.Page.FindControl("form1").Controls.Add(logoPC)
        End If
        logoPC.Controls.Add(New LiteralControl("<div id=""LogoHeader"">"))
        logoPC.Controls.Add(New LiteralControl("<img src=""Sliki/Logo.jpg""/>"))
        logoPC.Controls.Add(New LiteralControl("</div>"))
    End Sub
    Private Sub inicKopceZaPotvrda()

        If kopcePotvrdi Is Nothing Then
            kopcePotvrdi = New Button()
        End If

        kopcePotvrdi.ID = "btnPrikazi"
        kopcePotvrdi.Attributes.Add("class", "css3button")

        Dim txtKopce = ""

        If drzava.ToUpper() = "MK" Then
            txtKopce = My.Resources.GlavnaMaska_MK.txt_kopce_potvrdi_glavno
        ElseIf drzava.ToUpper() = "KS" Then
            txtKopce = My.Resources.GlavnaMaska_KS.txt_kopce_potvrdi_glavno
        End If

        kopcePotvrdi.Text = txtKopce
    End Sub
    Private Sub inicIDodadiLinkojVoHead()
        'Head vo koi se stavaat linkovi do skripti i metatagovi
        Me.head = Me.Page.Header

        If Me.head Is Nothing Then
            Me.DodadiInformacija("Nema HEAD") ' Prevod
        End If

        Me.head.Controls.Add(New LiteralControl(Me.linkojZaHead.ToString))
        Me.head.Title = NaslovNaMaska
    End Sub
    Private Function linkojZaHead() As StringBuilder
        Dim sBild As New StringBuilder
        sBild.AppendLine()
        'MNOGU VAZEN TAG - za da se prilagodouva na screen size
        sBild.AppendLine("<meta name=""viewport"" content=""width=device-width; initial-scale=1.0"" />")
        'Skripti
        sBild.AppendLine("<link rel=""icon"" href=""Sliki/favicon.ico""/>")
        sBild.AppendLine("<script src=""Scripts/json2.js"" type=""text/javascript"" ></script>")
        sBild.AppendLine("<script type=""text/javascript"" src=""http://code.jquery.com/jquery-1.8.2.min.js""></script>")
        sBild.AppendLine("<script type=""text/javascript"" src=""http://code.jquery.com/ui/1.9.0/jquery-ui.js""></script>")
        sBild.AppendLine("<script src=""Scripts/css3-mediaqueries.js"" type=""text/javascript""></script>")
        sBild.AppendLine("<script src=""Scripts/Poraki.js"" type=""text/javascript""></script>")
        sBild.AppendLine("<script src=""Scripts/Klasi.js"" type=""text/javascript""></script>")
        sBild.AppendLine("<script src=""Scripts/Glavna.js"" type=""text/javascript""></script>")
        sBild.AppendLine("<script src=""Scripts/Proverki.js"" type=""text/javascript""></script>")
        sBild.AppendLine("<script src=""Scripts/InicNas.js"" type=""text/javascript""></script>")
        'Stilovi
        sBild.AppendLine("<link rel=""stylesheet"" href=""http://code.jquery.com/ui/1.9.0/themes/base/jquery-ui.css"" />")
        sBild.AppendLine("<link href=""Stilovi/Glaven.css"" rel=""stylesheet"" type=""text/css"" />")
        sBild.AppendLine()

        Return sBild
    End Function
    Private Sub NacrtajKontroli()

        If Not contentPC Is Nothing Then

            contentPC.Controls.Clear()

            contentPC.Controls.Add(New LiteralControl("<div id=""Content"">"))
            contentPC.Controls.Add(New LiteralControl("<a id=""LinkMeni"" href=""Meni.aspx"">"))
            contentPC.Controls.Add(New LiteralControl("<img alt=""Meni"" src=""Sliki/StrelkaNazad.png"" />"))
            contentPC.Controls.Add(New LiteralControl("</a>"))
            contentPC.Controls.Add(New LiteralControl("<a id=""LinkMeni"" style=""margin-left:25px;"" href=""" + Request.Url.AbsolutePath + "?lout=D"">"))
            contentPC.Controls.Add(New LiteralControl("<img alt=""LOUT"" src=""Sliki/Lout.png"" />"))
            contentPC.Controls.Add(New LiteralControl("</a>"))
            'contentPC.Controls.Add(New LiteralControl("<div id=""Naslov"">" & Me.NaslovNaMaska & "</div>"))

            If Not Me.PatekaXMLFile Is Nothing Then
                If Not demoMasina.ProcitajXML(Me.PatekaXMLFile, demoMasina.PorakiErrLista(demoMasina.DajMiBrojPorakaZaVnes)) _
                Or Not demoMasina.ProcitajOdBaza(demoMasina.PorakiErrLista(demoMasina.DajMiBrojPorakaZaVnes)) _
                Then
                    Me.DodadiInformacija(demoMasina.PorakiErr)
                Else
                    Me.NaslovNaMaska = demoMasina.MaskaNaslov

                    contentPC.Controls.Add(New LiteralControl("<div id=""NaslovMaska""><span>" & Me.NaslovNaMaska.ToUpper() & "</span></div>"))

                    If Not demoMasina.Raboti(contentPC) Then
                        Me.DodadiInformacija(demoMasina.PorakiErr)
                    End If

                End If

                contentPC.Controls.Add(kopcePotvrdi) ' Se dodava glavnoto kopce za potvrdi
            Else
                Me.DodadiInformacija("Nema pateka za XML!") 'Prevod
            End If
            contentPC.Controls.Add(New LiteralControl("</div>"))
        Else
            Me.DodadiInformacija("Nema kontrola(placeholder) vo koj ke se postavat kontrolite!") 'Prevod
        End If
    End Sub
    Private Sub NacrtajMeniZaMaski()
        contentPC.Controls.Add(New LiteralControl("<div id=""Content"">"))
        contentPC.Controls.Add(New LiteralControl("<a id=""LinkMeni"" href=""Meni.aspx"">"))
        contentPC.Controls.Add(New LiteralControl("<img alt=""Meni"" src=""Sliki/StrelkaNazad.png"" />"))
        contentPC.Controls.Add(New LiteralControl("</a>"))
        contentPC.Controls.Add(New LiteralControl("<a id=""LinkMeni"" style=""margin-left:25px;"" href=""" + Request.Url.AbsolutePath + "?lout=D"">"))
        contentPC.Controls.Add(New LiteralControl("<img alt=""LOUT"" src=""Sliki/Lout.png"" />"))
        contentPC.Controls.Add(New LiteralControl("</a>"))
        contentPC.Controls.Add(New LiteralControl("<div id=""NaslovMaska"">МЕНИ</div>"))
        If Not meniMasina.GenerirajMeni(Me.PatekaXMLFile, Me.Poraka) Then
            Me.DodadiInformacija(Poraka)
        End If
        contentPC.Controls.Add(New LiteralControl("</div>"))
    End Sub
    ''' <summary>
    ''' Collects all the Values from the controls within the page
    ''' Every control implements NasiKlasi interface which abstracts the value property of every control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ZemiVrednostiOdKontroli()

        For Each pom As KontrolaOpis In Me.ZemiMasina.ZemiListaKontrOpis

            If Not Me.Page.FindControl(pom.SorsID) Is Nothing Or Not Me.Page.FindControl(pom.XML.NaslovGrupa & pom.XML.Grupa) Is Nothing Then

                Dim id = ""
                If Not Me.Page.FindControl(pom.SorsID) Is Nothing Then
                    id = pom.SorsID
                ElseIf Not Me.Page.FindControl(pom.XML.NaslovGrupa & pom.XML.Grupa) Is Nothing Then
                    id = pom.XML.NaslovGrupa & pom.XML.Grupa
                End If

                If Not TryCast(Page.FindControl(id), NasaLista) Is Nothing Then

                    Dim demo = CType(Me.Page.FindControl(id), NasaLista)

                    If Not parovIdVrednost.ContainsKey(id) Then
                        parovIdVrednost.Add(demo.IDKontrola.ToUpper(), demo.Vrednost)
                    End If


                ElseIf Not TryCast(Page.FindControl(id), NasTextBox) Is Nothing Then

                    Dim demo = CType(Me.Page.FindControl(id), NasTextBox)

                    parovIdVrednost.Add(demo.IDKontrola.ToUpper(), demo.Vrednost)
                ElseIf Not TryCast(Page.FindControl(id), DatumIzbor) Is Nothing Then

                    Dim demo = CType(Page.FindControl(id), DatumIzbor)

                    parovIdVrednost.Add(demo.IDKontrola.ToUpper(), demo.Vrednost)
                ElseIf Not TryCast(Page.FindControl(id), NasCheckBoxUC) Is Nothing Then

                    Dim demo = CType(Page.FindControl(id), NasCheckBoxUC)

                    parovIdVrednost.Add(demo.IDKontrola.ToUpper(), demo.Vrednost)
                ElseIf Not TryCast(Page.FindControl(id), NasRadioButtonUC) Is Nothing Then

                    Dim demo = CType(Page.FindControl(id), NasRadioButtonUC)

                    parovIdVrednost.Add(demo.IDKontrola.ToUpper(), demo.Vrednost)

                End If

            End If
        Next
    End Sub
    Public Sub PrevzemiDokument(ByVal pateka As String)
        Dim ime() As String = pateka.Split("\")

        If Not Request.QueryString.AllKeys.Contains("Zemi") Then
            Response.Redirect(DajTocnoURL() + "?Zemi=" + Server.UrlEncode(pateka))
        Else
            Dim url() As String = Request.Url.AbsoluteUri.Split("?")
            Response.Redirect(DajTocnoURL() + "?Zemi=" + Server.UrlEncode(pateka))          
        End If
    End Sub
    Private Function vratiVrednostOdKontrola(ByVal kojaKontrolaIme As String) As String
        If parovIdVrednost Is Nothing Or parovIdVrednost.Count < 1 Then
            ZemiVrednostiOdKontroli()
        End If
        If Not parovIdVrednost Is Nothing And parovIdVrednost.Count > 0 Then
            Try
                Return parovIdVrednost(kojaKontrolaIme.ToUpper())
            Catch ex As Exception
                Return Nothing
            End Try

        End If

        Return ""
    End Function
    Public Sub dodadiKorisnikVoSesija(ByVal korObj As Korisnik)
        Session("Korisnik") = korObj
    End Sub
    Private Sub IscistiKorisnikVoSesija()
        If Not Session("Korisnik") Is Nothing Then
            Session("Korisnik") = Nothing
            Session.Clear()
        End If
    End Sub
    Public Sub DodadiInformacija(ByVal poraka As String)
        Dim pom As New StringBuilder
        pom.AppendLine("<div id=""dialog"" title=""" & "ИНФО - Порака!" & """>")
        pom.AppendLine("<p>")
        pom.AppendLine(poraka)
        pom.AppendLine("</p>")
        pom.AppendLine("</div>")
        If Not Me.Page.FindControl("form1") Is Nothing Then
            Me.Page.FindControl("form1").Controls.Add(New LiteralControl(pom.ToString()))
        End If
    End Sub
    'Momentalno ne se koristi,prebaruva vrednost/kontrola vo Strana(Page)!
    Private Function NajdiParametar(ByVal vrednost As String) As Integer
        If String.IsNullOrEmpty(vrednost) Then

            Return -1
        End If

        Dim kolekcija As String() = Request.Form.AllKeys

        For i As Integer = 0 To kolekcija.Length - 1
            If kolekcija(i).EndsWith(vrednost) Then
                Return i
            End If
        Next
        Return -1
    End Function
    Private Function DajTocnoURL() As String
        Dim protok As String = Request.Url.Scheme
        Dim adresa As String = Request.Url.Host
        Dim strana As String = Request.Url.LocalPath
        Dim port As String = Request.Url.Port
        Dim celaPateka = protok & "://" & adresa & ":" & "8080" & strana
        Return celaPateka
        'If port = 80 Then
        '    Return
        'Else
        '    Return
        'End If

    End Function
    Public Shared Function ZemiParamOdKonfig(ByVal param As String) As String
        Dim vrednost As String = ""
        Try
            vrednost = Konv.ObjVoStr(ConfigurationManager.AppSettings(param))
        Catch ex As Exception
            vrednost = ""
        End Try
        Return vrednost
    End Function

    Public MustOverride Sub KopceKlik(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kopcePotvrdi.Click
    Public Enum Drzavi
        Makedonija
        Kosovo
        Srbija
        Italija
    End Enum
End Class
