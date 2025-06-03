using System.CommandLine;
using BankingFileGenerator.Lib.Generators;
using BankingFileGenerator.Lib.Utils;
using System.Threading.Tasks;

// Entry point for the Banking File Generator CLI application

// Create the root command with a description
var rootCommand = new RootCommand("Banking File Generator CLI");

// --- BAI2 File Generation Command ---

// Define options for the BAI2 file generation command
var groupsOption = new Option<int>("--groups", () => 1, "Number of groups");
var accountsOption = new Option<int>("--accounts", () => 1, "Accounts per group");
var transactionsOption = new Option<int>("--transactions", () => 1, "Transactions per account");

// Create the 'generate' command for BAI2 files and add options
var generateCommand = new Command("generate", "Generate a BAI2 file")
{
    groupsOption,
    accountsOption,
    transactionsOption
};

// Assign an async handler to the 'generate' command
generateCommand.SetHandler(
    async (int groups, int accounts, int transactions) =>
    {
        // Generate BAI2 file content
        var generator = new Bai2Generator();
        var content = generator.GenerateFile(groups, accounts, transactions);

        // Save the generated content to a file and output the file path
        var filePath = FileWriter.SaveFile(content, prefix: "BAI2");
        Console.WriteLine($"✅ BAI2 file saved to:\n{filePath}");

        await Task.CompletedTask; // Placeholder for future async operations
    },
    groupsOption,
    accountsOption,
    transactionsOption
);

// Add the 'generate' command to the root command
rootCommand.AddCommand(generateCommand);

// --- MT940 File Generation Command ---

// Define the MT940 file generation command with its option
var mt940TransactionsOption = new Option<int>("--transactions", () => 3, "Number of transactions");
var mt940Command = new Command("generate-mt940", "Generate an MT940 file")
{
    mt940TransactionsOption
};

// Assign an async handler to the 'generate-mt940' command
mt940Command.SetHandler(
    async (int transactions) =>
    {
        // Generate MT940 file content
        var generator = new Mt940Generator();
        var content = generator.GenerateFile(transactions);

        // Save the generated content to a file and output the file path
        var filePath = FileWriter.SaveFile(content, prefix: "MT940");
        Console.WriteLine($"✅ MT940 file saved to:\n{filePath}");

        await Task.CompletedTask; // Placeholder for future async operations
    },
    mt940TransactionsOption
);

// Add the 'generate-mt940' command to the root command
rootCommand.AddCommand(mt940Command);

// --- Fixed-width BAI2 File Generation Command ---

// Define options for the fixed-width BAI2 file generation command
var batchesOption = new Option<int>("--batches", () => 1, "Number of batches");
var paymentsOption = new Option<int>("--payments", () => 2, "Payments per batch");
var invoicesOption = new Option<int>("--invoices", () => 1, "Invoices per payment");

// Create the 'generate-fixed-bai2' command and add options
var fixedBai2Command = new Command("generate-fixed-bai2", "Generate a fixed-width BAI2 file")
{
    batchesOption,
    paymentsOption,
    invoicesOption
};

// Assign an async handler to the 'generate-fixed-bai2' command
fixedBai2Command.SetHandler(
    async (int batches, int payments, int invoices) =>
    {
        // Generate fixed-width BAI2 file content
        var generator = new FixedWidthBai2Generator();
        var content = generator.GenerateFile(batches, payments, invoices);

        // Save the generated content to a file and output the file path
        var filePath = FileWriter.SaveFile(content, prefix: "FixedBAI2");
        Console.WriteLine($"✅ Fixed-width BAI2 file saved to:\n{filePath}");

        await Task.CompletedTask; // Placeholder for future async operations
    },
    batchesOption,
    paymentsOption,
    invoicesOption
);

// Add the 'generate-fixed-bai2' command to the root command
rootCommand.AddCommand(fixedBai2Command);

// --- Command Invocation ---

// Parse and invoke the command-line arguments asynchronously
return await rootCommand.InvokeAsync(args);
