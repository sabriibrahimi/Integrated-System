using System.Security.Cryptography;
using System.Text;

namespace Domain.ExternalModels;

public class GuidHelper
{
    public static Guid FromLegacyId(string entityType, string legacyId)
    {
        var input = $"{entityType}:{legacyId}";
        var hash = MD5.HashData(Encoding.UTF8.GetBytes(input));
        return new Guid(hash);
    }
    
    public static Guid FromLegacyId(string entityType, int legacyId)
    {
        return FromLegacyId(entityType, legacyId.ToString());
    }
}