Public Class Komintent
    Implements IBazaObj

    Dim sifraKup As String
    Dim iKup As String

    Property Sifra_Kup As String
        Get
            Return sifraKup
        End Get


        Set(ByVal value As String)
            sifraKup = value
        End Set
    End Property


    Property ImeKup As String
        Get
            Return iKup
        End Get
        Set(ByVal value As String)
            iKup = value
        End Set
    End Property
End Class
