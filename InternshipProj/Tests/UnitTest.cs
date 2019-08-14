using System.Collections.Generic;
using System.IO;
using InternshipProj.Model;
using InternshipProj.Utility;
using NUnit.Framework;

namespace InternshipProj.Tests
{
    [TestFixture]
    public class UnitTest
    {
        //public const string TestCaseFile1 = @"\..\..\Tests\TestFiles\testCase1.csv";
        //public const string TestCaseFile2 = @"\..\..\Tests\TestFiles\testCase2.csv";
        //public const string TestCaseFile3 = @"\..\..\Tests\TestFiles\testCase3.csv";
        //public const string TestCaseFile4 = @"\..\..\Tests\TestFiles\testCase4.csv";

        //List<TodoItem> exportTestList = new List<TodoItem>
        //{
        //    new TodoItem("weeeeee", true),
        //    new TodoItem("jfjiwef", false),
        //    new TodoItem("ahhhhhhhhhhhhhhhhhhhhhhhh,h", true)
        //};

        //[Test]
        //[TestCase(TestCaseFile1, 6, 0, "dfaf ", false)]
        //[TestCase(TestCaseFile2, 5, 0, "kjhgfds", true)]
        //public void TestImporter(string fileName, int expectedCount, int testIdx, string expectedDesc,
        //    bool expectedDone)
        //{
        //    var testCase = TestContext.CurrentContext.TestDirectory + fileName;

        //    List<TodoItem> list = CSVImporter.Load(testCase);
        //    //make sure importer actually created a list
        //    Assert.IsNotNull(list);
        //    //make sure list has X number of items, X is dependent on file
        //    Assert.AreEqual(expectedCount, list.Count);
        //    //make sure items have expected values
        //    Assert.IsTrue(string.Equals(list[testIdx].Desc, expectedDesc));
        //    Assert.That(list[testIdx].Done == expectedDone);

        //}

        //[Test]
        //[TestCase(TestCaseFile3, 3)]
        //[TestCase(TestCaseFile4, 3)]

        ////public void TestExporter(string fileName, int expectedLines)
        ////{
        ////    var testCase = TestContext.CurrentContext.TestDirectory + fileName;
        ////    int lineCount = 0;
        ////    string desc;
        ////    CSVExporter exporter = new CSVExporter();
        ////    exporter.Save(exportTestList, testCase);
        ////    //Check if file is saved
        ////    Assert.That(File.Exists(testCase));
        ////    //Check number of lines is correct
        ////    using (StreamReader sr = new StreamReader(testCase))
        ////    {
        ////        while (!sr.EndOfStream)
        ////        {
        ////            desc = sr.ReadLine();
        ////            Assert.IsNotNull(desc);
        ////            string[] splitArr = desc.Split(new[] { '\"' });
        ////            Assert.That(exportTestList[lineCount].Desc == splitArr[1]);
        ////            lineCount += 1;
        ////        }
        ////    }
        ////    Assert.That(expectedLines == lineCount);
        ////    File.Delete(testCase);
        ////}
    }
}