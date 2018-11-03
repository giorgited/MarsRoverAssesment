using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Web.Models
{
    public class ImagesBasedOnDateViewModel
    {
        public Dictionary<DateTime, List<string>> ImageUrlsByDate { get; set; } = new Dictionary<DateTime, List<string>>();
    }
}
