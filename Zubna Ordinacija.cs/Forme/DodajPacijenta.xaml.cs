using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace Zubna_Ordinacija.cs.Forme
{
    /// <summary>
    /// Interaction logic for DodajPacijenta.xaml
    /// </summary>
    public partial class DodajPacijenta : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        bool azuriraj;
        DataRowView pomocniRed;

        public DodajPacijenta()
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }
        public DodajPacijenta(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            txtIme.Focus();
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
                    cmd.Parameters.Add("@ime", SqlDbType.NVarChar).Value = txtIme.Text;
                    cmd.Parameters.Add("@prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;
                    cmd.Parameters.Add("@adresa", SqlDbType.NVarChar).Value = txtAdresa.Text;
                    cmd.Parameters.Add("@grad", SqlDbType.NVarChar).Value = txtGrad.Text;
                    cmd.Parameters.Add("@jmbg", SqlDbType.NVarChar).Value = txtJMBG.Text;
                    cmd.Parameters.Add("@kontakt", SqlDbType.NVarChar).Value = txtKontakt.Text;
                    cmd.Parameters.Add("@medicinskiKartonID", SqlDbType.Int).Value = cbMedicinskiKarton.SelectedValue;

                if (this.azuriraj)
                    {
                        DataRowView red = this.pomocniRed;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                        cmd.CommandText = @"Update Pacijent Set imePacijenta=@ime, prezimePacijenta=@prezime, adresaPacijenta=@adresa, grad=@grad, jmbg=@jmbg, kontakt=@kontakt, kartonID=@medicinskiKartonID
                                            Where pacijentID = @id";
                    this.pomocniRed = null;
                    }
                    else
                    {
                        cmd.CommandText = @"insert into Pacijent (imePacijenta, prezimePacijenta, adresaPacijenta, grad, jmbg, kontakt, kartonID) 
                                        values(@ime, @prezime, @adresa, @grad, @jmbg, @kontakt, @medicinskiKartonID)";
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
                string vratiMedicinskiKarton = @"select kartonID, prethodnaDijagnoza + ' - ' + rezultatiPregleda as 'Medicinski karton' from [Medicinski karton]";
                DataTable dtMedicinskiKarton = new DataTable {
                    Locale = CultureInfo.InvariantCulture
                };
                SqlDataAdapter daMedicinskiKarton = new SqlDataAdapter(vratiMedicinskiKarton, konekcija);
                daMedicinskiKarton.Fill(dtMedicinskiKarton);
                cbMedicinskiKarton.ItemsSource = dtMedicinskiKarton.DefaultView;
                cbMedicinskiKarton.DisplayMemberPath = @"Medicinski karton";
                dtMedicinskiKarton.Dispose();
                daMedicinskiKarton.Dispose();
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
