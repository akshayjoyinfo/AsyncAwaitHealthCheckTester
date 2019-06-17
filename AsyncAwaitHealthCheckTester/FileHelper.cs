using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncAwaitHealthCheckTester
{
    public class FileHelper
    {
        public async Task<List<string>> GetUrlsFromFilesAsync()
        {
            var urls = new List<string>();
            using (var reader = File.OpenText("Urls.txt"))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    urls.Add(line);
                }
            }

            return urls;
        }
    }
}
