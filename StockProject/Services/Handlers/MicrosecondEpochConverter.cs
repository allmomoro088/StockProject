using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockProject.Services.Handlers
{
	public class MicrosecondEpochConverter : DateTimeConverterBase
	{
		private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.Value == null) return null;
			return (_epoch + TimeSpan.FromMilliseconds((Int64)reader.Value)).ToLocalTime();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteRaw(((DateTime)value - _epoch).TotalMilliseconds + "000");
		}
	}
}
