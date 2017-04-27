using System;
using OpenQA.Selenium;
using NUnit.Framework;
using ToolsQA.Utilities;
using ToolsQA.PageObjects;
using NLog;
using ToolsQA.Tests;


namespace ToolsQA.Tests
{
    [TestFixture]
    [Parallelizable]    
    [Author("Marchenko Andy", "marchenkoandy@gmail.com")]
    //[Ignore("Ignore a fixture")] 
    class NUnitTests
    {
        [SetUp]
        public void SetupTest() { }

        [TearDown]

        public void TearDownTest() { }

        [Test]
        [Repeat(1)]
        public void GoogleTest1()
        {
            //Test #1. Open Google. Search for “automation”. Open the first link on search results page.
            //Verify that title contains searched word.
            new TestsLibrary().Test1_GoogleTest();
        }

        [Test]
        [Category("Second")]
        public void GoogleTest2()
        {
            //Test #2. Open Google. Search for “automation”.
            //Verify that there is expected domain(“testautomationday.com”) on search results pages(page: 1-5).
            new TestsLibrary().Test2_GoogleTest();
        }
    }

    [TestFixture]
    [Parallelizable]
    [Author("Marchenko Andy", "marchenkoandy@gmail.com")]
    //[Ignore("Ignore a fixture")]
    class NUnitTests_SecondPart
    {
        [Test]
        //[MaxTime(8)]
        [Order(1)]
        [Author("AM", "AM@example.com")]
        public void GoogleTest3()
        {
            //Test #1. Open Google. Search for “automation”. Open the first link on search results page.
            //Verify that title contains searched word.
            new TestsLibrary().Test1_GoogleTest();
        }

        [Test]
        [Order(2)]
        [Category("Second")]
        public void GoogleTest4()
        {
            //Test #2. Open Google. Search for “automation”.
            //Verify that there is expected domain(“testautomationday.com”) on search results pages(page: 1-5).
            new TestsLibrary().Test2_GoogleTest();
        }
    }

    [TestFixture]
    [Parallelizable]
    [Author("Marchenko Andy", "marchenkoandy@gmail.com")]
    class NUnitTests_YandexLogin
    {
        [Test]
        public void YandexLoginTest()
        {
            new TestsLibrary().Test3_YandexTest();
        }
    }
}
