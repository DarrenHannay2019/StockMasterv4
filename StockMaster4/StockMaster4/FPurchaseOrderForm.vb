Imports System.Data.SqlClient

Public Class FPurchaseOrderForm
    Dim Save As New CSave
    Dim MyUpdate As New CUpdate
    Dim Check As New CCheck
    Dim Syslog As New CSystemLog
    Dim StockMovements As New CStockMovements
    Dim Ut As New CUtilities
    Dim stockCodeR As String
    Dim oldstockcode As String
    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click

    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you wish to cancel this input", "Cancel Input", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.No Then Exit Sub
        '  If result = DialogResult.Yes Then FGridPOrders.TsbRefresh.PerformClick() : Me.Close()
    End Sub

    Private Sub txtSupplierRef_Leave(sender As Object, e As EventArgs) Handles txtSupplierRef.Leave
        Using conn As New SqlConnection(Ut.GetConnString(1))
            Using SelectCmd As New SqlCommand
                With SelectCmd
                    .Connection = conn
                    .CommandText = "SELECT SupplierName from tblSuppliers WHERE SupplierRef = @SupplierRef"
                    .CommandType = CommandType.Text
                    .Parameters.AddWithValue("@SupplierRef", txtSupplierRef.Text.TrimEnd())
                    .Connection.Open()
                    txtSupplierName.Text = .ExecuteScalar
                End With
            End Using
        End Using
        txtSupplierRef.Text = UCase(txtSupplierRef.Text)
        If txtSupplierName.Text = "" Then MsgBox("Please Check your Supplier Reference", MsgBoxStyle.Critical, Application.ProductName) : txtSupplierRef.Clear() : txtSupplierRef.Select() : Exit Sub
        If txtSupplierName.Text <> "" And CheckBox1.Checked = False Then
            txtStockCode.Select()
            Exit Sub
        Else
            txtWarehouseRef.Select()
            Exit Sub
        End If
    End Sub

    Private Sub txtWarehouseRef_Leave(sender As Object, e As EventArgs) Handles txtWarehouseRef.Leave
        Using conn As New SqlConnection(Ut.GetConnString(1))
            Using selectcmd As New SqlCommand
                With selectcmd
                    .Connection = conn
                    .CommandText = "SELECT WarehouseName from qryAllLocations WHERE WarehouseRef = @WarehouseRef"
                    .CommandType = CommandType.Text
                    .Parameters.AddWithValue("@WarehouseRef", txtWarehouseRef.Text.TrimEnd())
                    .Connection.Open()
                    txtWarehouseName.Text = .ExecuteScalar
                End With
            End Using
        End Using
        txtWarehouseRef.Text = UCase(txtWarehouseRef.Text)
        If txtWarehouseName.Text = "" Then MsgBox("Please Check your Location Reference", MsgBoxStyle.Critical, Application.ProductName) : txtWarehouseRef.Select() : Exit Sub
        If txtWarehouseName.Text <> "" And CheckBox1.Checked = True Then
            txtStockCode.Select()
            Exit Sub
        Else
            txtWarehouseRef.Select()
            Exit Sub
        End If
    End Sub

    Private Sub txtStockCode_Leave(sender As Object, e As EventArgs) Handles txtStockCode.Leave

    End Sub

    Private Sub BtnClearGrid_Click(sender As Object, e As EventArgs) Handles BtnClearGrid.Click
        DgvItems.Rows.Clear()
    End Sub

    Private Sub BtnAddToGrid_Click(sender As Object, e As EventArgs) Handles BtnAddToGrid.Click
        Dim Garments, Boxes, Units As Integer
        Dim value As Decimal
        Dim rowNum As Integer = DgvItems.Rows.Add()
        DgvItems.Rows.Item(rowNum).Cells(0).Value = txtStockCode.Text.ToString
        DgvItems.Rows.Item(rowNum).Cells(1).Value = txtQtyGarments.Text
        DgvItems.Rows.Item(rowNum).Cells(2).Value = TxtBoxes.Text
        DgvItems.Rows.Item(rowNum).Cells(3).Value = TxtUnits.Text
        DgvItems.Rows.Item(rowNum).Cells(4).Value = txtNetCost.Text
        For i As Integer = 0 To DgvItems.Rows.Count - 1
            Garments += CInt(DgvItems.Rows.Item(rowNum).Cells(1).Value)
            Boxes += CInt(DgvItems.Rows.Item(rowNum).Cells(2).Value)
            Units += CInt(DgvItems.Rows.Item(rowNum).Cells(3).Value)
            value += CDec(DgvItems.Rows.Item(rowNum).Cells(4).Value)
        Next
        txtTotalNet.Text = value
        txtTotalGarments.Text = Garments.ToString()
        TxtTotalBoxes.Text = Boxes.ToString()
        TxtTotalUnits.Text = Units.ToString()
        txtTotalNet.Text = FormatCurrency(txtTotalNet.Text)
        VatCheck()
        txtStockCode.Clear()
        txtQtyGarments.Clear()
        TxtBoxes.Clear()
        TxtUnits.Clear()
        txtNetCost.Clear()
        txtStockCode.Select()
    End Sub
    Private Sub VatCheck()
        Dim VAT, total As Decimal
        Dim TotalNet, Comm, DelCharge As Decimal
        TotalNet = CDec(txtTotalNet.Text)
        Comm = CDec(txtCommission.Text)
        DelCharge = CDec(txtDelCharges.Text)

        VAT = (TotalNet + Comm + DelCharge) / 5
        total = VAT + TotalNet + Comm + DelCharge
        TxtVatAmount.Text = VAT.ToString()
        txtTotal.Text = total.ToString()
    End Sub
End Class

'  Save.SaveShopSalesLines(0, txtStockCode.Text.TrimEnd, 0, 0, 0, 0)
' StockMovements.SaveStockMovements(txtStockCode.Text.TrimEnd, txtSupplierRef.Text.TrimEnd, "DU", "Shop", 0, 0, "Delivery (S)", CDate(DateTimePicker1.Value), 0, txtStockCode.Text, CInt(txtOrderID.Text))
' txtstockcode_leave
'txtStockCode.Text = UCase(txtStockCode.Text)
'Dim CheckResult As Boolean
'If cmdOK.Text = "OK" Then
'If lblOldStockCode.Text.TrimEnd() <> txtStockCode.Text.TrimEnd() Then
'CheckResult = Check.CheckStockCode(txtStockCode.Text)
'Exit Sub
'ElseIf lblOldStockCode.Text = txtStockCode.Text.TrimEnd Then
'CheckResult = Check.CheckStockCode(txtStockCode.Text.TrimEnd())
'If CheckResult = True Then
'stockCodeR = "New"
'txtQtyGarments.Select()
'Exit Sub
'Else
'stockCodeR = "Old"
'Dim results As DialogResult = MessageBox.Show("Are you sure you wish to use an existing stock code", "Exisiting' StockCode", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
'If results = DialogResult.No Then txtStockCode.Select() : Exit Sub
'If results = DialogResult.Yes Then txtQtyGarments.Select() : Exit Sub
'End If
'End If
'ElseIf cmdOK.Text = "ADD" Then
'CheckResult = Check.CheckStockCode(txtStockCode.Text.TrimEnd())
'If CheckResult = True Then
'stockCodeR = "New"
'txtQtyGarments.Select()
'Exit Sub
'Else
'stockCodeR = "Old"
'Dim results As DialogResult = MessageBox.Show("Are you sure you wish to use an existing stock code", "Exisiting StockCode", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
'If results = DialogResult.No Then txtStockCode.Select() : Exit Sub
'If results = DialogResult.Yes Then txtQtyGarments.Select() : Exit Sub
'End If
'End If