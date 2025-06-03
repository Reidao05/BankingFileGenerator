using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankingFileGenerator.Lib.Generators;

namespace BankingFileGenerator.Tests
{
    [TestClass]
    public class FixedWidthBai2GeneratorTests
    {
        /// <summary>
        /// Verifies that the generated file starts with a header record of type '1'.
        /// </summary>
        [TestMethod]
        public void GenerateFile_ShouldStartWithHeaderRecordType1()
        {
            var generator = new FixedWidthBai2Generator();
            var content = generator.GenerateFile(1, 1, 1);

            // The first character should be '1' (header record)
            StringAssert.StartsWith(content, "1");
        }

        /// <summary>
        /// Ensures the generated file contains all expected record types: payment (2), invoice (3), and trailer (9).
        /// </summary>
        [TestMethod]
        public void GenerateFile_ShouldContainExpectedRecordTypes()
        {
            var generator = new FixedWidthBai2Generator();
            var content = generator.GenerateFile(1, 1, 2); // 1 batch, 1 payment, 2 invoices

            // Check for presence of each record type at the start of a line
            Assert.IsTrue(content.Contains("\n2"), "Missing payment record (2)");
            Assert.IsTrue(content.Contains("\n3"), "Missing invoice record (3)");
            Assert.IsTrue(content.Contains("\n9"), "Missing trailer record (9)");
        }

        /// <summary>
        /// Validates that the generated file contains the correct number of lines based on input parameters.
        /// </summary>
        [TestMethod]
        public void GenerateFile_ShouldProduceCorrectNumberOfLines()
        {
            var generator = new FixedWidthBai2Generator();

            int batches = 2, payments = 2, invoices = 2;
            var content = generator.GenerateFile(batches, payments, invoices);

            // Split content into lines, ignoring empty lines
            var lines = content.Split('\n', System.StringSplitOptions.RemoveEmptyEntries);

            // Calculation:
            // 1 header line
            // For each batch: payments * (1 payment line + N invoice lines)
            // 1 trailer line
            int expected = 1 +                                 // Header (1)
                           batches * payments * (1 + invoices) + // Each payment (2) and its invoices (3)
                           1;                                  // Trailer (9)

            Assert.AreEqual(expected, lines.Length);
        }
    }
}
