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

        private StringParser _stringParser;

        [TestInitialize]
        public void Initialize()
        {
            _stringParser = new StringParser();
        }

        [TestMethod]
        public void TestMethod1()
        {
            List<string> tokens = new List<string>();
            string toTest = "sdfsdfsf[asds]";

            List<string> tokenExpected = new List<string>() { "asds" };
            List<string> tokensFound = _stringParser.GetToken(toTest, tokens);
            Assert.AreEqual(tokenExpected[0], tokensFound[0]);
        }

        [TestMethod]
        public void TestMethod2()
        {
            List<string> tokens = new List<string>();
            string toTest = "sdfsdfsf£asds";

            _stringParser.StartTag = "£";
            _stringParser.EndTags.Add(" ");

            List<string> tokenExpected = new List<string>() { "asds" };
            List<string> tokensFound = _stringParser.GetToken(toTest, tokens);
            Assert.AreEqual(tokenExpected[0], tokensFound[0]);
        }

        [TestMethod]
        public void TestMethod3()
        {
            List<string> tokens = new List<string>();
            string toTest = "DISBAND_PRIDE_OF_THE_FLEET_COST:1	\"Disbanding your §HPride of the Fleet§! §H($NAME$)§!will cost $COST | R$ £pol_power\"";

            _stringParser.StartTag = "£";
            _stringParser.StartTag = "£";
            _stringParser.EndTags.Clear();
            _stringParser.EndTags.Add(" ");
            _stringParser.EndTags.Add("\n");
            _stringParser.EndTags.Add("\"");

            List<string> tokenExpected = new List<string>() { "pol_power" };
            List<string> tokensFound = _stringParser.GetToken(toTest, tokens);
            Assert.AreEqual(tokenExpected[0], tokensFound[0]);
        }

        
    }
}
