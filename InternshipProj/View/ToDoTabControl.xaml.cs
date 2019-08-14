﻿using System;
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
            InitializeComponent();
            DataContext = _tabList;

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

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
