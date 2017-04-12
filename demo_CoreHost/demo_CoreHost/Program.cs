using ArcGIS.Core.Data;
using ArcGIS.Core.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;

namespace demo_CoreHost
{
    class Program
    {
        //[STAThread] must be present on the Application entry point
        [STAThread]
        static void Main(string[] args)
        {
            var gdbPath = @"C:\DevDay16-Maps\Demo3\Demo3.gdb";
            if (args.Count() > 0)
            {
                gdbPath = args[0];
            }

            //Call Host.Initialize before constructing any objects from ArcGIS.Core
            try
            {
                Host.Initialize();
            }
            catch (Exception ex)
            {
                // Error (missing installation, no license, 64 bit mismatch, etc.)
                Console.WriteLine($@"Initialization failed: {ex.Message}");
                Console.ReadKey();
            }

            ListTables(gdbPath);

            Console.WriteLine("");
            Console.WriteLine("Druk een toets om af te sluiten.");
            Console.ReadKey();
        }

        private static void ListTables(string gdbPath)
        {
            try
            {                
                //if we are here, ArcGIS.Core is successfully initialized
                // open the filegeodatbase
                using (var gdb = new Geodatabase(new FileGeodatabaseConnectionPath(new Uri(gdbPath))))
                {
                    // retreieve the table definition
                    IReadOnlyList<TableDefinition> definitions = gdb.GetDefinitions<FeatureClassDefinition>();
                    foreach (var fdsDef in definitions)
                    {
                        // retrieve record count
                        int value = ReadRows(gdb, fdsDef.GetName());
                        Console.WriteLine($@"Table / FCL : {fdsDef.GetName()} has {value} records");
                    }
                }
            }
            catch (Exception ex)
            {
                // Error (missing installation, no license, 64 bit mismatch, etc.)
                Console.WriteLine($@"Cannot read file Geodatabase [{gdbPath}] error: {ex.Message}");
                return;
            }
        }

       
        private static int ReadRows(Geodatabase gdb, string tableName)
        {
            int value = 0;
            // open the table
            using (var table = gdb.OpenDataset<Table>(tableName))
            {

                RowCursor cursor = table.Search(new QueryFilter() { SubFields = "OBJECTID" });
                // loop through records
                while (cursor.MoveNext())
                {
                    value++;                    
                }
            }
            return value;
        }     
    }
}
