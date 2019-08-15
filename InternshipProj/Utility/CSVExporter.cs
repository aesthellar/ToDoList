using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using InternshipProj.ViewModel;

namespace InternshipProj.Utility
{
    public class CSVExporter
    {
        public const string EXTFILTER = "CSV Files|*.csv";

        public string FileName { get; private set; }

        //Method to open save dialog
        public void Save(List<TodoListVM> lists)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Filter = EXTFILTER;

            bool? result = dlg.ShowDialog();

            if(result == true)
            {
                FileName = dlg.FileName;
                CSVwrite(lists);
            }
        }

        public void Save(List<TodoListVM> lists, string path)
        {
            FileName = path;
            CSVwrite(lists);
        }
        
        //Method to create and write into CSV
        private void CSVwrite(List<TodoListVM> lists)
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                StringBuilder sb = new StringBuilder();

                foreach (TodoListVM list in lists)
                {
                    sb.Append(list.ListName);

                    foreach (TodoItemVM item in list.ItemList)
                    {
                        Console.Write(item.Desc);
                        string done;
                        sb.Append("-,");
                        sb.Append(item.Desc);
                        sb.Append("-,");
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
    }
}
