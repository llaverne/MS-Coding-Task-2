using Microsoft.VisualStudio.TestTools.UnitTesting;
using MS_Coding_Task_2;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            string fileSource;
            string fileDestination;
            string sumFilter;
            string[] sumOfInputFileRows;
            sumOfInputFileRows = MS_Coding_Task_2.CalculateSum(fileSource, fileDestination, sumFilter);

            // Act


            // Assert
        }
    }
}
