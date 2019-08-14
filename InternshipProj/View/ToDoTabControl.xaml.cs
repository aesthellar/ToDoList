using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            DataContext = _tabList;
            InitializeComponent();

            if (!string.IsNullOrEmpty(Properties.Settings.Default.userSavePath))
            {
                _tabList.InitializeList(Properties.Settings.Default.userSavePath);
            }
        }
        public void OnClose()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.userSavePath))
            {
                _tabList.ExitSave(Properties.Settings.Default.userSavePath);
            }
        }
    }
}
