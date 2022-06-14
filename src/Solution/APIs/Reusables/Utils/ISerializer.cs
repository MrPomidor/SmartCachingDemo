using System.Diagnostics.CodeAnalysis;

namespace Reusables.Utils
{
    public interface ISerializer
    {
        string Serialize<T>([NotNull] T item);
        T Deserialize<T>([NotNull] string serialized);
    }
}
