using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ACHPlugin
{
    public class CodeAnalyzer
    {

        static private char[] _ValidVariableCharacters = null;

        static private char[] ValidVariableCharacters()
        {
            if (_ValidVariableCharacters == null)
            {
                List<char> listChar = new List<char>();
                for (int n = 'a'; n <= 'z'; n++)
                    listChar.Add((char)n);
                for (int n = 'A'; n <= 'Z'; n++)
                    listChar.Add((char)n);
                for (int n = '0'; n <= '9'; n++)
                    listChar.Add((char)n);
                
                _ValidVariableCharacters = listChar.ToArray();
                return _ValidVariableCharacters;

            }
            else
                return _ValidVariableCharacters;

        }

        public static CodeResourceRef[] AnalyzeCodeFile(string[] Categories,string FilePath)
        {

            try
            {
                StreamReader sr = new StreamReader(FilePath);
                int nLineNumber = 1;
                List<CodeResourceRef> listCRR = new List<CodeResourceRef>();
                while (!sr.EndOfStream)
                {
                    string sLine = sr.ReadLine();
                    foreach (var c in Categories)
                    {
                        int start = sLine.IndexOf("R." + c + ".");
                        if (start >= 0)
                        {
                            int end = sLine.LastIndexOfAny(ValidVariableCharacters());
                            string sRef = sLine.Substring(start, (end - start)+1);
                            CodeResourceRef crr = new CodeResourceRef();
                            crr.ClassFile = Path.GetFileName(FilePath);
                            crr.CodeLineNumber = nLineNumber.ToString();
                            crr.CodePosistion = start.ToString();
                            ResourceRef rr = new ResourceRef();
                            rr.Category = c;
                            rr.RefId = sRef;
                            rr.RefName = sRef.Split('.')[2];
                            crr.Ref = rr;
                            listCRR.Add(crr);

                        }


                    }
                    nLineNumber++;

                }
                sr.Close();
                return listCRR.ToArray();
            }
            catch (System.Exception ex)
            {

            }


            return null;
        }


        public static ResourceRef[] AnalyzeRFile(string FilePath)
        {

            
            try
            {
                StreamReader sr = new StreamReader(FilePath);
                string CurrentCategory = string.Empty;
                List<string> listElements = new List<string>();
                List<ResourceRef> listRR = new List<ResourceRef>();
                while (!sr.EndOfStream)
                {
                    string sLine = sr.ReadLine();
                    
                    //public static final class attr {
                    
                    int indxclassdef = sLine.IndexOf("public static final class");
                    if(indxclassdef >= 0)
                    {
                        int end = sLine.IndexOf('{');
                        int start = indxclassdef + "public static final class".Length;
                        CurrentCategory = sLine.Substring(start, end - start).Trim();
                    }

                    //public static final int alpha=0x7f020000;

                    int indxresourcedef = sLine.IndexOf("public static final int");
                    if (indxresourcedef >= 0)
                    {
                        int end = sLine.IndexOf('=');
                        int start = indxresourcedef + "public static final int".Length;
                        string CurrentValue = sLine.Substring(start, end - start).Trim();
                        string[] cvs = CurrentValue.Split('=');
                        string Value = cvs[0];

                        ResourceRef rr = new ResourceRef();
                        rr.Category = CurrentCategory;
                        rr.RefId = "R." + rr.Category +"." +Value;
                        rr.RefName = Value;
                        listRR.Add(rr);
                    }
                }
                sr.Close();
                return listRR.ToArray();


            }
            catch (System.Exception ex)
            {
                return null;
            }

        }

        public class CodeResourceRef
        {
            public string ClassFile;
            public ResourceRef Ref;
            public string CodeLineNumber;
            public string CodePosistion;
        }

        public class ResourceRef
        {
            public string Category;
            public string RefName;
            public string RefId;
        }

        //private static string ExtractRowNumber(string ValueText)
        //{
        //    int nStart = ValueText.IndexOf("at line") + "at line".Length;
        //    string sValue = ValueText.Substring(nStart);
        //    return sValue;
        //}

        //private static string ExtractValue(string ValueText)
        //{
        //    int nStart = ValueText.IndexOf('\'')+1;
        //    int nLen =  ValueText.LastIndexOf('\'') - nStart;
        //    string sValue = ValueText.Substring(nStart, nLen);
        //    return sValue;
        //}

        private static ResourceRef FindRR(ResourceRef[] RRs, string RefId)
        {
            foreach (var rr in RRs)
            {
                if (rr.RefId.Equals(RefId))
                    return rr;
            }
            return null;
        }

        private static bool RRinCRR(ResourceRef RR, CodeResourceRef[] CRRs)
        {
            foreach (var crr in CRRs)
            {
                if (crr.Ref.Equals(RR))
                    return true;
            }
            return false;

        }

        public static ResourceRef[] UnusedResources(ResourceRef[] RRs, CodeResourceRef[] TotalCRR)
        {

            List<ResourceRef> listUnusedRR = new List<ResourceRef>();

            foreach (var rr in RRs)
            {
                if (!RRinCRR(rr, TotalCRR))
                    listUnusedRR.Add(rr);
            }


            return listUnusedRR.ToArray();

        }

        //public static CodeResourceRef[] ParseJavaFile(ResourceRef[] RR, string FilePath)
        //{
        //    StreamReader sr = new StreamReader(FilePath);
        //    List<CodeResourceRef> listRR = new List<CodeResourceRef>();
        //    while (!sr.EndOfStream)
        //    {
        //        string sLine = sr.ReadLine();
        //        if (sLine.StartsWith("QualifiedName"))
        //        {
        //            string curDec = ExtractValue(sLine);
        //            ResourceRef rr = FindRR(RR, curDec);
        //            if (rr != null)
        //            {

        //                CodeResourceRef crr = new CodeResourceRef();
        //                crr.CodeLineNumber = ExtractRowNumber(sLine);
        //                crr.Ref = rr;

        //                if (crr.Ref.RefName.Contains("crowd"))
        //                {
        //                    int ddd = 0;
        //                }

        //                crr.ClassFile = Path.GetFileNameWithoutExtension(FilePath) + ".java";
        //                listRR.Add(crr);
        //            }
        //        }

        //    }
        //    sr.Close();
        //    return listRR.ToArray();
        //}

    
    }
}
