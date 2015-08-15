using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using WpfApplication1;
using WpfApplication1.Annotations;

namespace DefiningCustomDataFields
{
    public class Employee : INotifyPropertyChanged
    {
        private string _ipText;
        private IpAddressPresentation _ipValue;

        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string Occupation
        {
            get;
            set;
        }
        public DateTime StartingDate
        {
            get;
            set;
        }
        public bool IsMarried
        {
            get;
            set;
        }

        public int Salary
        {
            get;
            set;
        }
        public Gender Gender
        {
            get;
            set;
        }

        public string IpText
        {
            get { return _ipText; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value != this._ipText)
                {
                    this._ipText = value;
                    this.ParseIpString(value);
                    this.OnPropertyChanged("IpText");
                }
            }
        }

        private void ParseIpString(string ipText)
        {
            string[] parts = ipText.Split(new char[] { '.' });

            byte partA = 0, partB = 0, partC = 0, partD = 0;

            if ((byte.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out partA) &&
            byte.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out partB) &&
            byte.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out partC) &&
            byte.TryParse(parts[3], NumberStyles.Integer, CultureInfo.InvariantCulture, out partD)) == false)
            {
                throw new ValidationException("Incorrect format of Ip address!");
            };

            this.IpValue = new IpAddressPresentation(partA, partB, partC, partD);
        }    

        public IpAddressPresentation IpValue
        {
            get { return _ipValue; }
            set 
            {
                if (value != this._ipValue)
                {
                    _ipValue = value;
                    this.OnPropertyChanged("IpValue");
                }
            }
        }

        public IPAddress IpAddress { get; set; }

        public Employee()
        { }

        public static ObservableCollection<Employee> GetEmployees()
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            employees.Add(new Employee() { FirstName = "Sarah", LastName = "Blake", Occupation = "Supplied Manager", StartingDate = new DateTime(2005, 04, 12), IsMarried = true, Salary = 3500, Gender = Gender.Female });
            employees.Add(new Employee() { FirstName = "Jane", LastName = "Simpson", Occupation = "Security", StartingDate = new DateTime(2008, 12, 03), IsMarried = true, Salary = 2000, Gender = Gender.Female });
            employees.Add(new Employee() { FirstName = "John", LastName = "Peterson", Occupation = "Consultant", StartingDate = new DateTime(2005, 04, 12), IsMarried = false, Salary = 2600, Gender = Gender.Male });
            employees.Add(new Employee() { FirstName = "Peter", LastName = "Bush", Occupation = "Cashier", StartingDate = new DateTime(2005, 04, 12), IsMarried = true, Salary = 2300, Gender = Gender.Male });

            foreach (var employee in employees)
            {
                employee.IpText = "192.168.1.100";
            }
            return employees;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum Gender
    {
        Female,
        Male
    }

    public class IPAddress : INotifyPropertyChanged
    {
        public string IpValue { get; set; }

        public string IpText { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
