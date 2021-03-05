using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace BootstrapWebApplication.Admin.Conversion
{
    public class CSVToJSON
    {
        private CSVReader csv;
        public string FileName { get; private set; }
        public string JsonResult { get; set; }
        public string DateTimeFormat { get; set; }

        public CSVToJSON(CSVReader csv, string _dateTimeFormat)
        {
            this.csv = csv;
            this.DateTimeFormat = _dateTimeFormat;
            FileName = Path.GetFileNameWithoutExtension(new FileInfo(csv.FileName).FullName) + ".json";
        }

        public List<TSource> Parse<TSource>() where TSource : class
        {
            var results = new List<TSource>();
            var header = csv.Header.First();
            int headerItemCount = header.Length;//.Count;
            int rowCount = csv.Rows.Count();
            var invalidRows = new List<string[]>();// new List<List<string>>();
            var csvList = csv.Rows.ToList();
            csvList.RemoveAll(l =>
            {
                var isInvalidRow = l.Length != headerItemCount; // l.Count != headerItemCount;
                if (isInvalidRow)
                {
                    invalidRows.Add(l);
                }
                return isInvalidRow;
            });
            var invalidRowCount = invalidRows.Count;
            if (invalidRowCount > 0)
            {
                throw new InvalidRowCountException("Make sure the CSV file has the same amont of columns on each row. Use CSVFix.exe or Excel to fix the CSV file.");
            }
            var c = 0;
            foreach (var row in csvList)
            {
                //var jsonObject = new Dictionary<dynamic, dynamic>();
                string jsonObject = "{";
                for (int i = 0; i < row.Length; i++) //for (int i = 0; i < row.Count; i++)
                {
                    jsonObject += "\"" + header[i] + "\":\"" + FixJsonValue(row[i]) + "\",";
                    //jsonObject.Add(header[i], FixJsonValue(row[i]));
                }
                jsonObject = jsonObject.Substring(0, jsonObject.LastIndexOf(','));
                jsonObject += "}";
                //TSource jsonObjectSerialized = null;
                //try
                //{
                //    jsonObjectSerialized = JsonConvert.DeserializeObject<TSource>(jsonObject, new IsoDateTimeConverter { DateTimeFormat = "dd-MM-yyyy" });
                //}
                //catch (FormatException ex)
                //{
                //    jsonObjectSerialized = JsonConvert.DeserializeObject<TSource>(jsonObject, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                //}
                var jsonObjectSerialized = JsonConvert.DeserializeObject<TSource>(jsonObject, GetJsonSerializerSettings());
                c++;
                //System.Diagnostics.Debug.WriteLine(c);
                results.Add(jsonObjectSerialized);
            }
            return results;
            //try
            //{
            //    co = JsonConvert.DeserializeObject<TSource>(obj, new JsonSerializerSettings() { DateFormatString = DateTimeFormat });
            //    convertedObjects.Add(co);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("CSV to JSON contains invalid data.");
            //    //objectsWithError.Add(new { Object = obj, Exception = ex });
            //}
        }

        private JsonSerializerSettings GetJsonSerializerSettings()
        {
            //return new IsoDateTimeConverter() { DateTimeFormat = this.DateTimeFormat };
            var jss = new JsonSerializerSettings();
            jss.Converters.Add(new IsoDateTimeConverter()
            {
                DateTimeFormat = "dd'/'MM'/'yyyy"
            });
            jss.Culture = new CultureInfo("en-US", false)
            {
                NumberFormat = new NumberFormatInfo
                {
                    //CurrencyDecimalSeparator = ",",
                    NumberDecimalSeparator = ",",
                    //PercentDecimalSeparator = ","
                }
            };
            //jss.FloatParseHandling = FloatParseHandling.Decimal;
            return jss;
        }

        public List<string> Parse()
        {
            var results = new List<string>();
            var header = csv.Header.First();
            int headerItemCount = header.Length;//.Count;
            int rowCount = csv.Rows.Count();
            var invalidRows = new List<string[]>();// new List<List<string>>();
            var csvList = csv.Rows.ToList();
            csvList.RemoveAll(l =>
            {
                var isInvalidRow = l.Length != headerItemCount; // l.Count != headerItemCount;
                if (isInvalidRow)
                {
                    invalidRows.Add(l);
                }
                return isInvalidRow;
            });
            var invalidRowCount = invalidRows.Count;
            if (invalidRowCount > 0)
            {
                throw new InvalidRowCountException("Make sure the CSV file has the same amont of columns on each row. Use CSVFix.exe or Excel to fix the CSV file.");
            }

            foreach (var row in csvList)
            {
                var jsonObject = new Dictionary<dynamic, dynamic>();
                for (int i = 0; i < row.Length; i++) //for (int i = 0; i < row.Count; i++)
                {
                    jsonObject.Add(header[i], FixJsonValue(row[i]));
                }
                var jsonObjectSerialized = JsonConvert.SerializeObject(jsonObject, new IsoDateTimeConverter() { DateTimeFormat = this.DateTimeFormat });
                results.Add(jsonObjectSerialized);
            }
            return results;
        }

        private dynamic FixJsonValue(dynamic value)
        {
            value = value.Replace("\"", "");
            DateTime dt = DateTime.Now;
            double tempDouble = 0.0;
            // If the value is a double, it's not a date, but if parsed using datetime, 
            // then it will become a datetime object (which is incorrect)
            bool isDouble = Double.TryParse(value, out tempDouble);
            //bool isDateTime = DateTime.TryParse(newValue, out dt);
            //29/01/10

            string[] formats = { 
                                   "dMyy",
                                   "ddMyy",
                                   "ddMMyy",
                                   "dd-MM-yy",
                                   "dd-MM-yyy",
                                   "dd-MM-yyyy",

                                   "d/M/yy",
                                   "d/MM/yy",
                                   "dd/MM/yy",
                                   "dd/M/yy",

                                   "d/M/yyyy",
                                   "d/MM/yyyy",
                                   
                                   "dd/M/yyyy",
                                   "dd/MM/yyyy",
                                   
                                   "MM/dd/yyyy"
                               };
            var isDateTime = DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

            //bool isDateTime = DateTime.TryParseExact(value, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            //if (!isDateTime)
            //{
            //    isDateTime = DateTime.TryParseExact(value, "ddMMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            //    if (!isDateTime)
            //    {
            //        isDateTime = DateTime.TryParseExact(value, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            //        if (!isDateTime)
            //        {
            //            isDateTime = DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            //        }
            //    }
            //}

            var pattern = "^[0-9]{1,2}/[0-9]{1,2}/[0-9]{3}";
            if (System.Text.RegularExpressions.Regex.IsMatch(value, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                string temp = value.ToString();
                value = temp.Substring(0, 3) + temp.Substring(3, 3) + y2kYearFix(temp.Substring(6, 3));
                isDateTime = DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            }

            if (!isDouble && isDateTime)
            {
                var dtYearStr = dt.Year.ToString();
                if (dtYearStr.Length == 3)
                {
                    //var year = 2000 + Int32.Parse(dtYearStr.Substring(1, dtYearStr.Length - 1));
                    value = new DateTime(y2kYearFix(dtYearStr), dt.Month, dt.Day).ToString(this.DateTimeFormat);
                }
                else
                {
                    value = new DateTime(dt.Year, dt.Month, dt.Day).ToString(this.DateTimeFormat);
                }
            }
            return value;
        }

        // y2k problem fix
        int y2kYearFix(string dtYearStr)
        {
            var year = 0;
            int.TryParse(dtYearStr.Substring(1, dtYearStr.Length - 1), out year);
            if (year >= 0 && year <= DateTime.Now.Year)
            {
                year += 2000;
            }
            else
            {
                year += 1900;
            }
            return year;
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

    public class InvalidRowCountException : Exception
    {
        public InvalidRowCountException()
            : base()
        {

        }

        public InvalidRowCountException(string message)
            : base(message)
        {

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
