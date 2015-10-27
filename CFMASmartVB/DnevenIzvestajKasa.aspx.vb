Public Class DnevenIzvestajKasa
    Inherits NasaStranaBase


    Public Sub New()
        MyBase.New("rk_Izvestaj_Dneven.xml", NasEnum.TipPage.Maska)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Overrides Sub KopceKlik(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ImeRpt As String = "crDnIzvKos"
        Dim ImePdf As String = ""
        Dim bdata As Byte()
        Try

            'If Not MyBase.ServisNas.DnevenIzvKasa(Nothing, ImeRpt, ImePdf, MyBase.ZemiVrednostKontrola("OrgEd"), MyBase.ZemiVrednostKontrola("DatumOd"), MyBase.ZemiVrednostKontrola("DatumDo"), "D", NasEnum.OpstoDaNe.Ne, MyBase.Poraka) Then
            '    MyBase.DodadiInformacija("Servis:" + Poraka)

            'Else

            '    MyBase.PrevzemiDokument(ImePdf)

            'End If

        Catch ex As Exception
            MyBase.DodadiInformacija("Servis:" + Poraka + ex.Message)

        End Try

    End Sub
End Class