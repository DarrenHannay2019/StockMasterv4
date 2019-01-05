Imports System.Data.SqlClient
Public Class CReports
    Inherits CUtilities
    Dim syslog As New CSystemLog
    Private Sub AddToLog()
        syslog.SaveSystemLog("All", "All", "All", 0, "Reports Gen", "Reports Gen", Date.Now, "Full Reports Generated")
    End Sub

End Class
