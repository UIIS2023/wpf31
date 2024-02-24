using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Zubna_Ordinacija.cs.Forme
{
    /// <summary>
    /// Interaction logic for MedicinskiKarton.xaml
    /// </summary>
    public partial class MedicinskiKarton : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public MedicinskiKarton()
        {
            InitializeComponent();
            txtPrethodnaDijagnoza.Focus();
            konekcija = kon.KreirajKonekciju();
        }
        public MedicinskiKarton(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            txtPrethodnaDijagnoza.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            konekcija = kon.KreirajKonekciju();
        }

         private void BtnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@prethodnaDijagnoza", SqlDbType.NVarChar).Value = txtPrethodnaDijagnoza.Text;
                cmd.Parameters.Add("@rezultatiTestova", SqlDbType.NVarChar).Value = txtRezultatiTestova.Text;
                
                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update [Medicinski karton Set prethodnaDijagnoza=@prethodnaDijagnoza, rezultatiPregleda=@rezultatiTestova Where kartonID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into [Medicinski karton] (prethodnaDijagnoza, rezultatiPregleda) 
                                        values(@prethodnaDijagnoza, @rezultatiTestova)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unos nije validan", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }

        

    }

         private void BtnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
