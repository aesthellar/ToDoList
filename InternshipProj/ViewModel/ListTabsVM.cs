using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using InternshipProj.Utility;

namespace InternshipProj.ViewModel
{
    public class ListTabsVM : ViewModelBase
    {
        private ObservableCollection<TodoListVM> _tabLists;
        private RelayCommand _deleteList, _addList;

        public ObservableCollection<TodoListVM> TabLists
        {
            get { return _tabLists; }
        }

        public RelayCommand DeleteList
        {
            get { return _deleteList; }
        }

        public RelayCommand AddList
        {
            get { return _addList; }
        }

        public ListTabsVM()
        {
            _tabLists = new ObservableCollection<TodoListVM>();
            TodoListVM list = new TodoListVM();
            _tabLists.Add(list);
            _deleteList = new RelayCommand(Delete);
            _addList = new RelayCommand(Add);
        }


        public ListTabsVM(List<TodoListVM> lists)
        {
            BuildViewModels(lists);
        }

        private void BuildViewModels(List<TodoListVM> lists)
        {
            if (lists != null)
            {
                foreach (TodoListVM list in lists)
                {
                    _tabLists.Add(list);
                }
            }
        }

        private void Add(object obj)
        {
            TodoListVM list = new TodoListVM();
            TabLists.Add(list);
            Console.Write(TabLists.Count);
        }

        private void Delete(object obj)
        {
            if (obj is TodoListVM)
            {
                if (TabLists.Count != 1)
                {
                    var result = MessageBox.Show("You are about to delete a list. Continue?", "List Delete Warning", MessageBoxButton.OKCancel);
                    if (!result.Equals(MessageBoxResult.OK))
                    {
                        return; //if cancel, return
                    }
                    else
                    {
                        var list = (TodoListVM)obj;
                        TabLists.Remove(list);
                        int count = TabLists.Count;
                    }
                    
                }
                else
                {
                    var result = MessageBox.Show("Must have at least 1 list", "1 List Warning", MessageBoxButton.OK);

                }
            }
        }


    }
}