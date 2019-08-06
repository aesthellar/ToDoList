namespace InternshipProj.Model
{
    public class TodoItem
    {
        private string _desc;
        private bool _done;

        public string Desc { get { return _desc; } set { _desc = value; } }
        public bool Done { get { return _done; } set { _done = value; } }

        public TodoItem(string desc, bool done)
        {
            _desc = desc;
            _done = done;
        }

        public TodoItem()
        {
            _desc = string.Empty;
            _done = false;
        }
    }
}
