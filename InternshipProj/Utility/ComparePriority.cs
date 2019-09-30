using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Forms.VisualStyles;
using InternshipProj.ViewModel;

namespace InternshipProj.Utility
{
    public class ComparePriority : IComparer<int>
    {
        private ObservableCollection<TodoItemVM> ItemList { get; set; }

        public ComparePriority(ObservableCollection<TodoItemVM> list)
        {
            ItemList = list;
        }

        public int Compare(int index, int priority)
        {

            if (priority == index)
            {
                return index;
            }
            else if (priority < index)
            {
                return ShiftUpInd(index, priority);
            }
            else
            {
                return ShiftDownInd(index, priority);
            }
        }

        public int ShiftUpInd(int index, int priority)
        {

            for(int i = index; i < priority; i++)
            {

            }
        }

        public int ShiftDownInd(int index, int priority)
        {

        }
    }
}