using System.Collections.Generic;
using System.IO;
using InternshipProj.Model;
using InternshipProj.Utility;
using InternshipProj.ViewModel;
using NUnit.Framework;

namespace InternshipProj.Tests
{
    [TestFixture]
    public class UnitTest
    {
        public const string TestCaseFile1 = @"\..\..\Tests\TestFiles\testCase1.csv";
        public const string TestCaseFile3 = @"\..\..\Tests\TestFiles\testCase3.csv";

        List<TodoItem> items1 = new List<TodoItem>
        {
            new TodoItem("desc1", true),
            new TodoItem("desc2", true)
        };

        List<TodoItem> items2 = new List<TodoItem>
        {
            new TodoItem("desc3", false),
            new TodoItem("desc4", false)
        };

        [Test]
        [TestCase(TestCaseFile1, 2, 2, "title1", "title2", "desc1", "desc2", "desc3", "desc4",
            true, false, true, true)]
        public void TestImport(string fileName, int expectedLists, int expectedItems,
            string title1, string title2, string desc1, string desc2, string desc3, string desc4,
            bool desc1bool, bool desc2bool, bool desc3bool, bool desc4bool)
        {
            var testCase = TestContext.CurrentContext.TestDirectory + fileName;

            List<TodoListVM> lists = CSVImporter.Load(testCase);

            //make sure importer actually created lists
            Assert.IsNotNull(lists);
            //make sure there are X number of lists
            Assert.AreEqual(expectedLists, lists.Count);
            //make sure there are X number of items in each list
            Assert.AreEqual(expectedItems, lists[0].ItemList.Count);
            Assert.AreEqual(expectedItems, lists[1].ItemList.Count);
            //make sure list names are set correctly
            Assert.IsTrue(string.Equals(lists[0].ListName, title1));
            Assert.IsTrue(string.Equals(lists[1].ListName, title2));
            //make sure list items are set correctly
            Assert.IsTrue(string.Equals(lists[0].ItemList[0].Desc, desc1));
            Assert.That(lists[0].ItemList[0].Done == desc1bool);
            Assert.IsTrue(string.Equals(lists[0].ItemList[1].Desc, desc2));
            Assert.That(lists[0].ItemList[1].Done == desc2bool);

            Assert.IsTrue(string.Equals(lists[1].ItemList[0].Desc, desc3));
            Assert.That(lists[1].ItemList[0].Done == desc3bool);
            Assert.IsTrue(string.Equals(lists[1].ItemList[1].Desc, desc4));
            Assert.That(lists[1].ItemList[1].Done == desc4bool);
        }

        [Test]
        [TestCase(TestCaseFile3, 2)]

        public void TestExporter(string fileName, int expectedLines)
        {
            var testCase = TestContext.CurrentContext.TestDirectory + fileName;
            int lineCount = 0;
            string desc;

            List<TodoListVM> exportTestList = new List<TodoListVM>
            {
                new TodoListVM(items1,"title1"),
                new TodoListVM(items2, "title2")
            };

            CSVExporter exporter = new CSVExporter();
            exporter.Save(exportTestList, testCase);

            //Check if file is saved
            Assert.That(File.Exists(testCase));
            //Check number of lists is correct
            using (StreamReader sr = new StreamReader(testCase))
            {
                while (!sr.EndOfStream)
                {
                    desc = sr.ReadLine();
                    Assert.IsNotNull(desc);
                    lineCount += 1;
                }
            }
            Assert.That(expectedLines == lineCount);
            File.Delete(testCase);
        }
    }
}