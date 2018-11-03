
using Microsoft.AspNetCore.Mvc;
using MarsRover.Logic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarsRover.Controllers
{
    public class GenerateImages : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Uri> result = new List<Uri>();
            List<DateTime> dates = new List<DateTime>()
            {
                new DateTime(2017, 02, 27),
                new DateTime(2018, 06, 02),
                new DateTime(2016, 07, 13),
                //new DateTime(2018, 04, 31),
            };

            MarsAPIOperator op = new MarsAPIOperator();
            foreach (var date in dates)
            {
                result.AddRange(await op.GetStoreDailyImageAsync(date));
            }

            return Ok(result);
        }
    }
}