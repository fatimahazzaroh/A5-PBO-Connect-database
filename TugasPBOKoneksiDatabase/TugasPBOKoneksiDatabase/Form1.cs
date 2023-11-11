using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TugasPBOKoneksiDatabase
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\USERS\ASUS\SOURCE\REPOS\TUGASPBOKONEKSIDATABASE\TUGASPBOKONEKSIDATABASE\Prestasi_db.mdf;Integrated Security=True";
        private readonly SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        private readonly SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        private readonly DataTable dataTable = new DataTable();

        public Form1()
        {
            InitializeComponent();
            LoadDataIntoDataGridView();
        }

        private void RefreshDataGridView()
        {
            string query = "SELECT P.id_prestasi, P.nama_prestasi, TP.nama_tingkat, PP.nama_posisi, P.bukti, AP.poin " +
                           "FROM Prestasi P " +
                           "JOIN TingkatPrestasi TP ON P.id_tingkat = TP.id_tingkat " +
                           "JOIN PosisiPrestasi PP ON P.id_posisi = PP.id_posisi " +
                           "JOIN AcuanPoin AP ON P.id_poin = AP.id_poin";

            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand(query, sqlConnection);
                dataTable.Clear();
                sqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataIntoDataGridView()
        {
            try
            {
                sqlConnection.Open();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        private void tambahbutton_Click(object sender, EventArgs e)
        {
            // Create an instance of Form2
            Form2 form2 = new Form2();

            // Show Form2 as a dialog and handle the result
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // If OK is returned, refresh the data grid view
                LoadDataIntoDataGridView();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}