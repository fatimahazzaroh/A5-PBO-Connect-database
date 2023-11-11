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
            // Clear existing items in posisicombobox
            posisicombobox.Items.Clear();

            // Get the selected category
            string? selectedTingkat = tingkatlombacombobox.SelectedItem?.ToString();

            // Populate posisicombobox based on the selected category
            if (selectedTingkat != null)
            {
                // Use the ConnectionString to fetch data
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

            // Enable posisicombobox
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
                        // Iterate through the results and sum up the points
                        while (reader.Read())
                        {
                            object result = reader["id_poin"];
                            if (result != null && result != DBNull.Value)
                            {
                                // Sum up the points
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
            // Disable posisicombobox until a value is selected in tingkatlombacombobox
            posisicombobox.Enabled = false;

            // Call the method to populate posisicombobox based on the selected level
            InitializePosisiComboBox();
        }

        private void simpanbutton_Click(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseConnection connection = new DatabaseConnection(ConnectionString))
                {
                    connection.OpenConnection();

                    // Get the selected values
                    string namaPrestasi = namaprestasitextbox.Text;
                    string selectedTingkat = tingkatlombacombobox.SelectedItem?.ToString() ?? "";
                    string selectedPosisi = posisicombobox.SelectedItem?.ToString() ?? "";
                    string bukti = buktitextbox.Text;

                    // Retrieve points from AcuanPoin
                    int points = RetrievePointsFromAcuanPoin(selectedTingkat, selectedPosisi);

                    // Insert data into Prestasi table
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
