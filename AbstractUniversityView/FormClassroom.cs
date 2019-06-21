using AbstractUniversityDAL.Interface;
using AbstractUniversityDAL.ViewModel;
using AbstractUniversityDAL.BindingModel;
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
    public partial class FormClassroom : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IClassroomService service;
        private int? id;
        private List<ClassroomCourseViewModel> classroomCourse;

        public FormClassroom(IClassroomService service)
        {
            InitializeComponent();
            this.service = service;
        }


        private void FormClassroom_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ClassroomViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxNumber.Text = view.Number.ToString();
                        textBoxStatus.Text = view.Status.ToString();
                        textBoxPov.Text = view.Pavilion.ToString();
                        classroomCourse = view.ClassroomCourses;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                classroomCourse = new List<ClassroomCourseViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (classroomCourse != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = classroomCourse;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
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
            if (string.IsNullOrEmpty(textBoxNumber.Text))
            {
                MessageBox.Show("Заполните номер аудитории", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxStatus.Text))
            {
                MessageBox.Show("Заполните статус", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPov.Text))
            {
                MessageBox.Show("Заполните номер корпуса", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            /*if (technicsEquipment == null || technicsEquipment.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }*/
            try
            {
                classroomCourse.Add(new ClassroomCourseViewModel
                {
                    ClassroomId = 3,
                    Number = Convert.ToInt32(textBoxNumber.Text)
                });
                List<ClassroomCourseBindingModel> ClassroomCourseBM = new List<ClassroomCourseBindingModel>();
                for (int i = 0; i < classroomCourse.Count; ++i)
                {
                    ClassroomCourseBM.Add(new ClassroomCourseBindingModel
                    {
                        Id = classroomCourse[i].Id,
                        ClassroomId = classroomCourse[i].ClassroomId,
                        // CourseId = classroomCourse[i].CourseId,
                        Number = classroomCourse[i].Number
                    });
                }
                /*List<ClassroomCourseViewModel> ClassroomCourseV = new List<ClassroomCourseViewModel>();
                for (int i = 0; i < classroomCourse.Count; ++i)
                {
                    ClassroomCourseV.Add(new ClassroomCourseViewModel
                    {
                        Id = classroomCourse[i].Id,
                        ClassroomId = classroomCourse[i].ClassroomId,
                        // CourseId = classroomCourse[i].CourseId,
                        Number = classroomCourse[i].Number
                    });
                }*/
                if (id.HasValue)
                {
                    service.UpdElement(new ClassroomBindingModel
                    {
                        Id = id.Value,
                        Number = Convert.ToInt32(textBoxNumber.Text),
                        Status = Convert.ToBoolean(textBoxStatus.Text),
                        Pavilion = Convert.ToInt32(textBoxPov.Text),
                        ClassroomCourses = ClassroomCourseBM
                    });
                }
                else
                {
                    service.AddElement(new ClassroomBindingModel
                    {
                        Number = Convert.ToInt32(textBoxNumber.Text),
                        Status = Convert.ToBoolean(textBoxStatus.Text),
                        Pavilion = Convert.ToInt32(textBoxPov.Text),
                        ClassroomCourses = ClassroomCourseBM
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormClassroomCourse>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.ClassroomId = id.Value;
                    }
                    classroomCourse.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        classroomCourse.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormClassroomCourse>();
                form.Model = classroomCourse[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    classroomCourse[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
