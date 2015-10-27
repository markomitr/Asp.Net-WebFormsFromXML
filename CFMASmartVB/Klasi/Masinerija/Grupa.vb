Public Class Grupa

    Dim brGr As Integer
    Dim nasGr As String
    Dim listaKontroli As List(Of KontrolaOpis)

    Dim daliPrikazi As Boolean = False

    Private webStranaSodrizna As Control
    Private daliWebPAGESodrizna As Boolean = False

    Private htmlSodrzina As HtmlGenericControl
    Private daliHtmlSodrzina As Boolean = False

    Private tipGrp As NasEnum.TipGrupa = NasEnum.TipGrupa.Obicna

    Private Property SodrzinaVoPAGE As Control
        Get
            Return Me.webStranaSodrizna
        End Get
        Set(ByVal value As Control)

            Me.daliHtmlSodrzina = False
            Me.daliWebPAGESodrizna = True

            Me.webStranaSodrizna = value


        End Set
    End Property

    Private Property SodrzinaVoHTML As HtmlGenericControl
        Get
            Return Me.htmlSodrzina

        End Get
        Set(ByVal value As HtmlGenericControl)

            Me.daliWebPAGESodrizna = False
            Me.daliHtmlSodrzina = True


            Me.htmlSodrzina = value


        End Set
    End Property

    Public ReadOnly Property BrojGrupa As Integer
        Get
            Return Me.brGr
        End Get
    End Property

    Public ReadOnly Property Kontroli As List(Of KontrolaOpis)
        Get
            Return Me.listaKontroli
        End Get
    End Property

    Private WriteOnly Property GrupaBr As Integer
        Set(ByVal value As Integer)
            If (value = 1) Then
                Me.daliPrikazi = True
            End If

            Me.brGr = value
        End Set
    End Property

    Public Sub New(ByVal htmlControl As HtmlGenericControl, ByVal tipGrupa As NasEnum.TipGrupa)
        Me.New(tipGrupa)

        Me.SodrzinaVoHTML = htmlControl


    End Sub


    Public Sub New(ByVal netControl As Control, ByVal tipGrupa As NasEnum.TipGrupa)
        Me.New(tipGrupa)

        Me.SodrzinaVoPAGE = netControl


    End Sub


    Public Sub New(ByVal htmlControl As HtmlGenericControl, ByVal brGrupa As Integer, ByVal naslovGrupa As String, ByVal tipGrupa As NasEnum.TipGrupa)
        Me.New(htmlControl, tipGrupa)

        Me.GrupaBr = brGrupa
        Me.nasGr = naslovGrupa



    End Sub

    Public Sub New(ByVal netControl As Control, ByVal brGrupa As Integer, ByVal naslovGrupa As String, ByVal tipGrupa As NasEnum.TipGrupa)
        Me.New(netControl, tipGrupa)

        Me.GrupaBr = brGrupa
        Me.nasGr = naslovGrupa


    End Sub

    Public Sub New(ByVal tipGrupa As NasEnum.TipGrupa)
        listaKontroli = New List(Of KontrolaOpis)
        tipGrp = tipGrupa
    End Sub

    Public Sub DodadiKontrola(ByVal kontrolaObj As KontrolaOpis)
        If listaKontroli Is Nothing Then
            listaKontroli = New List(Of KontrolaOpis)
        End If
        listaKontroli.Add(kontrolaObj)

    End Sub

    ''' <summary>
    ''' Vo ovaa funkcija se generiraat site kontroli sto pripagaat vo edna grupa
    ''' </summary>
    ''' <param name="poraka">Ako ima greska vraka poraka</param>
    ''' <returns>Vraka dali uspesno e zavrsena funkcijata</returns>
    ''' <remarks></remarks>
    Public Function Raboti(ByRef poraka As String) As Boolean

        If Me.tipGrp = NasEnum.TipGrupa.RadioList Then
            Dim kOpis As KontrolaOpis
            Me.GenerirajKontrolaOpisZaRadioList(kOpis, listaKontroli, poraka)
            Dim alat As KontrolaAlat
            alat = New KontrolaAlat(Me.SodrzinaVoPAGE, kOpis)
            alat.ListaRadiButtons = generirajRecnikZaRadioList(listaKontroli)
            If Not alat.Raboti(poraka, -1) Then
                Return False
            Else
                Return True
            End If

        End If

        Me.DodadiNaslov()

        For j = 0 To listaKontroli.Count - 1

            Dim alat As KontrolaAlat

            If Me.daliHtmlSodrzina Then
                alat = New KontrolaAlat(Me.SodrzinaVoHTML, listaKontroli(j))
            Else
                alat = New KontrolaAlat(Me.SodrzinaVoPAGE, listaKontroli(j))
            End If
            'If alat.ZemiKontrolaFLXS.XML.Prikazi.ToUpper = "D" Then

            'End If
            If Not alat.Raboti(poraka, j + 1) Then
                Return False
            End If

        Next
        Me.ZatvoriNaslov()
        Return True
    End Function
    Private Sub DodadiNaslov()

        Dim prikaz = "Prikazi="

        If daliPrikazi Then
            prikaz = "Prikazi=""DA"""
        Else
            prikaz = "Prikazi=""NE"""
        End If
        Dim grupaStr As String = ""
        grupaStr &= "<div id=""Grupa"" Grupa=""" & Me.BrojGrupa & """" & prikaz & ">"
        grupaStr &= "<div id=""NaslovGrupa" & """>" & UnvMakKodnaStr.CistoKonv(Me.nasGr) & "</div><div id=""Sodrzina"">"

        If Me.daliHtmlSodrzina Then
            Me.SodrzinaVoHTML.Controls.Add(New LiteralControl(grupaStr))
            'Me.SodrzinaVoHTML.Controls.Add(New LiteralControl("<div id=""Grupa"" Grupa=""" & Me.BrojGrupa & """>"))
            'Me.SodrzinaVoHTML.Controls.Add(New LiteralControl("<span id=""NaslovGrupa" & """>" & Me.nasGr & "</span><div id=""Sodrzina"">"))
        Else
            Me.SodrzinaVoPAGE.Controls.Add(New LiteralControl(grupaStr))
            'Me.SodrzinaVoPAGE.Controls.Add(New LiteralControl("<div id=""Grupa" & """>"))
            'Me.SodrzinaVoPAGE.Controls.Add(New LiteralControl("<span id=""GrupaNaslov" & Me.BrojGrupa & """>" & Me.nasGr & "</span"))
        End If
    End Sub
    Private Sub ZatvoriNaslov()
        If Me.daliHtmlSodrzina Then
            Me.SodrzinaVoHTML.Controls.Add(New LiteralControl("</div></div>"))
        Else
            Me.SodrzinaVoPAGE.Controls.Add(New LiteralControl("</div></div>"))
        End If
    End Sub
    Private Function GenerirajKontrolaOpisZaRadioList(ByRef konOpis As KontrolaOpis, ByVal listaKontroli As List(Of KontrolaOpis), ByRef poraka As String) As Boolean

        Dim rBaza As New RedBaza()
        Dim rXML As New RedXML()

        rBaza.SetirajPrazenObj()
        rXML.SetirajPrazenObj()

        If Not listaKontroli Is Nothing And listaKontroli.Count > 0 Then
            rBaza.SorsId = listaKontroli(0).XML.NaslovGrupa & listaKontroli(0).XML.Grupa
            rXML.SorsId = listaKontroli(0).XML.NaslovGrupa & listaKontroli(0).XML.Grupa

            rBaza.TipKontrola = "RadioButtonList"
            rXML.TipKontrola = "RadioButtonList"


            rXML.Ime = listaKontroli(0).XML.NaslovGrupa
            rXML.Prikazi = "D"
            rXML.OpisPole = ""
            rXML.RedenBroj = listaKontroli(0).XML.RedenBroj
            konOpis = New KontrolaOpis(rBaza.SorsId)

            If Not konOpis.splotiRedXMLredBaza(rXML, rBaza, poraka) Then
                Return False
            End If

            Return True
        End If

        poraka = poraka & "Nema lista od kontroli!" ' Prevod

        Return False

    End Function
    Private Function generirajRecnikZaRadioList(ByVal listaKontroli As List(Of KontrolaOpis)) As Dictionary(Of String, String)
        Dim listaRecniOdRadioButtons As New Dictionary(Of String, String)

        For index = 0 To listaKontroli.Count - 1

            listaRecniOdRadioButtons.Add(listaKontroli(index).XML.SorsId, listaKontroli(index).XML.Ime)
        Next
        Return listaRecniOdRadioButtons
    End Function

End Class
