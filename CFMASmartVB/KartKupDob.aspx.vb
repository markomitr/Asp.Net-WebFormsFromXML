Imports System.IO
Imports CFMASmartVB.TestServiceObicen
Public Class KartKupDob
    Inherits NasaStranaBase

    Public Sub New()
        MyBase.New("sp_KartKupDob.xml", NasEnum.TipPage.Maska)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Public Overrides Sub KopceKlik(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim pateka As String = ""
        If KartKupDob(pateka) Then

        Else
            ' Response.Write("Greska vo Service")
        End If

    End Sub
    Private Function KartKupDob(ByRef pateka As String) As Boolean
        Dim ImePdf As String = ""
        Dim bdata As Byte()

        Dim KupDob As KupuvacDobavuvac

        Dim Sifra_Kup As String = MyBase.ZemiVrednostKontrola("Sifra_Kup").ToString

        Dim SiteDat As String = MyBase.ZemiVrednostKontrola("SiteDat").ToString.ToUpper

        Dim Konto As Object = MyBase.ZemiVrednostKontrola("Kto_Anal")

        Dim Orged As Object = MyBase.ZemiVrednostKontrola("Orged")
        Dim GrOrg As Object = MyBase.ZemiVrednostKontrola("GrOrg")

        Dim DatumOd As Object = IIf(SiteDat = "TRUE", "", MyBase.ZemiVrednostKontrola("DatumOd"))
        Dim DatumDo As Object = IIf(SiteDat = "TRUE", "", MyBase.ZemiVrednostKontrola("DatumDo"))

        Dim GrPat As Object = MyBase.ZemiVrednostKontrola("GrPat").ToString.Trim

        If MyBase.ZemiVrednostKontrola("OptKup") Then
            KupDob = KupuvacDobavuvac.Kupuvac
        ElseIf MyBase.ZemiVrednostKontrola("OptDob") Then
            KupDob = KupuvacDobavuvac.Dobavuvac
        ElseIf MyBase.ZemiVrednostKontrola("OptKupDob") Then
            KupDob = KupuvacDobavuvac.IKupIDob
        End If

        Try

            If Not MyBase.ServisNas.KartIOS(ImePdf, Sifra_Kup, Orged, Konto, DaNe.Ne, KupDob, DatumOd, DatumDo, "", "", DaNe.Ne, GrOrg, Podreduvanje.PoDatum, _
                DaNe.Ne, "", "", DaNe.Da, "", "", "", "", "", "", DaNe.Ne, GrupirajPo.PoDatum, DaNe.Ne, "N", Poraka) Then


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