using System.Text.Json;
using System.Text.Json.Serialization;

namespace Avalanche.Core.Application.Helpers
{
    public class NullableDateOnlyJsonConverter : JsonConverter<DateOnly?>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Si es null JSON → devolver null
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            // Si es string → procesar
            if (reader.TokenType == JsonTokenType.String)
            {
                string str = reader.GetString();
                if (string.IsNullOrWhiteSpace(str))
                    return null; // << AQUÍ está la clave: "" o " " => null

                if (DateOnly.TryParse(str, out var date))
                    return date;

                throw new JsonException($"Invalid DateOnly format: '{str}'. Expected format: {Format}");
            }

            throw new JsonException($"Unexpected token parsing DateOnly. Token: {reader.TokenType}");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString(Format));
            else
                writer.WriteNullValue();
        }
    }
}
