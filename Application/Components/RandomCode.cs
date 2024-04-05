
namespace Application.Components
{
    public static class RandomCode
    {
        public static string GetRandomCode(int length)
        {
            var ran = new Random();

            var b = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string random = "";

            for (var i = 0; i < length; i++)
            {
                var a = ran.Next(b.Length);
                random = random + b.ElementAt(a);
            }

            return random;
        }
    }
}
