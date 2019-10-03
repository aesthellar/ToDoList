using System.ComponentModel;
using System.Windows;
using InternshipProj.ViewModel;

namespace InternshipProj.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM windowVM = new MainWindowVM();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = windowVM;
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            windowVM.ExitSave();
        }
    }
}
