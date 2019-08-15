using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using InternshipProj.ViewModel;

namespace InternshipProj.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            var tabControlVM = TabControl.DataContext as ListTabsVM;
            if (tabControlVM == null)
            {
                return;
            }
            tabControlVM.ExitSave(Properties.Settings.Default.userSavePath);
        }
    }
}
