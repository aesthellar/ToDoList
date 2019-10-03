namespace InternshipProj.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        private ListTabsVM _listTabs = new ListTabsVM();

        public ListTabsVM ListTabs
        {
            get { return _listTabs;}
            set
            {
                _listTabs = value;
                OnPropertyChanged();
            }
        }

        public void ExitSave()
        {
            ListTabs?.ExitSave(Properties.Settings.Default.userSavePath);
        }

        public MainWindowVM()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.userSavePath))
            {
                ListTabs.FileName = Properties.Settings.Default.userSavePath;
                ListTabs.InitializeList(ListTabs.FileName);
            }
            else
            {
                ListTabs.FileName = "New File";
                ListTabs.New(null);
            }
        }
    }
}
