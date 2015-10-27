Public Class KontrolaOpis

    Dim stranaTekovnaPAGE As NasaStranaBase

    Dim redBazaObj As RedBaza
    Dim redXMLObj As RedXML

    Dim tipKontrolaNas As NasEnum.TipKontrola

    Private sId As String
    Public Sub New(ByVal sorsId As String)
        sId = sorsId
    End Sub
    Public Sub New(ByVal sorsId As String, ByVal tekovnaStrana As NasaStranaBase)
        Me.New(sorsId)
        redBazaObj = New RedBaza(sId)
        redXMLObj = New RedXML(sId)
        stranaTekovnaPAGE = tekovnaStrana
    End Sub

    Public ReadOnly Property TekovnaStranica As NasaStranaBase
        Get
            Return stranaTekovnaPAGE
        End Get
    End Property
    ReadOnly Property Baza As RedBaza
        Get
            Return redBazaObj
        End Get
    End Property

    ReadOnly Property XML As RedXML
        Get
            Return redXMLObj
        End Get
    End Property

    ReadOnly Property SorsID As String
        Get
            Return sId
        End Get
    End Property

    Public ReadOnly Property KontrolaNasTIP As NasEnum.TipKontrola
        Get
            Return kakvaKontrola()
        End Get
    End Property


    Public Function splotiRedXMLredBaza(ByVal ObjRedXML As RedXML, ByVal ObjRedBaza As RedBaza, ByRef poraka As String) As Boolean

        If ObjRedXML.SorsId.ToUpper().Equals(ObjRedBaza.SorsId.ToUpper) And ObjRedBaza.SorsId.ToUpper().Equals(Me.SorsID.ToUpper()) Then
            redBazaObj = ObjRedBaza
            redXMLObj = ObjRedXML
            Return True
        Else
            poraka = "Redovite od Baza i XML imaat razlicno SorsID - " + ObjRedBaza.SorsId + " <>" + ObjRedXML.SorsId 'Prevod
            Return False
        End If

    End Function
    Private Function kakvaKontrola() As NasEnum.TipKontrola
        If Not Me.XML Is Nothing And Not Me.Baza Is Nothing Then
            If Me.XML.TipKontrola.ToUpper() = "LISTA" And Me.Baza.TipKontrola.ToUpper() = "LISTA" Then
                Return NasEnum.TipKontrola.Lista
            ElseIf Me.Baza.TipKontrola.ToUpper() = "ZIVOTEKSTPOLE" Then  'And Me.XML.TipKontrola.ToUpper() = "ZIVOTEKSTPOLE" Then
                Return NasEnum.TipKontrola.ZivoTekstPole
            ElseIf Me.Baza.TipKontrola.ToUpper() = "TEKSTPOLE" Then 'And Me.XML.TipKontrola.ToUpper() = "TEKSTPOLE" Then
                Return NasEnum.TipKontrola.TekstPole
            ElseIf Me.Baza.TipKontrola.ToUpper() = "DATUM" Then 'And Me.XML.TipKontrola.ToUpper() = "DATUM"  Then
                Return NasEnum.TipKontrola.Datum
            ElseIf Me.Baza.TipKontrola.ToUpper() = "RADIOBUTTON" Then 'And Me.XML.TipKontrola.ToUpper() = "RADIOBUTTON"Then
                Return NasEnum.TipKontrola.RadioButton
            ElseIf Me.Baza.TipKontrola.ToUpper() = "CHECKBOX" Then 'And  Me.XML.TipKontrola.ToUpper() = "CHECKBOX"  Then
                Return NasEnum.TipKontrola.CheckBox
            ElseIf Me.Baza.TipKontrola.ToUpper() = "RADIOBUTTONLIST" Then 'And Me.XML.TipKontrola.ToUpper() = "RADIOBUTTONLIST"  Then
                Return NasEnum.TipKontrola.RadioButtonList
            Else
                Return NasEnum.TipKontrola.Nedefiniran
            End If
        End If
        Return Nothing
    End Function
End Class
