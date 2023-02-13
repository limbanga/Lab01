using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    public class ResultFileHandler
    {
        private ResultFileHandler() { }

        public static List<Result> getAllResult()
        {
            List<Result> r = new List<Result>();

            string[] lines = System.IO.File.ReadAllLines(Env.dataResultFileName);
            for (int i = 0; i < lines.Length; i++)
            {
                string currentLine = lines[i];
                string[] info = currentLine.Split(";");

                string studentID = info[0];
                Student? student = StudentFileHandler.GetStudentByID(studentID);
                if (student == null)
                {

                }

                string subjectID = info[1];
                Subject? subject = SubjectFileHandler.GetSubjectById(subjectID);
                // MessageBox.Show(subject.ToString());
                if (subject == null)
                {

                }
                string place = info[2];
                int distance = Convert.ToInt32(info[3]);
                double score = Convert.ToDouble(info[4]);

                Result result = new Result(student, subject, place, distance, score);

                r.Add(result);
            }

            return r;
        }

        public static bool SaveFile()
        {
            try
            {
                using (TextWriter tw = new StreamWriter(Env.dataResultFileName))
                {
                    foreach (Result re in Env.results)
                    {
                        tw.WriteLine(re.GetStringForFile());
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

        public static Result? GetResultByID(string studentID, string subjectID)
        {
            return Env.results.SingleOrDefault(r => r.Subject.Id.Equals(subjectID) && r.Student.Id.Equals(subjectID));
        }

    }
}
