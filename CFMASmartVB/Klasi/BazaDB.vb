Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.Configuration

Public Class BazaDB

    Shared cnString As String = Convert.ToString(WebConfigurationManager.ConnectionStrings("Sql")) 
    Dim sqlCn As SqlConnection
    Dim command As String
    Dim sqlCmd As SqlCommand
    Dim sqldataA As SqlDataAdapter
    Dim ds As DataSet


    Public Enum TipNaSqlKomanda
        Procedura
        Tekst
    End Enum


    Public Function generirajDataSet(ByRef dSet As DataSet, ByVal tipKomanda As TipNaSqlKomanda, ByVal komanda As String, ByVal parametri As List(Of SqlParameter), ByRef poraka As String) As Boolean

        If InicijalizirajKonekci(poraka) Then

            command = komanda
            sqlCmd = New SqlCommand()

            sqlCmd.Connection = sqlCn
            sqlCmd.CommandText = command

            If tipKomanda = BazaDB.TipNaSqlKomanda.Procedura Then
                sqlCmd.CommandType = CommandType.StoredProcedure

                If Not parametri Is Nothing Then
                    sqlCmd.Parameters.AddRange(parametri.ToArray())
                End If



            Else
                sqlCmd.CommandType = CommandType.Text
            End If

            sqldataA = New SqlDataAdapter(sqlCmd)

            If dSet Is Nothing Then
                dSet = New DataSet()
            End If

            Try

                sqlCn.Open()

                sqldataA.Fill(dSet)

                sqlCn.Close()

                Return True

            Catch ex As Exception

                sqlCn.Close()

                poraka = "Problem pri generiranje na DATASET - " & ex.Message 'Prevod

                Return False
            End Try


        Else

            poraka = "Problem so konekcija! - " & poraka ' Prevod

            Return False
        End If

    End Function

    Private Function InicijalizirajKonekci(ByRef poraka As String) As Boolean
        sqlCn = New SqlConnection(cnString)

        Try
            sqlCn.Open()

            sqlCn.Close()

            Return True
        Catch ex As Exception
            sqlCn.Close()
            poraka = "Ne moze da se otvori konekcija! - " & ex.Message ' Prevod
            Return False
        End Try

    End Function

    Public ReadOnly Property VratiKonekcija As SqlConnection
        Get
            sqlCn = New SqlConnection(cnString)
            Return sqlCn
        End Get
    End Property

End Class
