using InternshipProj.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipProj.ViewModel
{
    public class TodoItemVM : ViewModelBase
    {
        private string _desc;
        private bool _done;
        private TodoItem _item;

        public string Desc
        {
            get { return _desc; }
            set
            {
                _desc = value;
                OnPropertyChanged();
            }
        }

        public bool Done
        {
            get { return _done; }
            set
            {
                _done = value;
                OnPropertyChanged();
            }
        }

        public TodoItemVM(string desc, bool done)
        {
            _desc = desc;
            _done = done;
            _item = new TodoItem(desc, done);
        }

        public TodoItemVM(TodoItem item)
        {
            _desc = item.Desc;
            _done = item.Done;
            _item = item;
        }

        public TodoItemVM()
        {
            _desc = string.Empty;
            _done = false;
            _item = new TodoItem();
        }


    }
}
