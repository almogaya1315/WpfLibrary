using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Main.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public UserControl Control { get; set; }

        PersonViewModel _selectedPerson;
        PersonViewModel SelectedPerson
        {
            get
            {
                if (_selectedPerson == null)
                    _selectedPerson = new PersonViewModel();
                return _selectedPerson;
            }
            set { _selectedPerson = value; }
        }

        ObservableCollection<PersonViewModel> _people;
        public ObservableCollection<PersonViewModel> People
        {
            get
            {
                if (_people == null)
                    _people = new ObservableCollection<PersonViewModel>();
                return _people;
            }
            set
            {
                _people = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<PersonViewModel> _siblings;
        public ObservableCollection<PersonViewModel> Siblings
        {
            get
            {
                if (_siblings == null)
                    _siblings = new ObservableCollection<PersonViewModel>();
                return _siblings;
            }
            set
            {
                _siblings = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            Control = new DataControl();

            var lior = new PersonViewModel() { Id = 1, Name = "lior", Age = 32, Year = 1985 };
            var keren = new PersonViewModel() { Id = 2, Name = "keren", Age = 33, Year = 1984 };
            var gaya = new PersonViewModel() { Id = 3, Name = "gaya", Age = 4, Year = 2013 };
            var almog = new PersonViewModel() { Id = 4, Name = "almog", Age = 2, Year = 2015 };

            lior.Relatives.Add(keren);
            lior.Relatives.Add(gaya);
            lior.Relatives.Add(almog);

            keren.Relatives.Add(lior);
            keren.Relatives.Add(gaya);
            keren.Relatives.Add(almog);

            gaya.Relatives.Add(lior);
            gaya.Relatives.Add(keren);
            gaya.Relatives.Add(almog);

            almog.Relatives.Add(lior);
            almog.Relatives.Add(keren);
            almog.Relatives.Add(gaya);

            People.Add(lior);
            People.Add(keren);
            People.Add(gaya);
            People.Add(almog);

            Siblings.Add(lior);
            Siblings.Add(keren);
            Siblings.Add(gaya);
            Siblings.Add(almog);
        }
    }
}