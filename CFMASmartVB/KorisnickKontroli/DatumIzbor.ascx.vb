Imports System.Globalization

Public Class DatumIzbor
    Inherits System.Web.UI.UserControl
    Implements NasiKlasi
    Dim datum As New Date

    Dim DaliPrvDatMes As Boolean



    Public Sub New()
        MyBase.New()

        datum = Date.Today
        DaliPrvDatMes = False

    End Sub
    Public Property DefVredonst As String Implements NasiKlasi.DefVrednost



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Mesec.MaxLength = 2
        Me.Den.MaxLength = 2
        Me.Godina.MaxLength = 4

        Dim denLblTxt = ""
        Dim mesecLblTxt = ""
        Dim godinaLblTxt = ""

        '''Ova treba dobro da se dotera.Ne e vaka ubavo, so resusite sami da znaat od kade da zemaat
        If NasaStranaBase.KojaDrzava = NasaStranaBase.Drzavi.Makedonija Then
            denLblTxt = My.Resources.GlavnaMaska_MK.lbl_datepic_den
            mesecLblTxt = My.Resources.GlavnaMaska_MK.lbl_datepic_mesec
            godinaLblTxt = My.Resources.GlavnaMaska_MK.lbl_datepic_godina
        ElseIf NasaStranaBase.KojaDrzava = NasaStranaBase.Drzavi.Kosovo Then
            denLblTxt = My.Resources.GlavnaMaska_KS.lbl_datepic_den
            mesecLblTxt = My.Resources.GlavnaMaska_KS.lbl_datepic_mesec
            godinaLblTxt = My.Resources.GlavnaMaska_KS.lbl_datepic_godina
        End If
        Me.LabelDen.Text = denLblTxt
        Me.LabelMesec.Text = mesecLblTxt
        Me.LabelGodina.Text = godinaLblTxt

        If Not IsPostBack Then

            Me.Den.Text = datum.Day.ToString()
            Me.Mesec.Text = datum.Month.ToString()
            Me.Godina.Text = datum.Year.ToString()

            If DaliPrvDatMes Then
                Dim d As Date = DatVre.PrvDatumMesecNova(Date.Now)

                Me.Den.Text = d.Day.ToString()
                Me.Mesec.Text = d.Month.ToString()
                Me.Godina.Text = d.Year.ToString()

            End If


        End If
    End Sub


    Public Property PDaliPrvDatMes() As Boolean
        Get
            Return DaliPrvDatMes
        End Get
        Set(ByVal value As Boolean)
            DaliPrvDatMes = value
        End Set
    End Property


    Public Sub dodadiNasId(ByVal id As String, ByVal attrName As String) Implements NasiKlasi.dodadiNasId
    End Sub

    Public ReadOnly Property IDKontrola As String Implements NasiKlasi.IDKontrola
        Get
            Return Me.ID
        End Get
    End Property

    Public ReadOnly Property Vrednost As String Implements NasiKlasi.Vrednost
        Get
            Return vratiDatum()
        End Get
    End Property
    Private Function vratiDatum() As String

        Dim den As String = Me.Den.Text
        Dim mesec As String = Me.Mesec.Text
        Dim god As String = Me.Godina.Text

        Dim datumStr = god & "-" & mesec & "-" & den
        'Dim datumStr = den & mesec & god

        'Dim datum As DateTime

        'Try
        '    datum = DateTime.ParseExact(datumStr, "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat)
        'Catch ex As Exception
        '    datum = Nothing
        'End Try

        'Return datum.ToString()

        Return datumStr
    End Function


End Class