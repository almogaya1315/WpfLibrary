using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModel
{
    public class PersonViewModel : ViewModelBase
    {
        public bool IsAdult
        {
            get
            {
                if (Age >= 18)
                    return true;
                else return false;
            }
            set { }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public double Age { get; set; }

        public int Year { get; set; }

        ObservableCollection<PersonViewModel> _relatives;
        public ObservableCollection<PersonViewModel> Relatives
        {
            get
            {
                if (_relatives == null)
                    _relatives = new ObservableCollection<PersonViewModel>();
                return _relatives;
            }
            set
            {
                _relatives = value;
                RaisePropertyChanged();
            }
        }
    }
}
