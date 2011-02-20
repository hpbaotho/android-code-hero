using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace ACHCodeGenerator
{
    public class Analyzer
    {

        public static string GetTempDirectory() {
        string path;
        do
        {
            path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        }
        while (Directory.Exists(path));
        Directory.CreateDirectory(path); 
        return path; 
        }

        static AnalyzerProgressBar apb = null;

        internal class AFHolder
        {
            public string[] FileNames;
            public string OutputDir;
            public BackgroundWorker bw;

            public AFHolder(string[] FileNames, string OutputDir)
            {
                this.FileNames = FileNames;
                this.OutputDir = OutputDir;
            }

        }

        public static void AnalyzeFiles(string[] FileNames, string OutputDir)
        {
            apb = new AnalyzerProgressBar(FileNames.Length);

            BackgroundWorker bw = new BackgroundWorker();
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);

            bw.WorkerReportsProgress = true;

            AFHolder afh = new AFHolder(FileNames, OutputDir);
            afh.bw = bw;

            bw.RunWorkerAsync(afh);

            apb.ShowDialog();

        }

        public static void FindResourceReferences(string InputDir)
        {

            //NodeName:Yellow,NodeType:42
            //QualifiedName 'R.drawable.c_r5_c1' at line1052
            //QualifiedName 'R.drawable' at line1052

        }


        static void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            apb.ProgressValue = e.ProgressPercentage;
        }

        static void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            AFHolder afh = (AFHolder)e.Argument;
            for (int n = 0; n < afh.FileNames.Length; n++)
            {
                afh.bw.ReportProgress(n + 1);
                AnalyzeFile(afh.FileNames[n], afh.OutputDir);
            }

        
        
        }



        static void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            apb.Close();            
        }

        private static void AnalyzeFile(string FileName, string OutputDir)
        {

            //java -jar javaparser.jar 
            //"C:\MyAndroidProjects\Dev\android\workspace\Brainitize\src\brainitize\paradoxia\se\Brainitize.java" 
            //"C:\Users\Mikael\Documents\Visual Studio 2010\Projects\AndroidCodeHero\Antlr\ast"


            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "\"" + @"javaw" + "\"";
            p.StartInfo.Arguments = "-jar javaparser.jar " + "\"" + FileName + "\" \"" + OutputDir + "\"";

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            p.Start();
            p.WaitForExit();

            return;
            
            //string InputFile = FileName;
            //StreamReader sr = new StreamReader(InputFile);
            //string Buffer = sr.ReadToEnd();
            //sr.Close();

            //Chunk CodeChunk = new Chunk();
            //var r = ParseText(Buffer, CodeChunk);

            //string sOut = VisualDrawCodeChunk(CodeChunk, 0);


        }

        //public class Chunk
        //{
        //    public string ChunkText;
        //    public List<Chunk> listChunk = new List<Chunk>();
        //}

        //static bool IsCommentStart(string Text)
        //{
        //    if (Text.Length >= 2)
        //    {
        //        if (Text.Substring(0, 2).Equals("/*") | Text.Substring(0, 2).Equals("//"))
        //            return true;
        //    }
        //    return false;
        //}

        //static int HowLongIsComment(string Text)
        //{
        //    if (Text.Substring(0, 2).Equals("/*"))
        //    {
        //        for (int n = 0; n < Text.Length - 1; n++)
        //        {
        //            if (Text.Substring(n, 2).Equals("*/"))
        //                return n + 2;
        //        }
        //    }
        //    if (Text.Substring(0, 2).Equals("//"))
        //    {
        //        for (int n = 0; n < Text.Length - 1; n++)
        //        {
        //            if (Text.Substring(n, 2).Equals("\n"))
        //                return n + 2;
        //        }
        //    }
        //    return 0;


        //}

        //public static string VisualDrawCodeChunk(Chunk CodeChunk, int nLevel)
        //{

        //    string sOutput = CodeChunk.ChunkText;

        //    string sTab = string.Empty;
        //    for (int n = 0; n < nLevel; n++)
        //    {
        //        sTab += "---";
        //    }
        //    sOutput = sOutput.Replace("\n", "\n" + sTab);

        //    for (int m = 0; m < CodeChunk.listChunk.Count; m++)
        //    {
        //        sOutput += VisualDrawCodeChunk(CodeChunk.listChunk[m], nLevel + 1);

        //    }

        //    return sOutput;

        //}

        //public static int ParseText(string Text, Chunk CodeChunk)
        //{
        //    CodeChunk.ChunkText = string.Empty;
        //    int nParsePointer;
        //    for (nParsePointer = 0; nParsePointer < Text.Length; nParsePointer++)
        //    {

        //        string SubText = Text.Substring(nParsePointer);

        //        if (IsCommentStart(SubText))
        //        {
        //            nParsePointer += HowLongIsComment(SubText);
        //        }

        //        if (Text[nParsePointer].Equals('{'))
        //        {
        //            Chunk ccchild = new Chunk();
        //            nParsePointer++;
        //            SubText = Text.Substring(nParsePointer);
        //            nParsePointer += ParseText(SubText, ccchild);
        //            CodeChunk.listChunk.Add(ccchild);

        //        }
        //        else if (Text[nParsePointer].Equals('}'))
        //        {
        //            return nParsePointer+1;
        //        }
        //        else
        //            CodeChunk.ChunkText += Text[nParsePointer];

        //    }

        //    return nParsePointer;
        //}



    }
}