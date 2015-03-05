using ImporterWindowsService.DBContexts.ImportDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImporterWindowsService.Formats
{
    class XML : ImportTask
    {
        public override ImportTask Process()
        {
            try
            {
                using (var Stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    XDocument file = XDocument.Load(Stream);
                    serialNo = (from x in file.Descendants("system")
                                         select x.Element("serialno").Value).First().ToString();
                    //LINQ to entities query to find serial number in data base
                    using (var ctx = new ImportDB())
                    {
                        var query = (from h in ctx.Hardwares
                                    where h.SerialNo == serialNo
                                    select h).ToList();
                        
                        foreach(var q in query)
                        {
                            var values = from v in file.Descendants("values")
                                         select v;

                            foreach(var v in values)
                            {
                                DateTime timestamp = Convert.ToDateTime(v.FirstAttribute.Value);
                                double value = Convert.ToDouble(v.Value);
                                int existingRecords = ctx.Data.Where(r => r.Timestamp == timestamp && r.Value == (decimal)value).Count();
                                if(existingRecords <= 0)
                                {
                                    Datum d = new Datum();
                                    d.HardwareId = q.Id;
                                    d.Timestamp = timestamp;
                                    d.Value = (decimal)value;
                                    ctx.Data.Add(d);
                                }
                            }
                        }
                        ctx.SaveChanges();
                    }

                }

                processed = true;
            }
            catch (Exception e)
            {
                LogException(e);
            }

            return new ImportTask(serialNo, exceptions, processed);
        }
    }
}
