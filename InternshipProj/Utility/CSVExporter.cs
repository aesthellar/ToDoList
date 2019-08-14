using System.Collections.Generic;
using System.IO;
using System.Text;
using InternshipProj.ViewModel;

namespace InternshipProj.Utility
{
    public class CSVExporter
    {
        private string _fileName;
        public const string EXTFILTER = "CSV Files|*.csv";

        public string FileName { get { return _fileName; } }
        
        //Method to open save dialog
        public void Save(List<TodoListVM> lists)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Filter = EXTFILTER;

            bool? result = dlg.ShowDialog();

            if(result == true)
            {
                _fileName = dlg.FileName;
                CSVwrite(lists);
            }
        }

        public void Save(List<TodoListVM> lists, string path)
        {
            _fileName = path;
            CSVwrite(lists);
        }
        
        //Method to create and write into CSV
        private void CSVwrite(List<TodoListVM> lists)
        {
            using (StreamWriter sw = new StreamWriter(_fileName))
            {
                StringBuilder sb = new StringBuilder();

                foreach (TodoListVM list in lists)
                {
                    var ListName = CleanReservedCharacters(list.ListName);
                    sb.Append(ListName);

                    foreach (TodoItemVM item in list.ItemList)
                    {
                        var desc = CleanReservedCharacters(item.Desc);
                        string done;
                        sb.Append("\",");
                        sb.Append(desc);
                        sb.Append("\",");
                        if (item.Done)
                        {
                            done = "Done";
                        }
                        else
                        {
                            done = "Not done";
                        }
                        sb.Append(done);
                    }
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
