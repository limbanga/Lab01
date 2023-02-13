using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    internal class SubjectFileHandler
    {
        private SubjectFileHandler() { }

        public static List<Subject> getAllSubject()
        {
            List<Subject> result = new List<Subject>();

            string[] subjects = System.IO.File.ReadAllLines(Env.dataSubjectFileName);
            for (int i = 0; i < subjects.Length; i++)
            {
                string currentLine = subjects[i];
                string[] info = currentLine.Split(";");

                string id = info[0];
                string subjectName = info[1];
                string superviser = info[2];
                string stringExpense = info[3];
                stringExpense = stringExpense.Replace(".", "");
                double expense = double.Parse(stringExpense);

                Subject student = new Subject(id, subjectName, superviser, expense);
                result.Add(student);
            }

            return result;
        }

        public static bool SaveFile()
        {
            try
            {
                using (TextWriter tw = new StreamWriter(Env.dataSubjectFileName))
                {
                    foreach (Subject subject in Env.subjects)
                    {
                        tw.WriteLine(subject.ToString(true));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public static Subject? GetSubjectById(string id)
        {
            if (Env.subjects.Count <= 0)
            {
                Env.subjects = getAllSubject();
            }
            return Env.subjects.SingleOrDefault(subject => subject.Id.Equals(id));
        }
    }
}
