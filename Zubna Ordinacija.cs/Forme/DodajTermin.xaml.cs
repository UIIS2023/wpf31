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
using System.Globalization;

namespace Zubna_Ordinacija.cs.Forme
{
    /// <summary>
    /// Interaction logic for DodajTermin.xaml
    /// </summary>
    public partial class DodajTermin : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public DodajTermin()
        {
            InitializeComponent();
            txtVreme.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }
        public DodajTermin (bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            txtVreme.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }

        private void BtnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                DateTime date = (DateTime)dpDatum.SelectedDate;
                string datum = date.ToString("yyyy - MM - dd", CultureInfo.CurrentCulture);
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@vreme", SqlDbType.Time).Value = txtVreme.Text;
                cmd.Parameters.Add("@datum", SqlDbType.DateTime).Value = dpDatum.Text;
                cmd.Parameters.Add("@pacijentID", SqlDbType.Int).Value = cbPacijent.SelectedValue;
                
                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update Termin Set vreme=@vreme, datum=@datum, pacijentID=@pacijentID Where terminID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Termin (vreme, datum, pacijentID) 
                                        values(@vreme, @datum, @pacijentID)";
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
