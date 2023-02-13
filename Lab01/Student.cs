using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    public class Student
    {
        private string id;
        private string fullName;    
        private string town;
        private DateTime dob;
        private double score;
        
        // getters and setters
        public string Id { get => id; set => id = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string Town { get => town; set => town = value; }
        public DateTime Dob { get => dob; set => dob = value; }
        public double Score { get => score; set => score = value; }


        public Student(string id, string fullName, string town, DateTime dob, double score)
        {
            this.id = id;
            this.fullName = fullName;
            this.town = town;
            this.dob = dob;
            this.score = score;
        }

        public override string? ToString()
        {
            return fullName;
        }

        public string? GetStringForFile()
        {
            return string.Format(
                "{0};{1};{2};{3};{4}",
                id, fullName, town, dob.ToString("dd/MM/yyyy"), score);
        }
    }
}
