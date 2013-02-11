using System;
using System.Net;
using System.Runtime.Serialization.Json;

namespace GoogleMaps.Geocode {
	public class ReverseGeocode {

		private Location location;
		private bool hasSensor;
		private string defaultLanguage = "EN";

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
		/// <summary>
		/// Contains the results of the Async-Query after successful retrieval
		/// </summary>
		public ReverseGeocodeResult[] Results {
			private set;
			get;
		}

		public event EventHandler Updated;

		public void QueryAsync() {
			try {
				QueryAsync(location.lat, location.lng, defaultLanguage);
			} catch (Exception) {
				throw new InvalidOperationException("Location is not set.");
			}
		}
		public void QueryAsync(string lat, string lng) {
			QueryAsync(lat, lng, defaultLanguage);
		}
		public void QueryAsync(string lat, string lng, string language) {
			var requestUri =
				new Uri("http://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat + "," + lng +
				        "&sensor=" + hasSensor.ToString().ToLower());
			try {
				var wr = WebRequest.CreateHttp(requestUri);
				wr.Headers["Accept-Language"] = language;
				wr.BeginGetResponse(WebRequestReady, wr);
			} catch (WebException) {}
		}
		private void WebRequestReady(IAsyncResult asyncResult) {
			try {
				var request = (HttpWebRequest)asyncResult.AsyncState;
				var response = request.EndGetResponse(asyncResult);
				var serializer = new DataContractJsonSerializer(typeof (ReverseGeocodeResults));
				var results = (ReverseGeocodeResults)serializer.ReadObject(response.GetResponseStream());
				Results = results.results;
				if (null != Updated) {
					Updated.Invoke(this, null);
				}
			} catch (WebException) { }
		}
	}
}
