Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.Script.Serialization
Imports System.Web.Services
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Windows.Forms

Imports System.Threading
Imports System.Net
Imports System.Diagnostics


Public Class Izvestaj
    Inherits System.Web.UI.Page

    'Shared sqlCnString As String = "Data Source=ibohrid.dyndns.org;Initial Catalog=Wtrg;User ID=dijana;Password=dm"

    'Public Shared sqlCnString As String = "Data Source=janmark.dyndns.org\SQLEXPRESS,1433;Initial Catalog=wtrgJanko;User ID=marko;Password=lepota"
    Public Shared sqlCnString As String = "Data Source=192.168.0.55;Initial Catalog=wtrg;User ID=dijana;Password=dm1991"

    Dim sifraArtIzbran As String
    Dim orgEdIzbor As String
    Dim grpOrgEdIzbor As String

    Public Class jazzinn

        Enum DaNe
            Da
            Ne
        End Enum

        Enum RastiOpagja
            Rastecki
            Opagacki
        End Enum



    End Class


    Public Class Artikal
        Dim sifraArt As String
        Dim imeArt As String

        Property SifraArtikal As String
            Get
                Return sifraArt
            End Get


            Set(ByVal value As String)
                sifraArt = value
            End Set
        End Property


        Property ImeArtikal As String
            Get
                Return imeArt
            End Get
            Set(ByVal value As String)
                imeArt = value
            End Set
        End Property
    End Class

    Protected Overrides Sub OnPreInit(ByVal e As System.EventArgs)
        MyBase.OnPreInit(e)

        NapolniGrpOrgEd()
        NapolniOrgEd()


        Dim hInput As New HiddenField()

        hInput.ID = "Chck"
        form1.Controls.Add(hInput)


    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Request.QueryString Is Nothing Then

            If Not Request.QueryString("Zemi") Is Nothing Then ' _
                'And Not Request.QueryString("OrgEd") Is Nothing _
                'And Not Request.QueryString("GrpOrgEd") Is Nothing _
                'And Not Request.QueryString("SifrArt") Is Nothing _
                'Then

                'orgEdIzbor = Request.QueryString("OrgEd")
                'grpOrgEdIzbor = Request.QueryString("GrpOrgEd")
                'sifraArtIzbran = Request.QueryString("SifrArt").ToString()
                'vrziService()
                ''ZemiFile()
                TestFile()
            End If

        End If




    End Sub

    Private Sub NapolniOrgEd()

        Dim sqlCn As SqlConnection = New SqlConnection(sqlCnString)
        Dim command As String = "Select  *  from OrgEd"
        Dim sqlCmd As SqlCommand = New SqlCommand()
        sqlCmd.Connection = sqlCn
        sqlCmd.CommandText = command
        sqlCmd.CommandType = CommandType.Text

        Dim pomTest As ListItem = New ListItem()
        pomTest.Text = ""
        pomTest.Value = ""
        ddlOrgEd.Items.Add(pomTest)

        Try
            sqlCn.Open()


            Dim sqlDr As SqlDataReader = sqlCmd.ExecuteReader()

            While sqlDr.Read()



                Dim sifraOE As String = sqlDr("Sifra_OE").ToString()
                Dim imeOrg As String = sqlDr("ImeOrg").ToString()

                Dim pom As ListItem = New ListItem()
                pom.Text = "[" + sifraOE.Trim() + "] " + imeOrg
                pom.Value = sifraOE
                ddlOrgEd.Items.Add(pom)

            End While



            sqlCn.Close()

        Catch ex As Exception

            sqlCn.Close()

        End Try

    End Sub
    Private Sub NapolniGrpOrgEd()

        Dim sqlCn As SqlConnection = New SqlConnection(sqlCnString)
        Dim command As String = "Select  *  from GrOrg"
        Dim sqlCmd As SqlCommand = New SqlCommand()
        sqlCmd.Connection = sqlCn
        sqlCmd.CommandText = command
        sqlCmd.CommandType = CommandType.Text

        Try
            sqlCn.Open()


            Dim sqlDr As SqlDataReader = sqlCmd.ExecuteReader()

            While sqlDr.Read()



                Dim sifraOE As String = sqlDr("Sif_GrOrg").ToString()
                Dim imeOrg As String = sqlDr("Ime_GrOrg").ToString()

                Dim pom As ListItem = New ListItem()
                pom.Text = "[" + sifraOE.Trim() + "] " + imeOrg
                pom.Value = sifraOE
                ddlGrpOrgEd.Items.Add(pom)
            End While

            sqlCn.Close()

        Catch ex As Exception

            sqlCn.Close()

        End Try

    End Sub

    Protected Sub ZemiFile()
        Dim file As String = Server.MapPath("~/pdf/marko.pdf")
        Dim f As FileInfo = New FileInfo(file)

        Response.Clear()

        Response.ClearHeaders()

        Response.ClearContent()

        Response.AddHeader("Content-Disposition", "attachment; filename=""marko.pdf""")

        Response.AddHeader("Content-Type", "application/octet-stream")

        Response.TransmitFile(file)

        Response.End()

    End Sub

    Protected Sub btnPrikazi_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrikazi.Click

        Response.Redirect(Request.Url.AbsoluteUri + "?Zemi=1")


        'Dim url As String = Request.Url.AbsoluteUri

        'sifraArtIzbran = tboxSifraArt.Text
        'orgEdIzbor = ddlOrgEd.SelectedValue
        'grpOrgEdIzbor = ddlGrpOrgEd.SelectedValue

        'Dim testChck = CType(Me.Page.FindControl("chck"), HiddenField)



        'vrziService()
        ''  Response.Redirect(url + "?Zemi=1&OrgEd=" + orgEdIzbor + "&GrpOrgEd=" + grpOrgEdIzbor + "&SifrArt=" + sifraArtIzbran)

    End Sub

    <WebMethod()> _
    Public Shared Function testzaajax(ByVal imeArtikal As String) As String

        Dim sqlCn As SqlConnection = New SqlConnection(sqlCnString)

        Try

            Dim js As JavaScriptSerializer = New JavaScriptSerializer()

            Dim imeA As String = js.Deserialize(Of String)(imeArtikal)


            Dim command As String = "Select TOP 10 * from KatArt where ImeArt like '" + imeA + "%'"
            Dim sqlCmd As SqlCommand = New SqlCommand()
            sqlCmd.Connection = sqlCn
            sqlCmd.CommandText = command
            sqlCmd.CommandType = CommandType.Text
            Dim artList As List(Of Artikal) = New List(Of Artikal)()


            sqlCn.Open()


            Dim sqlDr As SqlDataReader = sqlCmd.ExecuteReader()

            While sqlDr.Read()
                Dim sifraArt As String = sqlDr("Sifra_Art").ToString()
                Dim imeArt As String = sqlDr("ImeArt").ToString()

                Dim nov As Artikal = New Artikal()
                nov.ImeArtikal = imeArt.Trim()
                nov.SifraArtikal = sifraArt.Trim()

                artList.Add(nov)
            End While

            sqlCn.Close()

            Return js.Serialize(artList)
        Catch ex As Exception
            sqlCn.Close()
            Return "Server: Greska!"
        End Try
    End Function

    Public Sub vrziService()

        Dim ImeRpt As String = "CrPrTarifi"
        ImeRpt = "CrKartArt"
        Dim ImePdf As String = ""

        Dim Poraka As String = ""
        Dim DodPoraka As String = ""

        Try
            'Dim ws As New sors.wCFMASmartSrv
            'If ws.PreglTarifi(ImeRpt, ImePdf, Poraka) Then

            'End If


            'ws.KarticaArtikal(ImeRpt, ImePdf, sifraArtIzbran, orgEdIzbor, jazzinn.DaNe.Da, jazzinn.DaNe.Ne, "", "", "", "", _
            '         jazzinn.DaNe.Da, jazzinn.RastiOpagja.Rastecki, "", "", jazzinn.DaNe.Ne, "", jazzinn.DaNe.Ne, "", _
            '          jazzinn.DaNe.Ne, jazzinn.DaNe.Ne, jazzinn.DaNe.Ne, jazzinn.DaNe.Da, Poraka)


            ImeRpt = "crPrSporedbaPlanOst"


            'ws.MesecIzvReal(ImeRpt, ImePdf, "2012-10-1", "2012-10-1", "5", "N", "1, 3, 6, 7, 16, 131, 133, 141", "61.5", "P", sors.DaNe.Ne, sors.DaNe.Da, Poraka)


            testFtpDownNov(ImePdf)

        Catch ex As Exception
            DodPoraka = ex.Message
        End Try

    End Sub

    Public Sub testFtpDownload(ByVal pateka As String)
        Dim issecure As Boolean = True
        Dim username As String = "Administrator"
        Dim pass As [Char]() = "+321ewq".ToCharArray()
        Dim password As New System.Security.SecureString()


        password.AppendChar("q"c)
        password.AppendChar("w"c)
        password.AppendChar("e"c)
        password.AppendChar("1"c)
        password.AppendChar("2"c)
        password.AppendChar("3"c)
        password.AppendChar("+"c)



        Dim patekaNiza() As String
        patekaNiza = pateka.Split("\")
        Dim patekaFtp As String = "ftp://ibohrid.dyndns.org/"


        For index = 4 To patekaNiza.Length - 1
            patekaFtp += patekaNiza(index)

            If index <> patekaNiza.Length - 1 Then
                patekaFtp += "/"
            End If

        Next

        Dim client As System.Net.WebClient = New System.Net.WebClient() 'You can have this at class level so dont need to instantiate for each request
        If issecure = True Then
            'Set username/pass required for a url access
            Dim myCred As System.Net.NetworkCredential = New System.Net.NetworkCredential("", "")
            myCred.UserName = "Administrator"
            myCred.Password = password.ToString()
            client.Credentials = myCred
        End If
        Dim data As Byte() = client.DownloadData(patekaFtp)
    End Sub

    Public Sub testFtpDownNov(ByVal pateka As String)

        Dim patekaNiza() As String
        patekaNiza = pateka.Split("\")
        Dim patekaFile As String = patekaNiza(6)


        Dim _strHost As [String] = "ftp://ibohrid.dyndns.org"
        Dim _strUsername As [String] = "Administrator"
        Dim _strPassword As [String] = "+321ewq"
        Dim _strTargetFolder As [String] = "CFMASmart/Rpt"
        Dim _strSourceFile As [String] = patekaFile



        Dim ServerPath As [String] = [String].Format("{0}/{1}{2}", _strHost, If(_strTargetFolder = "", "", _strTargetFolder & "/"), _strSourceFile)
        If Not ServerPath.ToLower().StartsWith("ftp://") Then
            ServerPath = "ftp://" & ServerPath
        End If




        Dim request As FtpWebRequest = CType(WebRequest.Create(ServerPath), FtpWebRequest)


        request.Method = WebRequestMethods.Ftp.GetFileSize
        request.Credentials = New NetworkCredential(_strUsername, _strPassword)
        request.UsePassive = True
        request.UseBinary = True
        request.KeepAlive = True

        Dim dataLength As Integer = CInt(request.GetResponse().ContentLength)


        request = TryCast(FtpWebRequest.Create(ServerPath), FtpWebRequest)
        request.Method = WebRequestMethods.Ftp.DownloadFile
        request.Credentials = New NetworkCredential(_strUsername, _strPassword)
        request.UsePassive = True
        request.UseBinary = True
        request.KeepAlive = False


        Dim response__1 As FtpWebResponse = TryCast(request.GetResponse(), FtpWebResponse)
        Dim reader As Stream = response__1.GetResponseStream()

        Try
            Dim buffer As Byte() = New Byte(1023) {}

            Dim strExtenstion As String = Path.GetExtension(_strSourceFile).ToLower()

            Response.Clear()
            Response.Buffer = True

            If strExtenstion = ".doc" OrElse strExtenstion = ".docx" Then
                Response.ContentType = "application/vnd.ms-word"
                Response.AddHeader("content-disposition", "attachment;filename=" & _strSourceFile)
            ElseIf strExtenstion = ".xls" OrElse strExtenstion = ".xlsx" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("content-disposition", "attachment;filename=" & _strSourceFile)
            ElseIf strExtenstion = ".pdf" Then
                Response.ContentType = "application/pdf"
                Response.AddHeader("content-disposition", "attachment;filename=" & _strSourceFile)
            ElseIf strExtenstion = ".jpg" OrElse strExtenstion = ".jpeg" Then
                Response.ContentType = "image/jpeg"
                Response.AddHeader("content-disposition", "attachment;filename=" & _strSourceFile)
            ElseIf strExtenstion = ".ppt" OrElse strExtenstion = ".pptx" OrElse strExtenstion = ".pot" OrElse strExtenstion = "pos" Then
                Response.ContentType = "application/vnd.ms-powerpoint"
                Response.AddHeader("content-disposition", "attachment;filename=" & _strSourceFile)
            Else
                Response.ContentType = "application/force-download"
                Response.AddHeader("content-disposition", "attachment;filename=" & _strSourceFile)
            End If

            Response.AddHeader("Content-Length", dataLength.ToString())
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)



            While True
                If Response.IsClientConnected Then
                    Dim bytesRead As Integer = reader.Read(buffer, 0, buffer.Length)

                    If bytesRead = 0 Then

                        Exit While
                    Else
                        Response.OutputStream.Write(buffer, 0, bytesRead)
                        Response.Flush()
                    End If
                Else

                    Exit While
                End If
            End While

        Catch ex As Exception

            Response.Write("Error : " + ex.Message)
        Finally

            Try

                If reader IsNot Nothing Then
                    reader.Close()
                End If


                If response__1 IsNot Nothing Then
                    response__1.Close()
                End If


                Response.[End]()

            Catch gr As Exception

            End Try
        End Try


    End Sub

    Private Sub TestFile()
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.AddHeader("Content-Type", "application/octet-stream")
        Response.AddHeader("Content-Disposition", "attachment; filename=" & "test.pdf")

        'Response.BinaryWrite(System.IO.File.ReadAllBytes(Server.MapPath(String.Empty) & "\Test.pdf"))
        Response.TransmitFile(Server.MapPath(String.Empty) & "\Test.pdf")
        Response.Flush()
        Response.End()
    End Sub


End Class

