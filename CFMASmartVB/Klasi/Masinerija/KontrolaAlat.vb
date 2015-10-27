Public Class KontrolaAlat : Inherits KontrolaObvivka

    Dim stranaTekovnaPAGE As NasaStranaBase

    Dim sorsID As String
    Dim korisnik As String
    Dim grupaKor As String
    Dim lokacija As String
    Dim dozvola As Char
    Dim tipSors As String
    Dim sorsRun As String
    Dim tipKontrola As NasEnum.TipKontrola
    Dim dolzina As String

    Dim listaRecniOdRadioButtons As Dictionary(Of String, String) ' Ova se uptorebuva samo koga imame kontroli od tipot Radiobuttons

    Public ReadOnly Property TekovnaStranica As NasaStranaBase
        Get
            Return stranaTekovnaPAGE
        End Get
    End Property
    Public Property ListaRadiButtons As Dictionary(Of String, String)
        Get
            Return listaRecniOdRadioButtons
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            listaRecniOdRadioButtons = value
        End Set
    End Property

#Region "Funkcii"
    Public Sub New(ByVal sodrzinaPage As Control, ByVal konfiguracijaKontrola As KontrolaOpis)
        MyBase.New(sodrzinaPage, konfiguracijaKontrola)
        InicijalizirajPolinja()

    End Sub

    Private Sub InicijalizirajPolinja()
        sorsID = MyBase.ZemiKontrolaFLXS.Baza.SorsId
        korisnik = MyBase.ZemiKontrolaFLXS.Baza.Korisnik
        grupaKor = MyBase.ZemiKontrolaFLXS.Baza.GrupaKorisnici
        lokacija = MyBase.ZemiKontrolaFLXS.Baza.Lokocija
        tipSors = MyBase.ZemiKontrolaFLXS.Baza.TipSors
        sorsRun = MyBase.ZemiKontrolaFLXS.Baza.SorsRun
        tipKontrola = MyBase.ZemiKontrolaFLXS.KontrolaNasTIP
        Me.stranaTekovnaPAGE = MyBase.ZemiKontrolaFLXS.TekovnaStranica
    End Sub

    Public Function Raboti(ByRef poraka As String, ByVal redenBrVoGrupa As Integer) As Boolean

        If tipKontrola = NasEnum.TipKontrola.Lista Then

            Dim testLista As NasaLista

            If MyBase.ZemiKontrolaFLXS.XML.DaliPrikaziVrednost.Equals("D"c) And MyBase.ZemiKontrolaFLXS.XML.DaliDodadiPrazenRed.Equals("D"c) Then

                testLista = New NasaLista(MyBase.ZemiKontrolaFLXS.SorsID, NasaLista.DaliPrazenRed.DodadiPrazenRed, NasaLista.DaliPrikazVrednost.PrikaziVrednost)
            ElseIf MyBase.ZemiKontrolaFLXS.XML.DaliPrikaziVrednost.Equals("D"c) Then

                testLista = New NasaLista(MyBase.ZemiKontrolaFLXS.SorsID, NasaLista.DaliPrazenRed.BezPrazenRed, NasaLista.DaliPrikazVrednost.PrikaziVrednost)

            ElseIf MyBase.ZemiKontrolaFLXS.XML.DaliDodadiPrazenRed.Equals("D"c) Then

                testLista = New NasaLista(MyBase.ZemiKontrolaFLXS.SorsID, NasaLista.DaliPrazenRed.DodadiPrazenRed, NasaLista.DaliPrikazVrednost.NEprikazuvajVrednost)
            Else
                'default
                testLista = New NasaLista(MyBase.ZemiKontrolaFLXS.SorsID, NasaLista.DaliPrazenRed.BezPrazenRed, NasaLista.DaliPrikazVrednost.NEprikazuvajVrednost)
            End If

            If GenerirajLista(testLista, poraka) Then

                MyBase.DodadiGlavnaKontrolaVoUI(testLista, MyBase.ZemiKontrolaFLXS.XML.PrikaziKontrola)

                Return True
            Else
                Return False
            End If

        ElseIf tipKontrola = NasEnum.TipKontrola.ZivoTekstPole Then

            Dim tekstBoxZiv As NasTextBox = New NasTextBox(MyBase.ZemiKontrolaFLXS.SorsID)

            tekstBoxZiv.dodadiNasId(MyBase.ZemiKontrolaFLXS.SorsID, "NasID")

            If tekstBoxZiv.TipNasTextBox = NasEnum.TipNasTexBox.Artikal Or tekstBoxZiv.TipNasTextBox = NasEnum.TipNasTexBox.Komintent Then

                tekstBoxZiv.dodadiAtribut("povikajAjax(this.id,this.value)", "onkeyup")

                'Ova treba da se smeni - ne treba ovde vaka racno da se dodava. Najdobro da se cita od baza!
                If tekstBoxZiv.TipNasTextBox = NasEnum.TipNasTexBox.Artikal Then
                    tekstBoxZiv.dodadiAtribut("ZemiArtikal", "povikAJAX")
                ElseIf tekstBoxZiv.TipNasTextBox = NasEnum.TipNasTexBox.Komintent Then
                    tekstBoxZiv.dodadiAtribut("ZemiKomintenti", "povikAJAX")
                End If
                'tekstBoxZiv.dodadiNasId("tboxTest", "NasID")

            End If

            Try

                MyBase.DodadiGlavnaKontrolaVoUI(tekstBoxZiv, MyBase.ZemiKontrolaFLXS.XML.PrikaziKontrola)
                MyBase.DodadiVoUI("<div id=""SearchGrid" + "_" + MyBase.ZemiKontrolaFLXS.SorsID.ToUpper() + """></div>")

                Return True
            Catch ex As Exception
                Return False
            End Try
        ElseIf tipKontrola = NasEnum.TipKontrola.TekstPole Then
            Dim tekstBox As NasTextBox = New NasTextBox(MyBase.ZemiKontrolaFLXS.SorsID)

            tekstBox.dodadiNasId(MyBase.ZemiKontrolaFLXS.SorsID, "NasID")

            Try
                MyBase.DodadiGlavnaKontrolaVoUI(tekstBox, MyBase.ZemiKontrolaFLXS.XML.PrikaziKontrola)
                Return True
            Catch ex As Exception
                Return False
            End Try


        ElseIf tipKontrola = NasEnum.TipKontrola.Datum Then

            Dim datum As DatumIzbor = CType(Me.TekovnaStranica.LoadControl("~/KorisnickKontroli/DatumIzbor.ascx"), DatumIzbor)
            datum.ID = MyBase.ZemiKontrolaFLXS.SorsID

            Try
                MyBase.DodadiGlavnaKontrolaDatumVoUI(datum)

                Return True
            Catch ex As Exception
                Return False
            End Try
        ElseIf tipKontrola = NasEnum.TipKontrola.RadioButton Then

            Dim rb As NasRadioButtonUC = CType(Me.TekovnaStranica.LoadControl("~/KorisnickKontroli/NasRadioButtonUC.ascx"), NasRadioButtonUC)
            rb.setirajKotnrola(MyBase.ZemiKontrolaFLXS.SorsID, MyBase.ZemiKontrolaFLXS.XML.Grupa, MyBase.ZemiKontrolaFLXS.XML.Ime)

            If redenBrVoGrupa = 1 Then
                rb.DaliDAGOStikliram = True
            End If
            Try
                MyBase.DodadiGlavnaKontrolaDatumVoUI(rb)

                Return True
            Catch ex As Exception
                Return False
            End Try
        ElseIf tipKontrola = NasEnum.TipKontrola.CheckBox Then
            Dim cb As NasCheckBoxUC = CType(Me.TekovnaStranica.LoadControl("~/KorisnickKontroli/NasCheckBoxUC.ascx"), NasCheckBoxUC)
            cb.setirajKotnrola(MyBase.ZemiKontrolaFLXS.SorsID, MyBase.ZemiKontrolaFLXS.XML.Grupa, MyBase.ZemiKontrolaFLXS.XML.Ime)
            Try
                MyBase.DodadiGlavnaKontrolaDatumVoUI(cb)

                Return True
            Catch ex As Exception
                Return False
            End Try
        ElseIf tipKontrola = NasEnum.TipKontrola.RadioButtonList Then
            'Probuvav site radiobuttons da gi stavam vo edna dropdownlista
            Dim testLista As NasaLista
            testLista = New NasaLista(MyBase.ZemiKontrolaFLXS.SorsID, NasaLista.DaliPrazenRed.BezPrazenRed, NasaLista.DaliPrikazVrednost.NEprikazuvajVrednost)

            If GenerirajLista(testLista, Me.ListaRadiButtons, poraka) Then

                MyBase.DodadiGlavnaKontrolaVoUI(testLista, MyBase.ZemiKontrolaFLXS.XML.PrikaziKontrola)

                Return True
            Else
                Return False
            End If
        ElseIf tipKontrola = NasEnum.TipKontrola.Nedefiniran Then
            MyBase.DodadiGlavnaKontrolaVoUI(New LiteralControl("<div>" + MyBase.ZemiKontrolaFLXS.SorsID + " - NEDEFINIRAN TIP NA KONTROLA!--->Proveri BAZA i XML</div>")) 'Prevod
            Return True
        End If

        Return False

    End Function
    Private Function GenerirajLista(ByRef lista As NasaLista, ByRef poraka As String) As Boolean

        If lista Is Nothing Then

            lista = New NasaLista(MyBase.ZemiKontrolaFLXS.SorsID, NasaLista.DaliPrazenRed.BezPrazenRed, NasaLista.DaliPrikazVrednost.NEprikazuvajVrednost)
        End If

        If Not String.IsNullOrEmpty(MyBase.ZemiKontrolaFLXS.Baza.SorsRun) Then
            Dim db As New BazaDB()
            Dim ds As DataSet = New DataSet()

            If db.generirajDataSet(ds, BazaDB.TipNaSqlKomanda.Procedura, MyBase.ZemiKontrolaFLXS.Baza.SorsRun, Nothing, poraka) Then
                lista.NapolniListaOdDataSet(ds)
                Return True
            Else
                poraka = "Problem ima pri incijalizacija na lista so BAZA! " + poraka 'Prevod
                Return False
            End If
        Else
            poraka = "Nema definirana procedura za zemanje na podatoci - SorsID=" & MyBase.ZemiKontrolaFLXS.SorsID 'Prevod
            Return False
        End If



    End Function
    Private Function GenerirajLista(ByRef lista As NasaLista, ByVal recnik As Dictionary(Of String, String), ByRef poraka As String) As Boolean

        If lista Is Nothing Then

            lista = New NasaLista(Me.sorsID, NasaLista.DaliPrazenRed.BezPrazenRed, NasaLista.DaliPrikazVrednost.NEprikazuvajVrednost)
        End If
        If Not recnik Is Nothing Then
            For Each kvp As KeyValuePair(Of String, String) In recnik
                lista.DodadiElement(kvp.Key, kvp.Value)
            Next
            Return True
        Else
            poraka = "Nema podatoci so koi bi se napolnila radioListata!" 'Prevod
            Return False
        End If
    End Function
#End Region
End Class
