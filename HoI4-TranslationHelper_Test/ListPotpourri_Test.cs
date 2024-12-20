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
        List<string> listItemsNone = new List<string>();

        List<string> listItemsA = new List<string>();
        List<string> listItemsAB = new List<string>();
        List<string> listItemsABC = new List<string>();
        List<string> listItemsAC = new List<string>();
        List<string> listItemsBC = new List<string>();
        List<string> listItemsC = new List<string>();

        List<string> listItemsX = new List<string>();
        List<string> listItemsXY = new List<string>();
        List<string> listItemsXYZ = new List<string>();


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
            listItemsA.Add(a);

            listItemsAB.Add(a);
            listItemsAB.Add(b);

            listItemsABC.Add(a);
            listItemsABC.Add(b);
            listItemsABC.Add(c);
            
            listItemsAC.Add(a);
            listItemsAC.Add(c);

            listItemsBC.Add(b);
            listItemsBC.Add(c);

            listItemsC.Add(c);

            listItemsX.Add(x);

            listItemsXY.Add(x);
            listItemsXY.Add(y);

            listItemsXYZ.Add(x);
            listItemsXYZ.Add(y);
            listItemsXYZ.Add(z);
        }

        [TestMethod]
        public void TestMethod1()
        {
            List<string> list_A_except_None = listItemsA.Except(listItemsNone).ToList<string>();
            List<string> list_A_except_A = listItemsA.Except(listItemsA).ToList<string>();
            List<string> list_A_except_AB = listItemsA.Except(listItemsAB).ToList<string>();
            List<string> list_A_except_ABC = listItemsA.Except(listItemsABC).ToList<string>();

            List<string> list_AB_except_None = listItemsAB.Except(listItemsNone).ToList<string>();
            List<string> list_AB_except_A = listItemsAB.Except(listItemsA).ToList<string>();
            List<string> list_AB_except_ABC = listItemsAB.Except(listItemsABC).ToList<string>();

            List<string> list_ABC_except_None = listItemsABC.Except(listItemsNone).ToList<string>();
            List<string> list_ABC_except_A = listItemsABC.Except(listItemsA).ToList<string>();
            List<string> list_ABC_except_AB = listItemsABC.Except(listItemsAB).ToList<string>();


            List<string> inSecondOnly = listItemsAB.Except(listItemsA).ToList<string>();
            Assert.AreEqual(list_A_except_AB, inSecondOnly);
        }
    }
}
