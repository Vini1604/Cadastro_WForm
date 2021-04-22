using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro {
    class Person {
        public string Name { get; set; }
        public double Salary { get; set; }

        public Person(string name, double salary) {
            Name = name;
            Salary = salary;
        }
    }
}
