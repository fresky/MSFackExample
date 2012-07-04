using System;
using System.Windows.Forms;
using MSFackTarget.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSFackTarget;
using System.IO;

namespace MSFackTest
{
    [TestClass]
    public class FileWrapperTest
    {
        private FileWrapper sut;
        private const string fileName = @"D:\testFake.txt";



        [TestCleanup]
        public void DeleteFileIfExist()
        {
            try
            {
                File.Delete(fileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }

        [TestMethod]
        // virtual method
        public void IsFileExist_Return_True_If_New_Create_File()
        {
            sut = new FileWrapper(new FileHelper());
            using(File.Create(fileName))
            {
                
            }
            Assert.IsTrue(sut.IsFileExist(fileName));
        }

        [TestMethod]
        // virtual method
        public void Stub_IsFileExist_Return_True_If_New_Create_File()
        {

            var fileHelper = new StubFileHelper();
            fileHelper.IsNullString = name => false;

            sut = new FileWrapper(fileHelper);
            Assert.IsTrue(sut.IsFileExist(fileName));
        }

        [TestMethod]
        // non virtual method
        public void IsFileEmpty_Return_False_If_New_Create_File_WithText()
        {
            sut = new FileWrapper(new FileHelper());
            using (StreamWriter writer = File.CreateText(fileName))
            {
                writer.WriteLine("Hello, World!");
                writer.Flush();
            }
            Assert.IsFalse(sut.IsFileEmpty(fileName));
        }

        [TestMethod]
        // non virtual method
        public void Shim_All_IsFileEmpty_Return_False_If_New_Create_File_WithText()
        {
            sut = new FileWrapper(new FileHelper());
            using (ShimsContext.Create())
            {
                ShimFileHelper.AllInstances.IsEmptyString = delegate { return false; };
                Assert.IsFalse(sut.IsFileEmpty(fileName));
            }
        }

        [TestMethod]
        // non virtual method
        public void Shim_One_IsFileEmpty_Return_False_If_New_Create_File_WithText()
        {
            using (ShimsContext.Create())
            {
                var shimFileHelper = new ShimFileHelper();
                shimFileHelper.IsEmptyString = name => false;
                sut = new FileWrapper(shimFileHelper.Instance);
                Assert.IsFalse(sut.IsFileEmpty(fileName));
            }
        }


        [TestMethod]
        // static method
        public void IsFileNullOrEmpty_Return_False_If_New_Create_File_WithText()
        {
            sut = new FileWrapper(new FileHelper());

            using (StreamWriter writer = File.CreateText(fileName))
            {
                writer.WriteLine("Hello, World!");
                writer.Flush();
            }
            Assert.IsFalse(sut.IsFileNullOrEmpty(fileName));
                    }


        [TestMethod]
        // static method
        public void Shim_Static_IsFileNullOrEmpty_Return_False_If_New_Create_File_WithText()
        {
            sut = new FileWrapper(new FileHelper());
            using (ShimsContext.Create())
            {
                ShimFileHelper.IsNullOrEmptyString = name => false;
                Assert.IsFalse(sut.IsFileNullOrEmpty(fileName));
            }
        }

    }
}
