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
    /// Interaction logic for DodajTerapiju.xaml
    /// </summary>
    public partial class DodajTerapiju : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public DodajTerapiju()
        {
            InitializeComponent();
            
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }
        public DodajTerapiju(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            
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
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@razlog", SqlDbType.NVarChar).Value = txtRazlog.Text;
                cmd.Parameters.Add("@terapija", SqlDbType.NVarChar).Value = txtTerapija.Text;
                cmd.Parameters.Add("@pacijentID", SqlDbType.Int).Value = cbPacijent.SelectedValue;
                cmd.Parameters.Add("@dijagnozaID", SqlDbType.Int).Value = cbDijagnoza.SelectedValue;
                cmd.Parameters.Add("@zaposleniID", SqlDbType.Int).Value = cbZaposleni.SelectedValue;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update [Izvestaj lekara] Set razlogPosete=@razlog, terapija=@terapija, pacijentID=@pacijentID, dijagnozaID=@dijagnozaID, zaposleniID=@zaposleniID Where izvestajID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into [Izvestaj lekara] (razlogPosete, terapija, pacijentID, dijagnozaID, zaposleniID ) 
                                        values(@razlog, @terapija, @pacijentID, @dijagnozaID, @zaposleniID )";
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
                string vratiDijagnozu = @"select dijagnozaID, nazivDijagnoze as 'Dijagnoza' from Dijagnoza";
                DataTable dtDijagnoza = new DataTable();
                SqlDataAdapter daDijagnoza = new SqlDataAdapter(vratiDijagnozu, konekcija);
                daDijagnoza.Fill(dtDijagnoza);
                cbDijagnoza.ItemsSource = dtDijagnoza.DefaultView;
                cbDijagnoza.DisplayMemberPath = @"Dijagnoza";
                dtDijagnoza.Dispose();
                daDijagnoza.Dispose();
                string vratiZaposlenog = @"select zdravstveniID, imeZaposlenog + ' ' + prezimeZaposlenog as 'Zaposleni' from [Zdravstveni Radnik]";
                DataTable dtZaposleni = new DataTable
                {
                    Locale = CultureInfo.InvariantCulture
                };
                SqlDataAdapter daZaposleni = new SqlDataAdapter(vratiZaposlenog, konekcija);
                daZaposleni.Fill(dtZaposleni);
                cbZaposleni.ItemsSource = dtZaposleni.DefaultView;
                cbZaposleni.DisplayMemberPath = @"Zaposleni";
                dtZaposleni.Dispose();
                daZaposleni.Dispose();

                string vratiPacijenta = @"select pacijentID, imePacijenta + ' ' + prezimePacijenta as 'Pacijent' from Pacijent";
                DataTable dtPacijent = new DataTable
                {
                    Locale = CultureInfo.InvariantCulture
                };
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