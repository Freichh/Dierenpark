using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Dierenpark
{
    /// <summary>
    /// Dierenpark
    /// Het Noorder Dierenpark in Emmen hanteert onder andere de volgende tarieven voor abonnementen:
    ///• Echtpaar € 61,=
    ///• Gezin met 1 kind € 71,=
    ///• Gezin met 2 kinderen € 82,=
    ///• Persoonlijk € 30,=
    ///• Elk kind extra € 11,=
    ///• Echtpaar 65+ € 65,=
    ///• Persoonlijk 65+ € 26,=
    ///De te ontwikkelen software moet aan de hand van de gegevens van de abonnementaanvrager de
    ///abonnementsprijs bepalen.
    ///De leeftijd van de volwassene(n) moet aan de hand van geboortedatum en een peildatum worden
    ///berekend.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string achternaam;

        DateTime geboortedatum = new DateTime();
        DateTime peildatum = DateTime.Now;
        TimeSpan leeftijdSpan = new TimeSpan();
        int leeftijd;

        bool partner = false;
        
        int kinderen = 0;

        int totaalPrijs;

        int prijsEchtpaar = 61;
        int prijsGezinMet1Kind = 71;
        int prijsGezinMet2Kind = 82;
        int prijsPersoonlijk = 30;
        int prijsKindExtra = 11;
        int prijsEchtpaar65 = 65;
        int prijsPersoonlijk65 = 26;

        private void achternaam_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            achternaam = achternaam_textBox.Text;
            Debug.WriteLine(achternaam);
        }

        private void geboortedatum_DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            bereken_button.IsEnabled = true;
            geboortedatum = geboortedatum_DatePicker.SelectedDate.Value;
            leeftijdSpan = peildatum - geboortedatum;
            leeftijd = leeftijdSpan.Days / 365;
            Debug.WriteLine(leeftijd);
        }

        private void bereken_button_Click(object sender, RoutedEventArgs e)
        {
            if (kinderen == 1)
            {
                totaalPrijs = prijsGezinMet1Kind;
            }
            else if (kinderen == 2)
            {
                totaalPrijs = prijsGezinMet2Kind;
            }
            else if (kinderen > 2)
            {
                totaalPrijs = prijsGezinMet2Kind + ((kinderen - 2) * prijsKindExtra);
            }
            else
            {
                if (leeftijd >= 65)
                {
                    if (partner)
                    {
                        totaalPrijs = prijsEchtpaar65;
                    }
                    else
                    {
                        totaalPrijs = prijsPersoonlijk65;
                    }
                }
                else
                {
                    if (partner)
                    {
                        totaalPrijs = prijsEchtpaar;
                    }
                    else
                    {
                        totaalPrijs = prijsPersoonlijk;
                    }
                }
            }
            totaalprijs_textBox.Text = string.Format("Dhr./Mevr. {0}, uw totaalprijs wordt: {1}", achternaam, 
                totaalPrijs.ToString("c", CultureInfo.CreateSpecificCulture("nl-NL")));
        }

        private void kinderen_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            kinderen = (int)kinderen_slider.Value;
            Debug.WriteLine(kinderen);
        }

        private void partner_checkBox_Click(object sender, RoutedEventArgs e)
        {
            partner = partner ? partner = false : partner = true;
            Debug.WriteLine(partner);
        }


    }
}
