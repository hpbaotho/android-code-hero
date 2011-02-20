using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// DGMLContext is a class used to gather information during the actual parse
/// time to be output later
/// </summary>
public class DGMLContext
{
   private string _Name = "";
   private string _Container = "";
   private List<string> _Properties = new List<string>();

   /// <summary>
   /// Container is generally the 'Source' node in a link
   /// </summary>
   public string Container
   {
      get { return _Container; }
      set { _Container = value; }
   }
   /// <summary>
   /// Name is the name of the 'Target' node
   /// </summary>
   public string Name
   {
      get { return _Name; }
      set { _Name = value; }
   }
   /// <summary>
   /// A list of XML attribute / value strings.
   /// 
   /// For example of one entry:
   /// <code>isAbstract="true"</code>
   /// </summary>
   public List<string> Properties
   {
      get { return _Properties; }
   }
}