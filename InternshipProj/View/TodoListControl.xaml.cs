using InternshipProj.ViewModel;
using System.Windows.Controls;

namespace InternshipProj.View
{
    /// <summary>
    /// Interaction logic for TodoListControl.xaml
    /// </summary>
    public partial class TodoListControl : UserControl
    {
        public TodoListControl()
        {
            InitializeComponent();

            //if (!string.IsNullOrEmpty(Properties.Settings.Default.userSavePath))
            //{
            //    _listVM.InitializeList(Properties.Settings.Default.userSavePath);
            //}
        }

        public void OnClose()
        {
            //if (!string.IsNullOrEmpty(Properties.Settings.Default.userSavePath))
            //{
            //    _listVM.ExitSave(Properties.Settings.Default.userSavePath);
            //}
        }
    }
}
