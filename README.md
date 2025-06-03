# 🏦 Banking File Generator (.NET)

A modern .NET 9 CLI tool for generating standardized banking files in multiple formats:

- 📁 BAI2 Format
- 📁 Fixed-Width BAI2 Format
- 📁 MT940 Format

These files are commonly used for ERP, reconciliation, and transaction processing in banking integrations.

---

## 🛠️ Project Structure

- **BankingFileGenerator.App**: CLI entry point and command definitions
- **BankingFileGenerator.Lib**: Core generators and utilities
  - `Bai2Generator` – Standard BAI2 file generator
  - `FixedWidthBai2Generator` – Fixed-width BAI2 file generator
  - `Mt940Generator` – MT940 file generator
  - `FileWriter` – Utility for saving files with timestamped names
- **BankingFileGenerator.Tests**: Unit tests for all generators

---

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET 9.0 SDK or later](https://dotnet.microsoft.com/download)
- Visual Studio 2022 or VS Code with C# plugin

### 🧱 Setup

Clone the repo and navigate to the project root:


Restore dependencies and build the solution:


### 🧪 Running Tests

To run all unit tests:


---

## 💻 CLI Usage

### Generate a BAI2 file


- `--groups`: Number of groups (default: 1)
- `--accounts`: Accounts per group (default: 1)
- `--transactions`: Transactions per account (default: 1)

### Generate a Fixed-Width BAI2 file


- `--batches`: Number of batches (default: 1)
- `--payments`: Payments per batch (default: 2)
- `--invoices`: Invoices per payment (default: 1)

### Generate an MT940 file


- `--transactions`: Number of transactions (default: 3)

All files are saved in the `output/` directory with a timestamped filename.

---

## 🧩 Supported Formats

| Format                | Generator Class           | Output File Prefix   |
|-----------------------|--------------------------|---------------------|
| BAI2                  | Bai2Generator            | BAI2_*.txt          |
| Fixed-Width BAI2      | FixedWidthBai2Generator  | FixedBAI2_*.txt     |
| MT940                 | Mt940Generator           | MT940_*.txt         |

---

## 🧑‍💻 Contributing

Pull requests are welcome! If you want to add a new file format, enhance validations, or improve CLI features, feel free to fork and submit a PR.

---

## 📄 License

This project is licensed under the MIT License. See LICENSE for details.

---

## 📚 Extras

Let us know if you'd like to see:

- 📷 Screenshots or sample output
- 📚 API references
- 📝 GitHub Actions CI badges

Or if you want the file created for you automatically in your repo.
