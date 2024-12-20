﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    internal class DataSetMod
    {
        private string _name;
        private string _pathGerman;
        private string _pathEnglish;
        private DataSetMod() { }

        public string Name
        {
            get { return _name; }  
        }

        public string PathGerman
        {
            get { return _pathGerman;  }
            set { _pathGerman = value;}
        }

        public string PathEnglish
        {
            get { return _pathEnglish; }
            set { _pathEnglish = value;}
        }

        public DataSetMod( string name )
        { 
            this._name = name; 
        }

        public override bool Equals(object obj) => this.Equals(obj as DataSetMod);

        public bool Equals(DataSetMod dataSetMod)
        {
            if (dataSetMod is null)
            {
                return false;
            }

            if (Object.ReferenceEquals(this, dataSetMod))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != dataSetMod.GetType())
            {
                return false;
            }

            return _name.Equals(this._name);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_name);
        }

    }
}
