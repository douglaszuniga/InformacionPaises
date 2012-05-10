using System.Collections.Generic;

namespace InformacionPaisesBackEnd
{
    public class CountryInGroup : List<Country>
    {
        public CountryInGroup(string category)
        {
            Key = category;
        }

        public string Key { get; set; }

        public bool HasItems { get { return Count > 0; } }
    }
}
