using System.Data.Linq.Mapping;

namespace InformacionPaisesBackEnd
{
    [Table]
    public class Airport
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int ID { get; set; }
        [Column]
        public string Code { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public int CountryID { get; set; }
    }
}
//<NewDataSet> <Table> <AirportCode>LSL</AirportCode> <CityOrAirportName>LOS CHILES</CityOrAirportName> <Country>Costa Rica</Country> <CountryAbbrviation>CR</CountryAbbrviation> <CountryCode>110</CountryCode> <GMTOffset>6</GMTOffset> <RunwayLengthFeet>4265</RunwayLengthFeet> <RunwayElevationFeet>115</RunwayElevationFeet> <LatitudeDegree>11</LatitudeDegree> <LatitudeMinute>0</LatitudeMinute> <LatitudeSecond>0</LatitudeSecond> <LatitudeNpeerS>N</LatitudeNpeerS> <LongitudeDegree>84</LongitudeDegree> <LongitudeMinute>50</LongitudeMinute> <LongitudeSeconds>0</LongitudeSeconds> <LongitudeEperW>W</LongitudeEperW> </Table>