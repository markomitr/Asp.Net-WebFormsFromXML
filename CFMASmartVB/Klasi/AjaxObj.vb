Public Class AjaxObj
    Dim tipNaKlasa As NasEnum.Klasi
    Dim tipNaKlasaStr As String = ""
    Dim artObj As Artikal
    Dim kupObj As Komintent

    Dim obj As IBazaObj

    Public Property TipObjekt As String
        Get
            If tipNaKlasa = NasEnum.Klasi.Komintent Then
                Return NasEnum.Klasi.Komintent.ToString()
            ElseIf tipNaKlasa = NasEnum.Klasi.Artikal Then
                Return NasEnum.Klasi.Artikal.ToString()
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            tipNaKlasaStr = value
        End Set
    End Property

    Public Property Objekt As IBazaObj
        Get
            Return obj
        End Get

        Set(ByVal value As IBazaObj)

            If TypeOf value Is Artikal Then
                tipNaKlasa = NasEnum.Klasi.Artikal
            ElseIf TypeOf value Is Komintent Then
                tipNaKlasa = NasEnum.Klasi.Komintent
            End If
            obj = value
        End Set
    End Property
    Public Sub setirajObjekt(ByVal bazaObj As IBazaObj)

    End Sub
    Public Sub New()


    End Sub

End Class
