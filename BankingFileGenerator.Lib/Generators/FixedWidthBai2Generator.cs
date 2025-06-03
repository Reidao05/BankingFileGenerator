using System;
using System.Text;

namespace BankingFileGenerator.Lib.Generators
{
    public class FixedWidthBai2Generator
    {
        private int _paymentCounter = 1;

        private string Pad(string value, int length, bool leftAlign = true, char padChar = ' ')
        {
            value = value ?? "";
            return leftAlign ? value.PadRight(length, padChar) : value.PadLeft(length, padChar);
        }

        private string FormatAmount(decimal value)
        {
            return Pad(((int)(value * 100)).ToString(), 10, false, '0');
        }

        public string GenerateFile(int batches = 1, int paymentsPerBatch = 2, int invoicesPerPayment = 1)
        {
            var sb = new StringBuilder();
            string today = DateTime.Now.ToString("yyMMdd");

            // File Header (Record Type 1)
            sb.AppendLine(Pad("1", 1) + Pad("", 6) + Pad("1234567", 7, false, '0') + Pad(today, 6));

            int batchNum = 100;
            Random rand = new Random();

            for (int b = 0; b < batches; b++)
            {
                for (int p = 0; p < paymentsPerBatch; p++)
                {
                    decimal amount = Math.Round((decimal)(rand.Next(10000, 99999)) / 100, 2);
                    string name = Pad($"Customer {_paymentCounter}", 30);
                    string custId = Pad($"CUST{rand.Next(1000000, 9999999)}", 30);
                    string traceNum = Pad(rand.Next(100000000, 999999999).ToString(), 9, false, '0');
                    string checkNum = Pad(rand.Next(1000000000, 1999999999).ToString(), 10, false, '0');
                    string code = Pad("ABCD123", 8);

                    // Record Type 2: Payment
                    sb.AppendLine(
                        Pad("2", 1) +
                        Pad(batchNum.ToString(), 3, false, '0') +
                        Pad((p + 1).ToString(), 3, false, '0') +
                        FormatAmount(amount) +
                        traceNum +
                        checkNum +
                        code +
                        Pad("", 43) +
                        today +
                        name +
                        custId
                    );

                    // Record Type 3: Invoices
                    for (int i = 0; i < invoicesPerPayment; i++)
                    {
                        decimal invoiceAmt = Math.Round(amount / invoicesPerPayment, 2);
                        string invoiceNum = $"INV{rand.Next(1000000, 9999999)}";

                        sb.AppendLine(
                            Pad("3", 1) +
                            Pad(batchNum.ToString(), 3, false, '0') +
                            Pad((p + 1).ToString(), 3, false, '0') +
                            Pad(i == invoicesPerPayment - 1 ? "Y" : "N", 1) +
                            Pad(invoiceNum, 11) +
                            FormatAmount(invoiceAmt) +
                            Pad("", 3)
                        );
                    }

                    _paymentCounter++;
                }

                batchNum++;
            }

            // Record Type 9: File Trailer
            sb.AppendLine(
                Pad("9", 1) +
                Pad("", 19) +
                Pad((_paymentCounter - 1).ToString(), 3, false, '0') +
                FormatAmount((_paymentCounter - 1) * 123.45M) // mock total
            );

            return sb.ToString();
        }
    }
}
