Imports System.Web.UI
Imports System.Web.UI.HtmlControls

Public Class KontrolaObvivka

    Private ime As String
    Private opis As String
    Private daliPrikazi As Boolean
    Private zabraniPole As Boolean '  Ova pole nema zasega od kade da se inicijalizira - Mislam deka vo idnina ke treba

    Private konfKontrola As KontrolaOpis ' Vo ova se naoga strukturata procitana od Baza i XML - ja opsivua celosno kontrolata
    Private brKontroli As Integer
    ''' <summary>
    ''' Vo imeto na slednite promelivi ima ....Sodrzina - se odnesuva na kakov e tipot na kontrolata
    ''' vo koja se dodavat drugite kontroli t.e moze da se zamilisi kako kutija vo koja se stavaat kontrolite
    ''' Toa moze da bide celata strana ili nekoj del od stranta koja ge gi "PRIMA" kontrolite.
    ''' </summary>
    ''' <remarks></remarks>
    Private webStranaSodrizna As Control
    Private daliWebPAGESodrizna As Boolean = False

    Private htmlSodrzina As HtmlGenericControl
    Private daliHtmlSodrzina As Boolean = False
    '-----------------------------------------------------------
    Private daliZatvoreaObvivka As Boolean = False
#Region "Properties - GetSet"

    Public ReadOnly Property DaliObvivkataEZatvorena As Boolean
        Get
            Return daliZatvoreaObvivka
        End Get
    End Property

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
    Public ReadOnly Property ZemiKontrolaFLXS As KontrolaOpis
        Get
            Return Me.konfKontrola
        End Get
    End Property
    Public ReadOnly Property ZEMISodrzinaHTML As HtmlGenericControl
        Get
            Return Me.htmlSodrzina
        End Get
    End Property

    Public ReadOnly Property ZEMISodrzinaPAGE As Control
        Get
            Return Me.webStranaSodrizna
        End Get
    End Property

#End Region

#Region "Konstruktori"
    Public Sub New(ByVal sodrzinaPAGE As Control, ByVal opis As String, ByVal daliPrikazi As String)

        ''Ova go pram zaradi grupa - lista na izbor so radiobutton

        Me.SodrzinaVoPAGE = sodrzinaPAGE

        Me.ime = ""
        Me.opis = opis

        Me.daliPrikazi = daliPrikazPole(daliPrikazi)
        brKontroli = 0
    End Sub
    Public Sub New(ByVal sodrzinaPAGE As Control, ByVal konfiguracijaKontrola As KontrolaOpis)
        Me.New(konfiguracijaKontrola)


        Me.SodrzinaVoPAGE = sodrzinaPAGE

    End Sub

    Public Sub New(ByVal sodrzinaHTML As HtmlGenericControl, ByVal konfiguracijaKontrola As KontrolaOpis)
        Me.New(konfiguracijaKontrola)

        Me.SodrzinaVoHTML = sodrzinaHTML

    End Sub

    Private Sub New(ByVal konfiguracijaKontrola As KontrolaOpis)

        Me.konfKontrola = konfiguracijaKontrola

        setirajKonfiguracija()

    End Sub

#End Region


#Region "Funkcii"

    Private Function setirajKonfiguracija() As Boolean

        If (Not konfKontrola Is Nothing) Then

            Me.ime = konfKontrola.XML.Ime
            Me.opis = konfKontrola.XML.OpisPole

            Me.daliPrikazi = daliPrikazPole(konfKontrola.XML.Prikazi)

            Return True

        Else
            Return False
        End If


    End Function

    Private Function daliPrikazPole(ByVal prikaz As String) As Boolean

        If prikaz Is Nothing Then
            Return False
        ElseIf prikaz.Trim().ToUpper() = "DA" Or prikaz.Trim().ToUpper() = "D" Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub GenerirajObvivka(Optional ByVal daliPrikaziKontrola As Boolean = True) 'Ovde moze da se napravi da ne se prikazua kontrolata

        Dim imeDivHtml As String = ""
        Dim cssClassIme = "imeKontrola"
        Dim idIme = ""
        If Not konfKontrola.KontrolaNasTIP = NasEnum.TipKontrola.RadioButton Or konfKontrola.KontrolaNasTIP = NasEnum.TipKontrola.RadioButtonList Then
            idIme = Me.konfKontrola.SorsID & "Ime"
        End If

        Dim prikaz = "Prikazi="

        If daliPrikaziKontrola Then
            prikaz = "Prikazi=""DA"""
        Else
            prikaz = "Prikazi=""NE"""
        End If

        Dim tekstOpis As String = ""
        Dim tekst As String = ""
        If NasaStranaBase.KojaDrzava = NasaStranaBase.Drzavi.Makedonija Then
            tekstOpis = UnvMakKodnaStr.CistoKonv(ZemiKontrolaFLXS.XML.OpisPole)
            tekst = UnvMakKodnaStr.CistoKonv(Me.ime)
        Else
            tekstOpis = ZemiKontrolaFLXS.XML.OpisPole
            tekst = Me.ime
        End If

        imeDivHtml = "<div id=""Kontrola""" & prikaz & ">" 'style=""background-color:" & IIf(brKontroli Mod 2 = 0, "orange", "green") & """>"
        imeDivHtml += "<span id=""" & idIme & """ class=""" & cssClassIme & """>"
        If Not konfKontrola.KontrolaNasTIP = NasEnum.TipKontrola.RadioButton Or konfKontrola.KontrolaNasTIP = NasEnum.TipKontrola.RadioButtonList Then       
            imeDivHtml += tekst
        End If

        imeDivHtml += "</span>"
        imeDivHtml += "<br />"
        imeDivHtml += "<div id=""Opis_" & ZemiKontrolaFLXS.SorsID.ToUpper() & """>" & tekstOpis & "</div>"

        DodadiVoUI(imeDivHtml)

        daliZatvoreaObvivka = False

    End Sub

    Private Sub ZatvoriObvivka()

        Dim zatvoriDiv = "</div>"

        DodadiVoUI(zatvoriDiv)

        daliZatvoreaObvivka = True
    End Sub


    Public Sub DodadiVoUI(ByVal htmlKontrola As String)

        If daliHtmlSodrzina Then

            Me.htmlSodrzina.Controls.Add(New LiteralControl(htmlKontrola))

        ElseIf daliWebPAGESodrizna Then

            Me.webStranaSodrizna.Controls.Add(New LiteralControl(htmlKontrola))

        End If

    End Sub
    Public Sub DodadiVoUI(ByVal nasaKontrolaObj As Control)

        If daliHtmlSodrzina Then

            Me.htmlSodrzina.Controls.Add(nasaKontrolaObj)

        ElseIf daliWebPAGESodrizna Then

            Me.webStranaSodrizna.Controls.Add(nasaKontrolaObj)

        End If

    End Sub

    Public Sub DodadiGlavnaKontrolaVoUI(ByVal nasaKontrolaObj As Control, Optional ByVal DaliPrikaziKontrola As Boolean = True)
        GenerirajObvivka(DaliPrikaziKontrola)

        If daliHtmlSodrzina Then

            Me.htmlSodrzina.Controls.Add(nasaKontrolaObj)

        ElseIf daliWebPAGESodrizna Then

            Me.webStranaSodrizna.Controls.Add(nasaKontrolaObj)

        End If

        ZatvoriObvivka()
    End Sub

    Public Sub DodadiGlavnaKontrolaDatumVoUI(ByVal nasaKontrolaDatumObj As UserControl)
        GenerirajObvivka()

        If daliHtmlSodrzina Then

            Me.htmlSodrzina.Controls.Add(nasaKontrolaDatumObj)

        ElseIf daliWebPAGESodrizna Then

            Me.webStranaSodrizna.Controls.Add(nasaKontrolaDatumObj)

        End If

        ZatvoriObvivka()
    End Sub

#End Region


End Class
