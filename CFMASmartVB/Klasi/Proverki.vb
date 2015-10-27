Public Class Proverki

    Public Function ProverkaBarKod(ByVal barKod As String) As Boolean
        Dim nizaBarKod() = barKod.ToCharArray()

        Dim daliEbarkod As Boolean = False


        If nizaBarKod(0).Equals("+") Then

            For index = 1 To nizaBarKod.Length - 1

                If IsNumeric(nizaBarKod(index)) Then

                    daliEbarkod = True
                Else
                    daliEbarkod = False
                    Exit For

                End If

            Next
        End If
        Return daliEbarkod
    End Function

    Public Function ProverkaSifra(ByVal sifra As String) As Boolean
        Dim nizaSifra() = sifra.ToCharArray()
        Dim daliEsifra As Boolean = False


        For index = 0 To nizaSifra.Length - 1

            If IsNumeric(nizaSifra(index)) Then

                daliEsifra = True
            Else
                daliEsifra = False
                Exit For

            End If

        Next

        Return daliEsifra
    End Function



End Class
