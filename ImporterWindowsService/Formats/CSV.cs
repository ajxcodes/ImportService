using ImporterWindowsService.DBContexts.ImportDB;
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

                using(var ctx = new ImportDB()){

                
                    var query = (from h in ctx.Hardwares
                                join s in ctx.Sites on h.SiteId equals s.Id
                                where h.SerialNo == serialNo
                                select new {
                                    HardwareId = h.Id,
                                    SiteId = s.Id,
                                    SerialNo = h.SerialNo
                                }).ToList();

                    foreach(var q in query){

                        for (int i = 0; i < rows; i++)
                        {
                            DateTime timestamp = Convert.ToDateTime(parsedData.Rows[i].ItemArray[0].ToString());
                            double value = Double.Parse(parsedData.Rows[i].ItemArray[1].ToString());
                            int existingRecords = ctx.Data.Where(r => r.Timestamp == timestamp && r.Value == (decimal)value).Count();
                            if (existingRecords <= 0)
                            {
                                Datum d = new Datum();
                                d.HardwareId = q.HardwareId;
                                d.Timestamp = timestamp;
                                d.Value = (decimal)value;
                                ctx.Data.Add(d);
                            }
                        }
                    }
                    ctx.SaveChanges();
                    
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
