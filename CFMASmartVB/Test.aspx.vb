Public Class Test
    Inherits NasaStranaBase
    Public Sub New()
        'MyBase.New("KatArt_TestXML.xml", NasEnum.TipPage.Meni)
        MyBase.New("TopLista.xml", NasEnum.TipPage.Maska)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '  TopLista()
    End Sub
    Public Overrides Sub KopceKlik(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'KarticaNaArtikal()
        'MesecIzvReal()
        'TopLista()
    End Sub

    'Private Sub KarticaNaArtikal()

    '    Dim ImeRpt As String = "CrKartArt"
    '    Dim ImePdf As String = ""
    '    Dim bdata As Byte()
    '    Try
    '        If Not MyBase.ServisNas.KarticaArtikal(ImeRpt, _
    '                                      ImePdf, _
    '                                      MyBase.ZemiVrednostKontrola("Sifra_Art"), _
    '                                      MyBase.ZemiVrednostKontrola("OrgEd"), _
    '                                      TestServiceObicen.DaNe.Ne, TestServiceObicen.DaNe.Ne, _
    '                                      "", "", "", "", _
    '                                      TestServiceObicen.DaNe.Da, TestServiceObicen.RastiOpagja.Rastecki, _
    '                                       "", MyBase.ZemiVrednostKontrola("Sifra_Kup"), TestServiceObicen.DaNe.Ne, "", TestServiceObicen.DaNe.Ne, "", _
    '                                       TestServiceObicen.DaNe.Ne, TestServiceObicen.DaNe.Ne, TestServiceObicen.DaNe.Ne, TestServiceObicen.DaNe.Ne, MyBase.Poraka) _
    '                                   Then


    '            MyBase.DodadiInformacija("Servis: " & MyBase.Poraka)
    '        Else

    '            MyBase.PrevzemiDokument(ImePdf)
    '        End If
    '        'MyBase.ServisNas.Dispose()
    '    Catch ex As Exception
    '        'MyBase.ServisNas.Dispose()
    '        Response.Write(MyBase.Poraka + ex.Message)
    '    End Try

    'End Sub
    'Private Sub MesecIzvReal()

    '    Dim ImeRpt As String = "crPrSporedbaPlanOst"
    '    Dim ImePdf As String = ""
    '    Dim bdata As Byte()
    '    Try
    '        If Not MyBase.ServisNas.MesecIzvReal(bdata, ImeRpt, _
    '                                      ImePdf, _
    '                                      MyBase.ZemiVrednostKontrola("DatumOd"), _
    '                                      MyBase.ZemiVrednostKontrola("DatumDo"), _
    '                                      MyBase.ZemiVrednostKontrola("GrPat"), _
    '                                      "N", _
    '                                      "1, 3, 6, 7, 16, 131, 133, 141", _
    '                                      "61.5", _
    '                                      "P", _
    '                                      TestServiceObicen.DaNe.Ne, _
    '                                     TestServiceObicen.DaNe.Da, _
    '                                      MyBase.Poraka) _
    '        Then
    '            Response.Write("Servis: " & MyBase.Poraka)
    '        Else

    '            MyBase.PrevzemiDokument(ImePdf)
    '        End If
    '        'MyBase.ServisNas.Dispose()
    '    Catch ex As Exception
    '        'MyBase.ServisNas.Dispose()
    '        Response.Write(MyBase.Poraka + ex.Message)
    '    End Try
    'End Sub
    'Private Sub TopLista()
    '    Dim imePdf As String = ""
    '    If Not MyBase.ServisNas.TopLista("", _
    '                                    imePdf, _
    '                                    MyBase.Poraka, _
    '                                    "1", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    TestServiceObicen.PodreduvanjeKomerc.PoIme, _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    TestServiceObicen.KomintObjekt.Komintent, _
    '                                    TestServiceObicen.KojaCena.NabCena, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.Podvleci.PodvlKrajna, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    "TopVrednost", _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    "", _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    "", _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    "", _
    '                                    "", _
    '                                    TestServiceObicen.KakovReport.Standarden, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    "", _
    '                                    "", _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    "", _
    '                                    "", _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    "", _
    '                                    "", _
    '                                    "", _
    '                                    TestServiceObicen.DaNe.Ne, _
    '                                    TestServiceObicen.DaNe.Ne) Then
    '        MyBase.DodadiInformacija(MyBase.Poraka)
    '    Else
    '        MyBase.PrevzemiDokument(imePdf)
    '    End If

    'End Sub
End Class
