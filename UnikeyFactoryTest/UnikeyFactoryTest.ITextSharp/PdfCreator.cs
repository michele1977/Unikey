using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.ITextSharp
{
    public class PdfCreator
    {
        public bool CreatePdf(TestBusiness test)
        {
            bool res = false;

            using (Document doc = new Document())
            {
                try
                {
                    var writer = PdfWriter.GetInstance(doc, new FileStream($"D:/ITextSharpPdfs/{test.Title}.pdf", FileMode.Create));

                    doc.Open();

                    res = doc.Add(new Paragraph("\n\n\n"));
                    Image logo = Image.GetInstance(@"D:\Contents\UnikeyLogo.png");
                    logo.Alignment = Element.ALIGN_CENTER;

                    doc.Add(logo);

                    string personalInfos = "\n\n\nFirst name: _________________\nLast Name: _________________\nDate: __/__/____";
                    var font = FontFactory.GetFont("Segoe UI", 18.0f, BaseColor.BLACK);
                    var paragraphPersonalInfo = new Paragraph(personalInfos, font);
                    paragraphPersonalInfo.Alignment = Element.ALIGN_CENTER;

                    doc.Add(paragraphPersonalInfo);

                    doc.NewPage();

                    int questionNum = 1;
                    string[] indexes = { "a)", "b)", "c)", "d)" };

                    StringBuilder builder = new StringBuilder("");

                    foreach (var question in test.Questions)
                    {
                        //doc.Add(new Paragraph($"{questionNum}. {question.Text}\n \n"));
                        builder.Append($"{questionNum}. {question.Text}\n \n");

                        for (int i = 0; i < question.Answers.Count; ++i)
                        {
                            builder.Append($"{indexes[i]} {question.Answers[i].Text}\n");
                            //doc.Add(new Phrase($"{indexes[i]} {question.Answers[i].Text}\n"));
                        }

                        builder.Append("\n");

                        //doc.Add(new Phrase("\n"));

                        res = doc.Add(new Paragraph(builder.ToString()));

                        builder.Clear();

                        ++questionNum;
                    }

                    doc.Close();
                }
                catch (Exception e)
                {
                    return false;
                }

            }

            return res;
        }
    }
}
