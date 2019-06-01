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
    public partial class FormTeacher : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly ITeacherService service;
        private int? id;

        public FormTeacher(ITeacherService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormTeacher_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    TeacherViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxFirstName.Text = view.FirstName;
                        textBoxMiddleName.Text = view.MiddleName;
                        textBoxLastName.Text = view.LastName;
                        textBoxDepartment.Text = view.Department;
                        textBoxMail.Text = view.Mail;
                        textBoxpass.Text = view.Password;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLastName.Text))
            {
                MessageBox.Show("Заполните Фамилию", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxFirstName.Text))
            {
                MessageBox.Show("Заполните Имя", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxMiddleName.Text))
            {
                MessageBox.Show("Заполните Отчество", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxMail.Text))
            {
                MessageBox.Show("Заполните Почту", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxpass.Text))
            {
                MessageBox.Show("Заполните Пароль", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new TeacherBindingModel
                    {
                        Id = id.Value,
                        FirstName = textBoxFirstName.Text,
                        LastName = textBoxLastName.Text,
                        MiddleName = textBoxMiddleName.Text,
                        Mail = textBoxMail.Text,
                        Password = textBoxpass.Text
                    });
                }
                else
                {
                    service.AddElement(new TeacherBindingModel
                    {
                        FirstName = textBoxFirstName.Text,
                        LastName = textBoxLastName.Text,
                        MiddleName = textBoxMiddleName.Text,
                        Mail = textBoxMail.Text,
                        Password = textBoxpass.Text
                    });
                }
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
