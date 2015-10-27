Imports System.Data.SqlClient

Public Class Korisnik
    Implements IBazaObj

    Dim KorIme As String
    Dim Email As String
    Dim SifraKup As String
    Dim ImeKup As String
    Dim LokAdm As String
    Dim Saldo As String = ""
    Dim daliLogiran As Boolean
    Dim IPAdr As String = ""
    Dim _sifraOe As String = ""

    'Property za promenlivi
    Public Property KorisnikIme() As String
        Get
            Return Me.KorIme
        End Get
        Set(ByVal value As String)
            Me.KorIme = value
        End Set
    End Property
    Public Property KorisnikEmail() As String
        Get
            Return Me.Email
        End Get
        Set(ByVal value As String)
            Me.Email = value
        End Set
    End Property
    Public Property KorisnikSifraKup() As String
        Get
            Return Me.SifraKup
        End Get
        Set(ByVal value As String)
            Me.SifraKup = value
        End Set
    End Property
    Public Property KorisnikImeKup() As String
        Get
            Return Me.ImeKup
        End Get
        Set(ByVal value As String)
            Me.ImeKup = value
        End Set
    End Property
    Public Property KorisnikLokAdm() As String
        Get
            If String.IsNullOrEmpty(Me.LokAdm) Then
                Return ""
            End If
            Return Me.LokAdm
        End Get
        Set(ByVal value As String)
            Me.LokAdm = value
        End Set
    End Property
    Public Property TekovnoSaldo() As String
        Get
            Return Me.Saldo
        End Get
        Set(ByVal value As String)
            Me.Saldo = value
        End Set
    End Property
    Public Property IPAdresa() As String
        Get
            Return Me.IPAdr
        End Get
        Set(ByVal value As String)
            Me.IPAdr = value
        End Set
    End Property
    Public Property KorisnikDaliLogiran() As Boolean
        Get
            Return Me.daliLogiran
        End Get
        Set(ByVal value As Boolean)
            Me.daliLogiran = value
        End Set
    End Property
    Public Property SifraOe() As String
        Get
            Return Me._sifraOe
        End Get
        Set(ByVal value As String)
            Me._sifraOe = value
        End Set
    End Property
    Private Sub vnesiAtributi(ByVal KorisnikIme As String, ByVal KorisnikEmail As String, ByVal KorisnikSifraKup As String, ByVal KorisnikImeKup As String, ByVal KorisnikLokAdm As String, ByVal Saldo As String, ByVal SifraOe As String)
        Me.KorIme = Konv.ObjVoStr(KorisnikIme)
        Me.Email = Konv.ObjVoStr(KorisnikEmail)
        Me.SifraKup = Konv.ObjVoStr(KorisnikSifraKup)
        Me.ImeKup = Konv.ObjVoStr(KorisnikImeKup)
        Me.LokAdm = Konv.ObjVoStr(KorisnikLokAdm)
        Me.TekovnoSaldo = Konv.ObjVoStr(Saldo)
        Me.daliLogiran = True
        Me.SifraOe = Konv.ObjVoStr(SifraOe)
    End Sub

    Public Sub New()

    End Sub

    Public Shared Function proveriKorisnik(ByVal KorisnikIme As String, ByVal Lozinka As String, ByRef korisnik As Korisnik, ByRef poraka As String) As Boolean

        poraka = ""
        Dim proceduraIme = "sp_PodigniNadvKor"
        Dim parametri As New List(Of SqlParameter)

        'Deklaracija na parametri kor(KorIme) i loz(Lozinka) za procedura sp_PodigniNadvKor
        Dim kor As SqlParameter = New SqlParameter("@KorIme", SqlDbType.VarChar, 15)
        kor.Value = KorisnikIme.Trim()
        Dim loz As SqlParameter = New SqlParameter("@Lozinka", SqlDbType.VarChar, 100)
        loz.Value = Lozinka.Trim()
        parametri.Add(kor)
        parametri.Add(loz)

        Try
            Dim db As New BazaDB()
            Dim ds As New DataSet
            If db.generirajDataSet(ds, BazaDB.TipNaSqlKomanda.Procedura, proceduraIme, parametri, poraka) Then
                If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 AndAlso Not ds.Tables(0) Is Nothing Then

                    korisnik.vnesiAtributi(ds.Tables(0).Rows(0)("KorIme"),
                                           ds.Tables(0).Rows(0)("email"), _
                                           ds.Tables(0).Rows(0)("Sifra_Kup"), _
                                           ds.Tables(0).Rows(0)("ImeKup"), _
                                           ds.Tables(0).Rows(0)("LokAdm"), _
                                           "", _
                                           "")
                    Return True
                Else
                    poraka = "Ne postoi takov par korisnik/lozinka!" 'Prevod
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            poraka = "Грешка во проверка корисник(login)  :" & ex.Message 'Prevod
            Return False
        End Try
    End Function

End Class
