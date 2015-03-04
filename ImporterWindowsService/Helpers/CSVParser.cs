using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImporterWindowsService.Helpers
{
    public static class CSVParser
    {
        public static DataTable ParseCSVToDataTable(string path)
        {
            DataTable table = new DataTable();
            StreamReader sr = new StreamReader(path);

            //Get headers from the first line of the table
            string[] headers = sr.ReadLine().Split(',');
            if (headers.Length <= 1)
                headers = sr.ReadLine().Split(',');

            //Create columns from headers
            foreach (string header in headers)
            {
                table.Columns.Add(header);
            }

            //Add records from CSV to DataTable
            while (sr.Peek() >= 0)
            {
                DataRow dr = table.NewRow();
                dr.ItemArray = sr.ReadLine().Split(',');
                table.Rows.Add(dr);
            }

            sr.Close();

            return table;
        }
    }
}
