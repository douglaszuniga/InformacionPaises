using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using InformacionPaisesBackEnd.CountryWebReference;

namespace InformacionPaisesBackEnd
{
    public class CountryService : INotifyPropertyChanged
    {
        private static readonly string Groups = "#abcdefghijklmnopqrstuvwxyz";

        private ObservableCollection<CountryInGroup> _countries;
        public ObservableCollection<CountryInGroup> Countries
        {
            get { return _countries; }
            private set
            {
                _countries = value; if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Countries"));
                }
            }
        }

        private readonly PersistenceManager _persistenceManager;

        public CountryService()
        {
            _persistenceManager = new PersistenceManager();
            Countries = new ObservableCollection<CountryInGroup>();
            LoadCollectionWithGroups(_persistenceManager.LoadContries());

            Retrieve();
        }

        private void LoadCollectionWithGroups(List<Country> listOfCountries)
        {
            listOfCountries.Sort(Country.CompareByName);

            var groups = new Dictionary<string, CountryInGroup>();

            foreach (char c in Groups)
            {
                var group = new CountryInGroup(c.ToString(CultureInfo.InvariantCulture));
                Countries.Add(group);
                groups[c.ToString(CultureInfo.InvariantCulture)] = group;
            }

            foreach (var item in listOfCountries)
            {
                groups[Country.GetCountryKey(item)].Add(item);
            }
        }

        public void Retrieve()
        {
            var soapClient = new countrySoapClient();
            soapClient.GetCurrencyCodeCompleted += SoapClientGetCurrencyCodeCompleted;
            soapClient.GetCurrencyCodeAsync();
        }

        void SoapClientGetCurrencyCodeCompleted(object sender, GetCurrencyCodeCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled || e.Error != null) return;
                var resultXml = e.Result;
                if (string.IsNullOrEmpty(resultXml)) return;
                //<NewDataSet><Table><Name><CountryCode><Currency>
                var document = XDocument.Parse(resultXml);
                var query = from country in document.Descendants("Table")
                            select new Country
                            {
                                Code = country.Descendants("CountryCode").First().Value,
                                Name = country.Descendants("Name").First().Value,
                                Currency = country.Descendants("Currency").First().Value
                            };
                var result = query.ToList();
                _persistenceManager.DeleteCountries();
                _persistenceManager.AddListOfCountry(result);
                if (_countries == null || !_countries[1].HasItems)
                {
                    LoadCollectionWithGroups(result);
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
