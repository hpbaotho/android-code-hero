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

        public static string ffmpegPath = @"C:\Users\Mikael\Documents\Visual Studio 2010\Projects\AndroidCodeHero\ffmpeg.exe";

        private class ResourceObject
        {





            // ffmpeg -i audio.wav -acodec vorbis -aq 60 audio.ogg
            //Duration: 00:00:00.43, start: 0.000000, bitrate: 156 kb/s

            private static bool UsesAlphaChannel(string ImageFileName)
            {
                try
                {
                    Bitmap bmpPng = new Bitmap(ImageFileName);
                    for (int h = 0; h < bmpPng.Height; h++)
                    {
                        for (int w = 0; w < bmpPng.Width; w++)
                        {
                            Color col = bmpPng.GetPixel(w, h);
                            int alpha = col.A;
                            if (alpha < 255)
                                return true;
                        }

                    }

                }
                catch (System.Exception ex)
                {


                }
                return false;

            }


            private static void AnalyzeAudioFile(string FilePath, ResourceObject ro)
            {
                try
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = ffmpegPath;
                    p.StartInfo.Arguments = "-i \"" + FilePath + "\"";
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    while (!p.StandardError.EndOfStream)
                    {
                        string sLine = p.StandardError.ReadLine().Trim();
                        if (sLine.StartsWith("Duration: "))
                        {
                            ro.AudioLength = sLine.Substring("Duration: ".Length, "00:00:00.00".Length);
                        }
                        if (sLine.StartsWith("Stream #0.0:"))
                        {
                            string[] sParams = sLine.Substring("Stream #0.0: Audio: ".Length).Split(',');
                            ro.AudioBit = sParams[3].Trim();
                            ro.AudioChannels = sParams[2].Trim();
                            ro.AudioSampleRate = sParams[1].Trim();
                            ro.AudioBitRate = sParams[4].Trim();
                            ro.AudioType = sParams[0].Trim();
                        }

                    }

                    p.WaitForExit();
                }
                catch (System.Exception ex)
                {

                }

            }

            public enum ResourceObjectFileTypes { Xml, AudioWAV, AudioOGG, AudioMP3, ImagePNG, ImageJPG, Other };

            public ResourceObjectFileTypes resourceObjectFileType;

            public string resourceObjectPath; // res/drawable-hdpi/logo.png
            public long resourceObjectSize; // 2000 bytes
            public string resourceObjectName;

            public string ImageSize; // W200:H500
            public bool ImageUsesAlphaChannel; 

            public string AudioBit; // 8-bit, 16 bit
            public string AudioBitRate; // 160 kb/s
            public string AudioSampleRate; // 44Khz
            public string AudioChannels; // Stereo, Mono
            public string AudioLength; // 00:00:10.10
            public string AudioType; // WAV, MP3

            public override string ToString()
            {

                switch (resourceObjectFileType)
                {
                    case ResourceObjectFileTypes.Xml: return "XML";
                    case ResourceObjectFileTypes.Other: return "Other";
                    case ResourceObjectFileTypes.ImagePNG: if (ImageUsesAlphaChannel)
                            return "PNG " + ImageSize + " Using Alpha Channel";
                                else
                            return "PNG " + ImageSize + " Not using Alpha Channel";
                    case ResourceObjectFileTypes.ImageJPG:
                        return "JPG " + ImageSize;
                    case ResourceObjectFileTypes.AudioOGG:
                    case ResourceObjectFileTypes.AudioMP3:
                    case ResourceObjectFileTypes.AudioWAV:
                        return AudioType + "/" + AudioBit + "/" + AudioSampleRate + "/" + AudioChannels + "/" + AudioLength;

                }



                return "Other";
            }

            private static ResourceObjectFileTypes ExtensionToFileType(string FilePath)
            {
                string ext = System.IO.Path.GetExtension(FilePath);

                switch (ext.ToLower())
                {
                    case ".png": return ResourceObjectFileTypes.ImagePNG;
                    case ".bmp": return ResourceObjectFileTypes.ImageJPG;
                    case ".wav": return ResourceObjectFileTypes.AudioWAV;
                    case ".mp3": return ResourceObjectFileTypes.AudioMP3;
                    case ".ogg": return ResourceObjectFileTypes.AudioOGG;
                    case ".xml": return ResourceObjectFileTypes.Xml;
                    default: return ResourceObjectFileTypes.Other;
                }


            }

            static public ResourceObject[] MapResources(string Path)
            {

                List<ResourceObject> listRO = new List<ResourceObject>();
                string[] files = System.IO.Directory.GetFiles(Path, "*.*", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    if (!file.Contains(".svn"))
                    {

                        ResourceObject ro = new ResourceObject();
                        ro.resourceObjectFileType = ExtensionToFileType(file);
                        ro.resourceObjectPath = file.Substring(Path.Length+1);
                        ro.resourceObjectSize = new System.IO.FileInfo(file).Length;
                        ro.resourceObjectName = System.IO.Path.GetFileNameWithoutExtension(file);
                        if (ro.resourceObjectFileType.Equals(ResourceObject.ResourceObjectFileTypes.AudioWAV) || ro.resourceObjectFileType.Equals(ResourceObject.ResourceObjectFileTypes.AudioOGG) || ro.resourceObjectFileType.Equals(ResourceObject.ResourceObjectFileTypes.AudioMP3))
                        {
                            AnalyzeAudioFile(file, ro);
                        }

                        if (ro.resourceObjectFileType.Equals(ResourceObject.ResourceObjectFileTypes.ImagePNG))
                            ro.ImageUsesAlphaChannel = UsesAlphaChannel(file);

                        if (ro.resourceObjectFileType.Equals(ResourceObject.ResourceObjectFileTypes.ImagePNG) || ro.resourceObjectFileType.Equals(ResourceObject.ResourceObjectFileTypes.ImageJPG))
                        {
                            Bitmap bmpTmp = new Bitmap(file);
                            ro.ImageSize = string.Format("{0}x{1}", bmpTmp.Width, bmpTmp.Height);
                        }
                        listRO.Add(ro);
                    }

                }

                return listRO.ToArray();

            }
            

        }

        private ResourceObject[] FindResourceObjectByName(ResourceObject[] ros, string Name)
        {
            var lros = from l in ros
                       where l.resourceObjectName.Equals(Name, StringComparison.CurrentCultureIgnoreCase)
                       select l;

            if (lros.Count() > 0)
            {
                return new List<ResourceObject>(lros).ToArray();
            }
            else
                return null;

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

            ResourceObject[] ros = ResourceObject.MapResources(sActiveAndroidProjectFolder + "\\res");


            

            // Parse R. file

            var resPRF = CodeAnalyzer.ParseResourceFile(sAnalyzeProjectSourceCode + "\\R.jsp");

            DataSet ds = new DataSet();
            DataTable dtAllRes = new DataTable("All resources");
            DataColumn dcARCategory = new DataColumn("Category", typeof(string));
            DataColumn dcARName = new DataColumn("Name", typeof(string));
            DataColumn dcARRefId = new DataColumn("RefId", typeof(string));
            DataColumn dcARType = new DataColumn("Type", typeof(string));
            DataColumn dcARSize = new DataColumn("Size", typeof(string));
            DataColumn dcARResPath = new DataColumn("Path", typeof(string));

            dtAllRes.Columns.Add(dcARCategory);
            dtAllRes.Columns.Add(dcARName);
            dtAllRes.Columns.Add(dcARRefId);
            dtAllRes.Columns.Add(dcARType);
            dtAllRes.Columns.Add(dcARSize);
            dtAllRes.Columns.Add(dcARResPath);


            ds.Tables.Add(dtAllRes);

            foreach (var r in resPRF)
            {
                ResourceObject[] roFounds = FindResourceObjectByName(ros, r.RefName);

                if (roFounds != null)
                {
                    foreach (var roFound in roFounds)
                    {
                        DataRow dr = dtAllRes.NewRow();
                        dr["Category"] = r.Category;
                        dr["Name"] = r.RefName;
                        dr["RefId"] = r.RefId;
                        dr["Type"] = roFound.ToString();
                        dr["Size"] = string.Format("{0} bytes", roFound.resourceObjectSize.ToString("00000000"));
                        dr["Path"] = roFound.resourceObjectPath;
                        dtAllRes.Rows.Add(dr);
                    }

                }
                else
                {
                    DataRow dr = dtAllRes.NewRow();
                    dr["Category"] = r.Category;
                    dr["Name"] = r.RefName;
                    dr["RefId"] = r.RefId;
                    dtAllRes.Rows.Add(dr);
                }
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
            dgvUnusedResources.Columns.Clear();
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
