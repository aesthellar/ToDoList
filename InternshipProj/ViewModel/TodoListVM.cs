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
        public Stack<List<TodoItemVM>> _undo;
        public Stack<List<TodoItemVM>> _redo;
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
        public RelayCommand UndoCommand { get; }
        public RelayCommand RedoCommand { get; }

        public TodoListVM(List<TodoItem> items, string name)
        {
            _listName = name;
            _prioritizeToggle = false;
            _undo = new Stack<List<TodoItemVM>>();
            _redo = new Stack<List<TodoItemVM>>();
            ItemList = new ObservableCollection<TodoItemVM>();
            BuildViewModels(items);
            AddCommand = new RelayCommand(AddItem);
            DeleteCommand = new RelayCommand(DeleteItem);
            PrioritizeCommand = new RelayCommand(Prioritize);
            ToggleList = new RelayCommand(Toggle);
            UndoCommand = new RelayCommand(Undo);
            RedoCommand = new RelayCommand(Redo);
        }

        public TodoListVM()
        {
            _listName = "New List";
            _prioritizeToggle = false;
            _undo = new Stack<List<TodoItemVM>>();
            _redo = new Stack<List<TodoItemVM>>();
            ItemList = new ObservableCollection<TodoItemVM>();
            AddCommand = new RelayCommand(AddItem);
            DeleteCommand = new RelayCommand(DeleteItem);
            PrioritizeCommand = new RelayCommand(Prioritize);
            ToggleList = new RelayCommand(Toggle);
            UndoCommand = new RelayCommand(Undo);
            RedoCommand = new RelayCommand(Redo);
        }

        //Creates list of Item View Models
        private void BuildViewModels(List<TodoItem> items)
        {
            if (items != null)
            {
                List<TodoItemVM> originalList = new List<TodoItemVM>();
                foreach (TodoItem item in items)
                {
                    var newItemVM = new TodoItemVM(item);

                    originalList.Add(newItemVM);
                    ItemList.Add(newItemVM);
                }
                _undo.Push(originalList);
            }
        }

        //Adds item onto list
        private void AddItem(object obj)
        {
            var newItem = new TodoItemVM();
            newItem.Priority = ItemList.Count + 1;
            ItemList.Add(newItem);

            List<TodoItemVM> items = new List<TodoItemVM>();
            foreach (TodoItemVM item in ItemList)
            {
                items.Add(item);
            }
        
            _undo.Push(items);
            _redo.Clear();
        }

        //Removes item from list
        private void DeleteItem(object obj)
        {

            if (obj is TodoItemVM)
            {
                var newItem = (TodoItemVM) obj;
                ItemList.Remove(newItem);

                List<TodoItemVM> items = new List<TodoItemVM>();
                foreach (TodoItemVM item in ItemList)
                {
                    items.Add(item);
                    item.Priority = ItemList.IndexOf(item) + 1;
                }

                _undo.Push(items);
                _redo.Clear();
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

        //Undo list changes
        private void Undo(object obj)
        {
            if (_undo.Count > 1)
            {
                List<TodoItemVM> output = _undo.Pop();
                _redo.Push(output);
                ItemList.Clear();
                List<TodoItemVM> items = _undo.Peek(); 
                foreach (TodoItemVM item in items)
                { 
                    ItemList.Add(item);
                    item.Priority = ItemList.IndexOf(item) + 1;
                }
            }
        }

        //Redo list changes
        private void Redo(object obj)
        {
            if (_redo.Count > 0)
            {
                List<TodoItemVM> output = _redo.Pop();
                _undo.Push((output)); 
                ItemList.Clear();
                List<TodoItemVM> items = output; 
                foreach (TodoItemVM item in items) 
                {
                    ItemList.Add(item);
                    item.Priority = ItemList.IndexOf(item) + 1;
                }
            }
        }
    }
}