using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web.UI;
using KangaModeling.Facade;
using NUnit.Framework;

namespace KangaModeling.Visuals.Test.SequenceDiagrams.EndToEnd
{
    [TestFixture]
    public class VisualVerification
    {
        [TestCase(DiagramStyle.Classic)]
        [TestCase(DiagramStyle.Sketchy)]
        [Explicit]
        public void GenerateVerificationDiagrams(DiagramStyle diagramStyle)
        {
            string directory = diagramStyle.ToString();
            string htmlFileName = string.Format("{0}\\VisualVerificationDiagrams.html", directory);
            Directory.CreateDirectory(directory);

            using (var reader = File.OpenText(@"SequenceDiagrams\EndToEnd\TestCaseSources.txt"))
            {
                using (TextWriter textWriter = new StreamWriter(htmlFileName))
                using (var writer = new HtmlTextWriter(textWriter))
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Html);
                    writer.RenderBeginTag(HtmlTextWriterTag.Head);
                    writer.RenderBeginTag(HtmlTextWriterTag.Title);
                    writer.Write("KangaModeling.SequenceDiagram Visual Tests");
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                    writer.RenderBeginTag(HtmlTextWriterTag.Body);


                    writer.AddAttribute("border", "1");
                    writer.AddAttribute(HtmlTextWriterAttribute.Width, "250px");
                    writer.RenderBeginTag(HtmlTextWriterTag.Table);
                    foreach (var testCase in GetTestCases(reader))
                    {
                        Write(testCase.Item1, testCase.Item2, writer, diagramStyle);
                    }
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
            }
            System.Diagnostics.Process.Start(htmlFileName);
        }

        private void Write(string source, string testCaseName, HtmlTextWriter writer, DiagramStyle diagramStyle)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.Write(testCaseName);

            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.RenderBeginTag(HtmlTextWriterTag.Pre);
            writer.WriteLineNoTabs(source);
            writer.RenderEndTag();

            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            string fileName = GenetareImage(source, testCaseName, diagramStyle);
            writer.AddAttribute(HtmlTextWriterAttribute.Src, fileName);
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, testCaseName);
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();

            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        private string GenetareImage(string source, string testCaseName, DiagramStyle diagramStyle)
        {
            var arguments = new DiagramArguments(source, DiagramType.Sequence, diagramStyle);
			var result = DiagramFactory.Create(arguments);
            using (result)
            {
                string filename = testCaseName + ".png";
                result.Image.Save(diagramStyle + "\\" + filename, ImageFormat.Png);
                return filename;
            }
        }

        private IEnumerable<Tuple<string, string>> GetTestCases(TextReader reader)
        {
            string testCaseName=string.Empty;
            StringBuilder source = new StringBuilder();
            foreach (var line in GetLines(reader))
            {
                if (line.StartsWith("#"))
                {
                    if (source.Length!=0)
                    {
                        yield return new Tuple<string, string>(source.ToString(), testCaseName);
                    }
                    testCaseName = line.Remove(0, 1).Trim();
                    source.Clear();
                }
                else
                {
                    source.AppendLine(line);
                }
            }
        }

        private static IEnumerable<string> GetLines(TextReader reader)
        {
            while (true)
            {
                string line = reader.ReadLine();
                if (line != null)
                {
                    if (line.Trim().Length != 0)
                    {
                        yield return line;
                    }
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}
