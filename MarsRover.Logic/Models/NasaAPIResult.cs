using System;
namespace MarsRover.Logic.Models
{
    public class NasaAPIResult
    {
        public Photo[] Photos { get; set; }
    }

    public class Photo
    {
        public Uri ImgSrc { get; set; }
    }
}
