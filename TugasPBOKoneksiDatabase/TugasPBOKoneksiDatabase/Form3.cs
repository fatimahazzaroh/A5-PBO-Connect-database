using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TugasPBOKoneksiDatabase
{
    public partial class Form3 : Form
    {
        private readonly string ConnectionString;
        private readonly int PrestasiId;

        public Form3(string ConnectionString, int prestasiId)
        {
            InitializeComponent();
            this.ConnectionString = ConnectionString;
            this.PrestasiId = prestasiId;
            InitializeTingkatLombaComboBox();
            InitializePosisiComboBox();
            LoadPrestasiData();
        }

        private void InitializeTingkatLombaComboBox()
        {
            string query = "SELECT nama_tingkat FROM TingkatPrestasi";

            using (DatabaseConnection connection = new DatabaseConnection(ConnectionString))
            {
                connection.OpenConnection();

                using (SqlCommand command = new SqlCommand(query, connection.GetSqlConnection()))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Check if the value is DBNull before converting to string
                            object tingkatObject = reader["nama_tingkat"];
                            string? tingkat = tingkatObject != DBNull.Value ? tingkatObject.ToString() : null;

                            update_tingkatlombacombobox.Items.Add(tingkat);
                        }
                    }
                }
            }
        }

        private void InitializePosisiComboBox()
        {
            // Menghapus data yang sudah ada pada ComboBox
            update_posisicombobox.Items.Clear();

            // Mengambil data dari pilihan user
            string? selectedTingkat = update_tingkatlombacombobox.SelectedItem?.ToString();

            // Perkondisian dimana ketika nilai selectedTingkat tidak null
            if (selectedTingkat != null)
            {
                // Menampilkan opsi pada combobox posisi sesuai dengan tingkat yang dipilih
                string posisiQuery = "SELECT nama_posisi FROM PosisiPrestasi " +
                                     "WHERE id_tingkat = (SELECT id_tingkat FROM TingkatPrestasi WHERE nama_tingkat = @UpdatedTingkat)";

                using (DatabaseConnection connection = new DatabaseConnection(ConnectionString))
                {
                    connection.OpenConnection();

                    using (SqlCommand posisiCommand = new SqlCommand(posisiQuery, connection.GetSqlConnection()))
                    {
                        posisiCommand.Parameters.AddWithValue("@UpdatedTingkat", selectedTingkat);

                        using (SqlDataReader posisiReader = posisiCommand.ExecuteReader())
                        {
                            while (posisiReader.Read())
                            {
                                object posisiObject = posisiReader["nama_posisi"];
                                string? posisi = posisiObject != DBNull.Value ? posisiObject.ToString() : null;

                                update_posisicombobox.Items.Add(posisi);
                            }
                        }
                    }
                }
            }
        }
        private int RetrievePointsFromAcuanPoin(string tingkat, string posisi)
        {
            int points = 0;

            string query = "SELECT AP.id_poin " +
                           "FROM AcuanPoin AP " +
                           "JOIN TingkatPrestasi TP ON AP.id_tingkat = TP.id_tingkat " +
                           "JOIN PosisiPrestasi PP ON AP.id_posisi = PP.id_posisi " +
                           "WHERE TP.nama_tingkat = @UpdatedTingkat AND PP.nama_posisi = @UpdatedPosisi";

            using (DatabaseConnection connection = new DatabaseConnection(ConnectionString))
            {
                connection.OpenConnection();

                using (SqlCommand command = new SqlCommand(query, connection.GetSqlConnection()))
                {
                    command.Parameters.AddWithValue("@UpdatedTingkat", tingkat);
                    command.Parameters.AddWithValue("@UpdatedPosisi", posisi);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Melakukan perulangan untuk mencari dan menambahkan value poin yang ditemukan ke variabel points
                        while (reader.Read())
                        {
                            object result = reader["id_poin"];
                            if (result != null && result != DBNull.Value)
                            {
                                points += Convert.ToInt32(result);
                            }
                        }
                    }
                }
            }

            return points;
        }
        // Method LoadPrestasiData digunakan untuk mengambil data-data dari data Prestasi yang dipilih untuk dilakukan edit.
        // Pengambilan data ini berdasar pada id_prestasi
        private void LoadPrestasiData()
        {
            try
            {
                using (DatabaseConnection connection = new DatabaseConnection(ConnectionString))
                {
                    connection.OpenConnection();

                    string query = "SELECT P.nama_prestasi, TP.nama_tingkat, PP.nama_posisi, AP.poin, P.bukti " +
                                   "FROM Prestasi P " +
                                   "JOIN TingkatPrestasi TP ON P.id_tingkat = TP.id_tingkat " +
                                   "JOIN PosisiPrestasi PP ON P.id_posisi = PP.id_posisi " +
                                   "JOIN AcuanPoin AP ON P.id_poin = AP.id_poin " +
                                   "WHERE P.id_prestasi = @PrestasiId";

                    using (SqlCommand command = new SqlCommand(query, connection.GetSqlConnection()))
                    {
                        command.Parameters.AddWithValue("@PrestasiId", PrestasiId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                update_namaprestasitextbox.Text = reader["nama_prestasi"].ToString();
                                update_tingkatlombacombobox.SelectedItem = reader["nama_tingkat"].ToString();
                                update_posisicombobox.SelectedItem = reader["nama_posisi"].ToString();
                                update_buktitextbox.Text = reader["bukti"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void update_namaprestasitextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void update_buktitextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void update_posisicombobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void update_tingkatlombacombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializePosisiComboBox();
        }

        private void ubahbutton_Click(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseConnection connection = new DatabaseConnection(ConnectionString))
                {
                    connection.OpenConnection();

                    // Mengambil semua data atau value inputan user dan pilihan - pilihan user pada semua combobox yang ada 
                    string updatedNamaPrestasi = update_namaprestasitextbox.Text;
                    string updatedTingkat = update_tingkatlombacombobox.SelectedItem?.ToString() ?? "";
                    string updatedPosisi = update_posisicombobox.SelectedItem?.ToString() ?? "";
                    string updatedBukti = update_buktitextbox.Text;

                    int points = RetrievePointsFromAcuanPoin(updatedTingkat, updatedPosisi);

                    // Update data di tabel Prestasi
                    string query = "UPDATE Prestasi " +
                                   "SET nama_prestasi = @UpdatedNamaPrestasi, " +
                                   "id_tingkat = (SELECT TOP 1 id_tingkat FROM TingkatPrestasi WHERE nama_tingkat = @UpdatedTingkat), " +
                                   "id_posisi = (SELECT TOP 1 id_posisi FROM PosisiPrestasi WHERE nama_posisi = @UpdatedPosisi), " +
                                   "id_poin = @UpdatedPoin, " +
                                   "bukti = @UpdatedBukti " +
                                   "WHERE id_prestasi = @PrestasiId";

                    using (SqlCommand command = new SqlCommand(query, connection.GetSqlConnection()))
                    {
                        command.Parameters.AddWithValue("@UpdatedNamaPrestasi", updatedNamaPrestasi);
                        command.Parameters.AddWithValue("@UpdatedTingkat", updatedTingkat);
                        command.Parameters.AddWithValue("@UpdatedPosisi", updatedPosisi);
                        command.Parameters.AddWithValue("@UpdatedPoin", points);
                        command.Parameters.AddWithValue("@UpdatedBukti", updatedBukti);
                        command.Parameters.AddWithValue("@PrestasiId", PrestasiId);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Data berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
