using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ACHPlugin
{
    public class CodeAnalyzer
    {

        public class CodeResourceRef
        {
            public string ClassFile;
            public ResourceRef Ref;
            public string CodeLineNumber;
        }

        public class ResourceRef
        {
            public string Category;
            public string RefName;
            public string RefId;
        }

        private static string ExtractRowNumber(string ValueText)
        {
            int nStart = ValueText.IndexOf("at line") + "at line".Length;
            string sValue = ValueText.Substring(nStart);
            return sValue;
        }

        private static string ExtractValue(string ValueText)
        {
            int nStart = ValueText.IndexOf('\'')+1;
            int nLen =  ValueText.LastIndexOf('\'') - nStart;
            string sValue = ValueText.Substring(nStart, nLen);
            return sValue;
        }

        private static ResourceRef FindRR(ResourceRef[] RRs, string RefId)
        {
            foreach (var rr in RRs)
            {
                if (rr.RefId.Equals(RefId))
                    return rr;
            }
            return null;
        }

        public static CodeResourceRef[] ParseJavaFile(ResourceRef[] RR, string FilePath)
        {
            StreamReader sr = new StreamReader(FilePath);
            List<CodeResourceRef> listRR = new List<CodeResourceRef>();
            while (!sr.EndOfStream)
            {
                string sLine = sr.ReadLine();
                if (sLine.StartsWith("QualifiedName"))
                {
                    string curDec = ExtractValue(sLine);
                    ResourceRef rr = FindRR(RR, curDec);
                    if (rr != null)
                    {

                        CodeResourceRef crr = new CodeResourceRef();
                        crr.CodeLineNumber = ExtractRowNumber(sLine);
                        crr.Ref = rr;
                        crr.ClassFile = Path.GetFileNameWithoutExtension(FilePath) + ".java";
                        listRR.Add(crr);
                    }
                }

            }
            sr.Close();
            return listRR.ToArray();
        }


        public static ResourceRef[] ParseResourceFile(string FilePath)
        {
            StreamReader sr = new StreamReader(FilePath);
            List<ResourceRef> listRR = new List<ResourceRef>();
            string curTypeDec = string.Empty;
            while(!sr.EndOfStream)
            {
                string sLine = sr.ReadLine();
                if (sLine.StartsWith("TypeDeclaration"))
                {
                    curTypeDec = ExtractValue(sLine);
                }
                if (sLine.StartsWith("Declaration"))
                {
                    string curDec = ExtractValue(sLine);
                    ResourceRef rr = new ResourceRef();
                    rr.Category = curTypeDec;
                    rr.RefName = curDec;
                    rr.RefId = "R." + rr.Category + "." + rr.RefName;
                    listRR.Add(rr);
                }

            }
            sr.Close();
            return listRR.ToArray() ;
        }

    
    }
}
