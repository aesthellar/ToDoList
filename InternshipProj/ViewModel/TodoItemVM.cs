using InternshipProj.Model;

namespace InternshipProj.ViewModel
{
    public class TodoItemVM : ViewModelBase
    {
        private TodoItem _item;

        public string Desc
        {
            get { return _item.Desc; }
            set
            {
                _item.Desc = value;
                OnPropertyChanged();
            }
        }

        public bool Done
        {
            get { return _item.Done; }
            set
            {
                _item.Done = value;
                OnPropertyChanged();
            }
        }

        public TodoItem Item
        {
            get { return _item; }
        }

        public TodoItemVM(string desc, bool done)
        {
            _item = new TodoItem(desc, done);
        }

        public TodoItemVM(TodoItem item)
        {
            _item = item;
        }

        public TodoItemVM()
        {
            _item = new TodoItem();
        }
    }
}
