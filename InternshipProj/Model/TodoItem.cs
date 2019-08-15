namespace InternshipProj.Model
{
    public class TodoItem
    {
        public string Desc { get; set; }
        public bool Done { get; set; }

        public TodoItem(string desc, bool done)
        {
            Desc = desc;
            Done = done;
        }

        public TodoItem()
        {
            Desc = string.Empty;
            Done = false;
        }
    }
}
