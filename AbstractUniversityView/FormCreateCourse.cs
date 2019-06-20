using AbstractUniversityDAL.Interface;
using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace AbstractUniversityView
{
    public partial class FormCreateCourse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IStudyService serviceS;
        private readonly IMainService service;

        public FormCreateCourse(IStudyService serviceS, IMainService service)
        {
            InitializeComponent();
            this.service = service;
            this.serviceS = serviceS;
        }

        private void FormCreateCourse_Load(object sender, EventArgs e)
        {
            try
            {
                List<StudyViewModel> listS = serviceS.GetList();
                if (listS != null)
                {
                    comboBoxStudy.DisplayMember = "Name";
                    comboBoxStudy.ValueMember = "Id";
                    comboBoxStudy.DataSource = listS;
                    comboBoxStudy.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните поле Название", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxContent.Text))
            {
                MessageBox.Show("Заполните поле Содержание", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество студентов", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStudy.SelectedValue == null)
            {
                MessageBox.Show("Выберите Обучение", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                service.NotBeginCourse(new CourseBindingModel
                {
                    Name = textBoxName.Text,
                    Content = textBoxContent.Text,
                    Student_Count = Convert.ToInt32(textBoxCount.Text),
                    StudyId = Convert.ToInt32(comboBoxStudy.SelectedValue)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
