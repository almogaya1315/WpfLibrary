using GalaSoft.MvvmLight;
using Main.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModel
{
    public class DataViewModel : ViewModelBase, IControlViewModel
    {
        public string Name { get { return "DataViewModel"; } }

        public DataViewModel()
        {
            Columns = ColumnViewModel.SetColumns();

            People = PersonViewModel.SetInitialList();
        }

        List<ColumnViewModel> _columns;
        List<ColumnViewModel> Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<PersonViewModel> _people;
        public ObservableCollection<PersonViewModel> People
        {
            get { return _people; }
            set
            {
                _people = value;
                RaisePropertyChanged();
            }
        }
    }
}
