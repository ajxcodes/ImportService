using ImporterWindowsService.DBContexts.ExportDB;
using ImporterWindowsService.DBContexts.ImportDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImporterWindowsService.Formats
{
    class DB : ImportTask
    {
        public override ImportTask Process()
        {
            try
            {
                using(var e = new ExportDB())
                {
                    using(var i = new ImportDB())
                    {
                        var serialNums = from h in i.Hardwares
                                         select h;
                        foreach(var s in serialNums)
                        {
                            var values = from d in e.Data
                                         where d.SerialNo == s.SerialNo
                                         select d;
                            foreach(var v in values)
                            {
                                int existingRecords = i.Data.Where(r => r.Timestamp == v.Time && r.Value == v.Value).Count();
                                if(existingRecords <= 0)
                                {
                                    DBContexts.ImportDB.Datum d = new DBContexts.ImportDB.Datum();
                                    d.HardwareId = s.Id;
                                    d.Timestamp = v.Time;
                                    d.Value = v.Value;
                                    i.Data.Add(d);
                                }
                            }
                        }
                        i.SaveChanges();
                    }
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
