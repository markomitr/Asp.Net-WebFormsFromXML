Imports System.Web.UI.WebControls

Public Class NasaLista
    Inherits DropDownList
    Implements NasiKlasi

    Dim defVrdn As String
    Enum DaliPrazenRed
        DodadiPrazenRed
        BezPrazenRed
    End Enum
    Enum DaliPrikazVrednost
        PrikaziVrednost
        NEprikazuvajVrednost
    End Enum

    Dim showValue As DaliPrikazVrednost

    Public ReadOnly Property IDKontrola As String Implements NasiKlasi.IDKontrola
        Get
            Return Me.ID
        End Get
    End Property

    Public ReadOnly Property Vrednost As String Implements NasiKlasi.Vrednost

        Get
            Return Me.SelectedValue
        End Get
    End Property

    Public Property DefVredonst As String Implements NasiKlasi.DefVrednost
        Set(value As String)
            defVrdn = value         
        End Set
        Get
            Return defVrdn
        End Get
    End Property
    Public Sub New(ByVal dataSetPodatoci As DataSet, ByVal id As String, ByVal prazen As DaliPrazenRed, ByVal prikazVrednost As DaliPrikazVrednost)
        Me.New(id, prazen, prikazVrednost)
        Me.NapolniListaOdDataSet(dataSetPodatoci)
        Try
            Me.SelectedValue = DefVredonst
        Catch ex As Exception
        End Try
    End Sub

    Public Sub New(ByVal id As String, ByVal prazen As DaliPrazenRed, ByVal prikazVrednost As DaliPrikazVrednost)
        MyBase.New()

        Me.ID = id

        If prazen = DaliPrazenRed.DodadiPrazenRed Then
            Me.DodadiElement("", "")
        End If

        showValue = prikazVrednost

    End Sub

    ''' <summary>
    ''' Vo ovaa funkcija se kreira atribut ID  so cija pomos vo JScripta bi go raspoznavale elementot/kontrola
    ''' <param name="id">Ova e identifikatorot za kontrolata</param>
    ''' <remarks>Imalo potreba za funkcii na klientska strana</remarks>
    ''' 
    Public Sub DodadiElement(ByVal vredonst As String, ByVal tekst As String)

        Dim eleObj As New ListItem()

        Dim tekstElement As String
        If showValue = DaliPrikazVrednost.PrikaziVrednost And Not String.IsNullOrEmpty(tekst) Then
            tekstElement = "[" & vredonst & "] " & tekst
        Else
            tekstElement = tekst
        End If
        eleObj.Text = tekstElement
        eleObj.Value = vredonst

        Me.Items.Add(eleObj)

    End Sub

    Public Sub NapolniListaOdDataSet(ByVal ds As DataSet)


        If Not ds.Tables(0) Is Nothing Then

            If ds.Tables(0).Rows.Count > 0 Then


                For index = 0 To ds.Tables(0).Rows.Count - 1

                    Dim vrednost As String = ds.Tables(0).Rows(index)(ds.Tables(0).Columns(0).ToString()) 'Prvo pole - index 0
                    Dim tekst As String = ds.Tables(0).Rows(index)(ds.Tables(0).Columns(1).ToString()) ' Vtoro pole - index 1

                    tekst = UnvMakKodnaStr.CistoKonv(tekst)

                    Me.DodadiElement(vrednost, tekst)

                Next
            Else
                Me.DodadiElement("", "Нема податоци") ' Prevod
            End If

        End If
    End Sub
    Public Sub NapolniListaOdDataSet(ByVal ds As DataSet, ByVal imeKolonaVREDNOST As String, ByVal imeKolonaTEKST As String)

        If Not ds.Tables(0) Is Nothing Then


            For index = 0 To ds.Tables(0).Rows.Count - 1

                Dim vrednost As String = ds.Tables(0).Rows(index)(imeKolonaVREDNOST)
                Dim tekst As String = ds.Tables(0).Rows(index)(imeKolonaTEKST)

                Me.DodadiElement(vrednost, tekst)

            Next


        End If
    End Sub

    Public Sub dodadiNasId(ByVal id As String, ByVal attrName As String) Implements NasiKlasi.dodadiNasId

        Me.Attributes.Add(attrName, id)

    End Sub
End Class
