using ImporterWindowsService.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImporterWindowsService.Formats
{
    class CSV : ImportTask
    {
        public override ImportTask Process()
        {
            try
            {
                //Parse data from CSV file
                DataTable parsedData = CSVParser.ParseCSVToDataTable(filePath);
                serialNo = System.IO.File.ReadAllLines(filePath)[0];

                var rows = parsedData.Rows.Count;
                int column = 1;
                int hardwareId = 0;
                for(int i = 0; i < rows; i++)
                {
                    DateTime timestamp = Convert.ToDateTime(parsedData.Rows[i].ItemArray[0].ToString());
                    double value = Double.Parse(parsedData.Rows[i].ItemArray[1].ToString());
                    //insert to db
                }

                processed = true;
            }

            catch(Exception e)
            {
                LogException(e);
            }

            return new ImportTask(serialNo, exceptions, processed);
        }
    }
}
