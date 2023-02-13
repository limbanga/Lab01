using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    public class StudentFileHandler
    {
        private StudentFileHandler() { }

        public static List<Student> getAllStudent()
        {
            List<Student> result = new List<Student>();

            string[] students = System.IO.File.ReadAllLines(Env.dataStudentFileName);
            for (int i = 0; i < students.Length; i++)
            {
                string currentLine = students[i];
                string[] info = currentLine.Split(";");

                string id = info[0];
                string fullName = info[1];
                string town = info[2];
                string str_dayOfBirth = info[3];
                DateTime dayOfBirth = DateTime.Parse(str_dayOfBirth);
                double score = double.Parse(info[4]);
                
                Student student = new Student(id, fullName, town, dayOfBirth, score);
                result.Add(student);
            }

            return result;
        }

        public static bool SaveFile()
        {
            try
            {
                using (TextWriter tw = new StreamWriter(Env.dataStudentFileName))
                {
                    foreach (Student student in Env.students)
                    {
                        tw.WriteLine(student.GetStringForFile());
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

        public static Student? GetStudentByID(string _id)
        {
            if(Env.students.Count <= 0)
            {
                Env.students = getAllStudent();
            }

            Student? student = Env.students.SingleOrDefault(st => st.Id.Equals(_id));
            return student;
        }
    }
}
