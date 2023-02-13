using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    public class Subject
    {
        private string id;
        private string name;
        private string superviser;
        private double expense;


        // getters setters
        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Superviser { get => superviser; set => superviser = value; }
        public double Expense { get => expense; set => expense = value; }

        public Subject(string id, string name, string superviser, double expense)
        {
            this.id = id;
            this.name = name;
            this.superviser = superviser;
            this.expense = expense;
        }

        public override string? ToString()
        {
            return Name;
        }

        public string? ToString(bool forFile)
        {
            return string.Format("{0};{1};{2};{3}",
                id, name, superviser, expense);
        }
    }
}
