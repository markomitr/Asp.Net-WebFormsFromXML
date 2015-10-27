Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Serialization

Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<System.Web.Script.Services.ScriptService()> _
<ToolboxItem(False)> _
Public Class Funkcii
    Inherits System.Web.Services.WebService

    Dim cn As SqlConnection
    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function
#Region "Funkcii za APP"
    <WebMethod()> _
    Public Function ZemiArtikal(ByVal podatok As String) As String
        Dim poraka As String = ""
        Dim db As New BazaDB()
        Try
            cn = db.VratiKonekcija

            Dim js As JavaScriptSerializer = New JavaScriptSerializer()
            Dim imeA As String = js.Deserialize(Of String)(podatok)
            Dim command As String = "Select TOP 10 * from KatArt where ImeArt like '" + imeA + "%'"

            Dim sqlCmd As SqlCommand = New SqlCommand()
            sqlCmd.Connection = cn
            sqlCmd.CommandText = command
            sqlCmd.CommandType = CommandType.Text

            cn.Open()
            Dim sqlDr As SqlDataReader = sqlCmd.ExecuteReader()
            Dim listaAjaxObjekti As List(Of AjaxObj)
            listaAjaxObjekti = New List(Of AjaxObj)

            While sqlDr.Read()

                Dim sifraArt As String = sqlDr("Sifra_Art").ToString()
                Dim imeArt As String = sqlDr("ImeArt").ToString()

                Dim nov As Artikal = New Artikal()
                nov.ImeArt = imeArt.Trim()
                nov.Sifra_Art = sifraArt.Trim()

                Dim ajxObj As New AjaxObj
                ajxObj.Objekt = nov

                listaAjaxObjekti.Add(ajxObj)
            End While

            cn.Close()

            Return js.Serialize(listaAjaxObjekti)
        Catch ex As Exception
            cn.Close()
            Return "Server!Greska!"
        End Try
    End Function

    <WebMethod()> _
    Public Function ZemiKomintenti(ByVal podatok As String) As String
        Dim db As New BazaDB()
        Dim sqlCn As SqlConnection

        Try
            sqlCn = db.VratiKonekcija
            Dim js As JavaScriptSerializer = New JavaScriptSerializer()
            Dim imeK As String = js.Deserialize(Of String)(podatok)
            Dim command As String = "Select TOP 10 * from Komint where ImeKup like '" + imeK + "%'"

            Dim sqlCmd As SqlCommand = New SqlCommand()
            sqlCmd.Connection = sqlCn
            sqlCmd.CommandText = command
            sqlCmd.CommandType = CommandType.Text

            sqlCn.Open()
            Dim sqlDr As SqlDataReader = sqlCmd.ExecuteReader()
            Dim listaAjaxObjekti As List(Of AjaxObj)
            listaAjaxObjekti = New List(Of AjaxObj)

            While sqlDr.Read()

                Dim sifraKup As String = sqlDr("Sifra_Kup").ToString()
                Dim imeKup As String = sqlDr("ImeKup").ToString()

                Dim nov As Komintent = New Komintent()
                nov.ImeKup = imeKup.Trim()
                nov.Sifra_Kup = sifraKup.Trim()

                Dim ajxObj As New AjaxObj
                ajxObj.Objekt = nov

                listaAjaxObjekti.Add(ajxObj)

            End While

            sqlCn.Close()

            Return js.Serialize(listaAjaxObjekti)
        Catch ex As Exception
            sqlCn.Close()
            Return "Server: Greska!"
        End Try
    End Function
#End Region
End Class