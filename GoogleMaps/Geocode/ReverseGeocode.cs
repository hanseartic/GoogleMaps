using System;
using System.Net;
using System.Runtime.Serialization.Json;

namespace GoogleMaps.Geocode {
	public class ReverseGeocode {

		private Location location;
		private bool hasSensor;
		public ReverseGeocode() {
			
		}
		public ReverseGeocode(Location location) {
			this.location = location;
		}
		public ReverseGeocode(bool hasSensor) {
			this.hasSensor = hasSensor;
		}
		public ReverseGeocode(Location location, bool hasSensor) {
			this.location = location;
			this.hasSensor = hasSensor;
		}

		public event EventHandler Updated;

		public void QueryAsync() {
			try {
				QueryAsync(location);
			} catch (Exception) {
				throw new InvalidOperationException("Location is not set.");
			}
		}
		public void QueryAsync(Location location) {
			var requestUri =
				new Uri("http://maps.googleapis.com/maps/api/geocode/json?latlng=" + location.lat + "," + location.lng +
				        "&sensor=" + hasSensor.ToString().ToLower());
			var wr = WebRequest.CreateHttp(requestUri);
			wr.Headers["Accept-Language"] = "DE";
			wr.BeginGetResponse(WebRequestReady, wr);
		}

		private void WebRequestReady(IAsyncResult asyncResult) {
			var request = (HttpWebRequest)asyncResult.AsyncState;
			var response = request.EndGetResponse(asyncResult);
			var serializer = new DataContractJsonSerializer(typeof(ReverseGeocodeResults));
			var objects = (ReverseGeocodeResults)serializer.ReadObject(response.GetResponseStream());
			if (null != Updated) {
				Updated.Invoke(objects, null);
			}
		}
	}
}
