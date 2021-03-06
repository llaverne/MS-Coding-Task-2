using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MS_Coding_Task_2;


namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [DataRow(@"D:\InterviewTasks\input.txt", @"D:\InterviewTasks\result.txt", (num => num > 0), 10)]
        //[DataRow(@"D:\InterviewTasks\input.txt", @"D:\InterviewTasks\result.txt", "even", 4)]
        //[DataRow(@"D:\InterviewTasks\input.txt", @"D:\InterviewTasks\result.txt", null, 7)]

        //[DataRow("https://interviewsupport.blob.core.windows.net/inputs/simple.txt", "https://interviewsupport.blob.core.windows.net/outputs/result.txt?sp=racwdl&st=2021-08-18T20:47:46Z&se=2021-09-02T04:47:46Z&sv=2020-08-04&sr=c&sig=R9T1YGyB8c0hz9iwNpSk7iZB8sJgU6yV8kgc4b0tlq8%3D", "positive", 10)]
        //[DataRow("https://interviewsupport.blob.core.windows.net/inputs/simple.txt", "https://interviewsupport.blob.core.windows.net/outputs/result.txt?sp=racwdl&st=2021-08-18T20:47:46Z&se=2021-09-02T04:47:46Z&sv=2020-08-04&sr=c&sig=R9T1YGyB8c0hz9iwNpSk7iZB8sJgU6yV8kgc4b0tlq8%3D", "even", 4)]
        //[DataRow("https://interviewsupport.blob.core.windows.net/inputs/simple.txt", "https://interviewsupport.blob.core.windows.net/outputs/result.txt?sp=racwdl&st=2021-08-18T20:47:46Z&se=2021-09-02T04:47:46Z&sv=2020-08-04&sr=c&sig=R9T1YGyB8c0hz9iwNpSk7iZB8sJgU6yV8kgc4b0tlq8%3D", null, 7)]

        [DataTestMethod]
        public void TestSumMethod(string inputPath, string outputPath, Func<int, bool> sumFilter, int result)
        {
            CalculateSum calculateSum = new();
            int actual = calculateSum.SumInputRows(inputPath, outputPath, sumFilter);

            Assert.AreEqual(result, actual);

        }
    }
        
}

