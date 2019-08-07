using InternshipProj.Model;
using InternshipProj.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InternshipProj.ViewModel
{
    public class TodoListVM : ViewModelBase
    {
        private ObservableCollection<TodoItemVM> _itemList;
        private RelayCommand _addCommand, _deleteCommand, _saveCommand;

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

        public TodoListVM(List<TodoItem> items)
        {
            _itemList = new ObservableCollection<TodoItemVM>();
            BuildViewModels(items);
            _addCommand = new RelayCommand(AddItem);
            _deleteCommand = new RelayCommand(DeleteItem);
            _saveCommand = new RelayCommand(SaveList,CanSave);
        }

        public TodoListVM()
        {
            _itemList = new ObservableCollection<TodoItemVM>();
            _addCommand = new RelayCommand(AddItem);
            _deleteCommand = new RelayCommand(DeleteItem);
            _saveCommand = new RelayCommand(SaveList, CanSave);
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
        }

        private bool CanSave(object obj)
        {
            return ItemList.Count > 0;
        }

        private void TodoItemVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = sender as TodoItemVM;
            item.Update();
        }

    }
}
