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
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IMainService service;
        private readonly IBackUpService serviceB;
        private readonly IReportService rService;

        public FormMain(IMainService service, IBackUpService serviceB, IReportService rService)
        {
            InitializeComponent();
            this.service = service;
            this.serviceB = serviceB;
            this.rService = rService;
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
                    dataGridView.Columns[10].Visible = false;
                    dataGridView.Columns[11].Visible = false;
                    dataGridView.Columns[7].AutoSizeMode =
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
            var form = Container.Resolve<FormClassrooms>();
            form.ShowDialog();
            LoadData();
        }

        private void обучениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormStudies>();
            form.ShowDialog();
            LoadData();
        }

        private void buttonEndCourse_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    service.CourseFinished(new CourseBindingModel { Id = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void buttonGoingCourse_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    service.CourseGoing(new CourseBindingModel { Id = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void buttonCreateCourse_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCreateCourse>();
            form.ShowDialog();
            LoadData();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
           /* List<CourseViewModel> list = service.GetList();
            if (service.Check(new CourseBindingModel())) {

            }*/
        }

        private void сохранитьБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                serviceB.BackUpForAdmin();
                serviceB.BackUpForClent();
                MessageBox.Show("Успешно отправлено на почту", "Успех", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
            }
        }

        private void отчетПоКурсамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var model = (new ReportBindingModel
                {
                    Email = "kima.bright@mail.ru",
                    FileName = @"D:\University\TP\test.pdf",
                    DateFrom = new DateTime(2018, 1, 1),
                    DateTo = DateTime.Now
                });
                rService.getCourseList(model);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы должны быть залогинены!\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show("Сохранение и отправка прошли успешно", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
