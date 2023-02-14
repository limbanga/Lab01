using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Lab01
{

    public partial class FormResult : Form
    {
        private Result currentResult = null;
        public FormResult()
        {
            InitializeComponent();
        }

        private void FormResult_Load(object sender, EventArgs e)
        {
            initForm();
        }

        private void initForm()
        {

            loadResultData();
            BindComboboxStudent();
            BindComboboxSubject();
            SwitchTextBox(false);
            SwitchButton(false, buttonUpdate, buttonDelete, buttonSave, buttonCancel);
            ClearTextBox();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SwitchTextBox(true);
            SwitchButton(true, buttonSave, buttonCancel);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            initForm();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Student? student = (Student) comboBoxStudent.SelectedItem;
            if (student == null)
            {

            }

            Subject? subject = (Subject) comboBoxSubject.SelectedItem;
            if (subject == null)
            {

            }
            string place = textBoxPlace.Text;
            int distance = Convert.ToInt32(textBoxDistance.Text);
            double score = Convert.ToDouble(textBoxScore.Text);

            Result? result = ResultFileHandler.GetResultByID(student.Id, subject.Id);
            if (result == null)
            {
                result = new Result(student, subject, place, distance, score);
                Env.results.Add(result);
            }
            else
            {
                result.Student= student;    
                result.Subject = subject;
                result.Place = place;
                result.Distance = distance;
                result.Score = score;
            }

            bool isSuccess = ResultFileHandler.SaveFile();

            if (isSuccess)
            {
                MessageBox.Show("Cập nhật thành công.", "Thông báo");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi. Không thể cập nhật.", "Lỗi");
            }
            initForm();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void loadResultData()
        {
            Env.results = ResultFileHandler.getAllResult();
            dataGridViewResult.DataSource = Env.results;
        }

        private void SwitchTextBox(bool isEnable)
        {
            foreach(Control control in groupBox2.Controls) 
            {
                if (control is TextBox || control is ComboBox)
                {
                    control.Enabled = isEnable;
                }
            }
        }
        private void BindComboboxStudent()
        {
            Env.students = StudentFileHandler.getAllStudent();
            comboBoxStudent.DataSource = Env.students;
        }
        private void BindComboboxSubject()
        {
            Env.subjects = SubjectFileHandler.getAllSubject();
            comboBoxSubject.DataSource = Env.subjects;
        }

        private void SwitchButton(bool isOn, params Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                button.Enabled = isOn;
            }
        }

        private void ClearTextBox()
        {
            textBoxDistance.Clear();
            textBoxPlace.Clear();
            textBoxScore.Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (currentResult == null)
            {
                MessageBox.Show("Bạn chưa chọn kết quả để xóa.", "Lỗi");
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn muốn xóa kết quả {currentResult} không?", "Xác nhận xóa", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.No) { return; }

            Env.results.Remove(currentResult);

            bool isSuccess = ResultFileHandler.SaveFile();

            if (isSuccess)
            {
                MessageBox.Show("Lưu thành công");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi.");
            }

            initForm();

        }

        private void dataGridViewResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            Result result = Env.results[rowIndex];
            currentResult = result;
            if (result == null) { return; }

            comboBoxStudent.SelectedItem = result.Student;
            comboBoxSubject.SelectedItem = result.Subject;


            textBoxPlace.Text = result.Place;
            textBoxDistance.Text = result.Distance.ToString();
            textBoxScore.Text = result.Score.ToString();

            SwitchButton(true, buttonUpdate, buttonDelete);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (currentResult == null)
            {
                MessageBox.Show("bao loi gi day :<");
                return;
            }

            SwitchTextBox(true);
            comboBoxStudent.SelectedItem = currentResult.Student;
            comboBoxSubject.SelectedItem = currentResult.Subject;
            //Student student = (Student) comboBoxStudent.SelectedItem;
            //Subject subject = (Subject) comboBoxSubject.SelectedItem;

            //string place = textBoxPlace.Text;
            //int distance = Convert.ToInt32(textBoxDistance.Text);
            //double score = Convert.ToDouble(textBoxScore.Text);

            //currentResult.Student = student;
            //currentResult.Subject = subject;
            //currentResult.Score = score;
            //currentResult.Distance = distance;
            //currentResult.Place = place;

            buttonSave.Enabled = true;
        }

        private void ToolStripMenuItemStudent_Click(object sender, EventArgs e)
        {
            FormController.Change(this, FormController.formStudent);
        }

        private void FormResult_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormController.Close(this);
        }

        private void ToolStripMenuItemSubject_Click(object sender, EventArgs e)
        {
            FormController.Change(this, FormController.formSubject);
        }

        private void ToolStripMenuItemResult_Click(object sender, EventArgs e)
        {
            FormController.Change(this, FormController.formResult);
        }
    }
}
