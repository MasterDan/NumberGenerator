using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGen.Services;
using NuGen.Services.Interfaces;

namespace NuGen.Tests
{
    [TestClass]
    public class ConsoleHelperTests
    {
        private readonly IConsoleHelperService _helper = new ConsoleHelperService();
        
        [TestMethod]
        public void ProgressTest()
        {
            Assert.AreEqual(" 2/10 | ##________ | 20 % ",_helper.GenerateProgress(2 , 10));
            Assert.AreEqual(" 1/10 | #_________ | 10 % ",_helper.GenerateProgress(1 , 10));
            Assert.AreEqual(" 0/10 | __________ | 00 % ",_helper.GenerateProgress(0 , 10));
            Assert.AreEqual(" 9/10 | #########_ | 90 % ",_helper.GenerateProgress(9 , 10));
            Assert.AreEqual("DONE",_helper.GenerateProgress(10 , 10));
        }
    }
}