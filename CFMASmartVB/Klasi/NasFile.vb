Imports System.IO
Imports System.Web
Imports System.Web.UI.WebControls.WebParts
Imports System.Threading
Imports System.Net
Imports System.Diagnostics


Public Class NasFile
    Dim rsp As HttpResponse

    Public Sub New(ByVal responseObj As HttpResponse)
        Me.rsp = responseObj
    End Sub
    Public Sub New()

    End Sub

    Public Function ZemiFileLokalno(ByVal pateka As String, ByVal ime As String, ByRef response As HttpResponse, ByRef poraka As String) As Boolean


        Dim file As FileInfo = New FileInfo(pateka)

        If file.Exists Then



            response.Clear()

            response.ClearHeaders()

            response.ClearContent()

            response.Cache.SetCacheability(HttpCacheability.NoCache)

            ' response.AddHeader("Content-Type", "application/octet-stream")
            response.AddHeader("Content-Type", "application/pdf")
            'response.AddHeader("Content-Disposition", "inline; filename=" + file.Name)
            response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)

            response.AddHeader("Content-Length", file.Length.ToString())

            'response.ContentType = "application/pdf"
            'response.ContentType = "application/octet-stream"

            response.Flush()

            response.TransmitFile(file.FullName, 0, file.Length)

            response.End()

            Return True
        Else
            poraka = "File-ot ne postoi:" + pateka.Replace("\", " \ ") 'Prevod
            Return False
        End If

    End Function
    Public Function probaZaDownloadOutPut(ByVal pateka As String, ByVal ime As String, ByRef response As HttpResponse, ByRef poraka As String)
        Const bufferLength As Integer = 10000
        Dim buffer As Byte() = New Byte(bufferLength) {}
        Dim length As Integer = 0
        Dim doc As Stream
        Try
            Dim file As FileInfo = New FileInfo(pateka)
            doc = New FileStream(pateka, FileMode.Open, FileAccess.Read)

            response.Clear()
            response.ClearHeaders()
            response.ClearContent()
            'response.Cache.SetCacheability(HttpCacheability.NoCache)
            response.AddHeader("Content-Type", "application/pdf")
            response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            response.AddHeader("Content-Length", file.Length)
            response.Buffer = True

            While True
                If (response.IsClientConnected) Then
                    length = doc.Read(buffer, 0, buffer.Length)
                    If length = 0 Then
                        Exit While
                    End If
                    response.OutputStream.Write(buffer, 0, length)
                End If
            End While
            response.Flush()
            response.End()
            doc.Close()
            Return True
        Catch ex As Exception
            response.Write("Greksa vo Download - " + ex.Message)
        Finally
            doc.Close()
        End Try
    End Function
    Private Function ZemiFileLokalnoNE(ByVal pateka As String, ByVal ime As String, ByRef responseObj As HttpResponse, ByRef poraka As String) As Boolean
        'Try
        '    responseObj.Clear()
        '    ''Response.ContentType = "application/pdf"
        '    responseObj.AddHeader("Content-Type", "application/pdf")
        '    responseObj.AddHeader("content-disposition", "attachment;filename=" & ime)


        '    Dim bData As Byte()
        '    Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(pateka))

        '    responseObj.AddHeader("Content-Length", br.BaseStream.Length.ToString())
        '    responseObj.Charset = ""
        '    responseObj.Cache.SetCacheability(HttpCacheability.NoCache)


        '    While True
        '        If responseObj.IsClientConnected Then

        '            bData = br.ReadBytes(br.BaseStream.Length)
        '            Dim ms As MemoryStream = New MemoryStream(bData, 0, bData.Length)
        '            Dim bytesRead As Integer = ms.ReadByte()
        '            If bytesRead = 0 Then

        '                Exit While
        '            Else
        '                responseObj.OutputStream.Write(bData, 0, bData.Length)
        '                responseObj.Flush()
        '            End If
        '        Else

        '            Exit While
        '        End If
        '    End While

        '    br.Close()
        '    responseObj.End()
        '    'responseObj.ClearContent()
        '    'responseObj.ClearHeaders()
        '    'responseObj.Clear()
        '    'responseObj.Close()
        'Catch ex As Exception
        '    poraka = "Problem pri prevzemanje na filot" + ex.Message
        '    Return False
        'End Try

        'Return True
    End Function
    Public Function ZemiFileFtpDownNov(ByVal pateka As String, ByRef response As HttpResponse, ByRef poraka As String) As Boolean
        Try


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

                response.Clear()
                response.Buffer = True

                If strExtenstion = ".doc" OrElse strExtenstion = ".docx" Then
                    response.ContentType = "application/vnd.ms-word"
                    response.AddHeader("content-disposition", "attachment;filename=" & _strSourceFile)
                ElseIf strExtenstion = ".xls" OrElse strExtenstion = ".xlsx" Then
                    response.ContentType = "application/vnd.ms-excel"
                    response.AddHeader("content-disposition", "attachment;filename=" & _strSourceFile)
                ElseIf strExtenstion = ".pdf" Then
                    response.ContentType = "application/pdf"
                    response.AddHeader("content-disposition", "attachment;filename=" & _strSourceFile)
                ElseIf strExtenstion = ".jpg" OrElse strExtenstion = ".jpeg" Then
                    response.ContentType = "image/jpeg"
                    response.AddHeader("content-disposition", "attachment;filename=" & _strSourceFile)
                ElseIf strExtenstion = ".ppt" OrElse strExtenstion = ".pptx" OrElse strExtenstion = ".pot" OrElse strExtenstion = "pos" Then
                    response.ContentType = "application/vnd.ms-powerpoint"
                    response.AddHeader("content-disposition", "attachment;filename=" & _strSourceFile)
                Else
                    response.ContentType = "application/force-download"
                    response.AddHeader("content-disposition", "attachment;filename=" & _strSourceFile)
                End If

                response.AddHeader("Content-Length", dataLength.ToString())
                response.Charset = ""
                response.Cache.SetCacheability(HttpCacheability.NoCache)



                While True
                    If response.IsClientConnected Then
                        Dim bytesRead As Integer = reader.Read(buffer, 0, buffer.Length)

                        If bytesRead = 0 Then

                            Exit While
                        Else
                            response.OutputStream.Write(buffer, 0, bytesRead)
                            response.Flush()
                        End If
                    Else

                        Exit While
                    End If
                End While

            Catch ex As Exception

                response.Write("Error : " + ex.Message)
            Finally

                Try

                    If reader IsNot Nothing Then
                        reader.Close()
                    End If


                    If response__1 IsNot Nothing Then
                        response__1.Close()
                    End If


                    response.[End]()

                Catch gr As Exception

                End Try
            End Try

        Catch ex As Exception
            poraka = "Problem pri prevzemanje na File of FTP:" + ex.Message 'Prevod
            Return False
        End Try
        Return True
    End Function
    Public Function ZemiFileOdByte(ByVal bData As Byte(), ByVal imeFile As String, ByRef responseObj As HttpResponse, ByRef poraka As String) As Boolean

        responseObj.AddHeader("Content-Type", "application/pdf")
        responseObj.AddHeader("content-disposition", "attachment;filename=" & imeFile)

        responseObj.AddHeader("Content-Length", bData.Length)
        responseObj.Charset = ""
        responseObj.Cache.SetCacheability(HttpCacheability.NoCache)


        While True
            If responseObj.IsClientConnected Then

                Dim ms As MemoryStream = New MemoryStream(bData, 0, bData.Length)
                Dim bytesRead As Integer = ms.ReadByte()
                If bytesRead = 0 Then

                    Exit While
                Else
                    responseObj.OutputStream.Write(bData, 0, bData.Length)
                    responseObj.Flush()
                End If
            Else

                Exit While
            End If
        End While

    End Function
    Public Function ZemiFileLokalnoOsnoven(ByVal pateka As String, ByVal ime As String, ByRef response As HttpResponse, ByRef poraka As String) As Boolean
    End Function

    Public Function PratiFileOdByte(ByVal bData As Byte(), ByVal imeFile As String, ByRef responseObj As HttpResponse, ByRef poraka As String) As Boolean

        responseObj.AddHeader("Content-Type", "application/pdf")
        responseObj.AddHeader("content-disposition", "attachment;filename=" & imeFile)

        responseObj.AddHeader("Content-Length", bData.Length)
        responseObj.Charset = ""
        responseObj.Cache.SetCacheability(HttpCacheability.NoCache)


        While True
            If responseObj.IsClientConnected Then

                Dim ms As MemoryStream = New MemoryStream(bData, 0, bData.Length)
                Dim bytesRead As Integer = ms.ReadByte()
                If bytesRead = 0 Then

                    Exit While
                Else
                    responseObj.OutputStream.Write(bData, 0, bData.Length)
                    responseObj.Flush()
                End If
            Else

                Exit While
            End If
        End While

        responseObj.End()
        Return True
    End Function
End Class
