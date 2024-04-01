using ShipsGoAPI.NET.Enums;

namespace ShipsGoAPI.NET.Properties
{
    public class VoyageDataResponse
    {
        public string Status { get; set; }
        public StatusId StatusId { get; set; }
        public string ReferenceNo { get; set; }
        public string BLReferenceNo { get; set; }
        public string ShippingLine { get; set; }
        public string ContainerNumber { get; set; }
        public string ContainerTEU { get; set; }
        public string ContainerType { get; set; }

        // From Port Details
        public string FromCountry { get; set; }
        public string Pol { get; set; }
        public LoadingDate? LoadingDate { get; set; }

        // Departure Details
        public DepartureDate? DepartureDate { get; set; }

        // Transshipment Ports (array of objects)
        public List<TsPort> TSPorts { get; set; }

        // Vessel Details
        public string Vessel { get; set; }
        public string VesselIMO { get; set; }
        public VesselLocation VesselLocation { get; set; } // Combined Latitude & Longitude
        public string VesselVoyage { get; set; }

        // To Port Details
        public string ToCountry { get; set; }
        public string Pod { get; set; }
        public ArrivalDate? ArrivalDate { get; set; }
        public DischargeDate? DischargeDate { get; set; }

        // Additional Details
        public DateTime? EmptyToShipperDate { get; set; }
        public DateTime? GateInDate { get; set; }
        public DateTime? GateOutDate { get; set; }
        public DateTime? EmptyReturnDate { get; set; }
        public string FormattedTransitTime { get; set; }

        /* This is a text based notice for x days early or delayed
         * For example: "4 days delay" or "3 days early"
         * SUGGESTION: Use the ArrivalData property to get the current ETA Date property, unless you only want a text response.
         */
        public string ETA { get; set; }
        public DateTime? FirstETA { get; set; }
        public int BLContainerCount { get; set; }
        public List<BlContainer> BLContainers { get; set; }
        public string LiveMapUrl { get; set; }
        public List<string> Tags { get; set; }
        public string Co2Emission { get; set; }
    }
}
