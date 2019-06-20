using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.Interface;
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
    public partial class FormStudy : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IStudyService service;
        private readonly ITeacherService serviceT;
        private int? id;

        public FormStudy(IStudyService service, ITeacherService serviceT)
        {
            InitializeComponent();
            this.service = service;
            this.serviceT = serviceT;
        }

        private void FormTeacher_Load(object sender, EventArgs e)
        {
                try
                {
                    List<TeacherViewModel> listT = serviceT.GetList();
                    if (listT != null)
                    {
                        comboBoxTeacher.DisplayMember = "LastName";
                        comboBoxTeacher.ValueMember = "Id";
                        comboBoxTeacher.DataSource = listT;
                        comboBoxTeacher.SelectedItem = null;
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
                MessageBox.Show("Заполните поле Название обучения", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxOrientation.Text))
            {
                MessageBox.Show("Заполните поле Направление", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxTeacher.SelectedValue == null)
            {
                MessageBox.Show("Выберите преподавателя", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                service.AddElement(new StudyBindingModel
                {
                    TeacherId = Convert.ToInt32(comboBoxTeacher.SelectedValue),
                    Name = textBoxName.Text,
                    Orientation = textBoxOrientation.Text
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
