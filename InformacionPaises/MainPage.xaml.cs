using System;
using System.Windows.Controls;
using InformacionPaisesBackEnd;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace InformacionPaises
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void ApplicationBarIconButtonClick(object sender, EventArgs e)
        {
            var selectedCountry = Paises.SelectedItem as Country;

            //Move to Information Panorama Page
            string uri = "/Information.xaml?" ;
            //params : Country Code
            if (selectedCountry != null)
            {
                uri += "cId=" + selectedCountry.ID;
                uri += "&cc=" + selectedCountry.Code;
                uri += "&currency=" + selectedCountry.Currency;
            }
            
            NavigationService.Navigate(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        private void PaisesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ApplicationBar.Buttons == null || ApplicationBar.Buttons.Count != 1) return;
            var applicationBarIconButton = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
            if (applicationBarIconButton != null)
            {
                applicationBarIconButton.IsEnabled = true;                
            }
        }
    }
}