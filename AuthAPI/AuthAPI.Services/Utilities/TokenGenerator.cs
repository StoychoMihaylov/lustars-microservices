namespace AuthAPI.Services.Utilities
{
    using System;
    using System.Text;
    using System.Security.Cryptography;

    public static class TokenGenerator
    {
        public static string Generate(int size)
        {
            // Characters except I, l, O, 1, and 0 to decrease confusion when hand typing tokens
            var charSet = Guid.NewGuid().ToString() + "1Aa4Bd9Ca0Dy1El3F6fG9cH7hJ2aK4yL8zM6vN6jP2yQ1jR2fS1dT7sU4uV1pWq9Xq0Yz3Zq" + Guid.NewGuid().ToString();
            var chars = charSet.ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
