using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TugasPBOKoneksiDatabase
{
    public partial class Form2 : Form
    {
        private readonly string ConnectionString;

        public Form2(string ConnectionString)
        {
            InitializeComponent();
            this.ConnectionString = ConnectionString;
            InitializeTingkatLombaComboBox();
            InitializePosisiComboBox();
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
                            // Melakukan cek apakah value bernilai DBNull sebelum melakukan convert ke bentuk string
                            object tingkatObject = reader["nama_tingkat"];
                            string? tingkat = tingkatObject != DBNull.Value ? tingkatObject.ToString() : null;

                            tingkatlombacombobox.Items.Add(tingkat);
                        }
                    }
                }
            }
        }

        private void InitializePosisiComboBox()
        {
            // Menghapus data yang sudah ada pada ComboBox Posisi
            posisicombobox.Items.Clear();

            // Mengambil data dari pilihan user
            string? selectedTingkat = tingkatlombacombobox.SelectedItem?.ToString();

            // Perkondisian dimana ketika nilai selectedTingkat tidak null
            if (selectedTingkat != null)
            {
                // Menampilkan opsi pada combobox Posisi sesuai dengan tingkat yang dipilih
                string posisiQuery = "SELECT nama_posisi FROM PosisiPrestasi " +
                                     "WHERE id_tingkat = (SELECT id_tingkat FROM TingkatPrestasi WHERE nama_tingkat = @Tingkat)";

                using (DatabaseConnection connection = new DatabaseConnection(ConnectionString))
                {
                    connection.OpenConnection();

                    using (SqlCommand posisiCommand = new SqlCommand(posisiQuery, connection.GetSqlConnection()))
                    {
                        posisiCommand.Parameters.AddWithValue("@Tingkat", selectedTingkat);

                        using (SqlDataReader posisiReader = posisiCommand.ExecuteReader())
                        {
                            while (posisiReader.Read())
                            {
                                object posisiObject = posisiReader["nama_posisi"];
                                string? posisi = posisiObject != DBNull.Value ? posisiObject.ToString() : null;

                                posisicombobox.Items.Add(posisi);
                            }
                        }
                    }
                }
            }
            //Mengaktifkan combobox posisi
            posisicombobox.Enabled = true;
        }

        private int RetrievePointsFromAcuanPoin(string tingkat, string posisi)
        {
            int points = 0;

            string query = "SELECT AP.id_poin " +
                           "FROM AcuanPoin AP " +
                           "JOIN TingkatPrestasi TP ON AP.id_tingkat = TP.id_tingkat " +
                           "JOIN PosisiPrestasi PP ON AP.id_posisi = PP.id_posisi " +
                           "WHERE TP.nama_tingkat = @Tingkat AND PP.nama_posisi = @Posisi";

            using (DatabaseConnection connection = new DatabaseConnection(ConnectionString))
            {
                connection.OpenConnection();

                using (SqlCommand command = new SqlCommand(query, connection.GetSqlConnection()))
                {
                    command.Parameters.AddWithValue("@Tingkat", tingkat);
                    command.Parameters.AddWithValue("@Posisi", posisi);

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

        private void tingkatlombacombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Mematikan fungsi combobox posisi sampai user memilih salah satu tingkat lomba pada form2
            posisicombobox.Enabled = false;
            InitializePosisiComboBox();
        }

        private void simpanbutton_Click(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseConnection connection = new DatabaseConnection(ConnectionString))
                {
                    connection.OpenConnection();

                    // Mengambil semua data atau value inputan user dan pilihan - pilihan user pada semua combobox yang ada
                    string namaPrestasi = namaprestasitextbox.Text;
                    string selectedTingkat = tingkatlombacombobox.SelectedItem?.ToString() ?? "";
                    string selectedPosisi = posisicombobox.SelectedItem?.ToString() ?? "";
                    string bukti = buktitextbox.Text;

                    // Mendapatkan atau mengambil nilai poin dari tabel AcuanPoin menggunakan method RetrievePointsFromAcuanPoin
                    // Nilai poin diambil berdasarkan tingkat dan posisi yang dipilih oleh user pada combobox
                    int points = RetrievePointsFromAcuanPoin(selectedTingkat, selectedPosisi);

                    // Memasukkan data ke tabel Prestasi pada database
                    string query = "INSERT INTO Prestasi (nama_prestasi, id_tingkat, id_posisi, id_poin, bukti) " +
                                   "VALUES (@NamaPrestasi, " +
                                   "(SELECT TOP 1 id_tingkat FROM TingkatPrestasi WHERE nama_tingkat = @Tingkat), " +
                                   "(SELECT TOP 1 id_posisi FROM PosisiPrestasi WHERE nama_posisi = @Posisi), " +
                                   "@Poin, " +
                                   "@Bukti)";

                    using (SqlCommand command = new SqlCommand(query, connection.GetSqlConnection()))
                    {
                        command.Parameters.AddWithValue("@NamaPrestasi", namaPrestasi);
                        command.Parameters.AddWithValue("@Tingkat", selectedTingkat);
                        command.Parameters.AddWithValue("@Posisi", selectedPosisi);
                        command.Parameters.AddWithValue("@Poin", points);
                        command.Parameters.AddWithValue("@Bukti", bukti);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Data berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void namaprestasitextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void buktitextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void posisicombobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
