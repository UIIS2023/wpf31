using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zubna_Ordinacija.cs.Forme;




namespace Zubna_Ordinacija.cs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ucitanaTabela;
        bool azuriraj;
        DataRowView pomocniRed;
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        #region Select Upiti
        string zdravstveniRadniciSelect = @"Select zdravstveniID as ID, imeZaposlenog as 'Ime zaposlenog', prezimeZaposlenog as 'Prezime zaposlenog', adresaZaposlenog as 'Adresa zaposlenog', grad as 'Grad', kontakt as 'Kontakt', plata as 'Plata', zvanje as 'Zvanje' from [Zdravstveni Radnik]";
        string medicinskiKartoniSelect = @"Select kartonID as ID, prethodnaDijagnoza as 'Prethodna dijagnoza', rezultatiPregleda as 'Rezultati testova' from [Medicinski Karton]";
        string pacijentiSelect = @"Select pacijentID as ID, imePacijenta as 'Ime pacijenta', prezimePacijenta as 'Prezime Pacijenta', adresaPacijenta as 'Adresa', grad as 'Grad', jmbg as 'JMBG', kontakt as 'Kontakt', prethodnaDijagnoza as 'Prethodna dijagnoza', rezultatiPregleda as 'Medicinski karton'
                                   from Pacijent 
								   join [Medicinski karton] on Pacijent.kartonID = [Medicinski karton].kartonID";

        string dijagnozeSelect = @"Select dijagnozaID as ID, nazivDijagnoze as 'Naziv dijagnoze' from Dijagnoza";
        string izvestajSelect = @"Select izvestajID as ID, razlogPosete as 'Razlog posete', terapija, imePacijenta + ' ' + prezimePacijenta as 'Pacijent', imeZaposlenog+' ' + prezimeZaposlenog as Zaposleni 
                                    from [Izvestaj lekara]
                                    join Pacijent on [Izvestaj lekara].pacijentID = Pacijent.pacijentID
                                    join [Zdravstveni radnik] on [Izvestaj lekara].zaposleniID = [Zdravstveni radnik].zdravstveniID";
        string terminiSelect = @"Select terminID as ID, vreme as 'Vreme', datum as 'Datum', imePacijenta + ' ' + prezimePacijenta as 'Pacijent', imeZaposlenog+' ' + prezimeZaposlenog as Zaposleni from Termin
                                 join Pacijent on Termin.pacijentID = Pacijent.pacijentID
                                    join [Zdravstveni radnik] on Termin.zaposleniID = [Zdravstveni radnik].zdravstveniID";
        string isplataSelect = @"Select isplataID as ID, cena as 'Cena', imePacijenta + ' ' + prezimePacijenta as 'Pacijent' from Isplata
                                   join Pacijent on Isplata.pacijentID = Pacijent.pacijentID";
        string osiguranjaSelect = @"Select kucaID as ID, nazivKuce as 'Naziv osiguravajuce kuce', imePacijenta + ' ' + prezimePacijenta as 'Pacijent' from Osiguranje
                                   join Pacijent on Osiguranje.pacijentID = Pacijent.pacijentID";

        #endregion
        #region Select Upiti sa uslovom
        string selectUslovPacijenti = @"Select * from Pacijent where pacijentID=";
        string selectUslovZdravstveniRadnici = @"Select * from [Zdravstveni Radnik] where zdravstveniID=";
        string selectUslovMedicinskiKartoni = @"Select * from [Medicinski karton] where kartonID=";
        string selectUslovDijagnoze = @"Select * from Dijagnoza where dijagnozaID=";
        string selectUslovIzvestaj = @"Select * from [Izvestaj lekara] where izvestajID=";
        string selectUslovTermini = @"Select * from Termin where terminID=";
        string selectUslovIsplata = @"Select * from Isplata where isplataID=";
        string selectUslovOsiguranja = @"Select * from Osiguranje where kucaID=";
        #endregion
        #region Delete Upiti
        string pacijentiDelete = @"Delete from Pacijent where pacijentID=";
        string zdravstveniRadniciDelete = @"Delete from [Zdravstveni Radnik] where zdravstveniID=";
        string medicinskiKartoniDelete = @"Delete from [Medicinski karton] where kartonID=";
        string dijagnozeDelete = @"Delete from Dijagnoza where dijagnozaID=";
        string izvestajDelete = @"Delete from [Izvestaj lekara] where izvestajID=";
        string terminiDelete = @"Delete from Termin where terminID=";
        string isplataDelete = @"Delete from Isplata where isplataID=";
        string osiguranjaDelete = @"Delete from Osiguranje where kucaID=";
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            UcitajPodatke(dataGridCentralni, zdravstveniRadniciSelect);
            konekcija = kon.KreirajKonekciju();
        }
        private void UcitajPodatke(DataGrid grid, string selectUpit)
        {
            try
            {
                konekcija = kon.KreirajKonekciju();
                konekcija.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dt = new DataTable
                {
                    Locale = CultureInfo.InvariantCulture
                };
                dataAdapter.Fill(dt);
                if(grid != null)
                {
                    grid.ItemsSource = dt.DefaultView;
                }
                ucitanaTabela = selectUpit;
                dt.Dispose();
                dataAdapter.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Neuspesno ucitani podaci!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if(konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        private void BtnPacijenti_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, pacijentiSelect);
        }
        private void BtnZdravstveniRadnici_Click(Object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, zdravstveniRadniciSelect);
        }
        private void BtnMedicinskiKartoni_Click(object sender, RoutedEventArgs e) 
        {
            UcitajPodatke(dataGridCentralni, medicinskiKartoniSelect);
        }
        private void BtnDijagnoze_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, dijagnozeSelect);
        }
        private void BtnTerapije_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, izvestajSelect);
        }
        private void BtnTermini_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, terminiSelect);
        }
        private void BtnRacuni_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, isplataSelect);
        }
        private void BtnOsiguranja_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, osiguranjaSelect);
        }
        //CREATE
        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;

            if(ucitanaTabela.Equals(pacijentiSelect))
            {
                prozor = new DodajPacijenta();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, pacijentiSelect);
            }
            else if(ucitanaTabela.Equals(zdravstveniRadniciSelect))
            {
                prozor = new DodajZdravstvenogRadnika();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, zdravstveniRadniciSelect);
            }
            else if (ucitanaTabela.Equals(medicinskiKartoniSelect))
            {
                prozor = new MedicinskiKarton();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, medicinskiKartoniSelect);
            }
            else if (ucitanaTabela.Equals(dijagnozeSelect))
            {
                prozor = new DodajDijagnozu();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, dijagnozeSelect);
            }
            else if (ucitanaTabela.Equals(izvestajSelect))
            {
                prozor = new DodajTerapiju();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, izvestajSelect);
            }
            else if (ucitanaTabela.Equals(terminiSelect))
            {
                prozor = new DodajTermin();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, terminiSelect);
            }
            else if (ucitanaTabela.Equals(isplataSelect))
            {
                prozor = new DodajRacun();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, isplataSelect);
            }
            else if (ucitanaTabela.Equals(osiguranjaSelect))
            {
                prozor = new DodajOsiguranje();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, osiguranjaSelect);
            }
        }
        //UPDATE
        private void PopuniFormu(DataGrid grid, string selectUslov) 
        {
            try
            {
                konekcija.Open();
                azuriraj = true;
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                pomocniRed = red;
                SqlCommand komanda = new SqlCommand
                {
                    Connection = konekcija
                };
                komanda.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                komanda.CommandText = selectUslov + "@id";
                SqlDataReader citac = komanda.ExecuteReader();
                komanda.Dispose();
                while(citac.Read()) 
                {
                    if(ucitanaTabela.Equals(pacijentiSelect, StringComparison.Ordinal))
                    {
                        DodajPacijenta prozorPacijent = new DodajPacijenta(azuriraj, pomocniRed);
                        prozorPacijent.txtIme.Text = citac["imePacijenta"].ToString();
                        prozorPacijent.txtPrezime.Text = citac["prezimePacijenta"].ToString();
                        prozorPacijent.txtAdresa.Text = citac["adresaPacijenta"].ToString();
                        prozorPacijent.txtGrad.Text = citac["grad"].ToString();
                        prozorPacijent.txtJMBG.Text = citac["jmbg"].ToString();
                        prozorPacijent.txtKontakt.Text = citac["kontakt"].ToString();
                        prozorPacijent.cbMedicinskiKarton.SelectedValue = citac["kartonID"].ToString();
                        prozorPacijent.ShowDialog();
                    }
                    else if(ucitanaTabela.Equals(zdravstveniRadniciSelect, StringComparison.Ordinal))
                    {
                        DodajZdravstvenogRadnika prozorZdravstveniRadnik = new DodajZdravstvenogRadnika(azuriraj, pomocniRed);
                        prozorZdravstveniRadnik.txtIme.Text = citac["imeZaposlenog"].ToString();
                        prozorZdravstveniRadnik.txtPrezime.Text = citac["prezimeZaposlenog"].ToString();
                        prozorZdravstveniRadnik.txtAdresa.Text = citac["adresaZaposlenog"].ToString();
                        prozorZdravstveniRadnik.txtGrad.Text = citac["grad"].ToString();
                        prozorZdravstveniRadnik.txtKontakt.Text = citac["kontakt"].ToString();
                        prozorZdravstveniRadnik.txtPlata.Text = citac["plata"].ToString();
                        prozorZdravstveniRadnik.txtZvanje.Text = citac["zvanje"].ToString();
                        prozorZdravstveniRadnik.ShowDialog();
                    }
                    else if(ucitanaTabela.Equals(medicinskiKartoniSelect, StringComparison.Ordinal))
                    {
                        MedicinskiKarton prozorMedicinskiKarton = new MedicinskiKarton(azuriraj, pomocniRed);
                        prozorMedicinskiKarton.txtPrethodnaDijagnoza.Text = citac["prethodnaDijagnoza"].ToString();
                        prozorMedicinskiKarton.txtRezultatiTestova.Text = citac["rezultatiPregleda"].ToString();
                        prozorMedicinskiKarton.ShowDialog();
                    }
                    else if(ucitanaTabela.Equals(dijagnozeSelect, StringComparison.Ordinal))
                    {
                        DodajDijagnozu prozorDijagnoza = new DodajDijagnozu(azuriraj, pomocniRed);
                        prozorDijagnoza.txtNazivDijagnoze.Text = citac["NazivDijagnoze"].ToString();

                        prozorDijagnoza.ShowDialog();
                    }
                    else if(ucitanaTabela.Equals(izvestajSelect, StringComparison.Ordinal))
                    {
                        DodajTerapiju prozor = new DodajTerapiju(azuriraj, pomocniRed);
                        prozor.txtRazlog.Text = citac["razlogPosete"].ToString();
                        prozor.txtTerapija.Text = citac["terapija"].ToString();
                        prozor.cbPacijent.SelectedValue = citac["pacijentID"].ToString();
                        prozor.cbDijagnoza.SelectedValue = citac["dijagnozaID"].ToString();
                        prozor.cbZaposleni.SelectedValue = citac["zaposleniID"].ToString();
                        prozor.ShowDialog();
                    }
                    else if(ucitanaTabela.Equals(terminiSelect, StringComparison.Ordinal))
                    {
                        DodajTermin prozorTermin = new DodajTermin(azuriraj, pomocniRed);
                        prozorTermin.txtVreme.Text = citac["vreme"].ToString();
                        prozorTermin.dpDatum.SelectedDate = (DateTime)citac["datum"];
                        prozorTermin.cbPacijent.SelectedValue = citac["pacijentID"].ToString();
                        prozorTermin.ShowDialog();
                    }
                    else if(ucitanaTabela.Equals(isplataSelect, StringComparison.Ordinal)) 
                    {
                        DodajRacun prozorRacun = new DodajRacun(azuriraj, pomocniRed);
                        prozorRacun.txtCena.Text = citac["cena"].ToString();
                        prozorRacun.cbPacijent.SelectedValue = citac["pacijentID"].ToString();
                        prozorRacun.ShowDialog();
                    }
                    else if(ucitanaTabela.Equals(osiguranjaSelect, StringComparison.Ordinal)) 
                    {
                        DodajOsiguranje prozorOsiguranje = new DodajOsiguranje(azuriraj, pomocniRed);
                        prozorOsiguranje.txtNazivKompanije.Text = citac["nazivKuce"].ToString();
                        prozorOsiguranje.cbPacijent.SelectedValue = citac["pacijentID"].ToString();
                        prozorOsiguranje.ShowDialog();
                    }
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if(konekcija != null)
                {
                    konekcija.Close();
                }
                azuriraj = false;
            }
        }
        //DELETE
        private void ObrisiZapis(DataGrid grid, string deleteUpit)
        {
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(rezultat == MessageBoxResult.Yes)
                {
                    SqlCommand komanda = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    komanda.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    komanda.CommandText = deleteUpit + "@id";
                    komanda.ExecuteNonQuery();
                    komanda.Dispose();
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red", "Obavestenje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(SqlException)
            {
                MessageBox.Show("Postoje povezani podaci u drugim tabelama", "Obavestenje!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if(konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        private void BtnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if(ucitanaTabela.Equals(pacijentiSelect))
            {
                ObrisiZapis(dataGridCentralni, pacijentiDelete);
                UcitajPodatke(dataGridCentralni, pacijentiSelect);
            }
            else if(ucitanaTabela.Equals(zdravstveniRadniciSelect))
            {
                ObrisiZapis(dataGridCentralni, zdravstveniRadniciDelete);
                UcitajPodatke(dataGridCentralni, zdravstveniRadniciSelect);
            }
            else if (ucitanaTabela.Equals(medicinskiKartoniSelect))
            {
                ObrisiZapis(dataGridCentralni, medicinskiKartoniDelete);
                UcitajPodatke(dataGridCentralni, medicinskiKartoniSelect);
            }
            else if (ucitanaTabela.Equals(dijagnozeSelect))
            {
                ObrisiZapis(dataGridCentralni, dijagnozeDelete);
                UcitajPodatke(dataGridCentralni, dijagnozeSelect);
            }
            else if (ucitanaTabela.Equals(izvestajSelect))
            {
                ObrisiZapis(dataGridCentralni, izvestajDelete);
                UcitajPodatke(dataGridCentralni, izvestajSelect);
            }
            else if (ucitanaTabela.Equals(terminiSelect))
            {
                ObrisiZapis(dataGridCentralni, terminiDelete);
                UcitajPodatke(dataGridCentralni, terminiSelect);
            }
            else if (ucitanaTabela.Equals(isplataSelect))
            {
                ObrisiZapis(dataGridCentralni, isplataDelete);
                UcitajPodatke(dataGridCentralni, isplataSelect);
            }
            else if (ucitanaTabela.Equals(osiguranjaSelect))
            {
                ObrisiZapis(dataGridCentralni, osiguranjaDelete);
                UcitajPodatke(dataGridCentralni, osiguranjaSelect);
            }
        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(pacijentiSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovPacijenti);
                UcitajPodatke(dataGridCentralni, pacijentiSelect);
            }
            else if (ucitanaTabela.Equals(zdravstveniRadniciSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovZdravstveniRadnici);
                UcitajPodatke(dataGridCentralni, zdravstveniRadniciSelect);
            }
            else if (ucitanaTabela.Equals(medicinskiKartoniSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovMedicinskiKartoni);
                UcitajPodatke(dataGridCentralni, medicinskiKartoniSelect);
            }
            else if (ucitanaTabela.Equals(dijagnozeSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovDijagnoze);
                UcitajPodatke(dataGridCentralni, dijagnozeSelect);
            }
            else if (ucitanaTabela.Equals(izvestajSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovIzvestaj);
                UcitajPodatke(dataGridCentralni, izvestajSelect);
            }
            else if (ucitanaTabela.Equals(terminiSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovTermini);
                UcitajPodatke(dataGridCentralni, terminiSelect);
            }
            else if (ucitanaTabela.Equals(isplataSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovIsplata);
                UcitajPodatke(dataGridCentralni, isplataSelect);
            }
            else if (ucitanaTabela.Equals(osiguranjaSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovOsiguranja);
                UcitajPodatke(dataGridCentralni, osiguranjaSelect);
            }
        }
    }
}
