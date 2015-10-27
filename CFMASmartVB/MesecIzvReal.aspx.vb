Imports System.IO
Public Class MesecIzvReal
    Inherits NasaStranaBase

    Public Sub New()
        MyBase.New("sp_MesecIzvReal.xml", NasEnum.TipPage.Maska)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Public Overrides Sub KopceKlik(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim pateka As String = ""
        If MesecIzvReal(pateka) Then

        Else
            ' Response.Write("Greska vo Service")
        End If

    End Sub
    Private Function MesecIzvReal(ByRef pateka As String) As Boolean
        Dim ImePdf As String = ""
        Dim bdata As Byte()
        Dim GrpBy As String = "P"
        Dim Dev As String = ""
        Dim SoStavki As TestServiceObicen.DaNe

        Try

            If MyBase.ZemiVrednostKontrola("GrpPoPatnici") Then
                GrpBy = "P"
            ElseIf MyBase.ZemiVrednostKontrola("OptPoPodgrProiz") Then
                GrpBy = "D"
            ElseIf MyBase.ZemiVrednostKontrola("OptPoArtikal") Then
                GrpBy = "A"
            ElseIf MyBase.ZemiVrednostKontrola("OptPoKomint") Then
                GrpBy = "K"
            End If

            If MyBase.ZemiVrednostKontrola("OptIzvoz") Then
                Dev = "D"
            ElseIf MyBase.ZemiVrednostKontrola("OptDomProd") Then
                Dev = "N"
            End If

            If MyBase.ZemiVrednostKontrola("SoStavki") Then
                SoStavki = TestServiceObicen.DaNe.Da
            Else
                SoStavki = TestServiceObicen.DaNe.Ne
            End If

            If Not MyBase.ServisNas.MesecIzvReal(ImePdf, MyBase.ZemiVrednostKontrola("DatumOd"), MyBase.ZemiVrednostKontrola("DatumDo"), _
                                                 MyBase.ZemiVrednostKontrola("GrPat"), "", GrpBy, MyBase.ZemiVrednostKontrola("Kurs"), _
                                                 Dev, "", "", SoStavki, TestServiceObicen.DaNe.Da, TestServiceObicen.DaNe.Da, MyBase.Poraka) Then
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