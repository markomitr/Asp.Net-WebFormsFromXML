Public Class Artikal
    Implements IBazaObj
    Dim sifraArt As String
    Dim iArt As String

    Property Sifra_Art As String
        Get
            Return sifraArt
        End Get
        Set(ByVal value As String)
            sifraArt = value
        End Set
    End Property


    Property ImeArt As String
        Get
            Return iArt
        End Get
        Set(ByVal value As String)
            iArt = value
        End Set
    End Property
End Class
