using System.Text;

namespace BankingFileGenerator.Lib.Generators
{
    public class Bai2Generator
    {
        /// <summary>
        /// Generates a BAI2 formatted file with the specified number of groups, accounts per group, and transactions per account.
        /// </summary>
        /// <param name="groups">Number of groups to generate.</param>
        /// <param name="accountsPerGroup">Number of accounts in each group.</param>
        /// <param name="transactionsPerAccount">Number of transactions per account.</param>
        /// <returns>BAI2 formatted file content as a string.</returns>
        public string GenerateFile(int groups, int accountsPerGroup, int transactionsPerAccount)
        {
            var sb = new StringBuilder(4096);

            // File Header
            sb.AppendLine("01,BANKID,COMPANY,220603,0100,1,80,2/");

            int transactionCounter = 1;

            for (int g = 1; g <= groups; g++)
            {
                // Group Header
                sb.AppendLine($"02,GROUP{g},220603,220603,USD,1/");

                for (int a = 1; a <= accountsPerGroup; a++)
                {
                    // Account Identifier
                    sb.AppendLine("03,0001234567,USD,010,1000,,/");

                    for (int t = 0; t < transactionsPerAccount; t++)
                    {
                        // Transaction Detail
                        sb.AppendLine($"16,195,100,{transactionCounter++},/,/");
                    }

                    // Account Trailer
                    sb.AppendLine("49,1000,3/");
                }

                // Group Trailer
                sb.AppendLine("98,3000,6/");
            }

            // File Trailer
            sb.AppendLine("99,2000,6/");

            return sb.ToString();
        }
    }
}
