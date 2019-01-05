Imports System.Data.SqlClient
Public Class CCheck
    Inherits CUtilities
    Public Function GetID(ByVal CommandText As String) As Integer
        Dim result As Integer
        Using SqlConn As New SqlConnection(GetConnString(1))
            Using SelectCmd As New SqlCommand
                With SelectCmd
                    .Connection = SqlConn
                    .Connection.Open()
                    .CommandText = CommandText
                    .CommandType = CommandType.Text
                    result = .ExecuteScalar
                End With
            End Using
        End Using
        Return result
    End Function
    Public Function CheckStockCode(StockCode As String) As Boolean
        Dim result As Integer
        Dim returnresult As Boolean
        Using conn As New SqlConnection(GetConnString(1))
            Using selectcmd As New SqlCommand
                With selectcmd
                    .Connection = conn
                    .Connection.Open()
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT COUNT(*) AS numRows From tblStock WHERE StockCode =@StockCode"
                    .Parameters.AddWithValue("@StockCode", StockCode)
                    result = .ExecuteScalar
                End With
            End Using
        End Using
        If result = 0 Then
            returnresult = True
        Else
            returnresult = False
        End If
        Return returnresult
    End Function
End Class
' Original File 145 Lines
' This File 41 Lines