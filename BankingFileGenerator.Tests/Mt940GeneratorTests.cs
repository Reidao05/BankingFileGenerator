using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankingFileGenerator.Lib.Generators;

namespace BankingFileGenerator.Tests
{
    [TestClass]
    public class Mt940GeneratorTests
    {
        /// <summary>
        /// Verifies that the generated MT940 file includes the required header.
        /// </summary>
        [TestMethod]
        public void GenerateFile_ShouldIncludeHeader()
        {
            var generator = new Mt940Generator();
            var result = generator.GenerateFile(1);

            // The header should contain the transaction reference
            StringAssert.Contains(result, ":20:TRNREF123456");
        }

        /// <summary>
        /// Ensures the generated MT940 file contains the correct number of transaction records.
        /// </summary>
        [TestMethod]
        public void GenerateFile_ShouldIncludeTransactions()
        {
            var generator = new Mt940Generator();
            var result = generator.GenerateFile(3);

            // Count the number of transaction lines (":61:" is the transaction marker)
            int count = result.Split(":61:").Length - 1;
            Assert.AreEqual(3, count);
        }

        /// <summary>
        /// Checks that the generated MT940 file includes the trailer section.
        /// </summary>
        [TestMethod]
        public void GenerateFile_ShouldIncludeTrailer()
        {
            var generator = new Mt940Generator();
            var result = generator.GenerateFile(1);

            // The trailer should contain the closing balance marker
            StringAssert.Contains(result, ":62F:C");
        }
    }
}
