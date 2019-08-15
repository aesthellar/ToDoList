using InternshipProj.Model;

namespace InternshipProj.ViewModel
{
    public class TodoItemVM : ViewModelBase
    {
        public string Desc
        {
            get { return Item.Desc; }
            set
            {
                Item.Desc = value;
                OnPropertyChanged();
            }
        }

        public bool Done
        {
            get { return Item.Done; }
            set
            {
                Item.Done = value;
                OnPropertyChanged();
            }
        }

        public TodoItem Item { get; }

        public TodoItemVM(TodoItem item)
        {
            Item = item;
        }

        public TodoItemVM()
        {
            Item = new TodoItem();
        }
    }
}
