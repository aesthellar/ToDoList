using InternshipProj.Model;
using InternshipProj.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InternshipProj.ViewModel
{
    public class TodoListVM : ViewModelBase
    {
        private string _listName;

        public ObservableCollection<TodoItemVM> ItemList { get; }

        public string ListName
        {
            get { return _listName; }
            set
            {
                _listName = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddCommand { get; }

        public RelayCommand DeleteCommand { get; }

        public TodoListVM(List<TodoItem> items, string name)
        {
            _listName = name;
            ItemList = new ObservableCollection<TodoItemVM>();
            BuildViewModels(items);
            AddCommand = new RelayCommand(AddItem);
            DeleteCommand = new RelayCommand(DeleteItem);
        }

        public TodoListVM()
        {
            _listName = "New List";
            ItemList = new ObservableCollection<TodoItemVM>();
            AddCommand = new RelayCommand(AddItem);
            DeleteCommand = new RelayCommand(DeleteItem);
        }

        private void BuildViewModels(List<TodoItem> items)
        {
            if(items != null)
            {
                foreach(TodoItem item in items)
                {
                    var newItemVM = new TodoItemVM(item);
                    ItemList.Add(newItemVM);
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
