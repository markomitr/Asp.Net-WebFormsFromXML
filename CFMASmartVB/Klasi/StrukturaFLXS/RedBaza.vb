Public Class RedBaza

    Private sId As String
    Private kor As String
    Private grupaKor As String
    Private lok As String
    Private p1 As String

    Private tipS As String
    Private sorsR As String

    Private tipKontr As String
    Private dolz As Integer

    Private cP11 As String
    Private cP12 As String
    Private cP13 As String
    Private cP14 As String
    Private nP15 As Decimal
    Private nP16 As Decimal
    Private iP17 As Integer
    Private iP18 As Integer
    Private dP19 As DateTime
    Private dP20 As DateTime

    'pP# promenlivte sluzat za opis na soodvetnoto # pole
    Private pP11 As String
    Private pP12 As String
    Private pP13 As String
    Private pP14 As String
    Private pP15 As String
    Private pP16 As String
    Private pP17 As String
    Private pP18 As String
    Private pP19 As String
    Private pP20 As String

    Public Sub New(ByVal sId As String,
                   ByVal kor As String,
                   ByVal grupaKor As String,
                   ByVal lok As String,
                   ByVal param1 As String,
                   ByVal tipS As String,
                   ByVal sorsR As String,
                   ByVal tipKontr As String,
                   ByVal dolz As Integer,
                   ByVal cP11 As String,
                   ByVal pP11 As String,
                   ByVal cP12 As String,
                   ByVal pP12 As String,
                   ByVal cP13 As String,
                   ByVal pP13 As String,
                   ByVal cP14 As String,
                   ByVal pP14 As String,
                   ByVal nP15 As Decimal,
                   ByVal pP15 As String,
                   ByVal nP16 As Decimal,
                   ByVal pP16 As String,
                   ByVal iP17 As Integer,
                   ByVal pP17 As String,
                   ByVal iP18 As Integer,
                   ByVal pP18 As String,
                   ByVal dP19 As DateTime,
                   ByVal pP19 As String,
                   ByVal dP20 As DateTime,
                   ByVal pP20 As String)

        Me.sId = sId
        Me.kor = kor
        Me.grupaKor = grupaKor
        Me.lok = lok
        Me.p1 = param1

        Me.tipS = tipS
        Me.sorsR = sorsR
        Me.tipKontr = tipKontr

        Me.dolz = dolz
        Me.cP11 = cP11
        Me.cP12 = cP12
        Me.cP13 = cP13
        Me.cP14 = cP14
        Me.nP15 = nP15
        Me.nP16 = nP16
        Me.iP17 = iP17
        Me.iP18 = iP18
        Me.dP19 = dP19
        Me.dP20 = dP20

        Me.pP11 = pP11
        Me.pP12 = pP12
        Me.pP13 = pP13
        Me.pP14 = pP14
        Me.pP15 = pP15
        Me.pP16 = pP16
        Me.pP17 = pP17
        Me.pP18 = pP18
        Me.pP19 = pP19
        Me.pP20 = pP20


    End Sub

    Public Sub New(ByVal sorsId As String)
        sId = sorsId
    End Sub

    Public Sub New()

    End Sub

    Public Function IncijalizirajOdDataSet(ByVal ds As DataSet, ByRef poraka As String) As Boolean

        Dim nastavka As String = ""

        If ds.Tables.Count > 0 AndAlso Not ds.Tables(0) Is Nothing Then


            For index = 0 To ds.Tables(0).Rows.Count - 1

                If Not ds.Tables(0).Rows(index)(nastavka + "SorsId") Is Nothing Then
                    Me.SorsId = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "SorsId"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "Korisnik") Is Nothing Then
                    Me.Korisnik = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "Korisnik"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "GrupaKor") Is Nothing Then
                    Me.GrupaKorisnici = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "GrupaKor"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "Lokacija") Is Nothing Then
                    Me.Lokocija = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "Lokacija"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "Param1") Is Nothing Then
                    Me.Param1 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "Param1"))
                End If


                If Not ds.Tables(0).Rows(index)(nastavka + "Dozvola") Is Nothing Then
                    Me.Dolzina = Konv.ObjVoInt(ds.Tables(0).Rows(index)(nastavka + "Dozvola"))
                End If

                If Not ds.Tables(0).Rows(index)(nastavka + "TipKontrol") Is Nothing Then
                    Me.TipKontrola = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "TipKontrol"))
                End If


                If Not ds.Tables(0).Rows(index)(nastavka + "TipSors") Is Nothing Then
                    Me.TipSors = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "TipSors"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "SorsRun") Is Nothing Then
                    Me.SorsRun = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "SorsRun"))
                End If


                If Not ds.Tables(0).Rows(index)(nastavka + "cParam11") Is Nothing Then
                    Me.cParam11 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "cParam11"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "pParam11") Is Nothing Then
                    Me.pParam11 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "pParam11"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "cParam12") Is Nothing Then
                    Me.cParam12 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "cParam12"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "pParam12") Is Nothing Then
                    Me.pParam12 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "pParam12"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "cParam13") Is Nothing Then
                    Me.cParam13 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "cParam13"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "pParam13") Is Nothing Then
                    Me.pParam13 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "pParam13"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "cParam14") Is Nothing Then
                    Me.cParam14 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "cParam14"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "pParam14") Is Nothing Then
                    Me.pParam14 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "pParam14"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "nParam15") Is Nothing Then
                    Me.nParam15 = Konv.ObjVoDec(ds.Tables(0).Rows(index)(nastavka + "nParam15"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "pParam15") Is Nothing Then
                    Me.pParam15 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "pParam15"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "nParam16") Is Nothing Then
                    Me.nParam16 = Konv.ObjVoDec(ds.Tables(0).Rows(index)(nastavka + "nParam16"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "pParam16") Is Nothing Then
                    Me.pParam16 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "pParam16"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "iParam17") Is Nothing Then
                    Me.iParam17 = Konv.ObjVoInt(ds.Tables(0).Rows(index)(nastavka + "iParam17"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "pParam17") Is Nothing Then
                    Me.pParam17 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "pParam17"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "iParam18") Is Nothing Then
                    Me.iParam18 = Konv.ObjVoInt(ds.Tables(0).Rows(index)(nastavka + "iParam17"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "pParam18") Is Nothing Then
                    Me.pParam18 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "pParam18"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "dParam19") Is Nothing Then
                    Me.dParam19 = Konv.ObjVoDate(ds.Tables(0).Rows(index)(nastavka + "dParam19"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "pParam19") Is Nothing Then
                    Me.pParam19 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "pParam19"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "dParam20") Is Nothing Then
                    Me.dParam20 = Konv.ObjVoDate(ds.Tables(0).Rows(index)(nastavka + "dParam20"))
                End If
                If Not ds.Tables(0).Rows(index)(nastavka + "pParam20") Is Nothing Then
                    Me.pParam20 = Konv.ObjVoStr(ds.Tables(0).Rows(index)(nastavka + "pParam20"))
                End If

            Next

            Return True

        End If
        poraka = "Moze da ne postoi SorsID vo Baza za odreden ELEMENT od XML!"
        Return False
    End Function

    Public Sub SetirajPrazenObj()

        Me.sId = ""
        Me.kor = ""
        Me.grupaKor = ""
        Me.lok = ""
        Me.p1 = ""

        Me.tipS = ""
        Me.sorsR = ""
        Me.tipKontr = ""

        Me.dolz = 0
        Me.cP11 = ""
        Me.cP12 = ""
        Me.cP13 = ""
        Me.cP14 = ""
        Me.nP15 = 0
        Me.nP16 = 0
        Me.iP17 = 0
        Me.iP18 = 0
        Me.dP19 = Nothing
        Me.dP20 = Nothing

        Me.pP11 = ""
        Me.pP12 = ""
        Me.pP13 = ""
        Me.pP14 = ""
        Me.pP15 = ""
        Me.pP16 = ""
        Me.pP17 = ""
        Me.pP18 = ""
        Me.pP19 = ""
        Me.pP20 = ""
    End Sub

#Region "Get/Set"

    Property SorsId As String
        Get
            Return sId
        End Get
        Set(ByVal Value As String)
            sId = Value
        End Set
    End Property

    Property Korisnik As String
        Get
            Return kor
        End Get
        Set(ByVal Value As String)
            kor = Value
        End Set
    End Property

    Property GrupaKorisnici As String
        Get
            Return grupaKor
        End Get
        Set(ByVal Value As String)
            grupaKor = Value
        End Set
    End Property

    Property Lokocija As String
        Get
            Return lok
        End Get
        Set(ByVal Value As String)
            lok = Value
        End Set
    End Property

    Property Param1 As String
        Get
            Return p1
        End Get
        Set(ByVal Value As String)
            p1 = Value
        End Set
    End Property

    Property TipSors As String
        Get
            Return tipS
        End Get
        Set(ByVal Value As String)
            tipS = Value
        End Set
    End Property

    Property SorsRun As String
        Get
            Return sorsR
        End Get
        Set(ByVal Value As String)
            sorsR = Value
        End Set
    End Property

    Property TipKontrola As String
        Get
            Return tipKontr
        End Get
        Set(ByVal Value As String)
            tipKontr = Value
        End Set
    End Property

    Property Dolzina As Integer
        Get
            Return dolz
        End Get
        Set(ByVal Value As Integer)
            dolz = Value
        End Set
    End Property

    Property cParam11 As String
        Get
            Return cP11
        End Get
        Set(ByVal Value As String)
            cP11 = Value
        End Set
    End Property

    Property cParam12 As String
        Get
            Return cP12
        End Get
        Set(ByVal Value As String)
            cP12 = Value
        End Set
    End Property

    Property cParam13 As String
        Get
            Return cP13
        End Get
        Set(ByVal Value As String)
            cP13 = Value
        End Set
    End Property

    Property cParam14 As String
        Get
            Return cP14
        End Get
        Set(ByVal Value As String)
            cP14 = Value
        End Set
    End Property

    Property nParam15 As Decimal
        Get
            Return nP15
        End Get
        Set(ByVal Value As Decimal)
            nP15 = Value
        End Set
    End Property

    Property nParam16 As Decimal
        Get
            Return nP16
        End Get
        Set(ByVal Value As Decimal)
            nP16 = Value
        End Set
    End Property

    Property iParam17 As Integer
        Get
            Return iP17
        End Get
        Set(ByVal Value As Integer)
            iP17 = Value
        End Set
    End Property

    Property iParam18 As Integer
        Get
            Return iP18
        End Get
        Set(ByVal Value As Integer)
            iP18 = Value
        End Set
    End Property

    Property dParam19 As DateTime
        Get
            Return dP19
        End Get
        Set(ByVal Value As DateTime)
            dP19 = Value
        End Set
    End Property

    Property dParam20 As DateTime
        Get
            Return dP20
        End Get
        Set(ByVal Value As DateTime)
            dP20 = Value
        End Set
    End Property

    Property pParam11 As String
        Get
            Return pP11
        End Get
        Set(ByVal Value As String)
            pP11 = Value
        End Set
    End Property
    Property pParam12 As String
        Get
            Return pP12
        End Get
        Set(ByVal Value As String)
            pP12 = Value
        End Set
    End Property
    Property pParam13 As String
        Get
            Return pP13
        End Get
        Set(ByVal Value As String)
            pP13 = Value
        End Set
    End Property
    Property pParam14 As String
        Get
            Return pP14
        End Get
        Set(ByVal Value As String)
            pP14 = Value
        End Set
    End Property
    Property pParam15 As String
        Get
            Return pP15
        End Get
        Set(ByVal Value As String)
            pP15 = Value
        End Set
    End Property
    Property pParam16 As String
        Get
            Return pP16
        End Get
        Set(ByVal Value As String)
            pP16 = Value
        End Set
    End Property
    Property pParam17 As String
        Get
            Return pP17
        End Get
        Set(ByVal Value As String)
            pP17 = Value
        End Set
    End Property
    Property pParam18 As String
        Get
            Return pP18
        End Get
        Set(ByVal Value As String)
            pP18 = Value
        End Set
    End Property
    Property pParam19 As String
        Get
            Return pP19
        End Get
        Set(ByVal Value As String)
            pP19 = Value
        End Set
    End Property
    Property pParam20 As String
        Get
            Return pP20
        End Get
        Set(ByVal Value As String)
            pP20 = Value
        End Set
    End Property



#End Region
End Class
