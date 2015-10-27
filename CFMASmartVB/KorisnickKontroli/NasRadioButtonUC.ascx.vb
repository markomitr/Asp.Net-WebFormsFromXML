Imports System.IO
Public Class NasRadioButtonUC
    Inherits System.Web.UI.UserControl
    Implements NasiKlasi
    Dim daliDaStikliram As Boolean = False
    Dim kojaGrupa As Integer = 0
    Public Property DaliDAGOStikliram As Boolean
        Get
            Return Me.daliDaStikliram
        End Get
        Set(ByVal value As Boolean)
            daliDaStikliram = value
            If Me.DaliDAGOStikliram Then
                Me.RadioKopce.Checked = True
            End If
        End Set
    End Property
    Public Sub New()
    End Sub

    Public Property DefVredonst As String Implements NasiKlasi.DefVrednost

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    Public ReadOnly Property DaliStiklirano As Boolean
        Get
            If Me.HiddenField1.Value = "D" Then
                Me.RadioKopce.Checked = True
            End If
            Return Me.RadioKopce.Checked
        End Get
    End Property
    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        MyBase.OnPreRender(e)
        If Me.HiddenField1.Value = "D" Then
            Me.RadioKopce.Checked = True
        End If
    End Sub
    Public Sub setirajKotnrola(ByVal id As String, ByVal grupa As Integer, ByVal tekst As String)
        'Me.RadioKopce.Attributes.Add("Name", grupa)
        Me.RadioKopceDiv.Attributes.Add("GrupaRadio", grupa)
        Me.RadioKopceDiv.Attributes.Add("onclick", "setirajRB(this)")
        Me.RadioKopceDiv.Attributes.Add("style", "cursor:pointer;")
        Me.RadioKopce.GroupName = grupa
        Me.lblRadioKopce.Text = UnvMakKodnaStr.CistoKonv(tekst)
        Me.ID = id
        Me.HiddenField1.ID = id + "-hidden"
    End Sub

    Public Sub dodadiNasId(ByVal id As String, ByVal attrName As String) Implements NasiKlasi.dodadiNasId

    End Sub

    Public ReadOnly Property IDKontrola As String Implements NasiKlasi.IDKontrola
        Get
            Return Me.ID
        End Get
    End Property

    Public ReadOnly Property Vrednost As String Implements NasiKlasi.Vrednost
        Get
            Return Me.DaliStiklirano
        End Get
    End Property


End Class