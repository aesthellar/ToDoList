using InternshipProj.Model;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InternshipProj.Utility
{
    public class CSVExporter
    {
        private string _fileName;
        public const string EXTFILTER = "CSV Files|*.csv";

        public string FileName { get { return _fileName; } }
        
        //Method to open save dialog
        public void Save(List<TodoItem> items, string openPath = "")
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = openPath;
            dlg.DefaultExt = ".csv";
            dlg.Filter = EXTFILTER;

            bool? result = dlg.ShowDialog();

            if(result == true)
            {
                _fileName = dlg.FileName;
                CSVwrite(items);
            }
        }
        
        //Method to create and write into CSV
        private void CSVwrite(List<TodoItem> items)
        {
            using (StreamWriter sw = new StreamWriter(_fileName))
            {
                StringBuilder sb = new StringBuilder();

                foreach (TodoItem item in items)
                {
                    var itemDescription = CleanReservedCharacters(item.Desc);
                    string done;
                    sb.Append("\"");
                    sb.Append(itemDescription);
                    sb.Append("\"");
                    sb.Append(",");
                    if(item.Done)
                    {
                        done = "Done";
                    }
                    else
                    {
                        done = "Not done";
                    }
                    sb.Append(done);
                    sw.WriteLine(sb.ToString());
                    sb.Clear();
                }
            }
        }

        private string CleanReservedCharacters(string description)
        {
            if (description.Contains("\""))
            {
                string cleanDesc = description.Replace("\"", "\\\"");
                return cleanDesc;
            }

            return description;
        }
    }
}
