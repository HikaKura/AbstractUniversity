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

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
