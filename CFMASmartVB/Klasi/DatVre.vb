Option Strict On
Imports System.Globalization

Public Class DatVre
    Public Shared Function IscistiOdVremeKakoString(ByVal Datum As Date) As String
        Dim wcDatum As String = Datum.ToString
        Dim wKade As Integer = wcDatum.IndexOf(":")
        If wKade > 2 Then
            Return wcDatum.Substring(0, wKade - 2).Trim
        End If
        Return wcDatum
    End Function

    Public Shared Function PrvDatumGod(ByVal Datum As Date) As Date
        Return Convert.ToDateTime("01/01/" & Convert.ToString(Datum.Year))
        ''''OVaa da se revidira--zavisi jako od Regional Settings
    End Function

    Public Shared Function PrvDatumMesec(ByVal Datum As Date) As Date
        Return Convert.ToDateTime("01/" & Convert.ToString(Datum.Month) & "/" & Convert.ToString(Datum.Year))
        ''''OVaa da se revidira--zavisi jako od Regional Settings
    End Function

    Public Shared Function PrvDatumMesecNova(ByVal Datum As Date) As Date
        Return Convert.ToDateTime(New Date(Datum.Year, Datum.Month, 1))
    End Function

    Public Shared Function NekojDatVoMesec(ByVal Den As String, ByVal Datum As Date) As Date
        Return Convert.ToDateTime(Den & "/" & Convert.ToString(Datum.Month) & "/" & Convert.ToString(Datum.Year))
        ''''OVaa da se revidira--zavisi jako od Regional Settings
    End Function

    Public Shared Function PosledenDatVoMesec(ByVal Datum As Date) As Date
        ''''Return Convert.ToDateTime(datum. & Convert.ToString(Datum.Month) & "/" & Convert.ToString(Datum.Year))
        Return DateAdd("d", -Datum.Day, DateAdd("m", 1, Datum))
    End Function

    Public Shared Function OdzemiMeseci(ByVal Datum As Date, ByVal MinusMes As Integer) As Date
        Return Convert.ToDateTime("01/" & Konv.ObjVoStr(Datum.Month - MinusMes) & "/" & Convert.ToString(Datum.Year))
        ''''OVaa da se revidira--zavisi jako od Regional Settings
    End Function

    Public Shared Function PrvDatumGod(ByVal cDatum_dd_mm_gggg As String) As Date
        'Datumot go ocekuva vo format   dd/mm/gggg
        Dim wcVreme As String = "00:00:00"
        Dim wDatum As Date = DatVre.NapraviDatumVreme(cDatum_dd_mm_gggg, wcVreme)
        Return wDatum
    End Function

    Public Shared Function NapraviDatumVreme(ByVal cDatum As String, ByVal cVreme As String) As Date
        Dim NasDateTimeFormatInfo As New DateTimeFormatInfo()
        Dim cDatumVreme As String = cDatum & " " & cVreme

        NasDateTimeFormatInfo = CType(DateTimeFormatInfo.CurrentInfo.Clone, DateTimeFormatInfo)

        NasDateTimeFormatInfo.ShortDatePattern = "dd/MM/yyyy"
        NasDateTimeFormatInfo.LongTimePattern = "HH:mm:ss"

        Return Date.Parse(cDatumVreme, NasDateTimeFormatInfo)
    End Function

    Public Shared Function NapraviDatumMDY(ByVal cDatum As String, ByVal cVreme As String, ByRef Datum As Date) As Boolean
        Dim NasDateTimeFormatInfo As New DateTimeFormatInfo()
        Dim cDatumVreme As String = cDatum & " " & cVreme
        Dim bOK As Boolean

        NasDateTimeFormatInfo = CType(DateTimeFormatInfo.CurrentInfo.Clone, DateTimeFormatInfo)

        NasDateTimeFormatInfo.ShortDatePattern = "MM/dd/yyyy"
        NasDateTimeFormatInfo.LongTimePattern = "HH:mm:ss"

        Try
            Datum = Date.Parse(cDatumVreme, NasDateTimeFormatInfo)
            bOK = True
        Catch
            bOK = False
        End Try
        Return bOK
    End Function

    Public Shared Function PoslednaSekundaVoDatumot(ByVal Datumot As Date) As Date
        Return Datumot.AddHours(23).AddMinutes(59).AddSeconds(59)
    End Function
End Class