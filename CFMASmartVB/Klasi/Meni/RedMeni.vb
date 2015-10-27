Public Class RedMeni

    Dim naslov As String
    Dim pathFile As String

    Public ReadOnly Property PatekaFile As String
        Get
            Return Me.pathFile
        End Get
    End Property
    Public ReadOnly Property NaslovMeni As String
        Get
            Return Me.naslov
        End Get
    End Property
    Public Sub New(ByVal naslov As String, ByVal pateka As String)
        Me.naslov = naslov
        Me.pathFile = pateka
    End Sub
End Class
