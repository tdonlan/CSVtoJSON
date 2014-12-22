using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;


using Newtonsoft.Json;

namespace CSVtoJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                var dictList = getDictListFromStrArrayList(ReadFileCSVHelper(args[0]));

                string jsonStr = JsonConvert.SerializeObject(dictList);

                File.WriteAllText(args[1], jsonStr);
            }
            else
            {
                Console.WriteLine("Please specify input and output file");
            }
        }

        public static void ReadFile(string filename)
        {
            List<string> txtList = File.ReadLines(filename).ToList();
        }

        public static List<string[]> ReadFileCSVHelper(string filename)
        {
            StreamReader reader = File.OpenText(filename);
        
            var parser = new CsvHelper.CsvParser(reader);
            List<string[]> csvList = new List<string[]>();

            while (true)
            {
                var row = parser.Read();
                if(row != null)
                {
                    csvList.Add(row);
                }
                else
                {
                    break;
                }    
            }

            return csvList;

        }

        public static List<Dictionary<string, object>> getDictListFromStrArrayList(List<string[]> csvList)
        {

            List<Dictionary<string, object>> dictList = new List<Dictionary<string, object>>();

            //first row has key names
            List<string> keyNames = csvList[0].ToList();
            csvList.RemoveAt(0);

            foreach (var row in csvList)
            {
                Dictionary<string, object> rowDict = new Dictionary<string, object>();
               
                for (int i = 0; i < row.Length; i++)
                {
                    rowDict.Add(keyNames[i], getRowObject(row[i]));
                }

                dictList.Add(rowDict);
            }

            return dictList;

        }

        private static object getRowObject(string data)
        {
            object jsonObject=null;
            int num;

            if(Int32.TryParse(data, out num))
            {
                return num;
            }
            else if(data == "null")
            {
                return null;
            }
            else if(tryJsonParse(data, out jsonObject))
            {
                return jsonObject;
            }
            else
            {
                return data;
            }
        }

        private static bool tryJsonParse(string data, out object jsonObject)
        {
            bool retval = false;

            try
            {
                jsonObject = JsonConvert.DeserializeObject(data);
                retval = true;
            }
            catch(Exception ex)
            {
                jsonObject = null;
            }


            return retval;
        }

    }
}
