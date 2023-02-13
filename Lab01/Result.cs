using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    public class Result
    {
        private Student student;
        private Subject subject;
        private string place; 
        private int distance;
        private double score;
        public Student Student { get => student; set => student = value; }
        public Subject Subject { get => subject; set => subject = value; }
        public string Place { get => place; set => place = value; }
        public int Distance { get => distance; set => distance = value; }
        public double Score { get => score; set => score = value; }

        public Result(Student student, Subject subject, string place, int distance, double score)
        {
            this.Student = student;
            this.Subject = subject;
            this.Place = place;
            this.Distance = distance;
            this.Score = score;
        }

        public string? GetStringForFile()
        {
            return string.Format(
                "{0};{1};{2};{3};{4}",
                student.Id, Subject.Id, place, distance, score);
        }
    }
}
