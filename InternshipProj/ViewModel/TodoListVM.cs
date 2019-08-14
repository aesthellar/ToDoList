using InternshipProj.Model;
using InternshipProj.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InternshipProj.ViewModel
{
    public class TodoListVM : ViewModelBase
    {
        private string _listName;
        private ObservableCollection<TodoItemVM> _itemList;
        private RelayCommand _addCommand, _deleteCommand;

        public ObservableCollection<TodoItemVM> ItemList
        {
            get { return _itemList; }
        }

        public string ListName
        {
            get { return _listName; }
            set
            {
                _listName = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddCommand
        {
            get { return _addCommand; }
        }

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand; }
        }

        public TodoListVM(List<TodoItem> items, string name)
        {
            _listName = name;
            _itemList = new ObservableCollection<TodoItemVM>();
            BuildViewModels(items);
            _addCommand = new RelayCommand(AddItem);
            _deleteCommand = new RelayCommand(DeleteItem);
        }

        public TodoListVM()
        {
            _listName = "New List";
            _itemList = new ObservableCollection<TodoItemVM>();
            _addCommand = new RelayCommand(AddItem);
            _deleteCommand = new RelayCommand(DeleteItem);
        }

        private void BuildViewModels(List<TodoItem> items)
        {
            if(items != null)
            {
                foreach(TodoItem item in items)
                {
                    var newItemVM = new TodoItemVM(item);
                    _itemList.Add(newItemVM);
                }
            }
        }

        private void AddItem(object obj)
        {
            var newItem = new TodoItemVM();
            ItemList.Add(newItem);
        }

        private void DeleteItem(object obj)
        {
            if (obj is TodoItemVM)
            {
                var item = (TodoItemVM) obj;
                ItemList.Remove(item);
            }
        }
    }
}
