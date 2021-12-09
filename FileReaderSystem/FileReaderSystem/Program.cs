using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FileReaderSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //#region read xml and get codes and versions
            //XmlDocument xmlDocument = new XmlDocument();
            //xmlDocument.Load("C:\\Users\\jd\\Desktop\\Attachment_1638376325.vrx");
            //var finasNumber = xmlDocument.GetElementsByTagName("FiNASNumber")[0].InnerText; // Vehicle Name
            //Dictionary<string, Version> allCodesAndVersions = GetCodesAndVersions(xmlDocument);
            //#endregion

            //#region To compare versions
            ////Less than zero The current Version object is a version before version.
            ////Zero The current Version object is the same version as version.
            ////Greater than zero   The current Version object is a version subsequent to version, or version is null.
            //Version test2 = new Version("1.2.3");
            //int result = test2.CompareTo(allCodesAndVersions["PARK223"]);
            //#endregion

            //string finasName = test.SelectSingleNode("FiNASNumber").InnerText;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        //private static Dictionary<string, Version> GetCodesAndVersions(XmlDocument xmlDocument)
        //{
        //    var componentsList = xmlDocument.GetElementsByTagName("Component");
        //    var versions = xmlDocument.GetElementsByTagName("SMR");
        //    Dictionary<string, Version> allCodesAndVersions = new Dictionary<string, Version>(); // Code and Versions

        //    foreach (XmlNode item in componentsList)
        //    {
        //        string shortCode = item.ChildNodes[0].InnerText;
        //        foreach (XmlNode version in versions)
        //        {
        //            if (version.Attributes["ShortName"]?.Value == shortCode)
        //            {
        //                allCodesAndVersions[shortCode] = new Version(version.InnerText);
        //            }
        //        }

        //    }

        //    return allCodesAndVersions;
        //}
    }
}
