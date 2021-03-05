using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeTester
{
    public class CSVReader
    {
        public string FileName { get; set; }
        public IEnumerable<List<string>> Header { get; set; }
        public IEnumerable<List<string>> Rows { get; set; }

        public CSVReader(string csvFile, char delimiter = ',', char quotationChar = '"')
        {
            FileName = csvFile;
            var lines = File.ReadAllLines(csvFile).Select(s =>
            {
                return ParseCsvRow(s, delimiter, quotationChar);
                //return ParseCsvRow2(s, delimiter, quotationChar);
            });
            var csv = (from line in lines select line);
            Header = csv.Take(1).ToList();
            Rows = csv.Skip(1).ToList();
        }

        private List<string> ParseCsvRow2(string s, char delimiter = ',', char quotationChar = '"')
        {
            var result = new List<string>();
            var data = s;// Regex.Replace(s.Trim(), @"\t|\n|\r", "");
            var colVal = String.Empty;
            var count = 0;
            var isEndOfColValue = true;

            bool isEscStart = false;
            bool isEscEnd = false;
            bool isEscStartInString = false;
            bool isEscEndInString = false;

            for (int i = 0; i < data.Length; i++)
            {
                if ((i == data.Length - 1 || data[i] == delimiter) && isEndOfColValue)
                {
                    result.Add(colVal.Replace("\"", ""));
                    colVal = String.Empty;
                }
                else
                {
                    var currentChar = data[i];
                    var maxLength = data.Length - 1;
                    var nextChar = currentChar;
                    if (i < maxLength)
                        nextChar = data[i + 1];

                    var str1 = (char.ToString(currentChar) + char.ToString(nextChar));
                    if (!isEscStart)
                        isEscStart = i == 0 && currentChar == quotationChar;
                    if (!isEscEnd)
                        isEscEnd = i == maxLength && currentChar == quotationChar;
                    if (!isEscStartInString)
                        isEscStartInString = i > 0 && i < maxLength && str1 == (char.ToString(quotationChar) + char.ToString(delimiter));
                    if (!isEscEndInString)
                        isEscEndInString = i > 0 && i < maxLength && str1 == (char.ToString(delimiter) + char.ToString(quotationChar));

                    if (isEscStart && isEscStartInString)
                    {
                        count++;
                        isEscStart = false;
                        isEscStartInString = false;
                        continue;
                    }
                    if (isEscStartInString && isEscEndInString)
                    {
                        count++;
                        isEscStartInString = false;
                        isEscEndInString = false;
                        continue;
                    }
                    if (isEscEndInString && isEscEnd)
                    {
                        count++;
                        isEscEndInString = false;
                        isEscEnd = false;
                        continue;
                    }
                    if (isEscStartInString && isEscEnd)
                    {
                        count++;
                        isEscStartInString = false;
                        isEscEnd = false;
                        continue;
                    }
                    isEndOfColValue = count % 2 == 0;
                    if (isEndOfColValue)
                    {
                        colVal += data[i];
                    }
                }
            }
            return result;
        }

        private List<string> ParseCsvRow(string r, char delimiter = ',', char quotationChar = '"')
        {
            var results = new List<string>();
            bool cont = false;
            var columnValue = String.Empty;
            foreach (var y in r.Split(new char[] { delimiter }, StringSplitOptions.None))
            {
                var x = y;
                if (cont)
                {
                    // End of field
                    if (x.EndsWith("" + quotationChar))
                    {
                        columnValue += delimiter + x.Substring(0, x.Length - 1);
                        results.Add(columnValue);
                        columnValue = String.Empty;
                        cont = false;
                        continue;
                    }
                    else
                    {
                        // Field still not ended
                        columnValue += delimiter + x;
                        continue;
                    }
                }
                // Fully encapsulated with no comma within
                if (x.StartsWith("" + quotationChar) && x.EndsWith("" + quotationChar))
                {
                    if ((x.EndsWith(quotationChar + "" + quotationChar) && !x.EndsWith(quotationChar + "" + quotationChar + "" + quotationChar)) && x != quotationChar + "" + quotationChar)
                    {
                        cont = true;
                        columnValue = x;
                        continue;
                    }
                    results.Add(x.Substring(1, x.Length - 2));
                    continue;
                }
                // Start of encapsulation but comma has split it into at least next field
                if (x.StartsWith("" + quotationChar) && !x.EndsWith("" + quotationChar))
                {
                    cont = true;
                    columnValue += x.Substring(1);
                    continue;
                }
                // Non encapsulated complete field
                results.Add(x);
            }
            return results;
        }
    }
}
