Imports System.IO
Imports CFMASmartVB.TestServiceObicen

Public Class AnalProfaktNarac
    Inherits NasaStranaBase

    Public Sub New()
        MyBase.New("sp_AnalProfaktNarac.xml", NasEnum.TipPage.Maska)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Public Overrides Sub KopceKlik(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim pateka As String = ""
        If AnalProfaktNarac(pateka) Then ' MesecIzvReal(pateka) Then

        Else
            'Response.Write("Greska vo Service")
        End If

    End Sub
    Private Function AnalProfaktNarac(ByRef pateka As String) As Boolean
        Dim ImePdf As String = ""
        Dim bdata As Byte()

        Dim SiteDat As String = MyBase.ZemiVrednostKontrola("SiteDat").ToString.ToUpper
        Dim SiteDat_Dosp As String = MyBase.ZemiVrednostKontrola("SiteDat_Dosp").ToString.ToUpper
        Dim SiteDat_Isp As String = MyBase.ZemiVrednostKontrola("SiteDat_Isp").ToString.ToUpper


        Dim Orged As Object = MyBase.ZemiVrednostKontrola("Orged")
        Dim GrOrg As Object = MyBase.ZemiVrednostKontrola("GrOrg")

        Dim DatumOd As Object = IIf(SiteDat = "TRUE", "", MyBase.ZemiVrednostKontrola("DatumOd"))
        Dim DatumDo As Object = IIf(SiteDat = "TRUE", "", MyBase.ZemiVrednostKontrola("DatumDo"))

        Dim DatumOd_Dosp As Object = IIf(SiteDat_Dosp = "TRUE", "", MyBase.ZemiVrednostKontrola("DatumOd_Dosp"))
        Dim DatumDo_Dosp As Object = IIf(SiteDat_Dosp = "TRUE", "", MyBase.ZemiVrednostKontrola("DatumDo_Dosp"))

        Dim DatumOd_Isp As Object = IIf(SiteDat_Isp = "TRUE", "", MyBase.ZemiVrednostKontrola("DatumOd_Isp"))
        Dim DatumDo_Isp As Object = IIf(SiteDat_Isp = "TRUE", "", MyBase.ZemiVrednostKontrola("DatumDo_Isp"))


        Dim GrPat As Object = MyBase.ZemiVrednostKontrola("GrPat").ToString.Trim

        Dim PoArt As TestServiceObicen.DaNe = IIf(MyBase.ZemiVrednostKontrola("PoArt").ToString().ToUpper = "TRUE", TestServiceObicen.DaNe.Da, TestServiceObicen.DaNe.Ne)

        Dim KakovRep = IIf(PoArt = DaNe.Da, KakovReport.PoArtikl, KakovReport.Detalen)

        Dim Sostojba As TestServiceObicen.Sostojba

        If MyBase.ZemiVrednostKontrola("OptSite") Then
            Sostojba = TestServiceObicen.Sostojba.Site
        ElseIf MyBase.ZemiVrednostKontrola("OptReal") Then
            Sostojba = TestServiceObicen.Sostojba.Realizirani
        ElseIf MyBase.ZemiVrednostKontrola("OptNereal") Then
            Sostojba = TestServiceObicen.Sostojba.Nerealizirani
        ElseIf MyBase.ZemiVrednostKontrola("OptPotpNereal") Then
            Sostojba = TestServiceObicen.Sostojba.CelosnoNerealizirani
        End If

        Try

            If Not MyBase.ServisNas.AnalNaracki(ImePdf, "", Orged, "", "", DatumOd, DatumDo, "", TestServiceObicen.VlezIzlez.Site, "", GrOrg, KakovRep _
                                                , TestServiceObicen.DaNe.Nisto, "", "", Sostojba, "", "", "", "", TestServiceObicen.MaterUsl.Site, KakovRep, _
                                                 "", "", DaNe.Ne, _
                    "",
                    DaNe.Ne, _
                    Podreduvanje.Nema, _
                    DatumOd_Dosp, _
                    DatumDo_Dosp, _
                    GrPat, _
                    "", _
                    "", _
                    "", _
                   "", _
                  "", _
                     "", _
                    "", _
                    "", _
                    "", _
                    "", _
                    DaNe.Nisto, _
                   DaNe.Nisto, _
                    "", _
                    Sostojba.Site, _
                    DatumOd_Isp, _
                    DatumDo_Isp, _
                    DaNe.Nisto, _
                   "", _
                    GrupirajPo.PoNisto, _
                    Poraka) Then


                MyBase.DodadiInformacija("Servis:" + Poraka)
                Return False
            Else
                pateka = ImePdf
                MyBase.PrevzemiDokument(pateka)
                Return True
            End If



        Catch ex As Exception
            MyBase.DodadiInformacija("Servis:" + Poraka + ex.Message)
            Return False
        End Try
        Return True
    End Function
End Class