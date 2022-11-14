using System.Security.Cryptography;
using System.Text;

namespace GuitarStarBackOffice.ServerSide.Services.Hash;

public static class HashHelper
{
    private static string BytesToString(byte[] inputBytes)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (byte b in inputBytes)
        {
            stringBuilder.Append(b.ToString("X2"));
        }

        return stringBuilder.ToString();
    }

    public static string GetHashString(string inputString)
    {
        using HashAlgorithm hashAlgorithm = SHA1.Create();
        return BytesToString(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString)));
    }

    public static string GetHashString(byte[] inputBytes)
    {
        using HashAlgorithm hashAlgorithm = SHA1.Create();
        return BytesToString(hashAlgorithm.ComputeHash(inputBytes));
    }

    public static IEnumerable<string> GetHashStrings(IEnumerable<byte[]> inputBytes)
    {
        using HashAlgorithm algorithm = SHA1.Create();
        foreach (byte[] inputByte in inputBytes)
        {
            yield return BytesToString(algorithm.ComputeHash(inputByte));
        }
    }
}