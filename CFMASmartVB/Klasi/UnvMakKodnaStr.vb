Option Strict On
Imports System.IO
Imports System.Text

Public Class UnvMakKodnaStr
    Private Structure MapAscUnicode
        Dim OrigChar As Char
        Dim Prvbajt As Byte
        Dim VtorBajt As Byte
    End Structure

    Private Structure MapAscCodeInverseMK
        Dim OrigPrvByte As Byte
        Dim OrigVtorByte As Byte
        Dim RezChar As Char
    End Structure

    Private Shared acMap(127) As MapAscUnicode
    Private Shared acMapInverseMK(255) As MapAscCodeInverseMK
    Private Shared IniciranoE As Boolean = False

    Private Shared acMap_UTF8_MK(127) As MapAscUnicode
    Private Shared acMapInverse_UTF8_MK As Hashtable
    Private Shared IniciranoE_UTF8_MK As Boolean = False


    Private Shared Sub InicMap()
        If IniciranoE Then
            Return
        End If
        IniciranoE = True

        Dim iW As Integer
        For iW = 0 To 127
            acMap(iW).OrigChar = Chr(iW)
            acMap(iW).Prvbajt = 0
            acMap(iW).VtorBajt = Convert.ToByte(iW)
        Next

        For iW = 0 To 255
            acMapInverseMK(iW).OrigPrvByte = 255
            acMapInverseMK(iW).OrigVtorByte = 255
        Next

        DodajMap("A"c, &H4, &H10)
        DodajMap("a"c, &H4, &H30)

        DodajMap("B"c, &H4, &H11)
        DodajMap("b"c, &H4, &H31)

        DodajMap("V"c, &H4, &H12)
        DodajMap("v"c, &H4, &H32)

        DodajMap("G"c, &H4, &H13)
        DodajMap("g"c, &H4, &H33)

        DodajMap("D"c, &H4, &H14)
        DodajMap("d"c, &H4, &H34)

        'If Zemi.JazikEkr = "SR" Then
        'DodajMap("\"c, &H4, &H2)
        'DodajMap("|"c, &H4, &H52)
        'Else
        DodajMap("\"c, &H4, &H3)
        DodajMap("|"c, &H4, &H53)
        'End If

        DodajMap("E"c, &H4, &H15)
        DodajMap("e"c, &H4, &H35)

        DodajMap("@"c, &H4, &H16)
        DodajMap("`"c, &H4, &H36)

        DodajMap("Z"c, &H4, &H17)
        DodajMap("z"c, &H4, &H37)

        DodajMap("Y"c, &H4, &H5)
        DodajMap("y"c, &H4, &H55)

        DodajMap("I"c, &H4, &H18)
        DodajMap("i"c, &H4, &H38)

        DodajMap("J"c, &H4, &H8)
        DodajMap("j"c, &H4, &H58)

        DodajMap("K"c, &H4, &H1A)
        DodajMap("k"c, &H4, &H3A)

        DodajMap("L"c, &H4, &H1B)
        DodajMap("l"c, &H4, &H3B)

        DodajMap("Q"c, &H4, &H9)
        DodajMap("q"c, &H4, &H59)

        DodajMap("M"c, &H4, &H1C)
        DodajMap("m"c, &H4, &H3C)

        DodajMap("N"c, &H4, &H1D)
        DodajMap("n"c, &H4, &H3D)

        DodajMap("W"c, &H4, &HA)
        DodajMap("w"c, &H4, &H5A)

        DodajMap("O"c, &H4, &H1E)
        DodajMap("o"c, &H4, &H3E)

        DodajMap("P"c, &H4, &H1F)
        DodajMap("p"c, &H4, &H3F)

        DodajMap("R"c, &H4, &H20)
        DodajMap("r"c, &H4, &H40)

        DodajMap("S"c, &H4, &H21)
        DodajMap("s"c, &H4, &H41)

        DodajMap("T"c, &H4, &H22)
        DodajMap("t"c, &H4, &H42)

        DodajMap("U"c, &H4, &H23)
        DodajMap("u"c, &H4, &H43)

        'If Zemi.JazikEkr = "SR" Then
        'DodajMap("]"c, &H4, &HB)
        ' DodajMap("}"c, &H4, &H5B)
        ' Else
        DodajMap("]"c, &H4, &HC)
        DodajMap("}"c, &H4, &H5C)

        ' End If

        DodajMap("^"c, &H4, &H27)
        DodajMap("~"c, &H4, &H47)

        DodajMap("X"c, &H4, &HF)
        DodajMap("x"c, &H4, &H5F)

        DodajMap("C"c, &H4, &H26)
        DodajMap("c"c, &H4, &H46)

        DodajMap("H"c, &H4, &H25)
        DodajMap("h"c, &H4, &H45)

        DodajMap("F"c, &H4, &H24)
        DodajMap("f"c, &H4, &H44)

        DodajMap("["c, &H4, &H28)
        DodajMap("{"c, &H4, &H48)
    End Sub

    Private Shared Sub DodajMap(ByVal wChar As Char, ByVal PrvBajt As Byte, ByVal VtorBajt As Byte)
        With acMap(Asc(wChar))
            .OrigChar = wChar
            .Prvbajt = PrvBajt
            .VtorBajt = VtorBajt
        End With

        With acMapInverseMK(VtorBajt)
            .OrigPrvByte = PrvBajt
            .OrigVtorByte = VtorBajt
            .RezChar = wChar
        End With
    End Sub

    Public Shared Function CistoKonv(ByVal wOrig As String) As String
        InicMap()

        '        MessageBox.Show("The total number of encoded bytes is: " & btText.Length.ToString())
        Try
            'Pazi mora BINARNO da se raboti so fileto, a ne TEXT -- inaku ne raboti vo red
            Dim wPoc As Integer = 0
            Dim wCh As Byte
            Dim wChPrv As Byte

            Dim encText As New System.Text.UTF8Encoding()
            Dim btText() As Byte
            btText = encText.GetBytes(wOrig)

            Dim wLen As Integer = btText.Length
            Dim acBytes(wLen * 2 - 1) As Byte

            If wLen <> wOrig.Length Then
                Return wOrig    'Ako bytes ne e isto so dolzzinata na stringot, togass ima EXTENDED karakteri vo UTF-8 envodingot
                'pa ne reskirame, go vrakame nazad originalniot string
            End If

            Dim iW As Integer
            For iW = 0 To wLen - 1
                wCh = btText(iW)

                If wCh < 32 OrElse wCh > 127 Then
                    wChPrv = 0
                ElseIf acMap(wCh).OrigChar = Chr(255) Then
                    wChPrv = 0
                Else
                    wChPrv = acMap(wCh).Prvbajt
                    wCh = acMap(wCh).VtorBajt
                End If

                acBytes(iW * 2) = wCh
                acBytes(iW * 2 + 1) = wChPrv       'CUdno, no vistinito
            Next

            Return Encoding.Unicode.GetString(acBytes, 0, wLen * 2)
        Catch eX As Exception
            MsgBox(eX.Message)
        End Try

        Return ""
    End Function

    'Ovaa rutina NE E PROVERENA
    Public Shared Function CistoKonvVoAscii(ByVal wOrig As String) As String
        InicMap()

        '        MessageBox.Show("The total number of encoded bytes is: " & btText.Length.ToString())

        Try
            'Pazi mora BINARNO da se raboti so fileto, a ne TEXT -- inaku ne raboti vo red
            Dim wPoc As Integer = 0
            Dim wCh As Byte
            Dim wChPrv As Byte

            Dim encText As New System.Text.UTF8Encoding()
            Dim btText() As Byte
            btText = encText.GetBytes(wOrig)

            Dim wLen As Integer = btText.Length
            Dim acBytes(wLen - 1) As Byte

            '''MsgBox("sega bajti " & wLen)
            '''MsgBox("Dolzina na string " & wOrig.Length)

            '''  MsgBox(btText(0).ToString + " " + btText(1).ToString + " " + btText(2).ToString + " " + btText(3).ToString + " " + btText(4).ToString + " " + btText(5).ToString + " " + btText(6).ToString + " " + btText(7).ToString)

            '''''  Return wOrig

            If wLen <> wOrig.Length * 2 Then
                Return wOrig    'Ako bytes ne e isto so dolzzinata na stringot * 2, togass ne e slicaj na dosleden Unicode, tuku mozda e UTF-8 ili sl.
                'pa ne reskirame, go vrakame nazad originalniot string
            End If

            Dim wDodadeni As Integer = -1
            Dim iW As Integer = 0
            Dim wZapisi As Byte
            Dim wNajden As Boolean
            While iW <= wLen - 1
                wCh = btText(iW)
                wChPrv = btText(iW + 1)     'Sigurno postoi zasto gore proverivme deka e wLen paren
                wNajden = False
                If wChPrv = 0 OrElse wChPrv = 4 Then        'Fiksirano za MK i OSNOVNATA
                    If acMapInverseMK(wCh).OrigPrvByte = wChPrv Then
                        wZapisi = Convert.ToByte(acMapInverseMK(wCh).RezChar)
                        wNajden = True
                    End If
                End If

                If wNajden Then
                    wDodadeni += 1
                    acBytes(wDodadeni) = wZapisi
                Else
                    wDodadeni += 1
                    acBytes(wDodadeni) = wCh

                    wDodadeni += 1
                    acBytes(wDodadeni) = wChPrv       'CUdno, no vistinito
                End If
            End While

            Return Encoding.UTF8.GetString(acBytes, 0, wDodadeni + 1)
        Catch eX As Exception
            MsgBox(eX.Message)
        End Try

        Return ""
    End Function

    '------------------------------------------------------------------------------------
    'Saga su TUF-8 konverzijata, koja ja koristi izgleda VB za rabota vo maskite
    Private Shared Sub InicMap_UTF8_MK()
        If IniciranoE_UTF8_MK Then
            Return
        End If
        IniciranoE_UTF8_MK = True

        Dim iW As Integer
        For iW = 0 To 127
            acMap_UTF8_MK(iW).OrigChar = Chr(iW)
            acMap_UTF8_MK(iW).Prvbajt = 0
            acMap_UTF8_MK(iW).VtorBajt = Convert.ToByte(iW)
        Next

        acMapInverse_UTF8_MK = New hashtable()

        DodajMap_UTF8_MK("A"c, &HD0, &H90)
        DodajMap_UTF8_MK("a"c, &HD0, &HB0)

        DodajMap_UTF8_MK("B"c, &HD0, &H91)
        DodajMap_UTF8_MK("b"c, &HD0, &HB1)

        DodajMap_UTF8_MK("V"c, &HD0, &H92)
        DodajMap_UTF8_MK("v"c, &HD0, &HB2)

        DodajMap_UTF8_MK("G"c, &HD0, &H93)
        DodajMap_UTF8_MK("g"c, &HD0, &HB3)

        DodajMap_UTF8_MK("D"c, &HD0, &H94)
        DodajMap_UTF8_MK("d"c, &HD0, &HB4)

        'If Zemi.JazikEkr = "SR" Then
        'DodajMap_UTF8_MK("\"c, &HD0, &H82)
        'DodajMap_UTF8_MK("|"c, &HD1, &H92)
        'Else
        DodajMap_UTF8_MK("\"c, &HD0, &H83)
        DodajMap_UTF8_MK("|"c, &HD1, &H93)
        'End If

        DodajMap_UTF8_MK("E"c, &HD0, &H95)
        DodajMap_UTF8_MK("e"c, &HD0, &HB5)

        DodajMap_UTF8_MK("@"c, &HD0, &H96)
        DodajMap_UTF8_MK("`"c, &HD0, &HB6)

        DodajMap_UTF8_MK("Z"c, &HD0, &H97)
        DodajMap_UTF8_MK("z"c, &HD0, &HB7)

        DodajMap_UTF8_MK("Y"c, &HD0, &H85)
        DodajMap_UTF8_MK("y"c, &HD1, &H95)

        DodajMap_UTF8_MK("I"c, &HD0, &H98)
        DodajMap_UTF8_MK("i"c, &HD0, &HB8)

        DodajMap_UTF8_MK("J"c, &HD0, &H88)
        DodajMap_UTF8_MK("j"c, &HD1, &H98)

        DodajMap_UTF8_MK("K"c, &HD0, &H9A)
        DodajMap_UTF8_MK("k"c, &HD0, &HBA)

        DodajMap_UTF8_MK("L"c, &HD0, &H9B)
        DodajMap_UTF8_MK("l"c, &HD0, &HBB)

        DodajMap_UTF8_MK("Q"c, &HD0, &H89)
        DodajMap_UTF8_MK("q"c, &HD1, &H99)

        DodajMap_UTF8_MK("M"c, &HD0, &H9C)
        DodajMap_UTF8_MK("m"c, &HD0, &HBC)

        DodajMap_UTF8_MK("N"c, &HD0, &H9D)
        DodajMap_UTF8_MK("n"c, &HD0, &HBD)

        DodajMap_UTF8_MK("W"c, &HD0, &H8A)
        DodajMap_UTF8_MK("w"c, &HD1, &H9A)

        DodajMap_UTF8_MK("O"c, &HD0, &H9E)
        DodajMap_UTF8_MK("o"c, &HD0, &HBE)

        DodajMap_UTF8_MK("P"c, &HD0, &H9F)
        DodajMap_UTF8_MK("p"c, &HD0, &HBF)

        DodajMap_UTF8_MK("R"c, &HD0, &HA0)
        DodajMap_UTF8_MK("r"c, &HD1, &H80)

        DodajMap_UTF8_MK("S"c, &HD0, &HA1)
        DodajMap_UTF8_MK("s"c, &HD1, &H81)

        DodajMap_UTF8_MK("T"c, &HD0, &HA2)
        DodajMap_UTF8_MK("t"c, &HD1, &H82)

        DodajMap_UTF8_MK("U"c, &HD0, &HA3)
        DodajMap_UTF8_MK("u"c, &HD1, &H83)

        ''If Zemi.JazikEkr = "SR" Then
        'DodajMap_UTF8_MK("]"c, &HD0, &H8B)
        'DodajMap_UTF8_MK("}"c, &HD1, &H9B)
        'Else
        DodajMap_UTF8_MK("]"c, &HD0, &H8C)
        DodajMap_UTF8_MK("}"c, &HD1, &H9C)
        ' End If

        DodajMap_UTF8_MK("^"c, &HD0, &HA7)
        DodajMap_UTF8_MK("~"c, &HD1, &H87)

        DodajMap_UTF8_MK("X"c, &HD0, &H8F)
        DodajMap_UTF8_MK("x"c, &HD1, &H9F)

        DodajMap_UTF8_MK("C"c, &HD0, &HA6)
        DodajMap_UTF8_MK("c"c, &HD1, &H86)

        DodajMap_UTF8_MK("H"c, &HD0, &HA5)
        DodajMap_UTF8_MK("h"c, &HD1, &H85)

        DodajMap_UTF8_MK("F"c, &HD0, &HA4)
        DodajMap_UTF8_MK("f"c, &HD1, &H84)

        DodajMap_UTF8_MK("["c, &HD0, &HA8)
        DodajMap_UTF8_MK("{"c, &HD1, &H88)
    End Sub

    Private Shared Sub DodajMap_UTF8_MK(ByVal wChar As Char, ByVal PrvBajt As Byte, ByVal VtorBajt As Byte)
        With acMap_UTF8_MK(Asc(wChar))
            .OrigChar = wChar
            .Prvbajt = PrvBajt
            .VtorBajt = VtorBajt
        End With

        Dim wInverse As MapAscCodeInverseMK
        With wInverse
            .OrigPrvByte = PrvBajt
            .OrigVtorByte = VtorBajt
            .RezChar = wChar

            Dim wKluc As Integer = PrvBajt * 256 + VtorBajt
            acMapInverse_UTF8_MK(wKluc) = wInverse
        End With
    End Sub

    Public Shared Function CistoKonv_UTF8_MK(ByVal wOrig As String) As String
        InicMap_UTF8_MK()

        '        MessageBox.Show("The total number of encoded bytes is: " & btText.Length.ToString())
        Try
            'Pazi mora BINARNO da se raboti so fileto, a ne TEXT -- inaku ne raboti vo red
            Dim wPoc As Integer = 0
            Dim wCh As Byte
            Dim wChPrv As Byte

            Dim encText As New System.Text.UTF8Encoding()
            Dim btText() As Byte
            btText = encText.GetBytes(wOrig)

            Dim wLen As Integer = btText.Length
            Dim acBytes(wLen * 2 - 1) As Byte

            ''MsgBox(btText(0).ToString + " " + btText(1).ToString + " " + btText(2).ToString + " " + btText(3).ToString + " " + btText(4).ToString + " " + btText(5).ToString + " " + btText(6).ToString + " " + btText(7).ToString)

            If wLen <> wOrig.Length Then
                Return wOrig    'Ako bytes ne e isto so dolzzinata na stringot, togass ima EXTENDED karakteri vo UTF-8 envodingot
                'pa ne reskirame, go vrakame nazad originalniot string
            End If

            Dim wDodadeni As Integer = -1
            Dim iW As Integer
            For iW = 0 To wLen - 1
                wCh = btText(iW)

                If wCh <= 32 OrElse wCh > 127 Then
                    wChPrv = 0
                ElseIf acMap_UTF8_MK(wCh).Prvbajt = 0 Then
                    wChPrv = 0
                Else
                    wChPrv = acMap_UTF8_MK(wCh).Prvbajt
                    wCh = acMap_UTF8_MK(wCh).VtorBajt
                End If

                If wChPrv <> 0 Then
                    wDodadeni += 1
                    acBytes(wDodadeni) = wChPrv      'Ovde prvo go stava FLAGOT, kako sto e normalno
                End If

                wDodadeni += 1
                acBytes(wDodadeni) = wCh
            Next

            ''' MsgBox(acBytes(0).ToString + " " + acBytes(1).ToString + " " + acBytes(2).ToString + " " + acBytes(3).ToString + " " + acBytes(4).ToString + " " + acBytes(5).ToString + " " + acBytes(6).ToString + " " + acBytes(7).ToString)

            '''' MsgBox(Encoding.Unicode.GetString(acBytes, 0, wDodadeni + 1))

            Return Encoding.UTF8.GetString(acBytes, 0, wDodadeni + 1)
        Catch eX As Exception
            MsgBox(eX.Message)
        End Try

        Return ""
    End Function

    Public Shared Function CistoKonv_Vo_Ascii_UTF8_MK(ByVal wOrig As String) As String
        InicMap_UTF8_MK()

        Try
            'Pazi mora BINARNO da se raboti so fileto, a ne TEXT -- inaku ne raboti vo red
            Dim wPoc As Integer = 0
            Dim wCh As Byte
            Dim wChPrv As Byte
            Dim wChZapisi As Byte

            Dim encText As New System.Text.UTF8Encoding()
            Dim btText() As Byte
            btText = encText.GetBytes(wOrig)

            Dim wLen As Integer = btText.Length
            Dim acBytes(wLen - 1) As Byte

            ''' MsgBox(btText(0).ToString + " " + btText(1).ToString + " " + btText(2).ToString + " " + btText(3).ToString + " " + btText(4).ToString + " " + btText(5).ToString + " " + btText(6).ToString + " " + btText(7).ToString)

            If wLen = wOrig.Length Then
                Return wOrig    'Ako bytes e isto so dolzzinata na stringot, togass nema EXTENDED karakteri vo UTF-8 envodingot
                'pa ne reskirame, go vrakame nazad originalniot string
            End If

            Dim wKluc As Integer
            Dim wInverse As MapAscCodeInverseMK
            Dim wRezInverse As Object

            Dim wDodadeni As Integer = -1
            Dim iW As Integer = 0
            While iW <= wLen - 1
                'Pazi iw se menuva na poveke mesta vo loopot
                wCh = btText(iW)
                wChZapisi = wCh

                If (wCh = 208 OrElse wCh = 209) AndAlso iW < wLen - 1 Then     'Malku fiksirano, za MK poddrska
                    wChPrv = btText(iW + 1)

                    wKluc = wCh * 256 + wChPrv
                    wRezInverse = acMapInverse_UTF8_MK(wKluc)
                    If Not wRezInverse Is Nothing Then
                        wInverse = CType(wRezInverse, MapAscCodeInverseMK)
                        wChZapisi = Convert.ToByte(wInverse.RezChar)
                        iW += 1
                    End If
                End If

                wDodadeni += 1
                acBytes(wDodadeni) = wChZapisi

                iW += 1
            End While

            ''' MsgBox(acBytes(0).ToString + " " + acBytes(1).ToString + " " + acBytes(2).ToString + " " + acBytes(3).ToString + " " + acBytes(4).ToString + " " + acBytes(5).ToString + " " + acBytes(6).ToString + " " + acBytes(7).ToString)

            '''' MsgBox(Encoding.Unicode.GetString(acBytes, 0, wDodadeni + 1))

            Return Encoding.UTF8.GetString(acBytes, 0, wDodadeni + 1)
        Catch eX As Exception
            MsgBox(eX.Message)
        End Try

        Return ""
    End Function
End Class
