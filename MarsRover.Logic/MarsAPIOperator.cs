using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MarsRover.Logic.Models;
using Newtonsoft.Json;

namespace MarsRover.Logic
{
    public class MarsAPIOperator : IDailyImageAPIOperator
    {
        private string imagesDirectoy = Path.Combine(Path.GetTempPath(), "mars_rover_image");

        public async Task<List<Uri>> GetStoreDailyImageAsync(DateTime date)
        {
            if (date > DateTime.Now)
            {
                throw new Exception("Date can not be in future.");
            }

            if (ImageExist(date, out string[] urls)){
                Console.WriteLine($"Images for {date.ToShortDateString()} already exists..");
                return urls.Select(u => new Uri(u)).ToList();
            } else {
                var imageUri = await GetImagesForDateAsync(date);

                Console.WriteLine("Downloading the images for {0}", date.ToShortDateString());

                List<Uri> result = new List<Uri>();

                int i = 0;
                foreach (var uri in imageUri)
                {
                    var extension = uri.AbsoluteUri.Split('.').Last();
                    result.Add(DownloadImage(uri, Path.Combine(imagesDirectoy, date.ToString().GetHashString(), i.ToString() + "." + extension)));
                    i++;
                }
                return result;
            }
        }


        private Uri DownloadImage(Uri imageUri, string filePath)
        {
            WebClient client = new WebClient();
            client.DownloadFile(imageUri, filePath);

            return new Uri(filePath);
        }

        private async Task<IEnumerable<Uri>> GetImagesForDateAsync(DateTime date)
        {
            HttpClient httpClient = new HttpClient();
            var uri = $"https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date={date.Year}-{date.Month}-{date.Day}" + "&api_key=NNKOjkoul8n1CH18TWA9gwngW1s1SmjESPjNoUFo";
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            NasaAPIResult apiResult = JsonConvert.DeserializeObject<NasaAPIResult>(responseBody);
            return apiResult.Photos.Select(p => p.img_src);
        }

        private bool ImageExist(DateTime date, out string[] urls)
        {
            urls = null;

            string imageDirectoryForDate = date.ToString().GetHashString();
            var imagesDirectoryPath = Path.Combine(imagesDirectoy, imageDirectoryForDate);

            Directory.CreateDirectory(imagesDirectoy);
            Directory.CreateDirectory(imagesDirectoryPath);

            urls = Directory.GetFiles(imagesDirectoryPath);
            return urls.Count() >= 1;   
        }
    }
}
