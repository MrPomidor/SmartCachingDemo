using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Reusables.Utils
{
    public class SystemTextJsonSerializer : ISerializer
    {
        private static readonly JsonSerializerOptions _serializerOptions;
        static SystemTextJsonSerializer()
        {
            var options = new JsonSerializerOptions();
            options.AddContext<SystemTextJsonSerializationContext>();
            _serializerOptions = options;
        }

        public T Deserialize<T>([NotNull] string serialized)
        {
            return JsonSerializer.Deserialize<T>(serialized, _serializerOptions);
        }

        public string Serialize<T>([NotNull] T item)
        {
            return JsonSerializer.Serialize(item, _serializerOptions);
        }
    }
}
