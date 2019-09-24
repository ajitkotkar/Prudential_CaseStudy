using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WeatherService.Helper
{
    public class CSVReader: ICSVReader
    {
        public async Task<List<string>> ReadFile(StreamReader streamReader)
        {
            try
            {
                List<string> cityIds = new List<string>();
                // skip first line i.e columnheader
                streamReader.ReadLine();
                while (!streamReader.EndOfStream)
                {
                    var line = await streamReader.ReadLineAsync();
                    cityIds.Add(line);
                }
                return cityIds;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
    public interface ICSVReader
    {
        Task<List<string>> ReadFile(StreamReader streamReader);
    }
}