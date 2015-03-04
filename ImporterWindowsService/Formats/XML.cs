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

                    //LINQ to entities query to find serial number in data base
                    processed = true;
                    if(processed)
                    {
                        //LINQ to XML query to find values in XML file
                        //Iterate through values collection and add each value to the database
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
