using System.Windows.Controls;
using InternshipProj.ViewModel;

namespace InternshipProj.View
{
    /// <summary>
    /// Interaction logic for ToDoTabControl.xaml
    /// </summary>
    public partial class ToDoTabControl : UserControl
    {
        private ListTabsVM _tabList;

        public ToDoTabControl()
        {
            _tabList = new ListTabsVM();

            if (!string.IsNullOrEmpty(Properties.Settings.Default.userSavePath))
            {
                _tabList.InitializeList(Properties.Settings.Default.userSavePath);
            }
            else
            {
                _tabList.New(null);
            }

            DataContext = _tabList;

            InitializeComponent();
        }

    }
}
