using System.Globalization;

namespace InformacionPaisesBackEnd
{
    public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }

        public static string GetCountryKey(Country country)
        {
            var key = char.ToLower(country.Name[0]);
            if (key < 'a' || key > 'z')
            {
                key = '#';
            }
            return key.ToString(CultureInfo.InvariantCulture);
        }

        public static int CompareByName(object obj1, object obj2)
        {
            var p1 = obj1 as Country;
            var p2 = obj2 as Country;

            return p1.Name.CompareTo(p2.Name);
        }
    }
}
