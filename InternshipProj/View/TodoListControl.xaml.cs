using InternshipProj.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}
