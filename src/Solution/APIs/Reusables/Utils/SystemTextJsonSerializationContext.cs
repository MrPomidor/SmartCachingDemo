using System.Text.Json.Serialization;
using Reusables.Storage.Entities;

namespace Reusables.Utils
{
    [JsonSerializable(typeof(Product))]
    public partial class SystemTextJsonSerializationContext : JsonSerializerContext
    {
    }
}
