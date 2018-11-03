using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using MarsRover.Logic;

namespace MarsRover.Test.LogicTests
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void GetHashString()
        {
            //check to make sure hashing is working properly
            var testString = "testtesttesttest1234";
            Assert.AreEqual(testString.GetHashString(), testString.GetHashString());

        }
    }
}
