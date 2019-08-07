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

            if (!string.IsNullOrEmpty(Properties.Settings.Default.userSavePath))
            {
                _listVM.InitializeList(Properties.Settings.Default.userSavePath);
            }
        }
    }
}
