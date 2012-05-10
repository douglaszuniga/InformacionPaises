using System.Collections.Generic;
using System.Linq;

namespace InformacionPaisesBackEnd
{
    public class PersistenceManager
    {
        public void AddCountry(Country country)
        {
            CountryDataContext.Current.Countries.InsertOnSubmit(country);
            CountryDataContext.Current.SubmitChanges();
        }

        public void AddListOfCountry(List<Country> countries)
        {
            CountryDataContext.Current.Countries.InsertAllOnSubmit(countries);
            CountryDataContext.Current.SubmitChanges();
        }

        public List<Country> LoadContries()
        {
            var query = from country in CountryDataContext.Current.Countries
                        orderby country.ID
                        select country;

            return query.ToList();
        }

        public void DeleteCountries()
        {
            CountryDataContext.Current.Countries.DeleteAllOnSubmit(LoadContries());
            CountryDataContext.Current.SubmitChanges();
        }

        public void AddListOfAirport(List<Airport> airports)
        {
            CountryDataContext.Current.Airports.InsertAllOnSubmit(airports);
            CountryDataContext.Current.SubmitChanges();
        }

        public List<Airport> LoadAirports(int countryId)
        {
            var query = from airport in CountryDataContext.Current.Airports
                        where airport.CountryID == countryId
                        orderby airport.ID
                        select airport;

            return query.ToList();
        }

        public void DeleteAirportsByCountry(int countryID)
        {
            CountryDataContext.Current.Airports.DeleteAllOnSubmit(LoadAirports(countryID));
            CountryDataContext.Current.SubmitChanges();
        }
    }
}
