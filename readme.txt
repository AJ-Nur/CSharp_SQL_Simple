README.txt

1. In VS, manage NuGet packages:
	Tools>Nuget package manager>Manage NuGet Package for Solution.
	Search System.Data.SqlClient > Install.
	A package file will be created under project file.
	Now can use using System.Data.SqlClient in our program.
2.Set up SQL Server connection string
	At first download https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16.
	Create text document with extension .UDL.
	Right click the file, Open with>OLE DB Core Services.
	Data Link Properties window will prompt out.
	At connection tab, enter server name "your pc name", choose "Use Windows NT Integrated security" and select the database on the server.
	Test Connection.
	Now that there's connection string in the UDL file. Rename it to "ConnectionString.txt".
	Add "Data Source=" before word "Provider" in the txt file.
3. Create a database in the Microsoft SQL Management Studio" using SELECT:
CREATE TABLE Vehicles
(
   CarID INT PRIMARY KEY IDENTITY,
   VIN VARCHAR(10),
   Make VARCHAR(15),
   Model VARCHAR(15),
   CarYear SMALLINT,
   Mileage INT
)
INSERT INTO Vehicles (VIN, Make, Model, CarYear, Mileage)
VALUES ('1BDHGD', 'Jeep', 'Wrangler', 2013, 65000)

Use "SELECT * FROM Vehicles" to see the table.

