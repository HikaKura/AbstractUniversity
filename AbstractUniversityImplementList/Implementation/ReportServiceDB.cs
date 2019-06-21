﻿using AbstractUniversityDAL.BindingModel;
using AbstractUniversityDAL.Interface;
using System;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Word;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Data.Entity.SqlServer;
using AbstractUniversity;
using AbstractUniversityDAL.ViewModel;

namespace AbstractUniversityImplementList.Implementation
{
    public class ReportServiceDB : IReportService
    {
        private UniversityDbContext context;

        public ReportServiceDB(UniversityDbContext context)
        {
            this.context = context;
        }

        public List<CourseViewModel> GetCourse(ReportBindingModel model)
        {
            return context.Courses.Where(rec => rec.StartCourse >= model.DateFrom && rec.EndCourse <= model.DateTo).Select(rec => new CourseViewModel
            {
                TeacherFIO = rec.Study.Teacher.LastName,
                Name = rec.Name,
                StartCourse = SqlFunctions.DateName("dd", rec.StartCourse) + " " +
                              SqlFunctions.DateName("mm", rec.StartCourse) + " " +
                              SqlFunctions.DateName("yyyy", rec.StartCourse),
                EndCourse = SqlFunctions.DateName("dd", rec.EndCourse) + " " +
                              SqlFunctions.DateName("mm", rec.EndCourse) + " " +
                              SqlFunctions.DateName("yyyy", rec.EndCourse),
            }).ToList();
        }

        public void getCourseList(ReportBindingModel model)
        {
            if (!File.Exists("TIMCYR.TTF"))
            {
                File.WriteAllBytes("TIMCYR.TTF", Properties.Resources.TIMCYR);
            }
            //открываем файл для работы
            FileStream fs = new FileStream(model.FileName, FileMode.OpenOrCreate, FileAccess.Write);
            //создаем документ, задаем границы, связываем документ и поток
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            doc.SetMargins(0.5f, 0.5f, 0.5f, 0.5f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            BaseFont baseFont = BaseFont.CreateFont("D:\\University\\TP\\TIMCYR_TTF\\TIMCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);


            //вставляем заголовок
            var phraseTitle = new Phrase("Пройденные курсы",
            new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD));
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);
            var phrasePeriod = new Phrase("c " + model.DateFrom.Value.ToShortDateString() +
            " по " + model.DateTo.Value.ToShortDateString(),
            new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phrasePeriod)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);


            //вставляем таблицу, задаем количество столбцов, и ширину колонок
            PdfPTable table = new PdfPTable(4)
            {
                TotalWidth = 800F
            };
            table.SetTotalWidth(new float[] { 160, 140, 160, 100 });


            //вставляем шапку
            PdfPCell cell = new PdfPCell();
            var fontForCellBold = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
            table.AddCell(new PdfPCell(new Phrase("Фамилия препода", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Название курса", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Дата начала", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Дата конца", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });


            //заполняем таблицу
            var list = GetCourse(model);
            var fontForCells = new iTextSharp.text.Font(baseFont, 10);
            for (int i = 0; i < list.Count; i++)
            {

                cell = new PdfPCell(new Phrase(list[i].TeacherFIO, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].Name, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].StartCourse, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].EndCourse, fontForCells));
                table.AddCell(cell);

            }
            cell = new PdfPCell(new Phrase("", fontForCellBold))
            {
                Border = 0
            };
            table.AddCell(cell);


            //вставляем таблицу
            doc.Add(table);
            doc.Close();

            SendEmail(model.Email, "Оповещение по курсам", "", model.FileName);
        }

        public void createMaterialRequest(ReportBindingModel model)
        {
            /*var courses = new Dictionary<Course, int>();
            foreach (var o in context.Courses.Where(o => o.Status == CourseStatus.Finished))
                foreach (var op in context.OrderProducts.Where(x => x.OrderId == o.Id))
                {
                    var p = context.Products.FirstOrDefault(x => x.Id == op.ProductId);
                    foreach (var pm in context.ProductMaterials.Where(pm => pm.ProductId == p.Id))
                    {
                        var m = context.Materials.FirstOrDefault(x => x.Id == pm.MaterialId);
                        if (!materials.ContainsKey(m))
                            materials.Add(m, 0);
                        materials[m] += pm.Count * op.Count;
                    }
                }
                        
                        foreach (var m in materials.Keys.ToArray())
                            if (materials[m] > context.Materials.First(rec => rec.Id == m.Id).Count)
                            {
                                materials[m] = materials[m] - context.Materials.First(rec => rec.Id == m.Id).Count;
                            }
                            else
                            {
                                materials.Remove(m);
                            }

                        if (materials.Count == 0) return;
                        
            if (File.Exists(model.FileName))
            {
                File.Delete(model.FileName);
            }
            var winword = new Microsoft.Office.Interop.Word.Application();
            try
            {
                object missing = System.Reflection.Missing.Value;


                //создаем документ
                Microsoft.Office.Interop.Word.Document document =
                winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);


                //получаем ссылку на параграф
                var paragraph = document.Paragraphs.Add(missing);
                var range = paragraph.Range;
                //задаем текст
                range.Text = "Заявка на материалы";
                //задаем настройки шрифта
                var font = range.Font;
                font.Size = 16;
                font.Name = "Times New Roman";
                font.Bold = 1;
                //задаем настройки абзаца
                var paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 0;
                //добавляем абзац в документ
                range.InsertParagraphAfter();

                //создаем таблицу
                var paragraphTable = document.Paragraphs.Add(Type.Missing);
                var rangeTable = paragraphTable.Range;
                var table = document.Tables.Add(rangeTable, materials.Count, 2, ref missing, ref missing);
                font = table.Range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";
                var paragraphTableFormat = table.Range.ParagraphFormat;
                paragraphTableFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphTableFormat.SpaceAfter = 0;
                paragraphTableFormat.SpaceBefore = 0;
                var orderedMaterials = materials.ToList().OrderBy(kv => kv.Key.Name);
                for (int i = 0; i < materials.Count; ++i)
                {
                    table.Cell(i + 1, 1).Range.Text = orderedMaterials.ElementAt(i).Key.Name;
                    table.Cell(i + 1, 2).Range.Text = orderedMaterials.ElementAt(i).Value.ToString();
                }
                //задаем границы таблицы
                table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleInset;
                table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;


                paragraph = document.Paragraphs.Add(missing);
                range = paragraph.Range;
                range.Text = "Дата: " + DateTime.Now.ToLongDateString();
                font = range.Font;
                font.Size = 12;
                font.Name = "Times New Roman";
                paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 10;
                range.InsertParagraphAfter();


                //сохраняем
                object fileFormat = WdSaveFormat.wdFormatXMLDocument;
                document.SaveAs(model.FileName, ref fileFormat, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing);
                document.Close(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                winword.Quit();
            }

            SendEmail(model.Email, "Заявка на материалы", "", model.FileName);

            foreach (var m in materials)
            {
                context.Materials.First(x => x.Id == m.Key.Id).Count += m.Value;
                context.Requests.Add(new Request
                {
                    MaterialId = m.Key.Id,
                    Count = m.Value,
                    ImplementDate = DateTime.Now
                });
            }

            context.SaveChanges();*/
        }

        private void SendEmail(string mailAddress, string subject, string text, string attachmentPath)
        {
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
            SmtpClient smtpClient = null;
            try
            {
                m.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                m.To.Add(new MailAddress(mailAddress));
                m.Subject = subject;
                m.Body = text;
                m.SubjectEncoding = Encoding.UTF8;
                m.BodyEncoding = Encoding.UTF8;
                m.Attachments.Add(new Attachment(attachmentPath));
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
