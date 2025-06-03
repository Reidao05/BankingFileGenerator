using System;
using System.Text;

namespace BankingFileGenerator.Lib.Generators
{
    public class Mt940Generator
    {
        /// <summary>
        /// Generates an MT940 formatted file with the specified number of transactions.
        /// </summary>
        /// <param name="transactions">Number of transactions to include. Default is 3.</param>
        /// <returns>MT940 formatted file content as a string.</returns>
        public string GenerateFile(int transactions = 3)
        {
            var sb = new StringBuilder(1024); // Pre-allocate for efficiency

            string today = DateTime.Now.ToString("yyMMdd");

            // Add MT940 headers
            sb.AppendLine(":20:TRNREF123456"); // Transaction reference
            sb.AppendLine(":25:123456789/987654321"); // Account identification
            sb.AppendLine(":28C:1/1"); // Statement number/sequence number
            sb.AppendLine($":60F:C{today}USD000000000000,00"); // Opening balance

            // Add transaction records
            for (int i = 1; i <= transactions; i++)
            {
                // :61: - Statement line (transaction)
                sb.AppendLine($":61:{today}{today}C000000000404,00NTRFNONREF{i}");
                // :86: - Information to account owner
                sb.AppendLine($":86:Transaction description {i}");
            }

            // Add MT940 trailer (closing balance)
            sb.AppendLine($":62F:C{today}USD000000001212,12");

            return sb.ToString();
        }
    }
}
