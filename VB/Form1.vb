Imports Microsoft.VisualBasic
#Region "#Reference"
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Windows.Forms
Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.UI.PivotGrid
' ...
#End Region ' #Reference

Namespace docXRPivotGrid
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

#Region "#Code"
Private Sub button1_Click(ByVal sender As Object, _
ByVal e As EventArgs) Handles button1.Click
    ' Create a cross-tab report.
    Dim report As XtraReport = CreateReport()

    ' Show its Print Preview.
    report.ShowPreview()
End Sub

Private Function CreateReport() As XtraReport
    ' Create a blank report.
    Dim CrossTabReport As New XtraReport()

    ' Create a detail band and add it to the report.
    Dim detail As New DetailBand()
    CrossTabReport.Bands.Add(detail)

    ' Create a pivot grid and add it to the Detail band.
    Dim pivotGrid As New XRPivotGrid()
    detail.Controls.Add(pivotGrid)

    ' Create a data connection.
    Dim connection As New OleDbConnection( _
	    "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\nwind.mdb")

    ' Create a data adapter.
    Dim adapter As New OleDbDataAdapter( _
    "SELECT CategoryName, ProductName, Country, [Sales Person], Quantity, [Extended Price] FROM SalesPerson", _
    connection)

    ' Creata and populate a dataset.
    Dim dataSet1 As New DataSet()
    adapter.Fill(dataSet1, "SalesPerson")

    ' Bind the pivot grid to data.
    pivotGrid.DataSource = dataSet1
    pivotGrid.DataMember = "SalesPerson"

    ' Generate pivot grid's fields.
    Dim fieldCategoryName As New XRPivotGridField("CategoryName", PivotArea.RowArea)
    Dim fieldProductName As New XRPivotGridField("ProductName", PivotArea.RowArea)
    Dim fieldCountry As New XRPivotGridField("Country", PivotArea.ColumnArea)
    Dim fieldSalesPerson As New XRPivotGridField("Sales Person", PivotArea.ColumnArea)
    Dim fieldQuantity As New XRPivotGridField("Quantity", PivotArea.DataArea)
    Dim fieldExtendedPrice As New XRPivotGridField("Extended Price", PivotArea.DataArea)

    ' Add these fields to the pivot grid.
    pivotGrid.Fields.AddRange(New XRPivotGridField() {fieldCategoryName, fieldProductName, fieldCountry, _
	    fieldSalesPerson, fieldQuantity, fieldExtendedPrice})

    Return CrossTabReport
End Function
#End Region ' #Code
	End Class
End Namespace



