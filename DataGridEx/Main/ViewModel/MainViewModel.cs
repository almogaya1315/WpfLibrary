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

            People.Add(new Person() { Id = 1, Name = "lior", Age = 32, Year = 1985 });
            People.Add(new Person() { Id = 2, Name = "keren", Age = 33, Year = 1984 });
            People.Add(new Person() { Id = 3, Name = "gaya", Age = 4, Year = 2013 });
            People.Add(new Person() { Id = 4, Name = "almog", Age = 2, Year = 2015 });
        }
    }
}