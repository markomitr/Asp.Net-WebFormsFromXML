Imports System.Xml
Imports System.Data.SqlClient

Public Class MasinaMeni
    Private listaXmlRedovi As List(Of Dictionary(Of String, String))
    Private listaRedXmlMeni As List(Of RedMeni)
    Private webStranaSodrizna As Control
    Private Property SodrzinaKontrola As Control
        Get
            Return Me.webStranaSodrizna
        End Get
        Set(ByVal value As Control)
            Me.webStranaSodrizna = value
        End Set
    End Property
    Public Sub New(ByVal sodrzinaPage As Control)
        Me.SodrzinaKontrola = sodrzinaPage
    End Sub
    Public Function GenerirajMeni(ByVal pateka As String, ByRef poraka As String) As Boolean
        If Not ZemiMeniXML(pateka, poraka) Then
            Return False
        End If
        If Not NacrtajMeni(poraka) Then
            Return False
        End If

        Return True
    End Function
    Private Function ZemiMeniXML(ByVal pateka As String, ByRef poraka As String) As Boolean
        Dim brojac As Integer = 0
        listaXmlRedovi = New List(Of Dictionary(Of String, String))
        Dim recnikXmlRed As Dictionary(Of String, String)
        Dim reader As XmlTextReader = New XmlTextReader(pateka)

        Try
            Do While (reader.Read())
                If reader.NodeType = XmlNodeType.Element Then
                    If reader.Name.ToUpper().Equals("MENIRED") Then
                        recnikXmlRed = New Dictionary(Of String, String)

                        Select Case reader.NodeType
                            Case XmlNodeType.Element  'Display beginning of element.
                                If reader.HasAttributes Then 'If attributes exist

                                    While reader.MoveToNextAttribute()
                                        'Display attribute name and value.
                                        recnikXmlRed.Add(reader.Name.ToUpper(), reader.Value)
                                    End While
                                    listaXmlRedovi.Add(recnikXmlRed)
                                End If

                            Case XmlNodeType.Text 'Display the text in each element.

                            Case XmlNodeType.EndElement 'Display end of element.

                        End Select
                    End If

                End If
                brojac = brojac + 1
            Loop
            reader.Close()

            Return generirajListaRedMeni(poraka)
        Catch ex As Exception
            reader.Close()
            poraka = "Greska pri citanje na Meni XML - " & pateka & " # " & ex.Message 'Prevod
            Return False
        End Try

    End Function
    Private Function generirajListaRedMeni(ByVal poraka As String) As Boolean
        listaRedXmlMeni = New List(Of RedMeni)
        If Not listaXmlRedovi Is Nothing And listaXmlRedovi.Count > 0 Then
            For index = 0 To listaXmlRedovi.Count - 1
                Dim rMeni As RedMeni
                rMeni = New RedMeni(listaXmlRedovi(index)("Naslov".ToUpper()), listaXmlRedovi(index)("ImeFile".ToUpper()))
                listaRedXmlMeni.Add(rMeni)
            Next
            Return True
        Else
            poraka = "Prazna lista za redovi vo XML MENI!" 'Prevod
            Return False
        End If

    End Function
    Private Function NacrtajMeni(ByRef poraka As String) As Boolean
        If Not Me.SodrzinaKontrola Is Nothing Then
            If Not listaRedXmlMeni Is Nothing AndAlso listaRedXmlMeni.Count > 0 Then
                ' Me.SodrzinaKontrola.Controls.Add(New LiteralControl("<div id=""Content"">"))
                'Me.SodrzinaKontrola.Controls.Add(New LiteralControl("<div id=""NaslovMaska"">МЕНИ</div>"))
                For i = 0 To listaRedXmlMeni.Count - 1
                    Dim kontrolaTxt As String = ""
                    kontrolaTxt = "<div onclick=""navigacijaMeni(this)"" id=""Meni" & (i + 1) & """ class=""Meni"" imefile=""" & listaRedXmlMeni(i).PatekaFile & """>"
                    kontrolaTxt = kontrolaTxt & UnvMakKodnaStr.CistoKonv(listaRedXmlMeni(i).NaslovMeni)
                    kontrolaTxt = kontrolaTxt & "</div>"
                    Me.SodrzinaKontrola.Controls.Add(New LiteralControl(kontrolaTxt))
                Next
                '  Me.SodrzinaKontrola.Controls.Add(New LiteralControl("</div>"))
                Return True
            Else
                poraka = "Nema RED.MENI od XML. Proveri ko Meni.xml" 'Prevod
                Return False
            End If
        Else
            poraka = "Nema kontrola(placeHolder) vo koja bi se stavile kontrolite" 'Prevod
            Return False
        End If
    End Function


End Class
