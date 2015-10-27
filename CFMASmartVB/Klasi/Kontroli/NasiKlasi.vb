
''' <summary>
''' Vo ovoj interface definirame funkcii koi mora da gi sodrzi sekoja klasa
''' </summary>
''' <remarks></remarks>
Public Interface NasiKlasi

    Sub dodadiNasId(ByVal id As String, ByVal attrName As String)

    ReadOnly Property Vrednost As String
    ReadOnly Property IDKontrola As String
    Property DefVrednost As String

End Interface
