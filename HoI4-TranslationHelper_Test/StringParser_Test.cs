using HoI4_TranslationHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HoI4_TranslationHelper_Test
{
    [TestClass]
    public class StringParser_Test
    {
        private string testTokenStart = "[";
        private string testTokenEnd = "]";

        [TestMethod]
        public void TestMethod1()
        {
            string toTest = "sdfsdfsf[asds]";
            Assert.AreEqual(StringParser.GetToken(toTest, testTokenStart, testTokenEnd), "asds");
        }
    }
}
