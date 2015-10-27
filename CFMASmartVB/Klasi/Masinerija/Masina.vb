Imports System.Xml
Imports System.Data.SqlClient

Public Class Masina

    Dim stranaTekovnaPAGE As NasaStranaBase

    Dim ListaRedXML As List(Of RedXML)
    Dim ListaRedBaza As List(Of RedBaza)
    Dim ListaKontOpis As List(Of KontrolaOpis)
    Dim ListaGrupi As List(Of Grupa)

    Private listaXmlRedovi As List(Of Dictionary(Of String, String))
    Private listaSORSIDodXMLRed As List(Of String)

    Private maskaObj As Maska

    Dim poraki As List(Of String)

    Private webStranaSodrizna As Control
    Private daliWebPAGESodrizna As Boolean = False

    Private daliHtmlSodrzina As Boolean = False

#Region "GetSet"

    Public ReadOnly Property TekovnaStranica As NasaStranaBase
        Get
            Return stranaTekovnaPAGE
        End Get
    End Property
    Public ReadOnly Property MaskaNaslov As String
        Get
            If Not maskaObj Is Nothing Then
                Return UnvMakKodnaStr.CistoKonv(maskaObj.NaslovNaMaska)
            Else
                Return "Наслов"
            End If
        End Get
    End Property
    Public ReadOnly Property PorakiErrLista As List(Of String)
        Get
            Return poraki
        End Get
    End Property
    Public ReadOnly Property PorakiErr
        Get
            Return vratiPorakaErrCela()
        End Get
    End Property
    Public ReadOnly Property DajMiPorakaZaVnes As String
        Get
            Return poraki(dajMiSeldenBrPoraka())
        End Get
    End Property
    Public ReadOnly Property DajMiBrojPorakaZaVnes As Integer
        Get
            Return dajMiSeldenBrPoraka()
        End Get
    End Property
    Private Property SodrzinaVoPAGE As Control
        Get
            Return Me.webStranaSodrizna
        End Get
        Set(ByVal value As Control)

            Me.daliHtmlSodrzina = False
            Me.daliWebPAGESodrizna = True

            Me.webStranaSodrizna = value


        End Set
    End Property
    Public ReadOnly Property ZemiListaKontrOpis As List(Of KontrolaOpis)
        Get
            Return Me.ListaKontOpis
        End Get
    End Property

#End Region

#Region "Funkcii"

#Region "Konstruktori"

    Public Sub New(ByVal netControl As Control, ByVal tekovnaPage As NasaStranaBase)
        Me.New(tekovnaPage)

        Me.SodrzinaVoPAGE = netControl


    End Sub

    Public Sub New(ByVal tekovnaPage As NasaStranaBase)

        ListaKontOpis = New List(Of KontrolaOpis)
        ListaRedXML = New List(Of RedXML)
        ListaRedBaza = New List(Of RedBaza)
        ListaGrupi = New List(Of Grupa)

        listaSORSIDodXMLRed = New List(Of String)


        poraki = New List(Of String)
        Me.stranaTekovnaPAGE = tekovnaPage

    End Sub

#End Region

    Public Function Raboti(ByVal kontrola As Control) As Boolean

        Me.SodrzinaVoPAGE = kontrola

        Return Generiraj()

    End Function

    Private Function Generiraj() As Boolean
        If Me.daliHtmlSodrzina Or Me.daliWebPAGESodrizna Then

            If Me.Sploti(Me.PorakiErrLista(Me.DajMiBrojPorakaZaVnes)) Then
                GenerirajGrupi(ListaKontOpis) ' Se generirat grupi od kontroli
                For i = 0 To ListaGrupi.Count - 1

                    ' Kontrolite se generiraat vo grupi
                    If Not ListaGrupi(i).Raboti(Me.PorakiErrLista(Me.DajMiBrojPorakaZaVnes)) Then
                        Return False
                    End If
                Next
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Private Function Sploti(ByRef poraka As String) As Boolean

        SortirajListaRedBr() ' Se sortirat elementite od XML po atributot RedBr

        Dim brParovi As Integer = 0
        Dim brElementi = 0
        If ListaRedBaza.Count = ListaRedXML.Count Then
            'brojot na element e ist sto znaci deka i vo xml i bazata imam ist br na redovi
            brElementi = ListaRedBaza.Count
            For index = 0 To ListaRedXML.Count - 1

                For j = 0 To ListaRedBaza.Count - 1

                    If ListaRedXML(index).SorsId.ToUpper() = ListaRedBaza(j).SorsId.ToUpper() Then

                        Dim ObjKontrOpis = New KontrolaOpis(ListaRedXML(index).SorsId, Me.TekovnaStranica)
                        If Not ObjKontrOpis.splotiRedXMLredBaza(ListaRedXML(index), ListaRedBaza(j), Me.PorakiErrLista(Me.DajMiBrojPorakaZaVnes)) Then
                            poraki(dajMiSeldenBrPoraka()) = "Problem pri incijalizacija na Zbiren objekt of - Baza i XML (redovi) -" & ListaRedXML(index).SorsId & " <> " & ListaRedBaza(j).TipSors

                            Return False
                        End If

                        If ListaKontOpis Is Nothing Then

                            ListaKontOpis = New List(Of KontrolaOpis)

                        End If

                        ListaKontOpis.Add(ObjKontrOpis)
                        brParovi = brParovi + 1

                    End If

                Next

            Next

            If Not brParovi.Equals(brElementi) Then
                Me.PorakiErrLista(Me.DajMiBrojPorakaZaVnes) = "Postojat elementi(Redovi) koi ne mozat da se sparat - Baza i XML. Mozebi imaat razlicno SorsID"
                Return False
            End If
            Return True
        Else
            Me.PorakiErrLista(Me.DajMiBrojPorakaZaVnes) = "Brojot na (redovi) vo Baza i vo XML se razlikuvaat"
            Return False

        End If
    End Function


#Region "Grupi"

    Private Sub GenerirajGrupi(ByVal lista As List(Of KontrolaOpis))

        Dim pomRedBrGr As Integer = -1
        For index = 0 To lista.Count - 1

            If Not daliPostoiGrupa(lista(index).XML.Grupa, pomRedBrGr) Then
                Dim gr As Grupa
                gr = New Grupa(Me.SodrzinaVoPAGE, lista(index).XML.Grupa, lista(index).XML.NaslovGrupa, lista(index).XML.TipGrupa)
                ListaGrupi.Add(gr)
            End If
            ListaGrupi(pomRedBrGr).DodadiKontrola(lista(index))
        Next

        SortirajGrupi()
    End Sub

    Public Sub SortirajGrupi()
        Dim pom = ListaGrupi.Count
        For index = 0 To pom - 1
            For j = pom - 1 To index + 1 Step -1
                If (ListaGrupi(j).BrojGrupa < ListaGrupi(j - 1).BrojGrupa) Then
                    Dim pom1 As Grupa
                    pom1 = ListaGrupi(j)
                    ListaGrupi(j) = ListaGrupi(j - 1)
                    ListaGrupi(j - 1) = pom1
                End If
            Next
        Next
    End Sub

    Public Function daliPostoiGrupa(ByVal grpBr As Integer, ByRef redBr As Integer) As Boolean

        If ListaGrupi Is Nothing Then
            ListaGrupi = New List(Of Grupa)
        End If

        For index = 0 To ListaGrupi.Count - 1
            If grpBr = ListaGrupi(index).BrojGrupa Then
                redBr = index
                Return True
            End If

        Next

        redBr = ListaGrupi.Count
        Return False
    End Function
#End Region

#Region "RedXML"
    Public Function ProcitajXML(ByVal pateka As String, ByRef poraka As String) As Boolean

        Dim brojac As Integer = 0

        listaXmlRedovi = New List(Of Dictionary(Of String, String))

        Dim naslovOdMaskaXMLRed As Dictionary(Of String, String)
        Dim recnikXmlRed As Dictionary(Of String, String)

        Dim reader As XmlTextReader = New XmlTextReader(pateka)

        Try

            Do While (reader.Read())

                If reader.NodeType = XmlNodeType.Element Then

                    If reader.Name.ToUpper().Equals("MASKA") Or reader.Name.ToUpper().Equals("KONTROLA") Then

                        Dim tipRedXML As RedXML.KakovREDXML

                        If reader.Name.ToUpper().Equals("MASKA") Then
                            tipRedXML = RedXML.KakovREDXML.Maska
                        ElseIf reader.Name.ToUpper().Equals("KONTROLA") Then
                            tipRedXML = RedXML.KakovREDXML.Kontrola
                        End If

                        Select Case reader.NodeType
                            Case XmlNodeType.Element  'Display beginning of element.
                                If reader.HasAttributes Then 'If attributes exist


                                    If tipRedXML = RedXML.KakovREDXML.Kontrola Then
                                        recnikXmlRed = New Dictionary(Of String, String)
                                    ElseIf tipRedXML = RedXML.KakovREDXML.Maska Then
                                        naslovOdMaskaXMLRed = New Dictionary(Of String, String)
                                    End If


                                    While reader.MoveToNextAttribute()
                                        'Display attribute name and value.
                                        If tipRedXML = RedXML.KakovREDXML.Kontrola Then
                                            recnikXmlRed.Add(reader.Name.ToUpper(), reader.Value)
                                        ElseIf tipRedXML = RedXML.KakovREDXML.Maska Then
                                            naslovOdMaskaXMLRed.Add(reader.Name.ToUpper(), reader.Value)
                                        End If

                                    End While

                                    If tipRedXML = RedXML.KakovREDXML.Kontrola Then
                                        listaXmlRedovi.Add(recnikXmlRed)
                                    End If


                                End If

                            Case XmlNodeType.Text 'Display the text in each element.

                            Case XmlNodeType.EndElement 'Display end of element.

                        End Select
                    End If

                End If

                brojac = brojac + 1

            Loop

            generirajListaRedXML(poraka) 'Se sozdavaat elementite od tip RedXML
            generirajMaskaRedXML(naslovOdMaskaXMLRed, poraka)
            reader.Close()

            Return (True)
        Catch ex As Exception

            Me.PorakiErrLista(Me.DajMiBrojPorakaZaVnes) = "Greska pri citanje na XML - " & pateka & " # " & ex.Message
            Return False
        End Try
    End Function

    Private Sub generirajListaRedXML(ByRef poraka As String)
        If ListaRedXML Is Nothing Then
            ListaRedXML = New List(Of RedXML)
        End If

        For index = 0 To listaXmlRedovi.Count - 1

            Dim rXml As RedXML = New RedXML()
            rXml.generirajRedOdRecnik(listaXmlRedovi(index), poraka)
            ListaRedXML.Add(rXml)
            listaSORSIDodXMLRed.Add(rXml.SorsId)
        Next

    End Sub
    Private Sub generirajMaskaRedXML(ByVal vrednosti As Dictionary(Of String, String), ByRef poraka As String)

        If Not vrednosti Is Nothing Then
            maskaObj = New Maska(vrednosti("NASLOV"))
        Else
            maskaObj = New Maska("Нема Наслов")
        End If


    End Sub

    Private Sub SortirajListaRedBr()

        Dim pom = ListaRedXML.Count
        For index = 0 To pom - 1
            For j = pom - 1 To index + 1 Step -1
                If (ListaRedXML(j).RedenBroj < ListaRedXML(j - 1).RedenBroj) Then
                    Dim pom1 As RedXML
                    pom1 = ListaRedXML(j)
                    ListaRedXML(j) = ListaRedXML(j - 1)
                    ListaRedXML(j - 1) = pom1
                End If
            Next
        Next
    End Sub
#End Region

#Region "RedBaza"
    ''' <summary>
    ''' So ovaa funkcija se vcituvaat redovite(opisite za kontrolata) od Baza. 
    ''' Za da se povika ovaa funkcija mora prvo da se vcita XML-ot, bidejki preku nego
    ''' se zemaat SorsID.
    ''' </summary>
    ''' <param name="poraka">Poraka ako ima greska</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ProcitajOdBaza(ByRef poraka As String) As Boolean
        Try
            Dim db As New BazaDB()
            For index = 0 To listaSORSIDodXMLRed.Count - 1

                Dim ds As New DataSet

                Dim listSqlParam As New List(Of SqlParameter)
                Dim sqlParam As SqlParameter


                sqlParam = New SqlParameter("@SorsId", SqlDbType.VarChar)
                sqlParam.Value = listaSORSIDodXMLRed(index).ToString()
                listSqlParam.Add(sqlParam)
                ' Zasega ne gi koristime - Not USED

                'sqlParam = New SqlParameter("@Korisnik", SqlDbType.VarChar)
                'sqlParam.Value = listaSORSIDodXMLRed(index).ToString()
                'sqlParam = New SqlParameter("@GrupaKor", SqlDbType.VarChar)
                'sqlParam.Value = listaSORSIDodXMLRed(index).ToString()
                'sqlParam = New SqlParameter("@Lokacija", SqlDbType.VarChar)
                'sqlParam.Value = listaSORSIDodXMLRed(index).ToString()
                'sqlParam = New SqlParameter("@Param1", SqlDbType.VarChar)
                'sqlParam.Value = listaSORSIDodXMLRed(index).ToString()

                If db.generirajDataSet(ds, BazaDB.TipNaSqlKomanda.Procedura, "sp_PodigniRedFLXS_Sors", listSqlParam, poraka) Then
                    Dim rBaza As RedBaza = New RedBaza()
                    If Not rBaza.IncijalizirajOdDataSet(ds, poraka) Then
                        Return False
                    End If
                    ListaRedBaza.Add(rBaza)
                Else
                    Return False
                End If

            Next
            Return True
        Catch ex As Exception
            poraka = poraka + ex.Message
            Return False
        End Try

    End Function
#End Region

#Region "Poraki"
    Private Function dajMiSeldenBrPoraka() As Integer
        If poraki Is Nothing Then
            poraki = New List(Of String)
        End If
        poraki.Add("")
        Return poraki.Count - 1
    End Function

    Private Function vratiPorakaErrCela() As String

        Dim pomErr As String = ""

        For index = 0 To poraki.Count - 1
            If Not String.IsNullOrEmpty(poraki(index)) Then
                pomErr = pomErr & index + 1 & ". " & poraki(index) & "###"
            End If

        Next

        Return pomErr
    End Function
#End Region

    'Ovie funkcii zasega ne gi koristam!

    'Private Sub DodadiListaRedXML(ByVal sID As String,
    '           ByVal tipKontr As String,
    '           ByVal redBr As Integer,
    '           ByVal gr As Integer,
    '           ByVal naslGr As String,
    '           ByVal im As String,
    '           ByVal prik As String,
    '           ByVal opis As String,
    '           ByVal prikazVredonsti As Char,
    '           ByVal daliPraznaVrednost As Char)


    '    Dim ObjRedXML As New RedXML
    '    ObjRedXML.SorsId = sID
    '    ObjRedXML.TipKontrola = tipKontr
    '    ObjRedXML.RedenBroj = redBr
    '    ObjRedXML.Grupa = gr
    '    ObjRedXML.NaslovGrupa = naslGr
    '    ObjRedXML.Ime = im
    '    ObjRedXML.Prikazi = prik
    '    ObjRedXML.OpisPole = opis
    '    ObjRedXML.DaliPrikaziVrednost = prikazVredonsti
    '    ObjRedXML.DaliDodadiPrazenRed = daliPraznaVrednost

    '    If ListaRedXML Is Nothing Then
    '        ListaRedXML = New List(Of RedXML)
    '    End If

    '    ListaRedXML.Add(ObjRedXML)

    'End Sub

    'Private Sub DodadiListaRedBaza(ByVal sId As String,
    '               ByVal kor As String,
    '               ByVal grupaKor As String,
    '               ByVal lok As String,
    '               ByVal tipS As String,
    '               ByVal sorsR As String,
    '               ByVal tipKontr As String,
    '               ByVal dolz As Integer,
    '               ByVal cP11 As String,
    '               ByVal cP12 As String,
    '               ByVal cP13 As String,
    '               ByVal cP14 As String,
    '               ByVal nP15 As Decimal,
    '               ByVal nP16 As Decimal,
    '               ByVal iP17 As Integer,
    '               ByVal iP18 As Integer,
    '               ByVal cP19 As DateTime,
    '               ByVal cP20 As DateTime)

    '    Dim ObjRedBaza As New RedBaza

    '    ObjRedBaza.SorsId = sId
    '    ObjRedBaza.Korisnik = kor
    '    ObjRedBaza.GrupaKorisnici = grupaKor
    '    ObjRedBaza.Lokocija = lok
    '    ObjRedBaza.TipSors = tipS
    '    ObjRedBaza.SorsRun = sorsR
    '    ObjRedBaza.TipKontrola = tipKontr
    '    ObjRedBaza.Dolzina = dolz
    '    ObjRedBaza.cParam11 = cP11
    '    ObjRedBaza.cParam12 = cP12
    '    ObjRedBaza.cParam13 = cP13
    '    ObjRedBaza.cParam14 = cP14
    '    ObjRedBaza.nParam15 = nP15
    '    ObjRedBaza.nParam16 = nP16
    '    ObjRedBaza.iParam17 = iP17
    '    ObjRedBaza.iParam18 = iP18
    '    ObjRedBaza.dParam19 = cP19
    '    ObjRedBaza.dParam20 = cP20

    '    If ListaRedBaza Is Nothing Then
    '        ListaRedBaza = New List(Of RedBaza)
    '    End If

    '    ListaRedBaza.Add(ObjRedBaza)

    'End Sub
#End Region

End Class
