using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace InformacionPaisesBackEnd
{
    public class AirportService
    {
        public IEnumerable<Airport> List { get; private set; }

        public void Retrieve(string countryName)
        {
            var soapClient = new AirportWebReference.airportSoapClient();
            soapClient.GetAirportInformationByCountryCompleted += SoapClientGetAirportInformationByCountryCompleted;
            soapClient.GetAirportInformationByCountryAsync(countryName);
        }

        void SoapClientGetAirportInformationByCountryCompleted(object sender, AirportWebReference.GetAirportInformationByCountryCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled || e.Error == null) return;
                var resultXml = e.Result;
                if (string.IsNullOrEmpty(resultXml)) return;
                //<NewDataSet> <Table> <AirportCode> <CityOrAirportName>
                var document = XDocument.Parse(resultXml);
                var query = from cityAirport in document.Descendants("Table")
                            select new Airport()
                            {
                                Code = cityAirport.Descendants("AirportCode").First().Value,
                                Name = cityAirport.Descendants("CityOrAirportName").First().Value,
                            };
                List = query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
