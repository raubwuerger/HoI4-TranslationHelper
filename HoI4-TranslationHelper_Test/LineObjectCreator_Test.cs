﻿using HoI4_TranslationHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper_Test
{
    [TestClass]
    public class LineObjectCreator_Test
    {
        ulong LINE_NUMBER_ONE = 1UL;
        ulong LINE_NUMBER_TWO = 2UL;
        private void ParameterizeLineObject(LineObjectCreator lineObjectCreator)
        {
            string key = "key1";
            TranslationFile translationFile = new TranslationFile(key);
            List<string> nestingString = new List<string>();
            List<string> brackets = new List<string>();

            lineObjectCreator.TranslationFile = translationFile;
            lineObjectCreator.Key = key;
            lineObjectCreator.Brackets = brackets;
            lineObjectCreator.NestingStrings = nestingString;
        }

        [TestMethod]
        public void TestMethodCreate()
        {
            LineObjectCreator lineObjectCreator = new LineObjectCreator();
            LineObject lineObject = lineObjectCreator.Create(LINE_NUMBER_ONE);
            Assert.AreEqual(1UL, lineObject.LineNumber);
            Assert.IsNull(lineObject.TranslationFile);
            Assert.IsNull(lineObject.Key);
            Assert.IsNull(lineObject.NestingStrings);
            Assert.IsNull(lineObject.Brackets);
        }

        [TestMethod]
        public void TestMethodCreateWithAdditionalData()
        {
            LineObjectCreator lineObjectCreator = new LineObjectCreator();
            ParameterizeLineObject(lineObjectCreator);
            LineObject lineObject = lineObjectCreator.Create(LINE_NUMBER_ONE);

            Assert.AreEqual(LINE_NUMBER_ONE, lineObject.LineNumber);
            Assert.IsNotNull(lineObject.TranslationFile);
            Assert.IsNotNull(lineObject.Key);
            Assert.IsNotNull(lineObject.NestingStrings);
            Assert.IsNotNull(lineObject.Brackets);
        }

        [TestMethod]
        public void TestMethodCreateWithAdditionalDataTwice()
        {
            LineObjectCreator lineObjectCreator = new LineObjectCreator();
            ParameterizeLineObject(lineObjectCreator);
            LineObject lineObject = lineObjectCreator.Create(LINE_NUMBER_TWO);

            Assert.AreEqual(LINE_NUMBER_TWO, lineObject.LineNumber);
            Assert.IsNotNull(lineObject.TranslationFile);
            Assert.IsNotNull(lineObject.Key);
            Assert.IsNotNull(lineObject.NestingStrings);
            Assert.IsNotNull(lineObject.Brackets);

            lineObject = lineObjectCreator.Create(2UL);
            Assert.AreEqual(LINE_NUMBER_TWO, lineObject.LineNumber);
            Assert.IsNotNull(lineObject.TranslationFile);
            Assert.IsNull(lineObject.Key);
            Assert.IsNull(lineObject.NestingStrings);
            Assert.IsNull(lineObject.Brackets);

        }
    }
}
