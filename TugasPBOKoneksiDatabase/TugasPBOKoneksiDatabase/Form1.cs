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

            // Membuat panel untuk dataGridView agar lebih rapi
            Panel dataGridViewPanel = new Panel();
            dataGridViewPanel.Name = "dataGridViewPanel";
            dataGridViewPanel.Dock = DockStyle.Fill;
            Controls.Add(dataGridViewPanel);

            // Membuat dan Mempersiapkan DataGridView
            DataGridView dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;

            // Melakukan pengaturan properti parent ke Panel
            dataGridView.Parent = dataGridViewPanel;

            // Melakukan Load data untuk dimasukkan ke DataGridView
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

                // Menampilkan DataTable sebagai Data Source untuk DataGridView
                dataGridView1.DataSource = dataTable;

                // Menambahkan kolom tombol Edit jika belum ada
                if (dataGridView1.Columns["EditButtonColumn"] == null)
                {
                    AddButtonColumn("EditButtonColumn", "Ubah");
                }

                // Menambahkan kolom tombol Hapus jika belum ada
                if (dataGridView1.Columns["DeleteButtonColumn"] == null)
                {
                    AddButtonColumn("DeleteButtonColumn", "Hapus");
                }
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


        // Menambahkan metode untuk menambahkan kolom tombol ke DataGridView
        private void AddButtonColumn(string columnName, string buttonText)
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "";
            buttonColumn.Name = columnName;
            buttonColumn.Text = buttonText;
            buttonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(buttonColumn);
        }

        private void tambahbutton_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(ConnectionString);
            form2.ShowDialog();

            if (form2.DialogResult == DialogResult.OK)
            {
                LoadDataIntoDataGridView();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Logika inisialisasi tambahan bisa diletakkan di sini
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Periksa apakah tombol "Ubah" atau "Hapus" diklik
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridView1.Columns["EditButtonColumn"].Index)
                {
                    // Tombol "Ubah" diklik
                    int prestasiId = (int)dataGridView1.Rows[e.RowIndex].Cells["id_prestasi"].Value;

                    // Buka Form3 dengan prestasiId yang dipilih untuk diedit
                    Form3 form3 = new Form3(ConnectionString, prestasiId);
                    form3.ShowDialog();

                    // Muat ulang DataGridView setelah diedit
                    LoadDataIntoDataGridView();
                }
                else if (e.ColumnIndex == dataGridView1.Columns["DeleteButtonColumn"].Index)
                {
                    // Tombol "Hapus" diklik
                    int prestasiId = (int)dataGridView1.Rows[e.RowIndex].Cells["id_prestasi"].Value;

                    // Minta konfirmasi sebelum menghapus
                    DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Lakukan operasi hapus
                        DeletePrestasi(prestasiId);

                        // Muat ulang DataGridView setelah menghapus
                        LoadDataIntoDataGridView();
                    }
                }
            }
        }

        // Menambahkan metode untuk menghapus Prestasi berdasarkan prestasiId
        private void DeletePrestasi(int prestasiId)
        {
            try
            {
                databaseConnection.OpenConnection();

                string query = "DELETE FROM Prestasi WHERE id_prestasi = @PrestasiId";

                using (SqlCommand command = new SqlCommand(query, databaseConnection.GetSqlConnection()))
                {
                    command.Parameters.AddWithValue("@PrestasiId", prestasiId);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}