using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TugasPBOKoneksiDatabase
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\USERS\ASUS\SOURCE\REPOS\TUGASPBOKONEKSIDATABASE\TUGASPBOKONEKSIDATABASE\Prestasi_db.mdf;Integrated Security=True";
        private readonly DatabaseConnection databaseConnection;
        private readonly SqlDataAdapter sqlDataAdapter;
        private readonly DataTable dataTable;

        public Form1()
        {
            InitializeComponent();
            databaseConnection = new DatabaseConnection(ConnectionString);
            sqlDataAdapter = new SqlDataAdapter();
            dataTable = new DataTable();
            LoadDataIntoDataGridView();
        }

        private void LoadDataIntoDataGridView()
        {
            try
            {
                databaseConnection.OpenConnection();

                string query = "SELECT P.id_prestasi, P.nama_prestasi, TP.nama_tingkat, PP.nama_posisi, AP.poin, P.bukti " +
                               "FROM Prestasi P " +
                               "JOIN TingkatPrestasi TP ON P.id_tingkat = TP.id_tingkat " +
                               "JOIN PosisiPrestasi PP ON P.id_posisi = PP.id_posisi " +
                               "JOIN AcuanPoin AP ON P.id_poin = AP.id_poin";

                sqlDataAdapter.SelectCommand = new SqlCommand(query, databaseConnection.GetSqlConnection());
                dataTable.Clear();
                sqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                databaseConnection.CloseConnection();
            }
        }

        private void tambahbutton_Click(object sender, EventArgs e)
        {
            {
                Form2 form2 = new Form2(ConnectionString);
                form2.ShowDialog();

                if (form2.DialogResult == DialogResult.OK)
                {
                    LoadDataIntoDataGridView();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, EventArgs e)
        {
            // Any additional logic for cell content click
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Any additional initialization logic can go here
        }
    }
}