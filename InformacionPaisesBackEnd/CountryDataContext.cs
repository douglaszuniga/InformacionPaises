using System.Data.Linq;

namespace InformacionPaisesBackEnd
{
    public class CountryDataContext : DataContext
    {
        public Table<Country> Countries;
        public Table<Airport> Airports;

        public CountryDataContext(string connectionString) :base(connectionString)
        {
            
        }

        static CountryDataContext _dataContext;
        public static CountryDataContext Current
        {
            get
            {
                if (_dataContext == null)
                {
                    _dataContext = new CountryDataContext("isostore:/countryInfo.sdf");
                    if (!_dataContext.DatabaseExists())
                    {
                        _dataContext.CreateDatabase();
                    }
                }
                return _dataContext;
            }
        }
    }
}
