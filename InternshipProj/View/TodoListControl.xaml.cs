using InternshipProj.ViewModel;
using System.Windows.Controls;

namespace InternshipProj.View
{
    /// <summary>
    /// Interaction logic for TodoListControl.xaml
    /// </summary>
    public partial class TodoListControl : UserControl
    {
        private TodoListVM _listVM;

        public TodoListControl()
        {
            _listVM = new TodoListVM();
            InitializeComponent();
            DataContext = _listVM;
        }

        private void ScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
