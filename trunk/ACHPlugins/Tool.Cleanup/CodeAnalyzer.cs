using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ACHPlugin
{
    public class CodeAnalyzer
    {

        //TypeDeclaration 'R' at line10
        //NodeName:R,NodeType:42
        //TypeDeclaration 'attr' at line11
        //NodeName:attr,NodeType:42
        //TypeDeclaration 'drawable' at line13
        //NodeName:drawable,NodeType:42
        //Declaration of 'brainitizelogo' at line14
        //NodeName:brainitizelogo,NodeType:42
        //Declaration of 'c_r10_c1' at line15
        //NodeName:c_r10_c1,NodeType:42
        //Declaration of 'c_r10_c10' at line16

        public class ResourceRef
        {
            public string Category;
            public string RefName;
        }

        private static string ExtractValue(string ValueText)
        {
            int nStart = ValueText.IndexOf('\'')+1;
            int nLen =  ValueText.LastIndexOf('\'') - nStart;
            string sValue = ValueText.Substring(nStart, nLen);
            return sValue;
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
                    listRR.Add(rr);
                }

            }
            sr.Close();
            return listRR.ToArray() ;
        }

    
    }
}
