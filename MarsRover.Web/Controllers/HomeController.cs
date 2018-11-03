using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarsRover.Models;
using MarsRover.Logic;
using MarsRover.Web.Models;

namespace MarsRover.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            ImagesBasedOnDateViewModel model = new ImagesBasedOnDateViewModel();

            List<DateTime> dates = FileOperator.ReadParseDatesFromFile("dates.txt");

            MarsAPIOperator op = new MarsAPIOperator();
            foreach (var date in dates)
            {
                model.ImageUrlsByDate[date] = new List<string>();

                var images = await op.GetStoreDailyImageAsync(date);

                foreach (var image in images)
                {
                    byte[] imageByteData = System.IO.File.ReadAllBytes(image.AbsolutePath);
                    string imageBase64Data = Convert.ToBase64String(imageByteData);
                    string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                    model.ImageUrlsByDate[date].Add(imageDataURL);
                }
            }

            return View(model);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
