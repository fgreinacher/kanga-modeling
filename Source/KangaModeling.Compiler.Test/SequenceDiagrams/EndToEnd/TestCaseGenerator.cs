using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Compiler.Test.SequenceDiagrams.EndToEnd.Serailization;
using NUnit.Framework;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.EndToEnd
{
    [TestFixture]
    public class TestCaseGenerator
    {
        [Test]
        public void Test()
        {
            using (var reader = File.OpenText(@"SequenceDiagrams\EndToEnd\TestCaseSources.txt"))
            {
                var xDocument = new XDocument(
                    new XElement("TestCases", GetTestCases(reader)));
                xDocument.Save("TestCases.xml");
            }
        }

        public IEnumerable<XElement> GetTestCases(TextReader reader)
        {
            string testCaseName=string.Empty;
            StringBuilder source = new StringBuilder();
            foreach (var line in GetLines(reader))
            {
                if (line.StartsWith("#"))
                {
                    if (source.Length!=0)
                    {
                        yield return GetTestCaseXml(testCaseName, source.ToString());
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

        public XElement GetTestCaseXml(string testCaseName, string source)
        {
            Tuple<string, string, ISequenceDiagram, IEnumerable<ModelError>> testCase = GetTestCase(source, testCaseName);
            return new XElement("TestCase",
                                new XElement("Name", testCase.Item1),
                                new XElement("Source", testCase.Item2),
                                testCase.Item3.ToXml(),
                                testCase.Item4.ToXml());
        }

        private Tuple<string, string, ISequenceDiagram, IEnumerable<ModelError>> GetTestCase(string source, string testCaseName)
        {
            var errors = new ModelErrorsCollection();
            var sequenceDiagram = DiagramCreator.CreateFrom(source, errors);
            return new Tuple<string, string, ISequenceDiagram, IEnumerable<ModelError>>(testCaseName, source, sequenceDiagram, errors);
        }
    }
}
