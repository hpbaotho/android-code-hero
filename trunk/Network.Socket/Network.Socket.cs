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
    public partial class Network_Socket : UserControl
    {

        public string PluginGuid
        {
            get { return "FC85C33C-3A2E-47C5-9E1A-051C45928976"; }
        }
        
        public string PluginName
        {
            get
            {
                return "ACH Socket Engine";
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
                return "Network_Socket";
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
            PluginInstanceName = "ACH Socket Engine";
            return PluginInstanceId;
        }

        public Bitmap PluginIcon
        {
            get
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                Stream stream = asm.GetManifestResourceStream("ACHPlugin.Images.socket.png");
                Bitmap bmp = new Bitmap(stream);
                stream.Close();
                return bmp;
            }

        }

        public Network_Socket()
        {
            InitializeComponent();
        }
    }
}
