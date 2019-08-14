using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using InternshipProj.Utility;

namespace InternshipProj.ViewModel
{
    public class ListTabsVM : ViewModelBase
    {
        private ObservableCollection<TodoListVM> _tabLists;
        private RelayCommand _deleteList, _addList, _saveLists, _loadLists;

        public ObservableCollection<TodoListVM> TabLists
        {
            get { return _tabLists; }
        }

        public RelayCommand DeleteList
        {
            get { return _deleteList; }
        }

        public RelayCommand AddList
        {
            get { return _addList; }
        }

        public RelayCommand SaveLists
        {
            get { return _saveLists; }
        }

        public RelayCommand LoadLists
        {
            get { return _loadLists; }
        }
        public ListTabsVM()
        {
            _tabLists = new ObservableCollection<TodoListVM>();
            _deleteList = new RelayCommand(Delete);
            _addList = new RelayCommand(Add);
            _saveLists = new RelayCommand(Save, CanSave);
            _loadLists = new RelayCommand(Load);
        }


        public ListTabsVM(List<TodoListVM> lists)
        {
            BuildViewModels(lists);
            _tabLists = new ObservableCollection<TodoListVM>();
            _deleteList = new RelayCommand(Delete);
            _addList = new RelayCommand(Add);
            _saveLists = new RelayCommand(Save, CanSave);
            _loadLists = new RelayCommand(Load);
        }

        private void BuildViewModels(List<TodoListVM> lists)
        {
            if (lists != null)
            {
                foreach (TodoListVM list in lists)
                {
                    _tabLists.Add(list);
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