using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MarsRover.Logic.Models;
using Newtonsoft.Json;

namespace MarsRover.Logic
{
    public class MarsAPIOperator
    {
        private string imagesDirectoy = System.IO.Path.GetTempPath() + @"\mars_rover_image";

        public async Task<List<Uri>> GetStoreDailyImageAsync(DateTime date)
        {
            if (ImageExist(date, out string[] urls)){
                Console.WriteLine($"Image for {date.ToShortDateString()} already exists..");
                return urls.Select(u => new Uri(u)).ToList();
            } else {
                Console.WriteLine("Getting the image uri for {0}", date.ToShortDateString());
                var imageUri = await GetImagesForDateAsync(date);

                Console.WriteLine("Downloading the image for {0}", date.ToShortDateString());

                List<Uri> result = new List<Uri>();
                foreach (var uri in imageUri)
                {
                    result.Add(DownloadImage(uri, date.ToString().GetHashString()));
                }
                return result;
            }
        }


        private Uri DownloadImage(Uri imageUri, string fileName)
        {
            WebClient client = new WebClient();
            var imageLocalUri = imagesDirectoy + @"\" + fileName;
            client.DownloadFile(imageUri, imageLocalUri);

            return new Uri(imageLocalUri);
        }

        private async Task<IEnumerable<Uri>> GetImagesForDateAsync(DateTime date)
        {
            HttpClient httpClient = new HttpClient();
            var uri = $"https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date={date.Year}-{date.Month}-{date.Day}" + "&api_key=NNKOjkoul8n1CH18TWA9gwngW1s1SmjESPjNoUFo";
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            List<NasaAPIResult> apiResult = JsonConvert.DeserializeObject<List<NasaAPIResult>>(responseBody);
            return apiResult.FirstOrDefault().Photos.Select(p => p.ImgSrc);
        }

        private bool ImageExist(DateTime date, out string[] urls)
        {
            urls = null;
            System.IO.Directory.CreateDirectory(imagesDirectoy);

            string imageDirectoryForDate = date.ToString().GetHashString();
            if (System.IO.Directory.Exists(imagesDirectoy + @"\" + imageDirectoryForDate))
            {
                urls = System.IO.Directory.GetFiles(imagesDirectoy + @"\" + imageDirectoryForDate);
                return urls.Count() >= 1;                                   
            }
            return false;
        }
    }
}
