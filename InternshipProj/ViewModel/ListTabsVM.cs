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

        public RelayCommand LoadLists { get; }

        public ListTabsVM()
        {
            TabLists = new ObservableCollection<TodoListVM>();
            DeleteList = new RelayCommand(Delete);
            AddList = new RelayCommand(Add);
            SaveLists = new RelayCommand(Save, CanSave);
            LoadLists = new RelayCommand(Load);
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

                TabLists.Clear();   //otherwise, continue
            }

            var loadedList = CSVImporter.Load();
            BuildViewModels(loadedList);
            Properties.Settings.Default.Save();
        }

        private bool CanSave(object obj)
        {
            return TabLists.Count > 0;
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
            Properties.Settings.Default.userSavePath = csvexp.FileName;
            Properties.Settings.Default.Save();
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