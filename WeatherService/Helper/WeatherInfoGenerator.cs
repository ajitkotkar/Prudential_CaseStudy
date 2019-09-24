using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WeatherService.Helper
{
    public class WeatherInfoGenerator: IWeatherInfoGenerator
    {
        public void SaveWeatherInfoForCity(string content,string fileName)
        {
            try
            {
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Output/"+DateTime.Now.ToString("dd MMMM yyyy"));
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                File.WriteAllText(filePath + "\\" + fileName+".Json", content);
            }
            catch (Exception e)
            {

                throw;
            }
           
        }
    }
    public interface IWeatherInfoGenerator
    {
        void SaveWeatherInfoForCity(string content, string fileName);
    }
}