using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace HoI4_TranslationHelper_Test
{
    [TestClass]
    public class ListPotpourri_Test
    {
        List<string> listNoItems = new List<string>();

        List<string> listOneItem = new List<string>();
        List<string> listTwoItems = new List<string>();
        List<string> listThreeItems = new List<string>();

        List<string> listOneOtherItem = new List<string>();
        List<string> listTwoOtherItems = new List<string>();
        List<string> listThreeOtherItem = new List<string>();


        string a = "a";
        string b = "b";
        string c = "c";
        string d = "d";

        string x = "x";
        string y = "y";
        string z = "z"; 

        [TestInitialize]
        public void Initialize()
        {
            listOneItem.Add(a);

            listTwoItems.Add(a);
            listTwoItems.Add(b);

            listThreeItems.Add(a);
            listThreeItems.Add(b);
            listThreeItems.Add(c);
        }

        [TestMethod]
        public void TestMethod1()
        {
            IEnumerable<string> inFirstOnly = listOneItem.Except(listTwoItems);
            IEnumerable<string> inSecondOnly = listOneItem.Except(listTwoItems);
            Assert.AreEqual(inFirstOnly, inSecondOnly);
        }
    }
}
