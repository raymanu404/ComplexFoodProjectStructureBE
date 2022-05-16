
namespace Application.Components
{
    public static class GetDataFromFile
    {
        public static string? ReadFile(this string filename)
        {
            var encryptedResult = "";
            using (StreamReader streamReader = new StreamReader(filename))
            {
                while (!streamReader.EndOfStream)
                {
                    encryptedResult = streamReader.ReadLine();                  
                }
            }

            return encryptedResult;
        }
    }
}
