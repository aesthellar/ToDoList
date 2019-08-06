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
        private RelayCommand _addCommand, _deleteCommand;

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

        public TodoListVM(List<TodoItem> items)
        {
            _itemList = new ObservableCollection<TodoItemVM>();
            BuildViewModels(items);
            _addCommand = new RelayCommand(AddItem);
            _deleteCommand = new RelayCommand(DeleteItem);
        }

        public TodoListVM()
        {
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

        private void TodoItemVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = sender as TodoItemVM;
            item.Update();
        }

    }
}
