using System;
using System.Windows.Controls;
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
            //Move to Information Panorama Page
            NavigationService.Navigate(new Uri("/Information.xaml", UriKind.RelativeOrAbsolute));
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