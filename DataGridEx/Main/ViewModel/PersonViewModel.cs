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
        CellViewModel _isAdult = new CellViewModel(false);
        public CellViewModel IsAdult
        {
            get
            {
                double age = default(double);
                if (double.TryParse(Age.Value.ToString(), out age))
                {
                    if (age >= 18)
                        _isAdult.Value = true;
                    else _isAdult.Value = false;
                }
                return _isAdult;
            }
        }

        public CellViewModel Id { get; set; }

        public CellViewModel Name { get; set; }

        public CellViewModel Age { get; set; }

        public CellViewModel Year { get; set; }

        public CellViewModel Relatives { get; set; }

        public static ObservableCollection<PersonViewModel> SetInitialList()
        {
            var lior = new PersonViewModel()
            {
                Id = new CellViewModel(1),
                Name = new CellViewModel("lior"),
                Age = new CellViewModel(32),
                Year = new CellViewModel(1985)
            };
            var keren = new PersonViewModel()
            {
                Id = new CellViewModel(2),
                Name = new CellViewModel("keren"),
                Age = new CellViewModel(33),
                Year = new CellViewModel(1984)
            };
            var gaya = new PersonViewModel()
            {
                Id = new CellViewModel(3),
                Name = new CellViewModel("gaya"),
                Age = new CellViewModel(4),
                Year = new CellViewModel(2013)
            };
            var almog = new PersonViewModel()
            {
                Id = new CellViewModel(4),
                Name = new CellViewModel("almog"),
                Age = new CellViewModel(2),
                Year = new CellViewModel(2015)
            };

            var relatives = new ObservableCollection<PersonViewModel>() { keren, gaya, almog };
            lior.Relatives = new CellViewModel(relatives);

            relatives.Clear();
            relatives.Add(lior);
            relatives.Add(gaya);
            relatives.Add(almog);
            keren.Relatives = new CellViewModel(relatives);

            relatives.Clear();
            relatives.Add(lior);
            relatives.Add(keren);
            relatives.Add(almog);
            gaya.Relatives = new CellViewModel(relatives);

            relatives.Clear();
            relatives.Add(lior);
            relatives.Add(keren);
            relatives.Add(gaya);
            almog.Relatives = new CellViewModel(relatives);

            var people = new ObservableCollection<PersonViewModel>();

            people.Add(lior);
            people.Add(keren);
            people.Add(gaya);
            people.Add(almog);

            return people;
        }
    }
}
