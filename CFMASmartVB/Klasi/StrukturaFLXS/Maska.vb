Public Class Maska

    Dim nasl As String

    Public ReadOnly Property NaslovNaMaska As String
        Get
            Return Me.nasl
        End Get
    End Property
    Public Sub New(ByVal naslov As String)
        Me.nasl = naslov
    End Sub
End Class
