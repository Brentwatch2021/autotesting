using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Webtests.Agent
{
   
    public class CsvDataHelper
    {
        public static IEnumerable<TestCaseData> GetCsvData(string filePath)
        {
            var data = new List<TestCaseData>();
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    //data.Add(values);
                }
            }
            return data;
        }
    }
}
