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

        private bool ContainsResourceRef(string Value)
        {
            if (Value.StartsWith("@") && Value.IndexOf('/') > 0 && Value.IndexOf('+') == -1)
                return true;
            else
                return false;

        }

        private void ScanForAttributeElements(XElement parent, List<CodeAnalyzer.CodeResourceRef> listCRR, string xmlFile)
        {
            foreach (var xeChild in parent.Descendants())
            {
                foreach (var xa in xeChild.Attributes())
                {
                    
                    if(ContainsResourceRef(xa.Value.ToString()))
                    {
                        System.Xml.IXmlLineInfo info = xa;
                        CodeAnalyzer.CodeResourceRef crr = new CodeAnalyzer.CodeResourceRef();
                        crr.ClassFile = xmlFile;
                        crr.CodeLineNumber = info.LineNumber.ToString();

                        CodeAnalyzer.ResourceRef rr = new CodeAnalyzer.ResourceRef();
                        rr.Category = "XML";
                        rr.RefId = xa.Value.Replace("@","R.").Replace("/","."); // @drawable/xxxx
                        rr.RefName = "RefName";


                        crr.Ref = rr;


                        listCRR.Add(crr);

       
       

        


                    System.Diagnostics.Debug.WriteLine(xa.Name.LocalName + "=" + xa.Value.ToString()+" at "+info.LineNumber.ToString());
                    }

                
                }
            }

        }




        private List<CodeAnalyzer.CodeResourceRef> ScanXMLFiles(string ProjectFolder)
        {
            string[] sXMLFiles = Directory.GetFiles(ProjectFolder, "*.xml", SearchOption.AllDirectories);

            List<CodeAnalyzer.CodeResourceRef> listCR = new List<CodeAnalyzer.CodeResourceRef>();
            foreach (var sXMLFile in sXMLFiles)
            {
                XDocument xd = XDocument.Load(sXMLFile,LoadOptions.SetLineInfo);
                XElement xeRoot = xd.Root;
                System.Diagnostics.Debug.WriteLine("XMLFile=" + sXMLFile);
                ScanForAttributeElements(xeRoot, listCR,  System.IO.Path.GetFileName(sXMLFile));
            }

            return listCR;

        }

        private void BeginScanProject()
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
            List<CodeAnalyzer.CodeResourceRef> listCR = ScanXMLFiles(sActiveAndroidProjectFolder);

            

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
            DataColumn dcURType = new DataColumn("Type", typeof(string));

            dtUsedRes.Columns.Add(dcURRefId);
            dtUsedRes.Columns.Add(dcURRowLine);
            dtUsedRes.Columns.Add(dcURClassFile);
            dtUsedRes.Columns.Add(dcURType);

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


                foreach (var lcr in listCR)
                {
                    DataRow dr = dtUsedRes.NewRow();
                    dr["RefId"] = lcr.Ref.RefId;
                    dr["RowLine"] = lcr.CodeLineNumber;
                    dr["ClassFile"] = lcr.ClassFile;
                    dtUsedRes.Rows.Add(dr);
                }

            }

            dgvUsedResources.DataSource = null;
            dgvUsedResources.DataSource = ds.Tables["Used resources"];

            DataGridViewComboBoxColumn dcAction = new DataGridViewComboBoxColumn();
            dgvUsedResources.Columns.Add(dcAction);



            // Find Unused Resources

            DataTable dtUnusedRes = new DataTable("Unused resources");
            DataColumn dcUURCategory = new DataColumn("Category", typeof(string));
            DataColumn dcUURName = new DataColumn("Name", typeof(string));
            DataColumn dcUURRefId = new DataColumn("RefId", typeof(string));
            DataColumn dcIgnore = new DataColumn("Ignore", typeof(Boolean));

            dtUnusedRes.Columns.Add(dcUURCategory);
            dtUnusedRes.Columns.Add(dcUURName);
            dtUnusedRes.Columns.Add(dcUURRefId);
            dtUnusedRes.Columns.Add(dcIgnore);

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

        private void dgvUnusedResources_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.ColumnIndex == 3)
            {
                DataTable dt = (DataTable)dgvUnusedResources.DataSource;
                if (dt.Rows[e.RowIndex][3] == DBNull.Value)
                    dt.Rows[e.RowIndex][3] = true;
                else
                    dt.Rows[e.RowIndex][3] = !((bool)dt.Rows[e.RowIndex][3]);
            }

        }

        private void tsbBeginScanProject_Click(object sender, EventArgs e)
        {
            BeginScanProject();
        }

        private void dgvUnusedResources_SelectionChanged(object sender, EventArgs e)
        {
            tsbCheck.Enabled = true;
            tsbUncheck.Enabled = true;
        }

        private void tsbCheck_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvUnusedResources.DataSource;
            foreach (var r in dgvUnusedResources.SelectedRows)
            {
                DataGridViewRow dgvr = (DataGridViewRow)r;
                int ind = dgvr.Index;
                dt.Rows[ind][3] = true;
            }


        }

        private void tsbUncheck_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvUnusedResources.DataSource;
            foreach (var r in dgvUnusedResources.SelectedRows)
            {
                DataGridViewRow dgvr = (DataGridViewRow)r;
                int ind = dgvr.Index;
                dt.Rows[ind][3] = false;
            }
        }
    }
}
