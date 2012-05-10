using System;
using InformacionPaisesBackEnd;
using Microsoft.Phone.Controls;

namespace InformacionPaises
{
    public partial class Information : PhoneApplicationPage
    {
        public Information()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string countryCode, countryCurrency, countryId;
            if (!NavigationContext.QueryString.TryGetValue("cId", out countryId) ||
                !NavigationContext.QueryString.TryGetValue("cc", out countryCode) ||
                !NavigationContext.QueryString.TryGetValue("currency", out countryCurrency)) return;

            tbMoneda.Text = countryCurrency;
            var service = Resources["airportService"] as AirportService;
            if (service == null) return;
            service.Retrieve(countryCode);
            service.CountryId = Convert.ToInt32(countryId);
            service.CountryCode = countryCode;
            service.LoadListOfAirports();
        }
    }
}