using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarsRover.Logic
{
    public static class FileOperator
    {
        public static List<DateTime> ReadParseDatesFromFile(string fileName)
        {
            List<DateTime> result = new List<DateTime>();
            string[] allLines = ReadAllLinesFromFile(fileName);

            foreach (var line in allLines)
            {
                if (line.TryParseAsDateTime(out DateTime date))
                {
                    result.Add(date);
                }
            }

            return result;
        }
        
        public static string[] ReadAllLinesFromFile(string fileName)
        {
            try
            {
                var allLines = File.ReadAllLines(@"wwwroot\files\" + fileName);
                return allLines;

            }
            catch (FileNotFoundException notFoundEx)
            {
                //LogInformation in DB 
                throw;
            }
            catch (DirectoryNotFoundException directoryNotFoundEx)
            {
                //LogInformation in DB 
                throw;
            }
            catch (Exception ex)
            {
                //LogInformation in DB 
                throw;
            }
        }
    }
}
