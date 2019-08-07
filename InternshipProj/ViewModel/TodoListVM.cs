using InternshipProj.Model;
using InternshipProj.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace InternshipProj.ViewModel
{
    public class TodoListVM : ViewModelBase
    {
        private ObservableCollection<TodoItemVM> _itemList;
        private RelayCommand _addCommand, _deleteCommand, _saveCommand, _loadCommand;

        public ObservableCollection<TodoItemVM> ItemList
        {
            get { return _itemList; }
        }

        public RelayCommand AddCommand
        {
            get { return _addCommand; }
        }

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand; }
        }

        public RelayCommand SaveCommand
        {
            get { return _saveCommand; }
        }

        public RelayCommand LoadCommand
        {
            get { return _loadCommand; }
        }

        public TodoListVM(List<TodoItem> items)
        {
            _itemList = new ObservableCollection<TodoItemVM>();
            BuildViewModels(items);
            _addCommand = new RelayCommand(AddItem);
            _deleteCommand = new RelayCommand(DeleteItem);
            _saveCommand = new RelayCommand(SaveList,CanSave);
            _loadCommand = new RelayCommand(LoadList);
        }

        public TodoListVM()
        {
            _itemList = new ObservableCollection<TodoItemVM>();
            _addCommand = new RelayCommand(AddItem);
            _deleteCommand = new RelayCommand(DeleteItem);
            _saveCommand = new RelayCommand(SaveList, CanSave);
            _loadCommand = new RelayCommand(LoadList);
        }

        private void BuildViewModels(List<TodoItem> items)
        {
            if(items != null)
            {
                foreach(TodoItem item in items)
                {
                    var newItemVM = new TodoItemVM(item);
                    newItemVM.PropertyChanged += TodoItemVM_PropertyChanged;
                    _itemList.Add(newItemVM);
                }
            }
        }

        private void AddItem(object obj)
        {
            var newItem = new TodoItemVM();
            ItemList.Add(newItem);
            newItem.PropertyChanged += TodoItemVM_PropertyChanged;
        }

        private void DeleteItem(object obj)
        {
            if(obj is TodoItemVM)
            {
                var item = (TodoItemVM)obj;
                item.PropertyChanged -= TodoItemVM_PropertyChanged;
                ItemList.Remove(item);
            }
        }

        private void SaveList(object obj)
        {
            List<TodoItem> items = new List<TodoItem>();
            foreach(TodoItemVM itemVM in ItemList)
            {
                items.Add(itemVM.Item);
            }
            CSVExporter csvexp = new CSVExporter();
            csvexp.Save(items);
            Properties.Settings.Default.userSavePath = csvexp.FileName;
            Properties.Settings.Default.Save();
        }

        private bool CanSave(object obj)
        {
            return ItemList.Count > 0;
        }

        private void LoadList(object obj)
        {
            if(ItemList.Count>0)
            {
                //show message box with ok/cancel if user wants to clear items
                var result = MessageBox.Show("Loading a new list will clear your current list. Continue?", "Load Warning", MessageBoxButton.OKCancel);
                if (!result.Equals(MessageBoxResult.OK))
                {
                    return; //if cancel, return
                }

                ItemList.Clear();   //otherwise, continue
            }

            var loadedList = CSVImporter.Load();
            BuildViewModels(loadedList);
        }

        public void InitializeList(string path)
        {
            var loadedList = CSVImporter.Load(path);
            BuildViewModels(loadedList);
        }

        private void TodoItemVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = sender as TodoItemVM;
            item.Update();
        }

    }
}
