using InternshipProj.Model;
using InternshipProj.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InternshipProj.ViewModel
{
    public class TodoListVM : ViewModelBase
    {
        private bool _prioritizeToggle;
        private string _listName;

        public ObservableCollection<TodoItemVM> ItemList { get; }
        
        public bool PrioritizeToggle
        {
            get { return _prioritizeToggle; }
            set
            {
                _prioritizeToggle = value;
                OnPropertyChanged();
            }
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

        public RelayCommand AddCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand PrioritizeCommand { get; }
        public RelayCommand ToggleList { get; }

        public TodoListVM(List<TodoItem> items, string name)
        {
            _listName = name;
            _prioritizeToggle = false;
            ItemList = new ObservableCollection<TodoItemVM>();
            BuildViewModels(items);
            AddCommand = new RelayCommand(AddItem);
            DeleteCommand = new RelayCommand(DeleteItem);
            PrioritizeCommand = new RelayCommand(Prioritize);
            ToggleList = new RelayCommand(Toggle);
        }

        public TodoListVM()
        {
            _listName = "New List";
            _prioritizeToggle = false;
            ItemList = new ObservableCollection<TodoItemVM>();
            AddCommand = new RelayCommand(AddItem);
            DeleteCommand = new RelayCommand(DeleteItem);
            PrioritizeCommand = new RelayCommand(Prioritize);
            ToggleList = new RelayCommand(Toggle);
        }

        //Creates list of Item View Models
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

        //Adds item onto list
        private void AddItem(object obj)
        {
            var newItem = new TodoItemVM();
            ItemList.Add(newItem);
            newItem.Priority = ItemList.Count;
        }

        //Removes item from list
        private void DeleteItem(object obj)
        {
            if (obj is TodoItemVM)
            {
                var item = (TodoItemVM) obj;
                ItemList.Remove(item);
                foreach (TodoItemVM vm in ItemList)
                {
                    vm.Priority = ItemList.IndexOf(vm) + 1;
                }
            }
        }

        //Prioritizes or unprioritizes list
        private void Toggle(object obj)
        {
            if (PrioritizeToggle == false)
            {
                PrioritizeToggle = true;
            }
            else
            {
                PrioritizeToggle = false;
            }
        }

        //Sorts items according to their given priority
        public void Prioritize(object obj)
        {
            if (obj is TodoItemVM)
            {
                var item = (TodoItemVM)obj;
                if (ItemList.Count > 1 && item.Priority > 0)
                {
                    int priority = (int)item.Priority;
                    if (priority >= ItemList.Count) //If user given priority is larger than list, add items to fill
                    {
                        int newItems = priority - ItemList.Count;
                        for (int i = 1; i <= newItems; i++)
                        {
                            var newItem = new TodoItemVM();
                            ItemList.Add(newItem);
                        }
                    }
                    ItemList.Remove(item);
                    ItemList.Insert(priority-1, item);

                    foreach (TodoItemVM vm in ItemList) //Update rest of items' priority in relation to the new priority
                    {
                        vm.Priority = ItemList.IndexOf(vm) + 1;
                    }
                }
            }
        }
    }
}
