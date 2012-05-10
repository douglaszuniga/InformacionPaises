using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace InformacionPaisesBackEnd
{
    public class AirportService : INotifyPropertyChanged
    {
        private ObservableCollection<Airport> _list;
        public ObservableCollection<Airport> List
        {
            get { return _list; }
            private set
            {
                _list = value; if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("List"));
                }
            }
        }

        private readonly PersistenceManager _persistenceManager;
        public string CountryCode { get; set; }
        public int CountryId { get; set; }

        public AirportService()
        {
            _persistenceManager = new PersistenceManager();
            List = new ObservableCollection<Airport>();
        }

        public void LoadListOfAirports()
        {
            List = new ObservableCollection<Airport>(_persistenceManager.LoadAirports(CountryId));
        }

        public void Retrieve(string countryCode)
        {
            var soapClient = new AirportWebReference.airportSoapClient();
            soapClient.getAirportInformationByISOCountryCodeCompleted += SoapClientGetAirportInformationByIsoCountryCodeCompleted;
            soapClient.getAirportInformationByISOCountryCodeAsync(countryCode);
        }

        void SoapClientGetAirportInformationByIsoCountryCodeCompleted(object sender, AirportWebReference.getAirportInformationByISOCountryCodeCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled || e.Error != null) return;
                var resultXml = e.Result;
                if (string.IsNullOrEmpty(resultXml)) return;
                //<NewDataSet> <Table> <AirportCode> <CityOrAirportName>
                var document = XDocument.Parse(resultXml);
                var query = from cityAirport in document.Descendants("Table")
                            where cityAirport != null
                            select new Airport()
                            {
                                Code = cityAirport.Descendants("AirportCode").First().Value,
                                Name = cityAirport.Descendants("CityOrAirportName").First().Value,
                                CountryID = CountryId,
                            };
                var result = query.Distinct(new AirportComparer()).ToList();
                _persistenceManager.DeleteAirportsByCountry(CountryId);
                _persistenceManager.AddListOfAirport(result);
                if (List == null || List.Count == 0)
                {
                    LoadListOfAirports();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
