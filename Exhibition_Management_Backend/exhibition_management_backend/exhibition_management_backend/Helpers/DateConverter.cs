using System.Text.Json;
using System.Text.Json.Serialization;

namespace exhibition_management_backend.Helpers
{
    public class DateConverter : JsonConverter<DateOnly>
    {
        private const string DateFormat = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Console.WriteLine($"Reading date: {reader.GetString()}");
            var value = reader.GetString();
            return DateOnly.ParseExact(value, DateFormat);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            Console.WriteLine($"Writing date: {value}");
            writer.WriteStringValue(value.ToString(DateFormat));
        }
    }
}
