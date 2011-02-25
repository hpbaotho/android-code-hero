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

        private void btnScanAndroidProject_Click(object sender, EventArgs e)
        {

            Control cntrlParent = Parent;

            while (cntrlParent.Parent != null)
                cntrlParent = cntrlParent.Parent;

            MethodInfo miAPSC = cntrlParent.GetType().UnderlyingSystemType.GetMethod("AnalyzeProjectSourceCode");
            object oResult = miAPSC.Invoke(cntrlParent, null);

            string sResultFolder = (string)oResult;

            var resPRF = CodeAnalyzer.ParseResourceFile(sResultFolder + "\\R.jsp");

            DataSet ds = new DataSet();
            DataTable dt = new DataTable("All resources");
            DataColumn dcCategory = new DataColumn("Category", typeof(string));
            DataColumn dcName = new DataColumn("Name", typeof(string));
            dt.Columns.Add(dcCategory);
            dt.Columns.Add(dcName);
            ds.Tables.Add(dt);

            foreach (var r in resPRF)
            {
                DataRow dr = dt.NewRow();
                dr["Category"] = r.Category;
                dr["Name"] = r.RefName;
                dt.Rows.Add(dr);
            }

            dgvAllResources.DataSource = null;
            dgvAllResources.DataSource = ds.Tables[0];




        }
    }
}
