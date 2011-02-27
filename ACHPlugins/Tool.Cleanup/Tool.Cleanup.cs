using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml.Linq;

namespace ACHPlugin
{
    public partial class Tool_Cleanup : UserControl
    {

        public string PluginGuid
        {
            get { return "FC85C33C-3A2E-47C5-9E1A-051C45928977"; }
        }

        public string PluginName
        {
            get
            {
                return "ACH Cleanup Tool";
            }
        }

        public string PluginDescription
        {
            get
            {
                return "Plugin Description .....";
            }
        }

        public string PluginCategoryId
        {
            get
            {
                return "Tools";
            }
        }

        public string PluginAuthor
        {
            get
            {
                return "Mikael Olsson, mikael@paradoxia.se";
            }
        }

        public string PluginVersion
        {
            get
            {
                return "Beta 1.0";
            }
        }

        public string PluginHelpPage
        {
            get
            {
                return "http://xxx.xxx.xxx";
            }
        }

        public string PluginInstanceId
        {
            get;
            set;
        }
        public string PluginInstanceName
        {
            get;
            set;
        }

        public Bitmap PluginInstanceImage
        {
            get
            {
                return PluginIcon;
            }
        }

        public string PluginCreateInstance()
        {
            PluginInstanceId = Guid.NewGuid().ToString();
            PluginInstanceName = "ACH Cleanup Tool";
            return PluginInstanceId;
        }

        public Bitmap PluginIcon
        {
            get
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                Stream stream = asm.GetManifestResourceStream("ACHPlugin.Images.cleanup.png");
                Bitmap bmp = new Bitmap(stream);
                stream.Close();
                return bmp;
            }

        }


        public Tool_Cleanup()
        {
            InitializeComponent();
        }

        private void ScanForAttributeElements(XElement parent, List<XAttribute> listXA)
        {
            foreach (var xeChild in parent.Descendants())
            {
                foreach (var xa in xeChild.Attributes())
                {
                    listXA.Add(xa);

                    System.Xml.IXmlLineInfo info = xa;

                    System.Diagnostics.Debug.WriteLine(xa.Name.LocalName + "=" + xa.Value.ToString()+" at "+info.LineNumber.ToString());

                
                }
            }

        }




        private void ScanXMLFiles(string ProjectFolder)
        {
            string[] sXMLFiles = Directory.GetFiles(ProjectFolder, "*.xml", SearchOption.AllDirectories);


            foreach (var sXMLFile in sXMLFiles)
            {
                List<XAttribute> listXA = new List<XAttribute>();
                XDocument xd = XDocument.Load(sXMLFile,LoadOptions.SetLineInfo);
                XElement xeRoot = xd.Root;
                System.Diagnostics.Debug.WriteLine("XMLFile=" + sXMLFile);
                ScanForAttributeElements(xeRoot, listXA);


 
            }

        }

        private void btnScanAndroidProject_Click(object sender, EventArgs e)
        {

            Control cntrlParent = Parent;

            while (cntrlParent.Parent != null)
                cntrlParent = cntrlParent.Parent;

            MethodInfo miAPSC = cntrlParent.GetType().UnderlyingSystemType.GetMethod("AnalyzeProjectSourceCode");
            object oResultAPSC = miAPSC.Invoke(cntrlParent, null);
            string sAnalyzeProjectSourceCode = (string)oResultAPSC;

            PropertyInfo piAAPF = cntrlParent.GetType().UnderlyingSystemType.GetProperty("ActiveAndroidProjectFolder");
            object oResultAAPF = piAAPF.GetValue(cntrlParent, null);
            string sActiveAndroidProjectFolder = (string)oResultAAPF;
            ScanXMLFiles(sActiveAndroidProjectFolder);

            

            // Parse R. file

            var resPRF = CodeAnalyzer.ParseResourceFile(sAnalyzeProjectSourceCode + "\\R.jsp");

            DataSet ds = new DataSet();
            DataTable dtAllRes = new DataTable("All resources");
            DataColumn dcARCategory = new DataColumn("Category", typeof(string));
            DataColumn dcARName = new DataColumn("Name", typeof(string));
            DataColumn dcARRefId = new DataColumn("RefId", typeof(string));
            dtAllRes.Columns.Add(dcARCategory);
            dtAllRes.Columns.Add(dcARName);
            dtAllRes.Columns.Add(dcARRefId);
            ds.Tables.Add(dtAllRes);

            foreach (var r in resPRF)
            {
                DataRow dr = dtAllRes.NewRow();
                dr["Category"] = r.Category;
                dr["Name"] = r.RefName;
                dr["RefId"] = r.RefId;
                dtAllRes.Rows.Add(dr);
            }

            dgvAllResources.DataSource = null;
            dgvAllResources.DataSource = ds.Tables["All resources"];

            // Parse all other files

            DataTable dtUsedRes = new DataTable("Used resources");
            DataColumn dcURRefId = new DataColumn("RefId", typeof(string));
            DataColumn dcURRowLine = new DataColumn("RowLine", typeof(string));
            DataColumn dcURClassFile = new DataColumn("ClassFile", typeof(string));
            dtUsedRes.Columns.Add(dcURRefId);
            dtUsedRes.Columns.Add(dcURRowLine);
            dtUsedRes.Columns.Add(dcURClassFile);
            ds.Tables.Add(dtUsedRes);

            List<CodeAnalyzer.CodeResourceRef> listTotalCRR = new List<CodeAnalyzer.CodeResourceRef>();

            string[] sJSPFiles = Directory.GetFiles(sAnalyzeProjectSourceCode, "*.jsp");
            foreach (var sFile in sJSPFiles)
            {
                if (!sFile.EndsWith("R.jsp", StringComparison.CurrentCultureIgnoreCase))
                {
                    var resCRF = CodeAnalyzer.ParseJavaFile(resPRF, sFile);
                    listTotalCRR.AddRange(resCRF);
                    foreach (var rcrf in resCRF)
                    {
                        DataRow dr = dtUsedRes.NewRow();
                        dr["RefId"] = rcrf.Ref.RefId;
                        dr["RowLine"] = rcrf.CodeLineNumber;
                        dr["ClassFile"] = rcrf.ClassFile;
                        dtUsedRes.Rows.Add(dr);
                    }

                }
            }

            dgvUsedResources.DataSource = null;
            dgvUsedResources.DataSource = ds.Tables["Used resources"];


            // Find Unused Resources

            DataTable dtUnusedRes = new DataTable("Unused resources");
            DataColumn dcUURCategory = new DataColumn("Category", typeof(string));
            DataColumn dcUURName = new DataColumn("Name", typeof(string));
            DataColumn dcUURRefId = new DataColumn("RefId", typeof(string));
            dtUnusedRes.Columns.Add(dcUURCategory);
            dtUnusedRes.Columns.Add(dcUURName);
            dtUnusedRes.Columns.Add(dcUURRefId);
            ds.Tables.Add(dtUnusedRes);

            var unusedRR = CodeAnalyzer.UnusedResources(resPRF, listTotalCRR.ToArray());

            foreach (var urr in unusedRR)
            {
                DataRow dr = dtUnusedRes.NewRow();
                dr["Category"] = urr.Category;
                dr["Name"] = urr.RefName;
                dr["RefId"] = urr.RefId;
                dtUnusedRes.Rows.Add(dr);


            }

            dgvUnusedResources.DataSource = null;
            dgvUnusedResources.DataSource = ds.Tables["Unused resources"];


        }
    }
}
