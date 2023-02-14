using System.Drawing;
using System.Text.RegularExpressions;

namespace Lab01
{
    public partial class FormStudent : Form
    {
        public FormStudent()
        {
            InitializeComponent();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void FormStudent_Load(object sender, EventArgs e)
        {
            string[] townOptions = { 
                "Sài gòn",
                "Thanh Hóa",
                "Nghệ An",
                "Long An", 
                "Quảng ngãi" 
            };

            // load data
            Env.students = StudentFileHandler.getAllStudent();
            dataGridViewStudent.DataSource = Env.students;
            
            switchTextBoxOnOff(false);
            setTownOption(townOptions);
            switchButtonOnOff(false,
                buttonDelete,   
                buttonUpdate,   
                buttonSave
                );
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            switchTextBoxOnOff(true); // enable text box
            clearTextBox();
            textBoxStudentID.Focus();
            buttonSave.Enabled = true;
        }
        private void textBoxScore_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char key = e.KeyChar;
            if (!(Char.IsControl(key) || Char.IsDigit(key) || key == '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((key == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void clearTextBox()
        {
            textBoxStudentID.Text = "";
            textBoxFullName.Text = "";
            textBoxScore.Text = "";
        }

        private void switchButtonOnOff(bool isOn, params Button[] buttons)
        {
            foreach(Button btn in buttons)
            {
                btn.Enabled = isOn;
            }
        }

        private void switchTextBoxOnOff(bool isOn)
        {
            textBoxStudentID.Enabled = isOn;
            textBoxFullName.Enabled= isOn;
            comboBoxTown.Enabled= isOn;
            textBoxScore.Enabled= isOn; 
            dateTimePickerDOB.Enabled= isOn;
        }

        private void setTownOption(string[] options)
        {
            comboBoxTown.DataSource = options.ToList();
        }

        private void textBoxScore_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // fetch input
            string id = textBoxStudentID.Text;
            string fullName = textBoxFullName.Text;
            string town = (string) comboBoxTown.SelectedItem;
            DateTime dob = dateTimePickerDOB.Value;
            string stringScore = textBoxScore.Text;
            double score = Double.Parse(stringScore);

            Student? student =  StudentFileHandler.GetStudentByID(id);

            // update case, student already exists
            if(student != null)
            {
                student.FullName= fullName;
                student.Town= town;
                student.Dob = dob;
                student.Score= score;
            }
            else // add new case
            {
                student = new Student(id, fullName, town, dob, score);
                // add to global var
                Env.students.Add(student);
            }

            // save
            bool saveSuccess = StudentFileHandler.SaveFile();
            // alert
            if(saveSuccess)
            {
                MessageBox.Show("Lưu thành công", "Thông báo.");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi. Không thể cập nhật.", "Lỗi");
            }

            // reload data
            Env.students = StudentFileHandler.getAllStudent();
            dataGridViewStudent.DataSource = Env.students;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            string[] townOptions = {
                "Sài gòn",
                "Thanh Hóa",
                "Nghệ An",
                "Long An",
                "Quảng ngãi"
            };

            // load data
            Env.students = StudentFileHandler.getAllStudent();
            dataGridViewStudent.DataSource = Env.students;

            switchTextBoxOnOff(false);
            setTownOption(townOptions);
            switchButtonOnOff(false,
                buttonDelete,
                buttonUpdate,
                buttonSave
                );
        }

        private void dataGridViewStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int column = e.RowIndex;

            Student st = Env.students[column];

            if( st == null ) { return; }

            // filling data
            textBoxStudentID.Text = st.Id;
            textBoxFullName.Text = st.FullName;
            comboBoxTown.Text = st.Town;
            dateTimePickerDOB.Value = st.Dob;
            textBoxScore.Text = st.Score.ToString();

            // enable update delete btn
            switchButtonOnOff(true, buttonUpdate, buttonDelete);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // get id
            string id = textBoxStudentID.Text;
            // comfirm
            DialogResult result = MessageBox.Show(
                "Bạn có muốn xóa sinh viên có mã số " + id+ " ?",
                "Xác nhận xóa", MessageBoxButtons.YesNo);

            if( result != DialogResult.Yes )
            {
                return;
            }

            // remove and save to file
            Env.students.RemoveAll(st => st.Id.Equals(id));

            bool saveSuccess = StudentFileHandler.SaveFile();
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
            Env.students = StudentFileHandler.getAllStudent();
            dataGridViewStudent.DataSource = Env.students;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            switchTextBoxOnOff(true);
            // ID can't change
            textBoxStudentID.Enabled = false;
            // Enable to save
            buttonSave.Enabled = true;
            textBoxFullName.Focus();
        }
        private void buttonPreviewPrint_Click(object sender, EventArgs e)
        {
            //PrintDialog printDialog = new PrintDialog();
            //printDialog.ShowDialog();
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