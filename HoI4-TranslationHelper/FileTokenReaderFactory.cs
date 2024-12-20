﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class FileTokenReaderFactory
    {
        private static FileTokenReaderFactory instance;
        public static FileTokenReaderFactory Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new FileTokenReaderFactory();
                }
                return instance;
            }
        }

        private FileTokenReaderFactory() { }

        public FileTokenReader CreateReaderBrackets()
        {
            FileTokenReader fileReader = new FileTokenReader();
            fileReader.PathReplace = "brackets";
            fileReader.StringParser = StringParserFactory.Instance.CreateParserBrackets();
            return fileReader;
        }

        public FileTokenReader CreateReaderIcons()
        {
            FileTokenReader fileReader = new FileTokenReader();
            fileReader.PathReplace = "icons";
            fileReader.StringParser = StringParserFactory.Instance.CreateParserIcons();
            return fileReader;
        }

        public FileTokenReader CreateReaderVariables()
        {
            FileTokenReader fileReader = new FileTokenReader();
            fileReader.PathReplace = "variables";
            fileReader.StringParser = StringParserFactory.Instance.CreateParserVariables();
            return fileReader;
        }

        public FileTokenReader CreateReaderColors()
        {
            FileTokenReader fileReader = new FileTokenReader();
            fileReader.PathReplace = "colors";
            fileReader.StringParser = StringParserFactory.Instance.CreateParserColors();
            return fileReader;
        }

        public FileTokenReader CreateReaderInnerDoubleQuotes()
        {
            FileTokenReader fileReader = new FileTokenReader();
            fileReader.PathReplace = "innerDoubleQuotes";
            fileReader.StringParser = StringParserFactory.Instance.CreateParserInnerDoubleQuotes();
            return fileReader;
        }

        public FileTokenReader CreateReaderKeys()
        {
            FileTokenReader fileReader = new FileTokenReader();
            fileReader.PathReplace = "keys";
            fileReader.StringParser = StringParserFactory.Instance.CreateParserKey();
            return fileReader;
        }


    }
}
