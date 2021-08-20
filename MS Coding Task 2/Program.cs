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
            // CalculateSum(@"D:\InterviewTasks\input.txt", @"D:\InterviewTasks\result.txt");
            // CalculateSum("https://interviewsupport.blob.core.windows.net/inputs/simple.txt", "https://interviewsupport.blob.core.windows.net/outputs/result.txt?sp=racwdl&st=2021-08-18T20:47:46Z&se=2021-09-02T04:47:46Z&sv=2020-08-04&sr=c&sig=R9T1YGyB8c0hz9iwNpSk7iZB8sJgU6yV8kgc4b0tlq8%3D");

            int sumOfInputFileRows;

            Console.WriteLine("Lawrence LaVerne MS coding task 2");

            // "sum up only the positive numbers, or only the even numbers":
            //sumOfInputFileRows = CalculateSum(@"D:\InterviewTasks\input.txt", @"D:\InterviewTasks\result.txt", "even");
            sumOfInputFileRows = CalculateSum("https://interviewsupport.blob.core.windows.net/inputs/simple.txt", "https://interviewsupport.blob.core.windows.net/outputs/result.txt?sp=racwdl&st=2021-08-18T20:47:46Z&se=2021-09-02T04:47:46Z&sv=2020-08-04&sr=c&sig=R9T1YGyB8c0hz9iwNpSk7iZB8sJgU6yV8kgc4b0tlq8%3D", "positive");

            Console.WriteLine("Result is " + sumOfInputFileRows);
        }

        static int CalculateSum(string fullPathToInputFile, string fullPathToOutputFile, string sumFilter)
        {
            int sumOfInputFileRows = 0;
            string[] inputFileRows;

            inputFileRows = ReadInputFile(fullPathToInputFile);

            foreach (string thisInputFileRow in inputFileRows)
            {
                int thisInputFileRowToInt;
                thisInputFileRowToInt = int.Parse(thisInputFileRow);

                switch (sumFilter)
                {
                    case "even":
                        if (thisInputFileRowToInt % 2 == 0) // no remainder, so it's even
                        {
                            sumOfInputFileRows += int.Parse(thisInputFileRow);
                        }
                        break;

                    case "positive":
                        if (thisInputFileRowToInt > 0) // greater than zero, so it's positive
                        {
                            sumOfInputFileRows += int.Parse(thisInputFileRow);
                        }
                        break;

                    default:
                        sumOfInputFileRows += int.Parse(thisInputFileRow); // no filter, so add 'em all!
                        break;
                }
            }

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

        private async void SaveStringToFile(string fullPathToOutputFile, string sumOfInputFileRows)
        {
            // Compose output message and count:
            await File.WriteAllTextAsync(fullPathToOutputFile, sumOfInputFileRows.ToString());
            Console.WriteLine("Successfully saved result to " + fullPathToOutputFile);
        }

        void SaveStringToUrl(string urlDestination, string sumOfInputFileRows)
        {
            using (var webClient = new System.Net.WebClient())
            {
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
