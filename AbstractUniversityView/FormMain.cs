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
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IMainService service;

        public FormMain(IMainService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void LoadData()
        {
            try
            {
                List<CourseViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[6].Visible = false;
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

        private void преподавателиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormTeachers>();
            form.ShowDialog();
            LoadData();
        }

        private void аудиторииToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void обучениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormStudies>();
            form.ShowDialog();
            LoadData();
        }

        private void курсыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonEndCourse_Click(object sender, EventArgs e)
        {

        }

        private void buttonGoingCourse_Click(object sender, EventArgs e)
        {

        }

        private void buttonCreateCourse_Click(object sender, EventArgs e)
        {

        }
    }
}
