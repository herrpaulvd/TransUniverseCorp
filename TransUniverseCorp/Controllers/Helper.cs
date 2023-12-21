namespace TransUniverseCorp.Controllers
{
    internal static class Helper
    {
        public static int MakeHashCode(this string? s)
        {
            if (s is null) return 0;
            int result = 0;
            foreach (var c in s)
                result = result * 228 + c;
            return result;
        }
    }
}
