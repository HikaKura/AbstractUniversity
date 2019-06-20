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
    public partial class FormClassrooms : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IClassroomService service;

        public FormClassrooms(IClassroomService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormClassrooms_Load(object sender, EventArgs e)
        {
            LoadData();
            //if (id.HasValue)
            //{
            //    try
            //    {
            //        ClassroomViewModel view = service.GetElement(id.Value);
            //        if (view != null)
            //        {
            //            textBoxNumber.Text = view.Number;
            //            textBoxPavilion.Text = view.Pavilion;
            //            textBoxStatus.Text = view.Status;
            //            classroomCourses = view.ClassroomCourses;
            //            LoadData();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
            //       MessageBoxIcon.Error);
            //    }
            //}
            //else
            //{
            //    classroomCourses = new List<ClassroomCourseViewModel>();
            //}
        }

        private void LoadData()
        {
            try
            {
                List<ClassroomViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormClassroom>();
            if (form.ShowDialog() == DialogResult.OK)
            {
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
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        service.DelElement(id);
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
                var form = Container.Resolve<FormClassroom>();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
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
