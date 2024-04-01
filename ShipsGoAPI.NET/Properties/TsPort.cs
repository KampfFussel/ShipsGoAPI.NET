namespace ShipsGoAPI.NET.Properties
{
    public class TsPort
    {
        public string Port { get; set; }
        public ArrivalDate? ArrivalDate { get; set; }
        public DepartureDate? DepartureDate { get; set; }
        public string Vessel { get; set; }
        public string VesselIMO { get; set; }
        public string VesselLatitude { get; set; } // If it says "Not Supported," it means you need to add "mapPoint=true" at the end of your GET request URL
        public string VesselLongitude { get; set; } // If it says "Not Supported," it means you need to add "mapPoint=true" at the end of your GET request URL
        public string VesselVoyage { get; set; }
    }
}
