using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UI.PivotGrid;
// ...


namespace docXRPivotGrid {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            // Create a cross-tab report.
            XtraReport report = CreateReport();

            // Show its Print Preview.
            report.ShowPreview();
        }

        private XtraReport CreateReport() {
            // Create a blank report.
            XtraReport rep = new XtraReport();

            // Create a detail band and add it to the report.
            DetailBand detail = new DetailBand();
            rep.Bands.Add(detail);

            // Create a pivot grid and add it to the Detail band.
            XRPivotGrid pivotGrid = new XRPivotGrid();
            detail.Controls.Add(pivotGrid);

            // Create a data connection.
            OleDbConnection connection = new
                OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\\..\\nwind.mdb");

            // Create a data adapter.
            OleDbDataAdapter adapter = new
                OleDbDataAdapter("SELECT CategoryName, ProductName, Country, [Sales Person], Quantity, [Extended Price] FROM SalesPerson", connection);

            // Creata a dataset and fill it.
            DataSet dataSet1 = new DataSet();
            adapter.Fill(dataSet1, "SalesPerson");


            // Bind the pivot grid to data.
            pivotGrid.DataSource = dataSet1;
            pivotGrid.DataMember = "SalesPerson";

            // Generate pivot grid's fields.
            XRPivotGridField fieldCategoryName = new XRPivotGridField("CategoryName", PivotArea.RowArea);
            XRPivotGridField fieldProductName = new XRPivotGridField("ProductName", PivotArea.RowArea);
            XRPivotGridField fieldCountry = new XRPivotGridField("Country", PivotArea.ColumnArea);
            XRPivotGridField fieldSalesPerson = new XRPivotGridField("Sales Person", PivotArea.ColumnArea);
            XRPivotGridField fieldQuantity = new XRPivotGridField("Quantity", PivotArea.DataArea);
            XRPivotGridField fieldExtendedPrice = new XRPivotGridField("Extended Price", PivotArea.DataArea);

            // Add these fields to the pivot grid.
            pivotGrid.Fields.AddRange(new PivotGridField[] {fieldCategoryName, fieldProductName, fieldCountry,
                fieldSalesPerson, fieldQuantity, fieldExtendedPrice});

            return rep;
        }
    }
}



