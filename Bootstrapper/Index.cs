﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 10.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Bootstrapper
{
    using Items;
    using System;
    
    
    #line 1 "C:\Users\Ben\Desktop\Items\Bootstrapper\Index.tt"
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "10.0.0.0")]
    public partial class Index : IndexBase
    {
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
        public virtual string TransformText()
        {
            this.GenerationEnvironment = null;
            this.Write("\r\n");
            this.Write("\r\n<!DOCTYPE html>\r\n<html lang=\"en\">\r\n\t<head>\r\n\t\t<meta charset=\"utf-8\">\r\n\t\t<meta h" +
                    "ttp-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n\t\t<meta name=\"viewport\" content=" +
                    "\"width=device-width, initial-scale=1\">\r\n\t\t<title>Model documentation</title>\r\n\t\t" +
                    "<link href=\"css/bootstrap.min.css\" rel=\"stylesheet\">\r\n\t\t<!--[if lt IE 9]>\r\n\t\t  <" +
                    "script src=\"https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js\"></script>\r\n" +
                    "\t\t  <script src=\"https://oss.maxcdn.com/respond/1.4.2/respond.min.js\"></script>\r" +
                    "\n\t\t<![endif]-->\r\n\t\t\t<style>\r\n\t\t\tbody {\r\n\t\t\t\tcolor: #333;\r\n\t\t\t\tfont-family: \"Helv" +
                    "etica Neue\", Helvetica, Arial, sans-serif;\r\n\t\t\t}\r\n\t\t\th1, .h1,\r\n\t\t\th2, .h2,\r\n\t\t\th" +
                    "3, .h3,\r\n\t\t\th4, .h4,\r\n\t\t\th5, .h5,\r\n\t\t\th6, .h6 {\r\n\t\t\t\tfont-weight: normal;\r\n\t\t\t\tc" +
                    "olor: #333;\r\n\t\t\t}\r\n\t\t\t.mainbody h2, .mainbody h3 {\r\n\t\t\t\tmargin-top: 0;\r\n\t\t\t\tpadd" +
                    "ing: 20px;\r\n\t\t\t\tdisplay: block;\r\n\t\t\t\tbackground-color: #eee;\r\n\t\t\t}\r\n\t\t\t.mainbody" +
                    " h2 {\r\n\t\t\t\tborder-bottom: 2px solid #f88;\r\n\t\t\t}\r\n\t\t\t.header {\r\n\t\t\t\tpadding-top: " +
                    "20px;\r\n\t\t\t\tpadding-bottom: 20px;\r\n\t\t\t}\r\n\t\t\t.title {\r\n\t\t\t\tmargin-top: 30px;\r\n\t\t\t\t" +
                    "margin-bottom: 0;\r\n\t\t\t\tfont-size: 60px;\r\n\t\t\t\tfont-weight: normal;\r\n\t\t\t}\r\n\t\t\t.des" +
                    "cription {\r\n\t\t\t\tfont-size: 20px;\r\n\t\t\t\tcolor: #555;\r\n\t\t\t\tfont-family: Georgia, \"T" +
                    "imes New Roman\", Times, serif;\r\n\t\t\t}\r\n\t\t\t.sidebar {\r\n\t\t\t\tpadding: 15px;\r\n\t\t\t\tmar" +
                    "gin: 0 -15px 15px;\r\n\t\t\t\tbackground-color: #f5f5f5;\r\n\t\t\t\tborder-radius: 4px;\r\n\t\t\t" +
                    "}\r\n\t\t\t.sidebar p:last-child,\r\n\t\t\t.sidebar ul:last-child,\r\n\t\t\t.sidebar ol:last-ch" +
                    "ild {\r\n\t\t\t\tmargin-bottom: 0;\r\n\t\t\t}\r\n\t\t\t.attributedescription {\r\n\t\t\t\tfont-size: 0" +
                    ".9em;\r\n\t\t\t\tfont-weight: normal;\r\n\t\t\t}\r\n\t\t</style>\r\n\t</head>\r\n\t<body>\r\n\t\t<div cla" +
                    "ss=\"container\">\r\n\t\t\t<div class=\"header\">\r\n\t\t\t\t<h1 class=\"title\">Model</h1>\r\n\t\t\t\t" +
                    "<p class=\"description\">Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                    "Cras porttitor nunc in ligula aliquet venenatis. Vestibulum porta, ligula non vu" +
                    "lputate egestas, dolor eros egestas odio, pharetra porta ante urna vitae lectus." +
                    " Vivamus accumsan convallis erat, vel ullamcorper quam egestas vel. Pellentesque" +
                    " rutrum pulvinar mi euismod ullamcorper. Nulla vehicula elit purus. Fusce commod" +
                    "o tempus tempor. Pellentesque quis mi sit amet odio venenatis elementum. Suspend" +
                    "isse sed arcu non enim volutpat lacinia. Fusce sit amet gravida sapien. Nulla ne" +
                    "c est a sapien maximus vestibulum. Pellentesque porttitor eget dolor eu aliquam." +
                    "</p>\r\n\t\t\t</div>\r\n\t\t\t<div class=\"row\">\r\n\t\t\t\t<div class=\"col-sm-8 mainbody\">\r\n\t\t\t\t" +
                    "\t\t");
            
            #line 1 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Items.ttinclude"
 foreach(Item item in model.Items.Values) { 
            
            #line default
            #line hidden
            this.Write("\r\n\t<h2><a name=\"");
            
            #line 3 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Items.ttinclude"
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            
            #line default
            #line hidden
            this.Write("\"></a>");
            
            #line 3 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Items.ttinclude"
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            
            #line default
            #line hidden
            this.Write("</h2>\r\n\r\n\t<p>");
            
            #line 5 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Items.ttinclude"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetDescription(item)));
            
            #line default
            #line hidden
            this.Write("</p>\r\n\r\n\t");
            
            #line 7 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Items.ttinclude"
 foreach(DataAttribute attribute in item.Attributes.Values) { WriteAttribute(attribute, item.IntegerIdentifer == attribute); } 
            
            #line default
            #line hidden
            this.Write("\r\n\t<hr />\r\n\r\n");
            
            #line 11 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Items.ttinclude"
 } 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t");
            
            #line 1 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Categories.ttinclude"
 foreach(Category category in model.Categories.Values) { 
            
            #line default
            #line hidden
            this.Write("\r\n\t<h2><a name=\"");
            
            #line 3 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Categories.ttinclude"
            this.Write(this.ToStringHelper.ToStringWithCulture(category.Name));
            
            #line default
            #line hidden
            this.Write("\"></a>");
            
            #line 3 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Categories.ttinclude"
            this.Write(this.ToStringHelper.ToStringWithCulture(category.Name));
            
            #line default
            #line hidden
            this.Write(@"</h2>

	<p>Nulla ut venenatis justo. Etiam sit amet lorem neque. Ut laoreet mattis nisl, et luctus risus porta a. Nunc quis lectus mauris. Quisque mollis tincidunt pharetra. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam varius tempus erat at auctor. Curabitur ultrices faucibus lectus porta rutrum. Nullam risus libero, ornare sed eros sed, rhoncus interdum mi. Nulla dapibus viverra molestie. Proin tellus ex, finibus nec dolor vel, blandit mollis est. Fusce elementum mi at ipsum consequat faucibus. Pellentesque lorem libero, dictum ut dignissim id, fringilla quis lectus. Duis at arcu tempus, consequat nulla non, tincidunt dolor.</p>

	");
            
            #line 7 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Categories.ttinclude"
 foreach(DataAttribute attribute in category.Attributes.Values) { WriteAttribute(attribute, category.IntegerIdentifer == attribute); } 
            
            #line default
            #line hidden
            this.Write("\r\n\t<hr />\r\n\r\n");
            
            #line 11 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Categories.ttinclude"
 } 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t</div>\r\n\t\t\t\r\n\t\t\t\t<div class=\"col-sm-3 col-sm-offset-1\">\r\n\t\t\t\t\t<div class=\"sid" +
                    "ebar\">\r\n\t\t\t\t\t");
            this.Write("<h2>Items</h2>\r\n<ul>\r\n\r\n");
            
            #line 4 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Navigation.ttinclude"
 foreach(Item item in model.Items.Values) { 
            
            #line default
            #line hidden
            this.Write("\r\n\t<li><a href=\"#");
            
            #line 6 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Navigation.ttinclude"
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            
            #line default
            #line hidden
            this.Write("\">");
            
            #line 6 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Navigation.ttinclude"
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            
            #line default
            #line hidden
            this.Write("</a></li>\r\n\r\n");
            
            #line 8 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Navigation.ttinclude"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n</ul>\r\n\r\n<h2>Categories</h2>\r\n<ul>\r\n\r\n");
            
            #line 15 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Navigation.ttinclude"
 foreach(Category category in model.Categories.Values) { 
            
            #line default
            #line hidden
            this.Write("\r\n\t<li><a href=\"#");
            
            #line 17 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Navigation.ttinclude"
            this.Write(this.ToStringHelper.ToStringWithCulture(category.Name));
            
            #line default
            #line hidden
            this.Write("\">");
            
            #line 17 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Navigation.ttinclude"
            this.Write(this.ToStringHelper.ToStringWithCulture(category.Name));
            
            #line default
            #line hidden
            this.Write("</a></li>\r\n\r\n");
            
            #line 19 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\Navigation.ttinclude"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n</ul>");
            this.Write("\r\n\t\t\t\t\t</div>\r\n\t\t\t\t</div>\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t\t<script src=\"https://ajax.googl" +
                    "eapis.com/ajax/libs/jquery/1.11.2/jquery.min.js\"></script>\r\n\t\t<script src=\"js/bo" +
                    "otstrap.min.js\"></script>\r\n\t</body>\r\n</html>");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 public void WriteAttribute(DataAttribute attribute, bool isIdentifier) { 
        
        #line default
        #line hidden
        
        #line 1 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\r\n\t<h3>\r\n\r\n\t");

        
        #line default
        #line hidden
        
        #line 5 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 if(isIdentifier) { 
        
        #line default
        #line hidden
        
        #line 5 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\t\t<strong>\r\n\t");

        
        #line default
        #line hidden
        
        #line 7 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 } 
        
        #line default
        #line hidden
        
        #line 7 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\r\n\t");

        
        #line default
        #line hidden
        
        #line 9 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(attribute.Name));

        
        #line default
        #line hidden
        
        #line 9 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("</h3>\r\n\r\n\t");

        
        #line default
        #line hidden
        
        #line 11 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 if(isIdentifier) { 
        
        #line default
        #line hidden
        
        #line 11 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\t\t</strong>\r\n\t");

        
        #line default
        #line hidden
        
        #line 13 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 } 
        
        #line default
        #line hidden
        
        #line 13 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\r\n\t<p>");

        
        #line default
        #line hidden
        
        #line 15 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(attribute.Name));

        
        #line default
        #line hidden
        
        #line 15 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(" is a ");

        
        #line default
        #line hidden
        
        #line 15 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(attribute.GetType().Name));

        
        #line default
        #line hidden
        
        #line 15 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(".\r\n\r\n\t");

        
        #line default
        #line hidden
        
        #line 17 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 if(isIdentifier) { 
        
        #line default
        #line hidden
        
        #line 17 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\t\tThis attribute is the primary identifier for this item.\r\n\t");

        
        #line default
        #line hidden
        
        #line 19 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 } 
        
        #line default
        #line hidden
        
        #line 19 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\t\r\n\t");

        
        #line default
        #line hidden
        
        #line 21 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(PrettifyType(attribute.Type)));

        
        #line default
        #line hidden
        
        #line 21 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(" ");

        
        #line default
        #line hidden
        
        #line 21 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(PrettifyNullability(attribute.Nullability)));

        
        #line default
        #line hidden
        
        #line 21 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("</p>\r\n\r\n\t");

        
        #line default
        #line hidden
        
        #line 23 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 WriteConstraints(attribute); 
        
        #line default
        #line hidden
        
        #line 23 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\r\n\t");

        
        #line default
        #line hidden
        
        #line 25 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 WriteSqlDetails(attribute); 
        
        #line default
        #line hidden
        
        #line 27 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 }

public void WriteConstraints(DataAttribute attribute) { 
        
        #line default
        #line hidden
        
        #line 29 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\r\n\t");

        
        #line default
        #line hidden
        
        #line 31 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 if (attribute.Constraints.Count == 0) return; 
        
        #line default
        #line hidden
        
        #line 31 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\r\n\t<strong>Constraints</strong>\r\n\r\n\t<ul>\r\n\r\n\t\t");

        
        #line default
        #line hidden
        
        #line 37 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 foreach(IConstraint constraint in attribute.Constraints) { WriteConstraint(constraint); } 
        
        #line default
        #line hidden
        
        #line 37 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\r\n\t</ul>\r\n\r\n");

        
        #line default
        #line hidden
        
        #line 41 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 }

public void WriteSqlDetails(DataAttribute attribute) { 
        
        #line default
        #line hidden
        
        #line 43 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\r\n\t");

        
        #line default
        #line hidden
        
        #line 45 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 if(attribute.Type.Details.ContainsKey("SqlDataType") && attribute.Details.ContainsKey("SqlColumn")) return; 
        
        #line default
        #line hidden
        
        #line 45 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\r\n\t<strong>Sql Implementation Details:</strong>\r\n\r\n\t<ul>\r\n\r\n\t\t");

        
        #line default
        #line hidden
        
        #line 51 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 if(attribute.Details.ContainsKey("SqlColumn")) { 
        
        #line default
        #line hidden
        
        #line 51 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\t\t<li>The attribute\'s column name is ");

        
        #line default
        #line hidden
        
        #line 52 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(attribute.Details["SqlColumn"]));

        
        #line default
        #line hidden
        
        #line 52 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(".</li>\r\n\t\t");

        
        #line default
        #line hidden
        
        #line 53 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 } 
        
        #line default
        #line hidden
        
        #line 53 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\r\n\t\t");

        
        #line default
        #line hidden
        
        #line 55 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 if(attribute.Type.Details.ContainsKey("SqlDataType")) { 
        
        #line default
        #line hidden
        
        #line 55 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\t\t<li>The attribute\'s data type is ");

        
        #line default
        #line hidden
        
        #line 56 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(attribute.Type.Details["SqlDataType"]));

        
        #line default
        #line hidden
        
        #line 56 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(".</li>\r\n\t\t");

        
        #line default
        #line hidden
        
        #line 57 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 } 
        
        #line default
        #line hidden
        
        #line 57 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\r\n\t</ul>\r\n\r\n");

        
        #line default
        #line hidden
        
        #line 61 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 }

public void WriteConstraint(IConstraint constraint) {

	string constraintTypeName = constraint.GetType().Name;
	
	switch (constraintTypeName)
	{
		case "AttributeConstraint" :
			AttributeConstraint attributeConstraint = constraint as AttributeConstraint; 
        
        #line default
        #line hidden
        
        #line 70 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\t\t\r\n\t\t\t<li>The value must ");

        
        #line default
        #line hidden
        
        #line 72 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(PrettifyAttributeConstraint(attributeConstraint.Comparison)));

        
        #line default
        #line hidden
        
        #line 72 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(" the attribute ");

        
        #line default
        #line hidden
        
        #line 72 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(attributeConstraint.Attribute.Name));

        
        #line default
        #line hidden
        
        #line 72 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(".</li>\r\n\t\t\r\n\t\t\t");

        
        #line default
        #line hidden
        
        #line 74 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 break;

		case "AttributeValueConstraint" :
			AttributeValueConstraint attributeValueConstraint = constraint as AttributeValueConstraint; 
        
        #line default
        #line hidden
        
        #line 77 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\t\t\r\n\t\t\t<li>The value must abide by an attribute value constraint however they hav" +
        "e not been implemented fully.</li>\r\n\t\t\r\n\t\t\t");

        
        #line default
        #line hidden
        
        #line 81 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 break;

		case "NumericValueConstraint" :
			NumericValueConstraint<Int64> numericValueConstraint = constraint as NumericValueConstraint<Int64>;
        
        #line default
        #line hidden
        
        #line 84 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\t\t\r\n\t\t\t<li>The value must ");

        
        #line default
        #line hidden
        
        #line 86 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(PrettifyNumericConstraint(numericValueConstraint.Comparison)));

        
        #line default
        #line hidden
        
        #line 86 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(" ");

        
        #line default
        #line hidden
        
        #line 86 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(numericValueConstraint.Value));

        
        #line default
        #line hidden
        
        #line 86 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("</li>\r\n\t\t\r\n\t\t\t");

        
        #line default
        #line hidden
        
        #line 88 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 break;

		case "StringLengthConstraint" :
			StringLengthConstraint stringLengthConstraint = constraint as StringLengthConstraint; 
        
        #line default
        #line hidden
        
        #line 91 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\t\t\t\r\n\t\t\t<li>The length of the string must ");

        
        #line default
        #line hidden
        
        #line 93 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(PrettifyLengthConstraint(stringLengthConstraint.Comparison)));

        
        #line default
        #line hidden
        
        #line 93 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(" ");

        
        #line default
        #line hidden
        
        #line 93 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(stringLengthConstraint.Value));

        
        #line default
        #line hidden
        
        #line 93 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(" characters.</li>\r\n\t\t\t\r\n\t\t\t");

        
        #line default
        #line hidden
        
        #line 95 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 break;

		case "StringValueConstraint" :
			StringValueConstraint stringValueConstraint = constraint as StringValueConstraint; 
        
        #line default
        #line hidden
        
        #line 98 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\t\t\t\r\n\t\t\t<li>StringValueConstraint Value=\"");

        
        #line default
        #line hidden
        
        #line 100 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(stringValueConstraint.Value));

        
        #line default
        #line hidden
        
        #line 100 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\" Comparison=\"");

        
        #line default
        #line hidden
        
        #line 100 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(stringValueConstraint.Comparison));

        
        #line default
        #line hidden
        
        #line 100 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\" Comparer=\"");

        
        #line default
        #line hidden
        
        #line 100 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(stringValueConstraint.Comparer));

        
        #line default
        #line hidden
        
        #line 100 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\"</li>\r\n\t\t\t\r\n\t\t\t");

        
        #line default
        #line hidden
        
        #line 102 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 break;
		
		default:
        
        #line default
        #line hidden
        
        #line 104 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
this.Write("\t\t\r\n\t\t\t<li>UnknownConstraint</li>\r\n\t\t\t\r\n\t\t\t");

        
        #line default
        #line hidden
        
        #line 108 "C:\Users\Ben\Desktop\Items\Bootstrapper\ttincludes\AttributeMethods.ttinclude"
 break;
	}

} 
        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "10.0.0.0")]
    public class IndexBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
    }
    #endregion
}
