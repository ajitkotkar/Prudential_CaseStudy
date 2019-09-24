using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeatherService.Helper;

namespace WeatherService.Controllers
{
    public class HomeController : Controller
    {
        private static IOpenWeatherMapService _openWeatherMapService;
        private static IWeatherInfoGenerator _weatherInfoGenerator;
        private static ICSVReader _csvReader;

        public HomeController(IOpenWeatherMapService openWeatherMapService, IWeatherInfoGenerator weatherInfoGenerator,ICSVReader cSVReader)
        {
            _openWeatherMapService = openWeatherMapService;
            _weatherInfoGenerator = weatherInfoGenerator;
            _csvReader = cSVReader;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> StoreWeatherInfo(HttpPostedFileBase file)
        {
            try
            {
                //OpenWeatherMapService openWeatherMapService = new OpenWeatherMapService();
                //WeatherInfoGenerator weatherInfoGenerator = new WeatherInfoGenerator();

                if (ModelState.IsValid)
                {

                    if (file != null && file.ContentLength > 0)
                    {

                        if (file.FileName.EndsWith(".csv"))
                        {
                            using (var reader = new StreamReader(file.InputStream))
                            {
                                
                                var cityIds = await _csvReader.ReadFile(reader);
                                foreach (var city in cityIds)
                                {
                                    string cityWeatherInfo = await _openWeatherMapService.GetWeatherInfoByCity(city);
                                    _weatherInfoGenerator.SaveWeatherInfoForCity(cityWeatherInfo, city);
                                }
                            }

                        }
                        else
                        {
                            ViewBag.Message = "Only CSV files accepted";
                            return View("Index");
                        }
                        ViewBag.Message = "Weather information saved successfully in Output folder of the solution";
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Please upload file in CSV format only";
                        return View("Index");

                    }
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = "File upload failed!!";
                    return View("Index");
                }
            }

            catch (Exception e)
            {
                ViewBag.Message = "File upload failed!!";
                return View("Index");
            }
        }
    }
}