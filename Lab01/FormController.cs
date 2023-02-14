using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    internal class FormController
    {
        
        public static FormStudent formStudent = new FormStudent(); // main form
        public static FormResult formResult = new FormResult();
        public static FormSubject formSubject = new FormSubject();

        private FormController() { }
        public static void Change(Form formClose, Form formOpen)
        {
            if (formClose == formOpen)
            {
                return;
            }

            formClose.Hide();
            formOpen.Show();
        }

        public static void Close(Form formClose)
        {
            if (formClose != formStudent)
            {
                formClose.Hide();
                formStudent.Show();
            }
            else
            {
                Application.Exit();
            }

        }
    }
}
