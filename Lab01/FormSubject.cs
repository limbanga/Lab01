using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab01
{
    public partial class FormSubject : Form
    {
        public FormSubject()
        {
            InitializeComponent();
        }

        private void FormSubject_Load(object sender, EventArgs e)
        {
            switchTextBoxOnOff(false);
            switchButtonOnOff(false, buttonDelete, buttonUpdate, buttonSave);
            Env.subjects = SubjectFileHandler.getAllSubject();
            dataGridViewSubject.DataSource = Env.subjects;
        }

        private void switchTextBoxOnOff(bool isOn)
        {
            textBoxCost.Enabled = isOn;
            textBoxSubjectID.Enabled = isOn;
            textBoxSubjectName.Enabled = isOn;
            comboBoxSuperviser.Enabled = isOn;
        }

        private void switchButtonOnOff(bool isOn, params Button[] buttons)
        {
            foreach (Button btn in buttons)
            {
                btn.Enabled = isOn;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            switchTextBoxOnOff(true);
            buttonSave.Enabled = true;  
            textBoxSubjectID.Focus();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            switchTextBoxOnOff(false);
            switchButtonOnOff(false, buttonDelete, buttonUpdate, buttonSave);
            Env.subjects = SubjectFileHandler.getAllSubject();
            dataGridViewSubject.DataSource = Env.subjects;
            ClearTextBox();
        }

        private void dataGridViewSubject_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int column = e.RowIndex;

            Subject subject = Env.subjects[column];


            if (subject == null) { return; }

            //// filling data
            textBoxSubjectID.Text = subject.Id;
            textBoxSubjectName.Text = subject.Name;
            comboBoxSuperviser.Text = subject.Superviser;
            textBoxCost.Text = subject.Expense.ToString();

            // enable update delete btn
            switchButtonOnOff(true, buttonUpdate, buttonDelete);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string id = textBoxSubjectID.Text;

            if (id == null)
            {
                MessageBox.Show("Bạn chưa chọn đề tài để xóa!", "Lỗi", MessageBoxButtons.OK);
                return;
            }

            Subject? subject = SubjectFileHandler.GetSubjectById(id);

            if (subject == null)
            {
                MessageBox.Show("Đề tài bạn chọn không tồn tại hoặc đã bị xóa!", "Lỗi", MessageBoxButtons.OK);
                Env.subjects = SubjectFileHandler.getAllSubject();
                dataGridViewSubject.DataSource = Env.subjects;
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có thực sự muốn xóa đề tài {id} không?",
                                   "Xác nhận", 
                                    MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
            {
                return;
            }

            Env.subjects.RemoveAll(subject => subject.Id.Equals(id));

            bool saveSuccess = SubjectFileHandler.SaveFile();
            // alert
            if (saveSuccess)
            {
                MessageBox.Show("Lưu thành công", "Thông báo.");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi. Không thể cập nhật.", "Lỗi");
            }

            // reload data
            ClearTextBox();
            switchTextBoxOnOff(false);
            switchButtonOnOff(false, buttonDelete, buttonUpdate, buttonSave);
            Env.subjects = SubjectFileHandler.getAllSubject();
            dataGridViewSubject.DataSource = Env.subjects;
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            string id = textBoxSubjectID.Text;
            string name = textBoxSubjectName.Text;
            string superviser = comboBoxSuperviser.SelectedItem.ToString();
            double expense = Convert.ToDouble(textBoxCost.Text);

            Subject? subject = SubjectFileHandler.GetSubjectById(id);
            // add case
            if (subject == null) {
                subject = new Subject(id, name, superviser, expense);
                Env.subjects.Add(subject);
            }
            else // update case
            {
                subject.Name = name;
                subject.Superviser = superviser;
                subject.Expense = expense;
            }

            // save
            bool saveSuccess = SubjectFileHandler.SaveFile();
            // alert
            if (saveSuccess)
            {
                MessageBox.Show("Lưu thành công", "Thông báo.");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi. Không thể cập nhật.", "Lỗi");
            }

            // reload data
            Env.subjects = SubjectFileHandler.getAllSubject();
            dataGridViewSubject.DataSource = Env.subjects;
            ClearTextBox();

        }

        private void ClearTextBox()
        {
            textBoxCost.Clear();
            textBoxSubjectID.Clear();
            textBoxSubjectName.Clear();
        }
    }
}
