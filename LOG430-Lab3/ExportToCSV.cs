using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LOG430_VI
{
    public class ExportToCSV
    {
        private static List<string[]> rowData = new List<string[]>();
        private static string path = Environment.CurrentDirectory + @"\vi_db.csv";

        public static async Task ExportToCSVAsync(Aggregator aggregator)
        {
            string[] rowDataTemp = new string[7];
            rowDataTemp[0] = DateTime.Now.ToString();
            rowDataTemp[1] = aggregator.topic;
            rowDataTemp[2] = aggregator.startDate.ToString();
            rowDataTemp[3] = aggregator.endDate.ToString();
            rowDataTemp[4] = aggregator.interval.ToString();
            rowDataTemp[5] = aggregator.type.ToString();
            rowDataTemp[6] = aggregator.result.ToString();


            rowData.Add(rowDataTemp);

            await EcrireLigneAsync();
        }

        /// <summary>
        /// Writes line asynchronously into the CSV file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static async Task EcrireLigneAsync()
        {
            string[][] output = new string[rowData.Count][];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = rowData[i];
            }

            int length = output.GetLength(0);
            string delimiter = ",";

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < length; index++)
                sb.AppendLine(string.Join(delimiter, output[index]));

            rowData = new List<string[]>();

            IsFileLocked(path);

            using (FileStream sourceStream = new FileStream(path,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                UTF8Encoding lvUtf8EncodingWithBOM = new UTF8Encoding(true, true);
                await sourceStream.WriteAsync(lvUtf8EncodingWithBOM.GetBytes(sb.ToString()), 0, sb.Length);
                sourceStream.Close();
            };
        }

        /// <summary>
        /// Loops until the csv file isn't locked anymore
        /// </summary>
        /// <param name="path"></param>
        public static void IsFileLocked(string path)
        {
            SpinWait.SpinUntil(delegate
            {
                Stream stream = null;
                try
                {
                    stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None);
                }
                catch
                {
                    return false;
                }

                if (stream != null)
                    stream.Close();

                return true;
            });
        }
    }
}
