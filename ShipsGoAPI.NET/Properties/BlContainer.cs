namespace ShipsGoAPI.NET.Properties
{
    public class BlContainer
    {
        public string ContainerCode { get; set; }
        public string ContainerTEU { get; set; }
        public string ContainerType { get; set; }
        public string LiveMapUrl { get; set; }
        public DateTime BLEmptyToShipperDate { get; set; }
        public DateTime BLGateInDate { get; set; }
        public DateTime BLDateOutDate { get; set; }
        public DateTime BLEmptyReturnDate { get; set; }
    }
}
