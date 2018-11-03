using MarsRover.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Test.LogicTests
{
    [TestClass]
    public class MarsAPIOperatorTests
    {

        [TestMethod]
        public async Task GetStoreDailyImageAsyncTest()
        {
            MarsAPIOperator op = new MarsAPIOperator();

            try
            {
                await op.GetStoreDailyImageAsync(DateTime.Now.AddDays(5));
                Assert.Fail("Exception should have been thrown.");
            } catch { }

            Assert.IsFalse(
                (await op.GetStoreDailyImageAsync(DateTime.Now.AddDays(-5))).Count <= 0,
                "The function should have returned some pictures");



        }
    }
}
