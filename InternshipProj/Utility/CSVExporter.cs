﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            if (Equals(Properties.Settings.Default.userSavePath, null))
            {
                bool? result = dlg.ShowDialog();

                if (result == true)
                {
                    FileName = dlg.FileName;
                    CSVwrite(lists);
                }
            }
            else
            {
                FileName = Properties.Settings.Default.userSavePath;
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
            if (Equals(FileName, null) || FileName.Equals("")) //Checks if closing on new, unsaved lists.
            {
                return;
            }
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                StringBuilder sb = new StringBuilder();
                foreach (TodoListVM list in lists)
                {
                    sb.Append(list.ListName);
                    sb.Append("-,");
                    sb.Append(list.PrioritizeToggle);

                    foreach (TodoItemVM item in list.ItemList)
                    {
                        sb.Append("-,");
                        if (item.Priority == null)
                        {
                            sb.Append("");
                        }
                        else
                        {
                            sb.Append(item.Priority);
                        }
                        
                        sb.Append("-,");
                        sb.Append(item.Desc);
                        sb.Append("-,");
                        string done;
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
