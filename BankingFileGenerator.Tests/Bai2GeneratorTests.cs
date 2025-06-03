using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankingFileGenerator.Lib.Generators;

namespace BankingFileGenerator.Tests
{
    [TestClass]
    public class Bai2GeneratorTests
    {
        /// <summary>
        /// Verifies that the generated file starts with the correct BAI2 file header.
        /// </summary>
        [TestMethod]
        public void GenerateFile_ShouldContainFileHeader()
        {
            var generator = new Bai2Generator();
            var result = generator.GenerateFile(1, 1, 1);

            StringAssert.StartsWith(result, "01,BANKID,COMPANY");
        }

        /// <summary>
        /// Ensures the generated file contains the expected number of lines based on input parameters.
        /// </summary>
        [TestMethod]
        public void GenerateFile_ShouldContainCorrectNumberOfLines()
        {
            var generator = new Bai2Generator();
            int groups = 1, accounts = 2, transactions = 2;

            var result = generator.GenerateFile(groups, accounts, transactions);
            var lines = result.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            // Calculate expected lines:
            // 1 File Header
            // For each group:
            //   1 Group Header
            //   For each account:
            //     1 Account Identifier
            //     N Transaction Details
            //     1 Account Trailer
            //   1 Group Trailer
            // 1 File Trailer
            int expectedLines =
                1 + // File Header
                groups * (
                    1 + // Group Header
                    accounts * (1 + transactions + 1) + // Account Identifier + Transactions + Account Trailer
                    1 // Group Trailer
                ) +
                1; // File Trailer

            Assert.AreEqual(expectedLines, lines.Length);
        }

        /// <summary>
        /// Checks that the generated file ends with the correct BAI2 file trailer.
        /// </summary>
        [TestMethod]
        public void GenerateFile_ShouldEndWithFileTrailer()
        {
            var generator = new Bai2Generator();
            var result = generator.GenerateFile(1, 1, 1);

            StringAssert.Contains(result.TrimEnd(), "99,2000,6/");
        }
    }
}
