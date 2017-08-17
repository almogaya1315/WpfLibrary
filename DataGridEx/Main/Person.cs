using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Person
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

        IList<Person> _relatives;
        public IList<Person> Relatives
        {
            get
            {
                if (_relatives == null)
                    _relatives = new List<Person>();
                return _relatives;
            }
            set
            {
                _relatives = value;
            }
        }
    }
}
