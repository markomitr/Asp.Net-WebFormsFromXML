Public Class RedXML

    Private sId As String
    Private tipKontr As String
    Private redBr As Integer
    Private gr As Integer
    Private naslGr As String
    Private tipGrupaEnum As NasEnum.TipGrupa
    Private tGrupa As String
    Private im As String
    Private prik As String
    Private opis As String

    Private showVredonsti As Char
    Private praznaVrednost As Char
    Public Enum KakovREDXML
        Maska
        Kontrola
    End Enum

    Public Sub New(ByVal sID As String,
                   ByVal tipKontr As String,
                   ByVal redBr As String,
                   ByVal gr As String,
                   ByVal naslGr As String,
                   ByVal im As String,
                   ByVal prik As String,
                   ByVal opis As String,
                   ByVal prikazVredonsti As Char,
                   ByVal daliPraznaVrednost As Char)

        Me.SorsId = sID
        Me.TipKontrola = tipKontr
        Me.RedenBroj = redBr
        Me.Grupa = gr
        Me.NaslovGrupa = naslGr
        Me.Ime = im
        Me.Prikazi = prik
        Me.OpisPole = opis
        Me.DaliPrikaziVrednost = prikazVredonsti
        Me.DaliDodadiPrazenRed = daliPraznaVrednost

    End Sub

    Public Sub New(ByVal sorsId As String)
        Me.New()
        Me.SorsId = sorsId

    End Sub
    Public Sub New()
    End Sub

#Region "GetSet"
    Property SorsId As String
        Get
            Return sId
        End Get
        Set(ByVal Value As String)
            sId = Value
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

    Property RedenBroj As Integer
        Get
            Return redBr
        End Get
        Set(ByVal Value As Integer)
            redBr = Value
        End Set
    End Property

    Property Grupa As Integer
        Get
            Return gr
        End Get
        Set(ByVal Value As Integer)
            gr = Value
        End Set
    End Property

    Property NaslovGrupa As String
        Get
            Return naslGr
        End Get
        Set(ByVal Value As String)
            naslGr = Value
        End Set
    End Property

    Private Property TipNaGrupaStr As String
        Get
            Return tGrupa
        End Get
        Set(ByVal Value As String)
            tGrupa = Value
        End Set
    End Property

    Public ReadOnly Property TipGrupa As NasEnum.TipGrupa
        Get
            If Not TipNaGrupaStr Is Nothing Then

                If Me.TipNaGrupaStr.ToUpper() = "RADIOLIST" Then
                    tipGrupaEnum = NasEnum.TipGrupa.RadioList
                Else
                    tipGrupaEnum = NasEnum.TipGrupa.Obicna
                End If

            Else
                tipGrupaEnum = NasEnum.TipGrupa.Obicna
            End If
            Return tipGrupaEnum
        End Get

    End Property
    Property Ime As String
        Get
            Return im
        End Get
        Set(ByVal Value As String)
            im = Value
        End Set
    End Property
    Property OpisPole As String
        Get
            Return opis
        End Get
        Set(ByVal Value As String)
            opis = Value
        End Set
    End Property


    Property Prikazi As String
        Get
            Return prik
        End Get
        Set(ByVal Value As String)
            prik = Value
        End Set
    End Property
    ReadOnly Property PrikaziKontrola As Boolean
        Get
            Return Prikazi.ToUpper = "D"
        End Get
    End Property
    Property DaliPrikaziVrednost As Char
        Get
            Return showVredonsti
        End Get
        Set(ByVal Value As Char)
            If Char.ToUpper(Value) = "D" Then
                Me.showVredonsti = "D"
            Else
                Me.showVredonsti = "N"
            End If
        End Set
    End Property
    Property DaliDodadiPrazenRed As Char
        Get
            Return praznaVrednost
        End Get
        Set(ByVal Value As Char)
            If Char.ToUpper(Value) = "D" Then
                Me.praznaVrednost = "D"
            Else
                Me.praznaVrednost = "N"
            End If
        End Set
    End Property
#End Region


    Public Function generirajRedOdRecnik(ByVal recnikXmlRed As Dictionary(Of String, String), ByRef poraka As String) As Boolean

        SetirajPrazenObj()

        If Not recnikXmlRed Is Nothing Then

            If recnikXmlRed.Keys.Contains("SORSID") Then
                Me.SorsId = recnikXmlRed("SORSID")
            End If
            If recnikXmlRed.Keys.Contains("TIPKONTROLA") Then
                Me.TipKontrola = recnikXmlRed("TIPKONTROLA")
            End If
            If recnikXmlRed.Keys.Contains("REDBR") Then
                Me.RedenBroj = recnikXmlRed("REDBR")
            End If
            If recnikXmlRed.Keys.Contains("GRUPA") Then
                Me.Grupa = recnikXmlRed("GRUPA")
            End If
            If recnikXmlRed.Keys.Contains("NASLOVGRUPA") Then
                Me.NaslovGrupa = recnikXmlRed("NASLOVGRUPA")
            End If

            If recnikXmlRed.Keys.Contains("TIPGRUPA") Then
                Me.TipNaGrupaStr = recnikXmlRed("TIPGRUPA")
            End If

            If recnikXmlRed.Keys.Contains("IME") Then
                Me.Ime = recnikXmlRed("IME")
            End If
            If recnikXmlRed.Keys.Contains("OPIS") Then
                Me.OpisPole = recnikXmlRed("OPIS")
            End If
            If recnikXmlRed.Keys.Contains("PRIKAZI") Then
                Me.Prikazi = recnikXmlRed("PRIKAZI")
            End If
            If recnikXmlRed.Keys.Contains("MOZEPRAZNO") Then
                Me.DaliDodadiPrazenRed = recnikXmlRed("MOZEPRAZNO")
            End If
            If recnikXmlRed.Keys.Contains("PRIKAZVREDNOST") Then
                Me.DaliPrikaziVrednost = recnikXmlRed("PRIKAZVREDNOST")
            End If

            If recnikXmlRed.Keys.Contains("FONT") Then
            End If
            If recnikXmlRed.Keys.Contains("FONTSIZE") Then
            End If
            If recnikXmlRed.Keys.Contains("VISINA") Then
            End If
            If recnikXmlRed.Keys.Contains("SIRINA") Then
            End If
            If recnikXmlRed.Keys.Contains("BOJA") Then
            End If

        Else
            poraka = "Ima problem so citanje na podatocite od recnikot za XMLRed - objektot e Null" 'Prevod
        End If

        Return False
    End Function

    Public Sub SetirajPrazenObj()
        Me.SorsId = ""
        Me.TipKontrola = ""
        Me.RedenBroj = 0
        Me.Grupa = 0
        Me.NaslovGrupa = ""
        Me.Ime = ""
        Me.Prikazi = ""
        Me.OpisPole = ""
        Me.DaliPrikaziVrednost = ""
        Me.DaliDodadiPrazenRed = ""
    End Sub

End Class
