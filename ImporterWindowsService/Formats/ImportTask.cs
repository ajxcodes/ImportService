using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImporterWindowsService.Formats
{
    class ImportTask
    {
        public string serialNo { get; set; }
        public string exceptions { get; set; }
        public bool processed { get; set; }
        public string filePath { get; set; }
        public string dbConnString { get; set; }

        public ImportTask (string path, string importType)
        {
            if (importType == "file")
                filePath = path;
            else if (importType == "db")
                dbConnString = path;
        }

        public ImportTask (string _serialNo, string _exceptions, bool _processed)
        {
            serialNo = _serialNo;
            exceptions = _exceptions;
            processed = _processed;

        }

        public virtual ImportTask Process()
        {
            return new ImportTask(serialNo, exceptions, processed);
        }

        public void LogException(Exception e)
        {
            processed = false;
                exceptions += "Exception: " + e.Message + "\n";
                if (e.InnerException != null)
                    exceptions += "Inner Exception: " + e.InnerException + "\n";
        }
    }
}
