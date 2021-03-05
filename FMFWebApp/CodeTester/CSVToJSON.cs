using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeTester
{
    public class CSVToJSON
    {
        private CSVReader csv;
        public string FileName { get; private set; }
        public string JsonResult { get; set; }

        public CSVToJSON(CSVReader csv)
        {
            this.csv = csv;
            FileName = Path.GetFileNameWithoutExtension(new FileInfo(csv.FileName).FullName) + ".json";
        }

        public List<string> Parse()
        {
            bool hasError = false;
            //var results = new List<Dictionary<dynamic, dynamic>>();
            var results = new List<string>();
            var header = csv.Header.First();
            foreach (var row in csv.Rows.Take(3))
            {
                //hasError = header.Count != row.Count;
                //if (hasError)
                //{
                //    OnJsonConvertError(new JsonConvertErrorArgs()
                //    {
                //        Data = row,
                //        ExceptionData = null//new InvalidRowException("Row is not of valid length!")
                //    });
                //}
                //else
                //{
                //    var jsonObject = new Dictionary<dynamic, dynamic>();
                //    var json = header.Zip(row, (first, second) =>
                //        {
                //            //var r = first + " : " + second.Replace("\"", "");
                //            //return r;
                //            return new { First = first, Second = second.Replace("\"", "") };
                //        });

                //    json.ToList().ForEach(i=> {
                //        jsonObject.Add(i.First, i.Second);
                //    });

                //    //foreach (var col in row)
                //    //{
                //    //    jsonObject.Add(header[jsonObject.Count], col);
                //    //}
                //    results.Add(jsonObject);
                //}
                var jsonObject = new Dictionary<dynamic, dynamic>();
                var json = header.Zip(row, (first, second) =>
                {
                    //var r = first + " : " + second.Replace("\"", "");
                    //return r;
                    dynamic secondParsed = second.Replace("\"", "");
                    //try
                    //{
                    //    secondParsed = Convert.ToDateTime(second);
                    //}
                    //catch(Exception ex)
                    //{
                    //    secondParsed = second.Replace("\"", "");
                    //}
                    return new
                    {
                        First = first,
                        Second = secondParsed
                    };
                });

                json.ToList().ForEach(i =>
                {
                    jsonObject.Add(i.First, i.Second);
                });
                var settings = new IsoDateTimeConverter() { DateTimeFormat = "dd/MM/yyyy" };
                results.Add(JsonConvert.SerializeObject(jsonObject, settings));
                //results.Add(JsonConvert.SerializeObject(jsonObject, new IsoDateTimeConverter() { DateTimeFormat = "dd-MM-yyyy" }));
                Console.WriteLine(results.Count);
            }
            return results;
            //JsonResult = JsonConvert.SerializeObject(results, new IsoDateTimeConverter() { DateTimeFormat = "dd-MM-yyyy" });
        }

        public event JsonConvertEventHandler JsonConvertError;

        protected virtual void OnJsonConvertError(JsonConvertErrorArgs e)
        {
            if (JsonConvertError != null)
            {
                JsonConvertError(this, e);
            }
        }

        public void Save()
        {
            File.WriteAllText(FileName, JsonResult);
        }
    }

    public delegate void JsonConvertEventHandler(object sender, JsonConvertErrorArgs e);

    public class JsonConvertErrorArgs : EventArgs
    {
        public List<string> Data { get; set; }

        public Exception ExceptionData { get; set; }
    }

    public class InvalidRowException : Exception
    {
        public InvalidRowException() : base() { }
        public InvalidRowException(string message) : base(message) { }
    }
}
