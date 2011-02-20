using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Xml.Linq;

namespace AndroidCodeHero
{
    public partial class Main : Form
    {

        public static string GetTempDirectory()
        {
            string path;
            do
            {
                path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            }
            while (Directory.Exists(path));
            Directory.CreateDirectory(path);
            return path;
        }

        private object ACHCodeGenerator = null;

        public string AnalyzeProjectSourceCode()
        {

            if (ACHCodeGenerator == null)
            {
                try
                {
                    Assembly asmACG = LoadAssemblyPluginToMemory(GetCoreFolder() + "\\ACHCodeGenerator.dll");
                    ACHCodeGenerator = Activator.CreateInstance(asmACG.GetType("ACHCodeGenerator.Analyzer"));
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            // Analyze Java Source Code
            string TempDir = GetTempDirectory();
            string[] CodeFiles = System.IO.Directory.GetFiles(Properties.Settings.Default.LastUsedProjectFolder, "*.java", SearchOption.AllDirectories);

            MethodInfo miAnalyzer = ACHCodeGenerator.GetType().GetMethod("AnalyzeFiles");

            miAnalyzer.Invoke(ACHCodeGenerator, new object[] { GetCoreFolder(), CodeFiles, TempDir });

            //ACHCodeGenerator.Analyzer.AnalyzeFiles(CodeFiles,TempDir);

            return TempDir;
        }

        UserControl CreateInstanceOfUserControlPlugin(Assembly asmUC)
        {
            UserControl UControl = null;
            try
            {
                Type[] tlist = asmUC.GetTypes();
                foreach (Type t in tlist)
                {
                    if (t.BaseType == typeof(UserControl))
                    {
                        UControl = Activator.CreateInstance(t) as UserControl;
                        break;
                    }
                }
            }
            catch (Exception ex) { UControl = null; }
            return UControl;
        }


        Assembly LoadAssemblyPluginToMemory(string PluginPath)
        {
            try
            {
                byte[] buffer = File.ReadAllBytes(PluginPath);
                Assembly asm = Assembly.Load(buffer);
                return asm;
            }
            catch (Exception ex) { return null; }
        }

        public string GetPluginFolder()
        {
            return System.Configuration.ConfigurationManager.AppSettings["PluginFolder"].ToString();
        }

        public string GetCoreFolder()
        {
            return System.Configuration.ConfigurationManager.AppSettings["CoreFolder"].ToString();
        }

        public class PluginInfo
        {
            public string Name;
            public string Description;
            public string CategoryId;
            public string Author;
            public string Version;
            public string HelpPage;
            public Bitmap TreeIcon;
            public string Guid;
            public Assembly UserControlAsm;
        }

        public PluginInfo[] ReadPluginList()
        {
            try
            {
                List<PluginInfo> listPI = new List<PluginInfo>();
                string[] DLLs = System.IO.Directory.GetFiles(GetPluginFolder(), "*.dll");
                foreach (var dllfile in DLLs)
                {
                    try
                    {
                        PluginInfo pi = new PluginInfo();
                        Assembly asmUC = LoadAssemblyPluginToMemory(dllfile);
                        pi.UserControlAsm = asmUC;
                        UserControl UCPlugin = CreateInstanceOfUserControlPlugin(asmUC);
                        pi.Name = (string)UCPlugin.GetType().UnderlyingSystemType.InvokeMember("PluginName", BindingFlags.GetProperty, null, UCPlugin, null);
                        pi.Description = (string)UCPlugin.GetType().UnderlyingSystemType.InvokeMember("PluginDescription", BindingFlags.GetProperty, null, UCPlugin, null);
                        pi.Author = (string)UCPlugin.GetType().UnderlyingSystemType.InvokeMember("PluginAuthor", BindingFlags.GetProperty, null, UCPlugin, null);
                        pi.CategoryId = (string)UCPlugin.GetType().UnderlyingSystemType.InvokeMember("PluginCategoryId", BindingFlags.GetProperty, null, UCPlugin, null);
                        pi.HelpPage = (string)UCPlugin.GetType().UnderlyingSystemType.InvokeMember("PluginHelpPage", BindingFlags.GetProperty, null, UCPlugin, null);
                        pi.Version = (string)UCPlugin.GetType().UnderlyingSystemType.InvokeMember("PluginVersion", BindingFlags.GetProperty, null, UCPlugin, null);
                        pi.Guid = (string)UCPlugin.GetType().UnderlyingSystemType.InvokeMember("PluginGuid", BindingFlags.GetProperty, null, UCPlugin, null);
                        pi.TreeIcon = (Bitmap)UCPlugin.GetType().UnderlyingSystemType.InvokeMember("PluginIcon", BindingFlags.GetProperty, null, UCPlugin, null);
                        UCPlugin.Dispose();
                        listPI.Add(pi);
                    }
                    catch (System.Exception ex)
                    {

                    }

                }

                return listPI.ToArray();


            }
            catch (System.Exception ex)
            {
                return null;
            }
        
        }

        public class Category
        {
            static public Category[] GetCategoryTreeFromFile(string FileName)
            {
                try
                {
                    List<Category> listCat = new List<Category>();
                    XDocument xd = XDocument.Load(FileName);
                    var xmlcats = xd.Element("categories").Elements("category");
                    foreach (var xmlcat in xmlcats)
                    {
                        Category cat = new Category();
                        cat.Id = xmlcat.Attribute("id").Value;
                        try
                        {
                            cat.ParentId = xmlcat.Attribute("parent").Value;
                        }
                        catch (System.Exception) { }
                        cat.Name = xmlcat.Element("name").Value;
                        cat.Description = xmlcat.Element("description").Value;
                        try
                        {
                            cat.CategoryIcon = new Bitmap("Images\\"+xmlcat.Element("image").Value);
                            cat.IconName = xmlcat.Element("image").Value;
                        }
                        catch (System.Exception) { }

                        listCat.Add(cat);

                    }

                    List<Category> listFinalCatList = new List<Category>();
                    foreach (var lcat in listCat)
                    {
                        if (lcat.ParentId == null)
                        {
                            listFinalCatList.Add(lcat);
                            List<Category> listSubCat = new List<Category>();
                            foreach (var subcat in listCat)
                            {
                                if (subcat.ParentId != null && subcat.ParentId.Equals(lcat.Id))
                                {
                                    subcat.ParentCategory = lcat;
                                    listSubCat.Add(subcat);

                                    var sorteSubList = from s in listSubCat
                                                          orderby s.Name
                                                          select s;

                                    lcat.SubCategories = new List<Category>(sorteSubList).ToArray();
                                }
                            }
                        }
                    }

                    var sortedFinalList = from s in listFinalCatList
                                          orderby s.Name
                                          select s;

                    return new List<Category>(sortedFinalList).ToArray();
                }
                catch (System.Exception ex)
                {
                    return null;
                }
            
            }
                
            public string Id;
            public Bitmap CategoryIcon;
            public string IconName;
            public string Name;
            public string Description;
            public Category[] SubCategories;
            public Category ParentCategory;
            public string ParentId;
        }

        private ImageList ilTreeIcons = new ImageList();
        private PluginInfo[] pis = null;

        private PluginInfo FindPIFromCategoryId(string CategoryId)
        {
            foreach (var p in pis)
            {
                if(p.CategoryId.Equals(CategoryId))
                    return p;
            }
            return null;
        }

        public Main()
        {
            InitializeComponent();

            pis = ReadPluginList();
            Category[] categories = Category.GetCategoryTreeFromFile("Categories.xml");

            tvAndroidProject.ImageList = ilTreeIcons;
            Bitmap bmpAndProj = new Bitmap("Images\\androidproject.png");
            ilTreeIcons.Images.Add("AndroidProject", bmpAndProj);
            tvAndroidProject.Nodes[0].SelectedImageKey = "AndroidProject";
            tvAndroidProject.Nodes[0].ImageKey = "AndroidProject";

            FillNodesFromCategoryList(tvAndroidProject.Nodes[0], categories);

            tvAndroidProject.ExpandAll();
            tvAndroidProject.BeforeCollapse += new TreeViewCancelEventHandler(tvAndroidProject_BeforeCollapse);
            tvAndroidProject.NodeMouseClick += new TreeNodeMouseClickEventHandler(tvAndroidProject_NodeMouseClick);
            tvAndroidProject.AfterLabelEdit += new NodeLabelEditEventHandler(tvAndroidProject_AfterLabelEdit);
            tvAndroidProject.BeforeLabelEdit += new NodeLabelEditEventHandler(tvAndroidProject_BeforeLabelEdit);

            string LastProjectFolder = Properties.Settings.Default.LastUsedProjectFolder;
            if (LastProjectFolder.Length > 0)
                SetProjectFolder(LastProjectFolder);




        }

        void tvAndroidProject_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Name != null && e.Node.Name.StartsWith("plgid_"))
                e.CancelEdit = false;
            else
                e.CancelEdit = true;
        
        }

        void tvAndroidProject_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null && e.Label.Length == 0)
                e.CancelEdit = true;
            else
            {
                UserControl UCPlugin = (UserControl)tvAndroidProject.SelectedNode.Tag;
                UCPlugin.GetType().UnderlyingSystemType.InvokeMember("PluginInstanceName", BindingFlags.SetProperty, null, UCPlugin, new object[] { e.Label });
                string s = (string)UCPlugin.GetType().UnderlyingSystemType.InvokeMember("PluginInstanceName", BindingFlags.GetProperty, null, UCPlugin, null);

            }
        }

        private void FillNodesFromCategoryList(TreeNode ParentNode, Category[] categories)
        {
            foreach (var cat in categories)
            {
                TreeNode tn = new TreeNode();
                tn.Name = cat.Id;
                tn.Tag = cat;

                if (FindPIFromCategoryId(cat.Id) != null)
                    tn.NodeFont = new System.Drawing.Font(((Control)tvAndroidProject).Font, FontStyle.Bold);

                tn.Text = cat.Name;
                if (!ilTreeIcons.Images.ContainsKey(cat.IconName))
                    ilTreeIcons.Images.Add(cat.IconName, cat.CategoryIcon);
                tn.SelectedImageKey = cat.IconName;
                tn.ImageKey = cat.IconName;
                ParentNode.Nodes.Add(tn);
                if (cat.SubCategories != null)
                {
                    FillNodesFromCategoryList(tn, cat.SubCategories);
                }



            }
        }

        void tvAndroidProject_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                tvAndroidProject.SelectedNode = e.Node;

                if (e.Node.Name != null && FindPIFromCategoryId(e.Node.Name) != null)
                {
                    PluginInfo pi = FindPIFromCategoryId(e.Node.Name);

                    ContextMenu cm = new ContextMenu();
                    MenuItem mi = cm.MenuItems.Add("Create instance of " + pi.Name);
                    mi.Tag = pi;
                    mi.Click += new EventHandler(CreatePluginInstance_Click);

                    cm.Show(tvAndroidProject, new Point(e.X, e.Y));

                }

                if (e.Node.Name != null && e.Node.Name.StartsWith("plgid_"))
                {

                    UserControl uc = (UserControl)e.Node.Tag;

                    ContextMenu cm = new ContextMenu();
                    
                    MenuItem miRename = cm.MenuItems.Add("Rename");
                    miRename.Tag = uc;
                    miRename.Click += new EventHandler(RenamePluginInstanceName_Click);

                    MenuItem miRemove = cm.MenuItems.Add("Remove");
                    miRemove.Tag = uc;
                    miRemove.Click += new EventHandler(RemovePluginInstance_Click);


                    cm.Show(tvAndroidProject, new Point(e.X, e.Y));

                }


            }

            this.splitMain.Panel2.Controls.Clear();
            if (e.Node.Tag != null && e.Node.Name.StartsWith("plgid_"))
            {
                ((UserControl)e.Node.Tag).Dock = DockStyle.Fill;
                this.splitMain.Panel2.Controls.Add((UserControl)e.Node.Tag);
                //AndroidCodeHero.Controls.DataTable cDT = new Controls.DataTable();
                //this.splitMain.Panel2.Controls.Add(cDT);
            }

        }

        void RenamePluginInstanceName_Click(object sender, EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            UserControl uc = (UserControl)mi.Tag;
            tvAndroidProject.SelectedNode.BeginEdit();

        }

        void RemovePluginInstance_Click(object sender, EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            UserControl uc = (UserControl)mi.Tag;
            if (MessageBox.Show("Would you like to remove Instance from code ?", "Remove", MessageBoxButtons.YesNoCancel) == System.Windows.Forms.DialogResult.Yes)
            {

                TreeNode tnParent = tvAndroidProject.SelectedNode.Parent;

                
                tvAndroidProject.SelectedNode.Remove();

                UpdateNumOfPlugins(tnParent, tnParent.Nodes.Count);


                this.splitMain.Panel2.Controls.Clear();
            }

        }

        void UpdateNumOfPlugins(TreeNode tn, int NumOfPlugins)
        {
            if(tn.Tag != null && tn.Tag.GetType().Name.EndsWith("Category"))
            {
                Category cat = (Category)tn.Tag;
                if (NumOfPlugins > 0)
                    tn.Text = cat.Name + " (" + NumOfPlugins.ToString() + ")";
                else
                    tn.Text = cat.Name;
            }

        }

        void CreatePluginInstance_Click(object sender, EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            PluginInfo pi = (PluginInfo)mi.Tag;

            UserControl UCPlugin = CreateInstanceOfUserControlPlugin(pi.UserControlAsm);
            string PluginInstanceId = (string)UCPlugin.GetType().UnderlyingSystemType.InvokeMember("PluginCreateInstance", BindingFlags.InvokeMethod, null, UCPlugin, null);

            if(!ilTreeIcons.Images.ContainsKey("tv_" + pi.Guid.ToString()))
                ilTreeIcons.Images.Add("tv_" + pi.Guid.ToString(), pi.TreeIcon);

            TreeNode tnPlugin = new TreeNode();
            tnPlugin.ImageKey = "tv_" + pi.Guid.ToString();
            tnPlugin.SelectedImageKey = "tv_" + pi.Guid.ToString();
            tnPlugin.Text = pi.Name;

            tnPlugin.Name = "plgid_" + pi.Guid + PluginInstanceId;
            tnPlugin.Tag = UCPlugin;

            tvAndroidProject.SelectedNode.Nodes.Add(tnPlugin);

            UpdateNumOfPlugins(tvAndroidProject.SelectedNode, tvAndroidProject.SelectedNode.Nodes.Count);

            tvAndroidProject.ExpandAll();


            
        
        }

        void tvAndroidProject_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        public string sAssistingAndroidProject
        {
            get;
            set;
        }

        private bool SetProjectFolder(string FolderName)
        {
            if (!File.Exists(FolderName + "\\AndroidManifest.xml"))
            {
                sAssistingAndroidProject = "";
                MessageBox.Show("Selected Folder Does Not Contain An Android Project", "Error");
                return false;
            }
            else
            {
                tslAssistingAndroidProject.Text = "Assisting Android Project : " + FolderName;
                sAssistingAndroidProject = FolderName;
                Properties.Settings.Default.LastUsedProjectFolder = FolderName;
                Properties.Settings.Default.Save();

                return true;


            }

        }


        private void tsbAssistAndroidProject_Click(object sender, EventArgs e)
        {


            string LastProjectFolder = Properties.Settings.Default.LastUsedProjectFolder;
            if (LastProjectFolder.Length > 0)
                fbdAndroidProject.SelectedPath = LastProjectFolder;
            
            if (fbdAndroidProject.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string FolderName = fbdAndroidProject.SelectedPath;
                SetProjectFolder(FolderName);

            }
        }

    
    }
}
