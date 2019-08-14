using System;
using InternshipProj.Model;
using System.Collections.Generic;
using System.IO;
using InternshipProj.ViewModel;

namespace InternshipProj.Utility
{
    public static class CSVImporter
    {
        public static List<TodoListVM> Load()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
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
        public static List<TodoListVM> Load(string fileName)
        {
            return CSVRead(fileName);
        }
        private static List<TodoListVM> CSVRead(string userFile)
        {
            List<TodoListVM> lists = new List<TodoListVM>();

            using (StreamReader sr = new StreamReader(userFile))
            {
                while (!sr.EndOfStream)
                {
                    TodoListVM list = new TodoListVM();
                    
                    string line = sr.ReadLine();
                    string[] splitArr = line.Split(new string[] { "\"," }, StringSplitOptions.None);
                    list.ListName = splitArr[0];

                    for (int x = 0; x < splitArr.Length - 2; x+=2)
                    {
                        TodoItemVM item = new TodoItemVM();
                        if (splitArr[x + 2] == "Done")
                        {
                            item.Done = true;
                        }
                        else
                        {
                            item.Done = false;
                        }

                        item.Desc = splitArr[x+1];
                        list.ItemList.Add(item);
                    }
                    
                    lists.Add(list);
                }
            }
            return lists;
        }
    }
}
