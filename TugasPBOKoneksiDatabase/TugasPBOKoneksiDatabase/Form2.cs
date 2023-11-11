using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace TugasPBOKoneksiDatabase
{
    public partial class Form2 : Form
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\USERS\ASUS\SOURCE\REPOS\TUGASPBOKONEKSIDATABASE\TUGASPBOKONEKSIDATABASE\Prestasi_db.mdf;Integrated Security=True";

        public Form2()
        {
            InitializeComponent();
            InitializeTingkatLombaComboBox();
            InitializePosisiComboBox();
        }

        private void InitializeTingkatLombaComboBox()
        {
            string query = "SELECT nama_tingkat FROM TingkatPrestasi";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Check if the value is DBNull before converting to string
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
            posisicombobox.Items.Clear();

            string query = "SELECT nama_posisi FROM PosisiPrestasi";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Check if the value is DBNull before converting to string
                            object posisiObject = reader["nama_posisi"];
                            string? posisi = posisiObject != DBNull.Value ? posisiObject.ToString() : null;

                            posisicombobox.Items.Add(posisi);
                        }
                    }
                }
            }

            // Enable posisicombobox
            posisicombobox.Enabled = true;
        }

        private void namaprestasitextbox_TextChanged(object sender, EventArgs e)
        {

        }
        private void posisicombobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void tingkatlombacombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Disable posisicombobox until a value is selected in tingkatlombacombobox
            posisicombobox.Enabled = false;

            string? selectedLevel = tingkatlombacombobox.SelectedItem?.ToString();

            if (selectedLevel != null)
            {
                if (selectedLevel == "Nasional DIKTI")
                {
                    // Enable posisicombobox for Nasional DIKTI
                    posisicombobox.Enabled = true;
                }
                else
                {
                    // For other levels, initialize posisicombobox as before
                    InitializePosisiComboBox();

                    // Remove "Terpilih / Didanai" if it exists in the items
                    if (posisicombobox.Items.Contains("Terpilih / Didanai"))
                    {
                        posisicombobox.Items.Remove("Terpilih / Didanai");
                    }
                }
            }
        }

        private void simpanbutton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Prestasi (nama_prestasi, id_tingkat, id_posisi, id_poin, bukti) " +
                                   "VALUES (@NamaPrestasi, " +
                                   "(SELECT TP.id_tingkat FROM TingkatPrestasi TP WHERE TP.nama_tingkat = @Tingkat), " +
                                   "(SELECT PP.id_posisi FROM PosisiPrestasi PP WHERE PP.nama_posisi = @Posisi), " +
                                   "(SELECT AP.id_poin FROM AcuanPoin AP " +
                                   "  JOIN TingkatPrestasi TP ON AP.id_tingkat = TP.id_tingkat " +
                                   "  JOIN PosisiPrestasi PP ON AP.id_posisi = PP.id_posisi " +
                                   "  WHERE TP.nama_tingkat = @Tingkat AND PP.nama_posisi = @Posisi), " +
                                   "@Bukti)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NamaPrestasi", namaprestasitextbox.Text);
                        command.Parameters.AddWithValue("@Tingkat", tingkatlombacombobox.SelectedItem?.ToString() ?? "");
                        command.Parameters.AddWithValue("@Posisi", posisicombobox.SelectedItem?.ToString() ?? "");
                        command.Parameters.AddWithValue("@Bukti", buktitextbox.Text);

                        int? poin = RetrievePointsFromAcuanPoin(tingkatlombacombobox.SelectedItem?.ToString() ?? "", posisicombobox.SelectedItem?.ToString() ?? "");
                        command.Parameters.AddWithValue("@Poin", poin);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Data berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Set DialogResult to OK before closing the form
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);       
            }
        }

        private int RetrievePointsFromAcuanPoin(string tingkat, string posisi)
        {
            int points = 0;

            string query = "SELECT AP.poin " +
                           "FROM AcuanPoin AP " +
                           "JOIN TingkatPrestasi TP ON AP.id_tingkat = TP.id_tingkat " +
                           "JOIN PosisiPrestasi PP ON AP.id_posisi = PP.id_posisi " +
                           "WHERE TP.nama_tingkat = @Tingkat AND PP.nama_posisi = @Posisi";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Tingkat", tingkat);
                    command.Parameters.AddWithValue("@Posisi", posisi);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        points = Convert.ToInt32(result);
                    }
                }
            }

            return points;
        }

        private void buktitextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}