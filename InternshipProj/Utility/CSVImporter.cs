using System;
using System.Collections.Generic;
using System.IO;
using InternshipProj.ViewModel;

namespace InternshipProj.Utility
{
    public static class CSVImporter
    {
        public static string _fileName;
        public static List<TodoListVM> Load()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".csv";
            ofd.Filter = CSVExporter.EXTFILTER;

            bool? result = ofd.ShowDialog();

            if(result == true)
            {
                _fileName = ofd.FileName;
                Properties.Settings.Default.userSavePath = _fileName;
                Properties.Settings.Default.Save();
                return CSVRead(_fileName);
            }

            return null;
        }

        public static List<TodoListVM> Load(string fileName)
        {
            return CSVRead(fileName);
        }

        //Creates and returns lists from CSV file
        private static List<TodoListVM> CSVRead(string userFile)
        {
            List<TodoListVM> lists = new List<TodoListVM>();
            try
            {
                using (StreamReader sr = new StreamReader(userFile))
                {
                    while (!sr.EndOfStream)
                    {
                        TodoListVM list = new TodoListVM();
                        List<TodoItemVM> items = new List<TodoItemVM>();

                        string line = sr.ReadLine();
                        string[] splitArr = line.Split(new string[] {"-,"}, StringSplitOptions.None);
                        list.ListName = splitArr[0];
                        list.PrioritizeToggle = bool.Parse(splitArr[1]);

                        for (int x = 2; x < splitArr.Length - 2; x += 3)
                        {
                            TodoItemVM item = new TodoItemVM();

                            if (!string.IsNullOrEmpty(splitArr[x]))
                            {
                                item.Priority = Int32.Parse(splitArr[x]);
                            }
                            else
                            {
                                item.Priority = null;
                            }

                            if (splitArr[x + 2] == "Done")
                            {
                                item.Done = true;
                            }
                            else
                            {
                                item.Done = false;
                            }

                            item.Desc = splitArr[x + 1];
                            list.ItemList.Add(item);
                            items.Add(item);
                        }
                        list._undo.Push(items);
                        lists.Add(list);
                    }
                }
            }
            catch
            {
                return lists;
            }
            return lists;
        }
    }
}
