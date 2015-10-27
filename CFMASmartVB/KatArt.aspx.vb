Public Class KatArt
    Inherits NasaStranaBase

    Public Sub New()
        MyBase.New("KatArt_TestXML.xml", NasEnum.TipPage.Maska)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Overrides Sub KopceKlik(ByVal sender As Object, ByVal e As System.EventArgs)
        KarticaNaArtikal()
    End Sub

    Private Sub KarticaNaArtikal()

        Dim ImeRpt As String = "CrKartArt"
        Dim ImePdf As String = ""
        Dim bdata As Byte()
        Try
            If Not MyBase.ServisNas.KarticaArtikal(ImeRpt, _
                                          ImePdf, _
                                          MyBase.ZemiVrednostKontrola("Sifra_Art"), _
                                          MyBase.ZemiVrednostKontrola("OrgEd"), _
                                          IIf(MyBase.ZemiVrednostKontrola("SoDDV").ToString.ToUpper() = "TRUE", TestServiceObicen.DaNe.Da, TestServiceObicen.DaNe.Ne), TestServiceObicen.DaNe.Ne, _
                                          IIf(MyBase.ZemiVrednostKontrola("SiteDat").ToString.ToUpper() = "TRUE", "", MyBase.ZemiVrednostKontrola("DatumOd")), _
                                          IIf(MyBase.ZemiVrednostKontrola("SiteDat").ToString.ToUpper() = "TRUE", "", MyBase.ZemiVrednostKontrola("DatumDo")), _
                                          "", "", _
                                          TestServiceObicen.DaNe.Da, TestServiceObicen.RastiOpagja.Rastecki, _
                                           "", MyBase.ZemiVrednostKontrola("Sifra_Kup"), TestServiceObicen.DaNe.Ne, "", TestServiceObicen.DaNe.Ne, "", _
                                           TestServiceObicen.DaNe.Ne, TestServiceObicen.DaNe.Ne, TestServiceObicen.DaNe.Ne, TestServiceObicen.DaNe.Ne, MyBase.Poraka) _
                                       Then


                MyBase.DodadiInformacija("Servis:" & Poraka)
            Else

                MyBase.PrevzemiDokument(ImePdf)
            End If
            'MyBase.ServisNas.Dispose()
        Catch ex As Exception
            'MyBase.ServisNas.Dispose()
            MyBase.DodadiInformacija(MyBase.Poraka + ex.Message)
        End Try

    End Sub

End Class