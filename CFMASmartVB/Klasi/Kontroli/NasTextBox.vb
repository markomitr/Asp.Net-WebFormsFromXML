Imports System.Web.UI.WebControls

Public Class NasTextBox
    Inherits TextBox
    Implements NasiKlasi
    Dim defVrdn As String

    Dim tipTexBox As NasEnum.TipNasTexBox = NasEnum.TipNasTexBox.Obicen

    Public ReadOnly Property IDKontrola As String Implements NasiKlasi.IDKontrola

        Get
            Return Me.ID
        End Get
    End Property

    Public ReadOnly Property Vrednost As String Implements NasiKlasi.Vrednost

        Get
            Return Me.Text
        End Get
    End Property
    Public ReadOnly Property TipNasTextBox
        Get
            Return tipTexBox
        End Get
    End Property
    Public Property DefVredonst As String Implements NasiKlasi.DefVrednost
        Set(value As String)
            defVrdn = value
            Me.Text = value
        End Set
        Get
            Return defVrdn
        End Get
    End Property
    Public Sub dodadiNasId(ByVal id As String, ByVal attrName As String) Implements NasiKlasi.dodadiNasId
        Me.dodadiAtribut(id, attrName)
    End Sub

    Public Sub dodadiAtribut(ByVal vredonst As String, ByVal attrName As String)
        Me.Attributes.Add(attrName, vredonst)
    End Sub
    Public Sub New(ByVal id As String)
        MyBase.ID = id
        MyBase.AutoCompleteType = AutoCompleteType.None

        If id.ToUpper().Contains("SIFRA_ART") Then
            tipTexBox = NasEnum.TipNasTexBox.Artikal
        ElseIf id.ToUpper().Contains("SIFRA_KUP") Then
            tipTexBox = NasEnum.TipNasTexBox.Komintent
        Else
            tipTexBox = NasEnum.TipNasTexBox.Obicen
        End If
        Me.dodadiAtribut("off", "AutoComplete")
    End Sub
End Class