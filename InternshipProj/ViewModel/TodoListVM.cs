using InternshipProj.Model;
using InternshipProj.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipProj.ViewModel
{
    public class TodoListVM : ViewModelBase
    {
        private ObservableCollection<TodoItemVM> _itemList;
        private RelayCommand _addCommand;

        public ObservableCollection<TodoItemVM> ItemList
        {
            get { return _itemList; }
        }

        public RelayCommand AddCommand
        {
            get { return _addCommand; }
        }

        public TodoListVM(List<TodoItem> items)
        {
            _itemList = new ObservableCollection<TodoItemVM>();
            BuildViewModels(items);
            _addCommand = new RelayCommand(AddItem);
        }

        public TodoListVM()
        {
            _itemList = new ObservableCollection<TodoItemVM>();
            _addCommand = new RelayCommand(AddItem);
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
    }
}
