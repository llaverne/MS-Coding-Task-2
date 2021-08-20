using System;
using System.IO;

// enable the caller (you can assume that the caller/user is the Main method) to specify the filtering logic for the summation code. For example, user may want to sum up only the positive numbers, or only the even numbers.
// write couple of unit tests for the summation logic. Since there is no “unit test project” equivalent to Visual Studio in LinqPad, this can be written simply as methods that get called from Main().
// expand the code to support inputs/result residing somewhere else (URL, local file, database etc.). For example, if user specifies input and output paths that start with “http”, then get read/write data from the URL, but if not assume those strings are file paths. Assume that both input and output must be on the same medium (either both are URLs or both are files).
// What I am looking for here is a “IDataProvider” interface and logic to select one of the two implementations based on the input/output paths. Pass the selected implementation to the summation method and have the summation method read/write the data.
// write MockDataProvider class and use it in the unit test


// Don't prefix vars, and be more descriptive with names, use camelCasing for locals.

namespace MS_Coding_Task_2
{
    interface IDataProvider
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lawrence LaVerne MS coding task 2");

            // Example execution using lambda::
            //CalculateSum calculateSum = new();
            //calculateSum.SumInputRows(@"D:\InterviewTasks\input.txt", @"D:\InterviewTasks\result.txt", (num => num > 0));
        }
    }

    public class CalculateSum
    {

        public int SumInputRows(string fullPathToInputFile, string fullPathToOutputFile, Func<int, bool> sumFilter)
        {
            int sumOfInputFileRows = 0;
            string[] inputFileRows;
            inputFileRows = ReadInputFile(fullPathToInputFile);

            foreach (string thisInputFileRow in inputFileRows)
            {
                int thisInputFileRowToInt;
                thisInputFileRowToInt = int.Parse(thisInputFileRow);
                
                // Sum, applying filter criterion (lambda expression):
                if (sumFilter(thisInputFileRowToInt))
                {
                    sumOfInputFileRows += thisInputFileRowToInt;
                }
            }

            Console.WriteLine("Sum of " + sumFilter + " numbers is " + sumOfInputFileRows);

            // Save result:
            SaveResult(fullPathToOutputFile, sumOfInputFileRows.ToString());

            return sumOfInputFileRows;
        }

        static string[] ReadInputFile(string fullPathToInputFile)
        {
            string[] inputFileRowsRead;

            if (fullPathToInputFile.StartsWith("http", StringComparison.CurrentCultureIgnoreCase)) // ignore case
            {
                // Read from a URI:
                Console.WriteLine("Reading URI...");
                string inputURLRowsRead = ReadStringFromUrl(fullPathToInputFile);
                inputFileRowsRead = inputURLRowsRead.Split("\r\n");
                Console.WriteLine("Received data successfully.");
            }
            else
            {
                // Read from a file:
                Console.WriteLine("Reading from file...");
                inputFileRowsRead = File.ReadAllLines(fullPathToInputFile);
                Console.WriteLine("Read file successfully.");
            }

            return inputFileRowsRead;
        }

        static void SaveResult(string fullPathToOutputFile, string sumOfInputFileRows)
        {
            if (fullPathToOutputFile.StartsWith("http", StringComparison.CurrentCultureIgnoreCase)) // ignore case
            {
                // Save to a URI:
                SaveStringToUri(fullPathToOutputFile, sumOfInputFileRows);
            }
            else
            {
                // Save to a file:
                SaveStringToFile(fullPathToOutputFile, sumOfInputFileRows);
            }
        }

        static async void SaveStringToFile(string fullPathToOutputFile, string sumOfInputFileRows)
        {
            Console.WriteLine("Saving to file...");

            // Compose output message and count:
            await File.WriteAllTextAsync(fullPathToOutputFile, sumOfInputFileRows.ToString());
            Console.WriteLine("Successfully saved result to " + fullPathToOutputFile);
        }

        static void SaveStringToUri(string urlDestination, string sumOfInputFileRows)
        {
            using (var webClient = new System.Net.WebClient())
            {
                Console.WriteLine("Saving to URI...");
                webClient.Headers.Add("x-ms-blob-type", "BlockBlob");
                webClient.UploadString(urlDestination, "PUT", sumOfInputFileRows);
                Console.WriteLine("Successfully saved result to " + urlDestination);
            }
        }

        static string ReadStringFromUrl(string url)
        {
            using (var webClient = new System.Net.WebClient())
            {
                return webClient.DownloadString(url);
            }
        }
    }

}
