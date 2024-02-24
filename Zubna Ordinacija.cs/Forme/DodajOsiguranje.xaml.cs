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
    /// Interaction logic for DodajOsiguranje.xaml
    /// </summary>
    public partial class DodajOsiguranje : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public DodajOsiguranje()
        {
            InitializeComponent();
            txtNazivKompanije.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }
        public DodajOsiguranje(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            txtNazivKompanije.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }

        private void ButtonSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@nazivKompanije", SqlDbType.NVarChar).Value = txtNazivKompanije.Text;
                cmd.Parameters.Add("@pacijentID", SqlDbType.Int).Value = cbPacijent.SelectedValue;
              
                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update Osiguranje Set nazivKuce=@nazivKompanije, pacijentID=@pacijentID Where kucaID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Osiguranje (nazivKuce, pacijentID) 
                                        values(@nazivKompanije, @pacijentID)";
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

        private void ButtonZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();
                string vratiPacijenta = @"select pacijentID,  imePacijenta + ' ' + prezimePacijenta as 'Pacijent' from Pacijent";
                DataTable dtPacijent = new DataTable();
                SqlDataAdapter daPacijent = new SqlDataAdapter(vratiPacijenta, konekcija);
                daPacijent.Fill(dtPacijent);
                cbPacijent.ItemsSource = dtPacijent.DefaultView;
                cbPacijent.DisplayMemberPath = @"Pacijent";
                dtPacijent.Dispose();
                daPacijent.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuca lista nije popunjena", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
    }
}
