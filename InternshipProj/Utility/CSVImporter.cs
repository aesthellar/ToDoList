using InternshipProj.Model;
using System.Collections.Generic;
using System.IO;

namespace InternshipProj.Utility
{
    public static class CSVImporter
    {
        public static List<TodoItem> Load(string fileName = "")
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.FileName = fileName;
            ofd.DefaultExt = ".csv";
            ofd.Filter = CSVExporter.EXTFILTER;

            bool? result = ofd.ShowDialog();

            if(result == true)
            {
                var userFile = ofd.FileName;
                return CSVRead(userFile);
            }

            return null;
        }

        private static List<TodoItem> CSVRead(string userFile)
        {
            List<TodoItem> list = new List<TodoItem>();

            using (StreamReader sr = new StreamReader(userFile))
            {
                while (!sr.EndOfStream)
                {
                    TodoItem item = new TodoItem();
                    string line = sr.ReadLine();
                    string[] splitArr = line.Split(new[] { '\"' });
                    item.Desc = splitArr[1];
                    if (splitArr[splitArr.Length-1] == ",Done")
                    {
                        item.Done = true;
                    }
                    else
                    {
                        item.Done = false;
                    }
                    list.Add(item);
                }
            }

            return list;
        }

    }
}
