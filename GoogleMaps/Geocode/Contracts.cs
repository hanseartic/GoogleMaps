using System;
using System.Net;
using System.Runtime.Serialization;

namespace GoogleMaps.Geocode {

	[DataContract]
	public class ReverseGeocodeResults {
		[DataMember]
		public ReverseGeocodeResult[] results { get; set; }
	}

	[DataContract]
	public class ReverseGeocodeResult {
		[DataMember]
		public AddressComponent[] address_components { get; set; }

		[DataMember]
		public string formatted_address { get; set; }

		[DataMember]
		public GeocodeGeometry geometry { get; set; }

		[DataMember]
		public string[] types { get; set; }
	}
	[DataContract]
	public struct AddressComponent {
		[DataMember]
		public string long_name { get; set; }

		[DataMember]
		public string short_name { get; set; }

		[DataMember]
		public string[] types { get; set; }
	}
	[DataContract]
	public struct GeocodeGeometry {
		[DataMember]
		public Location location { get; set; }

		[DataMember]
		public string location_type { get; set; }

		[DataMember]
		public Viewport viewport { get; set; }

		[DataMember]
		public Viewport bounds { get; set; }
	}
	[DataContract]
	public struct Location {
		[DataMember]
		public string lat { get; set; }
		[DataMember]
		public string lng { get; set; }
	}
	[DataContract]
	public struct Viewport {
		[DataMember]
		public Location northeast { get; set; }

		[DataMember]
		public Location southwest { get; set; }
	}
}
