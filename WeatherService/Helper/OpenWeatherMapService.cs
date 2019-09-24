using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace WeatherService.Helper
{
    public class OpenWeatherMapService: IOpenWeatherMapService
    {
        public async Task<string> GetWeatherInfoByCity(string cityId)
        {
            try
            {
                
                string weatherInfo = "";
                var httpClient = GetHttpClient();
                httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["openweathermapURI"]);
               
                HttpResponseMessage response = await httpClient.GetAsync(ConfigurationManager.AppSettings["openWeatherServiceContext"]+ "id="+cityId+ "&appid="+ ConfigurationManager.AppSettings["appID"]);

                if (response.IsSuccessStatusCode)
                {
                    weatherInfo = await response.Content.ReadAsStringAsync();
                }
                return weatherInfo;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        private HttpClient GetHttpClient()
        {
            try
            {
                var client = new HttpClient();
                return client;

            }
            catch (Exception e)
            {

                throw;
            }
        }
    }

    public interface IOpenWeatherMapService
    {
        Task<string> GetWeatherInfoByCity(string cityId);
    }
}