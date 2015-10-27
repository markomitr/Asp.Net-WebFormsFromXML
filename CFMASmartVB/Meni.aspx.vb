Public Class Meni
    Inherits NasaStranaBase

    Public Sub New()
        MyBase.New("Meni.xml", NasEnum.TipPage.Meni)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Overrides Sub KopceKlik(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
End Class