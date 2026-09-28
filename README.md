# Easy Inventory

A lightweight C# WinForms desktop application built to help small businesses manage their day-to-day operations locally, complete with multi-language support and Excel reporting.

## Features
* **Inventory & Warehouse:** Track products, stock quantities, prices, and SKUs.
* **Customer Management:** Manage customer profiles, contact info, notes, and analysis.
* **Sales & Payments:** Record daily transactions and track customer payments.
* **Multi-Language Support:** Dynamic localization using JSON files (English & Greek).
* **Excel Reports:** Generate and export reports using ClosedXML.

## Tech Stack & Packages
* **Language:** C# (.NET WinForms)
* **Database:** SQLite (Local `.db` file created automatically)
* **NuGet Packages:**
  * `ClosedXML`
  * `Newtonsoft.Json`
  * `System.Data.SQLite.Core`

## Project Structure
    Easy Inventory/
    ├── Classes/               # Database helper, translation manager, and utilities
    ├── Language/              # JSON localization files (English & Greek)
    ├── UserControls/          # Modular UI windows (Customers, Sales, Warehouse, etc.)
    ├── MainProgram.cs         # Main application window
    └── Program.cs             # Application entry point

## Getting Started
To get a copy of the project up and running locally, follow these simple steps:

1. **Clone the repository:**
   git clone https://github.com/konmichos/easy-inventory.git

2. **Open the project:**
   Launch Visual Studio 2022 and open the `Easy Inventory.sln` solution file.

3. **Restore NuGet Packages:**
   Right-click the solution in the Solution Explorer and select **Restore NuGet Packages**.

4. **Run the Application:**
   Click the **Start** button (or press `F5`) in Visual Studio to build and run the app. The SQLite database will be created automatically on first launch.