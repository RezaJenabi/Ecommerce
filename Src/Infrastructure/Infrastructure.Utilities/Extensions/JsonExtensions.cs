using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Utilities.Extensions
{
    public static class JsonExtensions
    {
        public static TTarget DeserializeObject<TTarget>(this string source) => JsonSerializer.Deserialize<TTarget>(source, new JsonSerializerOptions
        {

            PropertyNameCaseInsensitive = true
        });

        public static string SerializeObject(this object source) => JsonSerializer.Serialize(source, new JsonSerializerOptions
        {

            PropertyNameCaseInsensitive = true
        });
    }
}
