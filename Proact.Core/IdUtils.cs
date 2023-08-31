using System.Reflection;
using System.Security.Cryptography;

namespace Proact.Core;

public static class IdUtils
{
    public static string CreateId(MethodInfo methodInfo)
    {
        var ilCode = methodInfo.GetMethodBody().GetILAsByteArray();
        var hashBytes = MD5.HashData(ilCode);
        return Convert.ToBase64String(hashBytes, 0, 6);
    } 
}