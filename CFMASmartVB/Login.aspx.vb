Public Class Login
    Inherits NasaStranaBase

    Public Sub New()
        MyBase.New("", NasEnum.TipPage.Login)
    End Sub
    Protected Overrides Sub OnPreInit(ByVal e As System.EventArgs)
        MyBase.OnPreInit(e)
        InicBtnKopcePotvrdi()
        setirajLabeli()

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not MyBase.Korisnik Is Nothing Then
            Response.Redirect("Meni.aspx")
        End If
        Me.txtBoxKorisnik.Focus()
    End Sub

    Public Overrides Sub KopceKlik(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim kor As New Korisnik

        If Korisnik.proveriKorisnik(txtBoxKorisnik.Text, txtBoxLozinka.Text, kor, MyBase.Poraka) Then
            MyBase.dodadiKorisnikVoSesija(kor)
            Response.Redirect("Meni.aspx")
        Else
            lblPorakaLogin.Text = UnvMakKodnaStr.CistoKonv(Poraka)
            MyBase.DodadiInformacija(Poraka)
            'Response.Write("Vnimanie: " + Poraka)
        End If

    End Sub
    Private Sub setirajLabeli()
        LblNaslovMaska.Text = "Најави се!"
        lblKorisnikLogin.Text = "Корисник"
        lblLozinkaLogin.Text = "Лозинка"
        lblPorakaLogin.Text = ""
    End Sub
    Private Sub InicBtnKopcePotvrdi()
        btnLoginPotvrdi.Text = "Потврди"
        btnLoginPotvrdi.ID = "btnPrikazi"
        AddHandler btnLoginPotvrdi.Click, AddressOf KopceKlik
        btnLoginPotvrdi.Attributes.Add("class", "css3button")
    End Sub
End Class