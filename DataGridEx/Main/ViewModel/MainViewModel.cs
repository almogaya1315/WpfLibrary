using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Main.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public UserControl Control { get; set; }

        ObservableCollection<Person> _people;
        public ObservableCollection<Person> People
        {
            get
            {
                if (_people == null)
                    _people = new ObservableCollection<Person>();
                return _people;
            }
            set
            {
                _people = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            Control = new DataControl();

            var lior = new Person() { Id = 1, Name = "lior", Age = 32, Year = 1985 };
            var keren = new Person() { Id = 2, Name = "keren", Age = 33, Year = 1984 };
            var gaya = new Person() { Id = 3, Name = "gaya", Age = 4, Year = 2013 };
            var almog = new Person() { Id = 4, Name = "almog", Age = 2, Year = 2015 };

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
        }
    }
}