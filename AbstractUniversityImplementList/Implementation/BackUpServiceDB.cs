using AbstractUniversityDAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AbstractUniversity;

namespace AbstractUniversityImplementList.Implementation
{
    public class BackUpServiceDB : IBackUpService
    {
        private UniversityDbContext context;

        public BackUpServiceDB(UniversityDbContext context)
        {
            this.context = context;
        }

        public void BackUpForAdmin()
        {
            var ms = context.Classrooms.ToList();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Classroom>));
            using (FileStream fs = new FileStream("classrooms.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, ms);
            }

            var cc = context.ClassroomCourses.ToList();
            jsonFormatter = new DataContractJsonSerializer(typeof(List<ClassroomCourse>));

            using (FileStream fs = new FileStream("classroomcourses.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, cc);
            }

            var ps = context.Courses.ToList();
            jsonFormatter = new DataContractJsonSerializer(typeof(List<Course>));

            using (FileStream fs = new FileStream("courses.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, ps);
            }

            SendEmail(@"kima.bright@mail.ru", "Бекап бд для админа", "", new string[] { "classrooms.json", "classroomcourses.json", "courses.json" });
        }

        public void BackUpForClent()
        {
            var os = context.Teachers.ToList();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Teacher>));
            using (FileStream fs = new FileStream("teachers.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, os);
            }

            var ops = context.Requests.ToList();
            jsonFormatter = new DataContractJsonSerializer(typeof(List<Request>));

            using (FileStream fs = new FileStream("requests.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, ops);
            }

            var ps = context.Studies.ToList();
            jsonFormatter = new DataContractJsonSerializer(typeof(List<Study>));

            using (FileStream fs = new FileStream("studies.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, ps);
            }

            SendEmail(@"kima.bright@mail.ru", "Бекап бд для клиента", "", new string[] { "teachers.json", "requests.json", "studies.json" });
        }

        private void SendEmail(string mailAddress, string subject, string text, string[] attachmentPath)
        {
            MailMessage m = new MailMessage();
            SmtpClient smtpClient = null;
            try
            {
                m.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                m.To.Add(new MailAddress(mailAddress));
                m.Subject = subject;
                m.Body = text;
                m.SubjectEncoding = Encoding.UTF8;
                m.BodyEncoding = Encoding.UTF8;
                foreach (var f in attachmentPath)
                    m.Attachments.Add(new Attachment(f));
                smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["MailLogin"],
                    ConfigurationManager.AppSettings["MailPassword"]
                    );
                smtpClient.Send(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m = null;
                smtpClient = null;
            }
        }
    }
}
