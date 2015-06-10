﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 10.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace ItemSerialiser
{
    using Items;
    using System;
    
    
    #line 1 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "10.0.0.0")]
    public partial class XmlCreator : XmlCreatorBase
    {
        public virtual string TransformText()
        {
            this.Write("<Model>\r\n");
            
            #line 4 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
	foreach(Item item in _model.Items)
	{ 

            
            #line default
            #line hidden
            this.Write("\t<Item Name=\"");
            
            #line 7 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            
            #line default
            #line hidden
            this.Write("\">\r\n\t\t<Attributes>\r\n");
            
            #line 9 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
			foreach(DataMember attribute in item.Attributes)
			{
				WriteAttribute(attribute);
			}

            
            #line default
            #line hidden
            this.Write("\t\t</Attributes>\r\n\t\t<Behavior>\r\n");
            
            #line 16 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
		foreach(Behavior behavior in item.Behaviors)
		{

            
            #line default
            #line hidden
            this.Write("\t\t\t<Behavior Name=\"");
            
            #line 19 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(behavior.Name));
            
            #line default
            #line hidden
            this.Write("\">\r\n\t\t\t\t<Type>\r\n\t\t\t\t\t<!-- how can I get SystemType vs ItemType? SystemType is gen" +
                    "eric D: -->\r\n\t\t\t\t</Type>\r\n\t\t\t\t<Conditions>\r\n");
            
            #line 24 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
				foreach(Condition condition in behavior.Conditions)
                {

            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t<Condition Name=\"");
            
            #line 27 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(condition.Name));
            
            #line default
            #line hidden
            this.Write("\">\r\n\t\t\t\t\t\t<!-- WTF does a condition have -->\r\n\t\t\t\t\t\t<Inputs>\r\n\t\t\t\t\t</Condition>\r\n" +
                    "");
            
            #line 31 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
				}

            
            #line default
            #line hidden
            this.Write("\t\t\t\t</Conditions>\r\n\t\t\t\t<Parameters>\r\n");
            
            #line 35 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
				foreach(Parameter parameter in behavior.Parameters)
                {

            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t<!-- TODO Infer type of IParameter? -->\r\n\t\t\t\t\t<Parameter />\r\n");
            
            #line 40 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
				}

            
            #line default
            #line hidden
            this.Write("\t\t\t\t</Conditions>\r\n\t\t\t\t<Nullability>\r\n\t\t\t</Behavior>\r\n");
            
            #line 45 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
		}

            
            #line default
            #line hidden
            this.Write("\t\t</Behaviors>\r\n\t</Item>\r\n");
            
            #line 49 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
	}

            
            #line default
            #line hidden
            this.Write("</Model>\r\n\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 53 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
 public void WriteAttribute(DataMember attribute)
	{

        
        #line default
        #line hidden
        
        #line 55 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\t\t\t<");

        
        #line default
        #line hidden
        
        #line 56 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(attribute.GetType().Name));

        
        #line default
        #line hidden
        
        #line 56 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(" Name=\"");

        
        #line default
        #line hidden
        
        #line 56 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(attribute.Name));

        
        #line default
        #line hidden
        
        #line 56 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\">\r\n\t\t\t\t<Type>\r\n\t\t\t\t\t");

        
        #line default
        #line hidden
        
        #line 58 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(TypeToXml(attribute.DataType)));

        
        #line default
        #line hidden
        
        #line 58 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\r\n\t\t\t\t</Type>\r\n");

        
        #line default
        #line hidden
        
        #line 60 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
			if(attribute.Constraints.Count > 0)
            {

        
        #line default
        #line hidden
        
        #line 62 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\t\t\t\t<Constraints>\r\n");

        
        #line default
        #line hidden
        
        #line 64 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
				foreach(IConstraint constraint in attribute.Constraints)
				{
					WriteConstraint(constraint);
				}

        
        #line default
        #line hidden
        
        #line 68 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\t\t\t\t</Constraints>\r\n");

        
        #line default
        #line hidden
        
        #line 70 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
         }
			else
            {

        
        #line default
        #line hidden
        
        #line 73 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\t\t\t\t<Constraints />\r\n");

        
        #line default
        #line hidden
        
        #line 75 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
         }

        
        #line default
        #line hidden
        
        #line 76 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\t\t\t\t<Nullability>\r\n\t\t\t\t\t<");

        
        #line default
        #line hidden
        
        #line 78 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(attribute.NullConstraint));

        
        #line default
        #line hidden
        
        #line 78 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(" />\r\n\t\t\t\t</Nullability>\r\n\t\t\t</");

        
        #line default
        #line hidden
        
        #line 80 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(attribute.GetType().Name));

        
        #line default
        #line hidden
        
        #line 80 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(">\r\n");

        
        #line default
        #line hidden
        
        #line 81 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
 }

        
        #line default
        #line hidden
        
        #line 84 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
	public void WriteConstraint(IConstraint constraint)
	{
		string constraintTypeName = constraint.GetType().Name;
	
		switch (constraintTypeName)
		{
			case "AttributeConstraint" :
				AttributeConstraint attributeConstraint = constraint as AttributeConstraint;

        
        #line default
        #line hidden
        
        #line 92 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\t\t\t\t\t<AttributeConstraint Attribute=\"");

        
        #line default
        #line hidden
        
        #line 93 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(attributeConstraint.Attribute.Name));

        
        #line default
        #line hidden
        
        #line 93 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\" Comparison=\"");

        
        #line default
        #line hidden
        
        #line 93 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(attributeConstraint.Comparison));

        
        #line default
        #line hidden
        
        #line 93 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\" />\r\n");

        
        #line default
        #line hidden
        
        #line 94 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"

				break;
			case "AttributeValueConstraint" :
				AttributeValueConstraint attributeValueConstraint = constraint as AttributeValueConstraint;

        
        #line default
        #line hidden
        
        #line 98 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\t\t\t\t\t<AttributeValueConstraint Implemented=\"False\" />\r\n");

        
        #line default
        #line hidden
        
        #line 100 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"

				break;
			case "NumericValueConstraint`1" :
				NumericValueConstraint<Int32> numericValueConstraint = (NumericValueConstraint<Int32>)constraint;

        
        #line default
        #line hidden
        
        #line 104 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\t\t\t\t\t<NumericValueConstraint Value=\"");

        
        #line default
        #line hidden
        
        #line 105 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(numericValueConstraint.Value));

        
        #line default
        #line hidden
        
        #line 105 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\" Comparison=\"");

        
        #line default
        #line hidden
        
        #line 105 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(numericValueConstraint.Comparison));

        
        #line default
        #line hidden
        
        #line 105 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\" />\r\n");

        
        #line default
        #line hidden
        
        #line 106 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"

				break;
		case "StringLengthConstraint" :
				StringLengthConstraint stringLengthConstraint = constraint as StringLengthConstraint;

        
        #line default
        #line hidden
        
        #line 110 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\t\t\t\t\t<StringLengthConstraint Value=\"");

        
        #line default
        #line hidden
        
        #line 111 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(stringLengthConstraint.Value));

        
        #line default
        #line hidden
        
        #line 111 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\" Comparison=\"");

        
        #line default
        #line hidden
        
        #line 111 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(stringLengthConstraint.Comparison));

        
        #line default
        #line hidden
        
        #line 111 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\" />\r\n");

        
        #line default
        #line hidden
        
        #line 112 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"

				break;
		case "StringValueConstraint" :
				StringValueConstraint stringValueConstraint = constraint as StringValueConstraint;

        
        #line default
        #line hidden
        
        #line 116 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\t\t\t\t\t<StringValueConstraint Value=\"");

        
        #line default
        #line hidden
        
        #line 117 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(stringValueConstraint.Value));

        
        #line default
        #line hidden
        
        #line 117 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\" Comparison=\"");

        
        #line default
        #line hidden
        
        #line 117 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(stringValueConstraint.Comparison));

        
        #line default
        #line hidden
        
        #line 117 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\" Comparer=\"");

        
        #line default
        #line hidden
        
        #line 117 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(stringValueConstraint.Comparer));

        
        #line default
        #line hidden
        
        #line 117 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\" />\r\n");

        
        #line default
        #line hidden
        
        #line 118 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"

				break;
		default:

        
        #line default
        #line hidden
        
        #line 121 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\t\t\t\t\t<UnknownConstraint TypeName=\"");

        
        #line default
        #line hidden
        
        #line 122 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write(this.ToStringHelper.ToStringWithCulture(constraintTypeName));

        
        #line default
        #line hidden
        
        #line 122 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"
this.Write("\" />\r\n");

        
        #line default
        #line hidden
        
        #line 123 "C:\Users\Ben\Desktop\Items\ItemSerialiser\XmlCreator.tt"

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
    public class XmlCreatorBase
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
    }
    #endregion
}
