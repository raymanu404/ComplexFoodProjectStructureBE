using Application.Contracts.FileUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FileUtils
{
    public class FileReader : IFileReader
    {
        public  string ReadFile(string filename)
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
