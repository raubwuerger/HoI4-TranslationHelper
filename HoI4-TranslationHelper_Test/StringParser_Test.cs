using HoI4_TranslationHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            List<string> tokens = new List<string>();
            string toTest = "sdfsdfsf[asds]";

            List<string> tokenExpected = new List<string>() { "asds" };
            List<string> tokensFound = StringParser.GetToken(toTest, tokens);
            Assert.AreEqual(tokenExpected[0], tokensFound[0]);
        }
    }
}
