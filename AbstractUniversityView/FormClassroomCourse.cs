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
    public partial class FormClassroomCourse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public ClassroomCourseViewModel Model
        {
            set { model = value; }
            get
            {
                return model;
            }
        }
        private readonly ICourseService service;
        private ClassroomCourseViewModel model;

        public FormClassroomCourse(ICourseService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormClassroomCourse_Load(object sender, EventArgs e)
        {
            try
            {
                List<CourseViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBoxCourse.DisplayMember = "Name";
                    comboBoxCourse.ValueMember = "Id";
                    comboBoxCourse.DataSource = list;
                    comboBoxCourse.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxCourse.Enabled = false;
                comboBoxCourse.SelectedValue = model.CourseId;
                textBoxName.Text = model.Name;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
