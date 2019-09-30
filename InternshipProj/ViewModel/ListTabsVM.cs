using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using InternshipProj.Utility;

namespace InternshipProj.ViewModel
{
    public class ListTabsVM : ViewModelBase
    {
        public ObservableCollection<TodoListVM> TabLists { get; }
        public RelayCommand DeleteList { get; }
        public RelayCommand AddList { get; }
        public RelayCommand SaveLists { get; }
        public RelayCommand SaveAsLists { get; }
        public RelayCommand LoadLists { get; }
        public RelayCommand NewList { get; }
        

        public ListTabsVM()
        {
            TabLists = new ObservableCollection<TodoListVM>();
            DeleteList = new RelayCommand(Delete);
            AddList = new RelayCommand(Add);
            SaveLists = new RelayCommand(Save);
            SaveAsLists = new RelayCommand(SaveAs);
            LoadLists = new RelayCommand(Load);
            NewList = new RelayCommand(New);
            
        }

        private void BuildViewModels(List<TodoListVM> lists)
        {
            if (lists != null)
            {
                foreach (TodoListVM list in lists)
                {
                    TabLists.Add(list);
                }
            }
        }

        private void Load(object obj)
        {
            if (TabLists.Count > 0)
            {
                //show message box with ok/cancel if user wants to clear items
                var result = MessageBox.Show("Loading saved lists will clear your current lists. Continue?", "Load Warning", MessageBoxButton.OKCancel);
                if (!result.Equals(MessageBoxResult.OK))
                {
                    return; //if cancel, return
                }

                //otherwise, continue
            }

            var loadedList = CSVImporter.Load();
            if (!Equals(loadedList, null))
            {
                TabLists.Clear();
                BuildViewModels(loadedList);
                Properties.Settings.Default.Save();
            }
        }

        private void Save(object obj)
        {
            List<TodoListVM> lists = new List<TodoListVM>();
            foreach (TodoListVM list in TabLists)
            {
                lists.Add(list);
            }
            CSVExporter csvexp = new CSVExporter();
            csvexp.Save(lists);
        }

        private void SaveAs(object obj)
        {
            List<TodoListVM> lists = new List<TodoListVM>();
            foreach (TodoListVM list in TabLists)
            {
                lists.Add(list);
            }
            CSVExporter csvexp = new CSVExporter();
            csvexp.SaveAs(lists);
        }

        private void Add(object obj)
        {
            TodoListVM list = new TodoListVM();
            TabLists.Add(list);
        }

        private void Delete(object obj)
        {
            if (obj is TodoListVM)
            {
                if (TabLists.Count != 1)
                {
                    var result = MessageBox.Show("You are about to delete a list. Continue?", "List Delete Warning", MessageBoxButton.OKCancel);
                    if (!result.Equals(MessageBoxResult.OK))
                    {
                        return; //if cancel, return
                    }
                    else
                    {
                        var list = (TodoListVM)obj;
                        TabLists.Remove(list);
                        int count = TabLists.Count;
                    }
                }
                else
                {
                    var result = MessageBox.Show("Must have at least 1 list", "1 List Warning", MessageBoxButton.OK);

                }
            }
        }

        private void New(object obj)
        {
            Properties.Settings.Default.userSavePath = null;
            Properties.Settings.Default.Save();
            TabLists.Clear();
            TodoListVM list = new TodoListVM();
            TabLists.Add(list);
        }

        public void New()
        {
            Properties.Settings.Default.userSavePath = null;
            Properties.Settings.Default.Save();
            TabLists.Clear();
            TodoListVM list = new TodoListVM();
            TabLists.Add(list);
        }

        public void InitializeList(string path)
        {
            var loadedList = CSVImporter.Load(path);
            BuildViewModels(loadedList);
        }

        public void ExitSave(string path)
        {
            List<TodoListVM> lists = new List<TodoListVM>();
            foreach (TodoListVM list in TabLists)
            {
                lists.Add(list);
            }
            CSVExporter csvexp = new CSVExporter();
            csvexp.Save(lists, path);
        }
    }
}