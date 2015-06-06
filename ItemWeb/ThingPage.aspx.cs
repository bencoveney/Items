using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Items;

namespace ItemWeb
{
	public partial class ThingPage : System.Web.UI.Page
	{
		public Thing Thing
		{
			get;
			set;
		}

		public string ThingType
		{
			get;
			set;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				ThingType = RouteData.Values["ThingType"].ToString();
				string thingName = RouteData.Values["ThingName"].ToString();

				Thing = Global.Model.Things.Single(modelThing => modelThing.Name == thingName);

				if (Thing.GetType().ToString() != "Items." + ThingType)
				{
					throw new ArgumentException("Item doesn't match specified type", ThingType);
				}
			}
			catch
			{
				throw new HttpException(404, "Unable to find thing of specified type in model");
			}
		}

		protected void WriteDescription()
		{
			if (String.IsNullOrEmpty(Thing.Description))
			{
				Response.Write("Nulla ut venenatis justo. Etiam sit amet lorem neque. Ut laoreet mattis nisl, et luctus risus porta a. Nunc quis lectus mauris. Quisque mollis tincidunt pharetra. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam varius tempus erat at auctor. Curabitur ultrices faucibus lectus porta rutrum. Nullam risus libero, ornare sed eros sed, rhoncus interdum mi. Nulla dapibus viverra molestie. Proin tellus ex, finibus nec dolor vel, blandit mollis est. Fusce elementum mi at ipsum consequat faucibus. Pellentesque lorem libero, dictum ut dignissim id, fringilla quis lectus. Duis at arcu tempus, consequat nulla non, tincidunt dolor.");
			}
			else
			{
				Response.Write(Thing.Description);
			}
		}

		protected void WriteType(IType type)
		{
			Type typeType = type.GetType();

			if (typeType.IsGenericType && typeType.GetGenericTypeDefinition() == typeof(SystemType<>))
			{
				String pureDefinition = String.Format("The attribute stores {0} data.", typeType.GetGenericArguments()[0].Name);
				Response.Write(pureDefinition);
			}
			else if (typeType == typeof(ItemType))
			{
				Response.Write(String.Format("This attribute stores {0} items.", type.Name));
			}
			else if (typeType == typeof(CategoryType))
			{
				Response.Write(String.Format("This attribute is a {0} categorisation.", type.Name));
			}
			else
			{
				Response.Write(String.Format("This attribute is of unknown type ({0})", typeType.FullName));
			}
		}

		protected void WriteAttributeConstraint(CollectionComparison constraint)
		{
			switch (constraint)
			{
				case CollectionComparison.DoesNotExistIn:
					Response.Write("not exist within");
					break;
				case CollectionComparison.ExistsIn:
					Response.Write("exist within");
					break;
				case CollectionComparison.IsUniqueWithin:
					Response.Write("be unique within");
					break;
				default:
					Response.Write("ERROR");
					break;
			}
		}

		protected void WriteNullability(NullConstraints nullability)
		{
			switch (nullability)
			{
				case NullConstraints.Empty:
					Response.Write("This attribute can be empty but cannot be non-applicable.");
					break;
				case NullConstraints.EmptyOrNotApplicable:
					Response.Write("This attribute can be both empty and non-applicable.");
					break;
				case NullConstraints.None:
					Response.Write("This attribute cannot be empty or non-applicable.");
					break;
				case NullConstraints.NotApplicable:
					Response.Write("This attribute can not be empty but can be non-applicable.");
					break;
				default:
					Response.Write("ERROR");
					break;
			}
		}

		protected void WriteNumericConstraint(NumericValueComparison comparison)
		{
			switch (comparison)
			{
				case NumericValueComparison.EqualTo:
					Response.Write("be equal to");
					break;
				case NumericValueComparison.NotEqualTo:
					Response.Write("not be equal to");
					break;
				case NumericValueComparison.EvenlyDivisibleBy:
					Response.Write("be evenly divisible by");
					break;
				case NumericValueComparison.NotEvenlyDivisibleBy:
					Response.Write("not be evenly divisible by");
					break;
				case NumericValueComparison.GreaterThan:
					Response.Write("be greater than");
					break;
				case NumericValueComparison.GreaterThanOrEqualTo:
					Response.Write("be greater than or equal to");
					break;
				case NumericValueComparison.LessThan:
					Response.Write("be less than");
					break;
				case NumericValueComparison.LessThanOrEqualTo:
					Response.Write("be less than or equal to");
					break;
				default:
					Response.Write("ERROR");
					break;
			}
		}

		protected void WriteStringLengthConstraint(LengthComparison comparison)
		{
			switch (comparison)
			{
				case LengthComparison.Exactly:
					Response.Write("be exactly");
					break;
				case LengthComparison.LongerThan:
					Response.Write("be longer than");
					break;
				case LengthComparison.ShorterThan:
					Response.Write("be shorter than");
					break;
				default:
					Response.Write("ERROR");
					break;
			}
		}

		protected void WriteStringValueConstraint(Items.StringComparison comparison)
		{
			switch (comparison)
			{
				case global::Items.StringComparison.BeginsWith:
					Response.Write("begin with the string");
					break;
				case global::Items.StringComparison.Contains:
					Response.Write("contain the string");
					break;
				case global::Items.StringComparison.DoesNotBeginWith:
					Response.Write("not begin with the string");
					break;
				case global::Items.StringComparison.DoesNotContain:
					Response.Write("not contain the string");
					break;
				case global::Items.StringComparison.DoesNotEndWith:
					Response.Write("not end with the string");
					break;
				case global::Items.StringComparison.DoesNotMatch:
					Response.Write("not match the string");
					break;
				case global::Items.StringComparison.DoesNotMatchRegex:
					Response.Write("not match the regular expression");
					break;
				case global::Items.StringComparison.EndWith:
					Response.Write("end with the string");
					break;
				case global::Items.StringComparison.IsContainedBy:
					Response.Write("be contained by the string");
					break;
				case global::Items.StringComparison.IsNotContainedBy:
					Response.Write("not be contained by the string");
					break;
				case global::Items.StringComparison.Match:
					Response.Write("match the string");
					break;
				case global::Items.StringComparison.MatchesRegex:
					Response.Write("match the regular expression");
					break;
				default:
					Response.Write("ERROR");
					break;
			}
		}

		protected void WriteConstraint(IConstraint constraint)
		{
			string constraintTypeName = constraint.GetType().Name;

			switch (constraintTypeName)
			{
				case "AttributeConstraint" :
					AttributeConstraint attributeConstraint = constraint as AttributeConstraint;
					Response.Write("<li>The value must ");
					WriteAttributeConstraint(attributeConstraint.Comparison);
					Response.Write(" the attribute ");
					Response.Write(attributeConstraint.Attribute.Name);
					Response.Write(".</li>");
					break;

				case "AttributeValueConstraint" :
					AttributeValueConstraint attributeValueConstraint = constraint as AttributeValueConstraint;
					Response.Write("<li>>The value must abide by an attribute value constraint however they have not been implemented fully.</li>");
					break;

				case "NumericValueConstraint" :
					NumericValueConstraint<Int64> numericValueConstraint = constraint as NumericValueConstraint<Int64>;
					Response.Write("<li>The value must ");
					WriteNumericConstraint(numericValueConstraint.Comparison);
					Response.Write(" ");
					Response.Write(numericValueConstraint.Value);
					Response.Write(".</li>");
					break;

				case "StringLengthConstraint" :
					StringLengthConstraint stringLengthConstraint = constraint as StringLengthConstraint;
					Response.Write("<li>The length of the string must ");
					WriteStringLengthConstraint(stringLengthConstraint.Comparison);
					Response.Write(" ");
					Response.Write(stringLengthConstraint.Value);
					Response.Write(" characters.</li>");
					break;

				case "StringValueConstraint" :
					StringValueConstraint stringValueConstraint = constraint as StringValueConstraint;
					Response.Write("<li>The value of the string must ");
					WriteStringValueConstraint(stringValueConstraint.Comparison);
					Response.Write(" ");
					Response.Write(stringValueConstraint.Comparison);
					Response.Write(". The check will be made using the ");
					Response.Write(stringValueConstraint.Comparer);
					Response.Write(" comparer.</li>");
					break;

				default:
					Response.Write("Unknown constraint");
					break;
			}
		}

		protected void WriteSqlDetails(DataMember attribute)
		{
			if(!attribute.DataType.Details.ContainsKey("SqlDataType") && !attribute.Details.ContainsKey("SqlColumn"))
			{
				return;
			}
			
			Response.Write("<strong>Sql Implementation Details:</strong>");
			Response.Write("<ul>");

			if(attribute.Details.ContainsKey("SqlColumn"))
			{
				Response.Write("<li>The attribute's column name is ");
				Response.Write(attribute.Details["SqlColumn"]);
				Response.Write(".</li>");
			}

			if(attribute.DataType.Details.ContainsKey("SqlDataType"))
			{
				Response.Write("<li>The attribute's data type is ");
				Response.Write(attribute.DataType.Details["SqlDataType"]);
				Response.Write(".</li>");
			}

			Response.Write("</ul>");
		}

		protected void WriteConstraints(DataMember attribute)
		{
			if(attribute.Constraints.Count == 0) return;

			Response.Write("<strong>Constraints</strong>");

			Response.Write("<ul>");
			foreach(IConstraint constraint in attribute.Constraints)
			{
				WriteConstraint(constraint);
			}
			Response.Write("</ul>");
		}

		protected void WriteAttribute(DataMember attribute, bool isIdentifier)
		{
			Response.Write("<div class=\"col-sm-6\">");
			Response.Write("<div class=\"panel panel-default\">");

			Response.Write("<div class=\"panel-heading\">");
			Response.Write("<h4 class=\"panel-title\">");
			if(isIdentifier)
			{
				Response.Write("<strong>");
			}
			Response.Write(attribute.Name);
			if(isIdentifier)
			{
				Response.Write("</strong>");
			}
			Response.Write("</h4>");
			Response.Write("</div>");

			Response.Write("<div class=\"panel-body\">");
			Response.Write("<p>");
			Response.Write(attribute.Name);
			Response.Write(" is a ");
			Response.Write(attribute.GetType().Name);
			Response.Write(". ");
			if(isIdentifier)
			{
				Response.Write("This attribute is the primary identifier for this item.");
			}
			WriteType(attribute.DataType);
			WriteNullability(attribute.NullConstraint);
			Response.Write("</p>");
			WriteConstraints(attribute);
			WriteSqlDetails(attribute);
			Response.Write("</div>");

			Response.Write("</div>");
			Response.Write("</div>");
		}

		protected void WriteRelationship(Relationship relationship)
		{
			Response.Write("<div class=\"col-sm-6\">");
			Response.Write("<div class=\"panel panel-default\">");

			Response.Write("<div class=\"panel-heading\">");
			Response.Write("<h4 class=\"panel-title\">");
			WriteThingLink(relationship);
			Response.Write(relationship.Name);
			Response.Write("</a>");
			Response.Write("</h4>");
			Response.Write("</div>");

			Response.Write("<div class=\"panel-body\">");
			Response.Write("<div class=\"row\">");

			Response.Write("<div class=\"col-sm-5 text-left\">");
			Response.Write("<p>");
			WriteThingLink(relationship.LeftLink.Thing);
			Response.Write(relationship.LeftLink);
			Response.Write("</a>");
			Response.Write("</p>");
			Response.Write("</div>");

			Response.Write("<div class=\"col-sm-2 text-center\">");
			Response.Write("<p>-</p>");
			Response.Write("</div>");

			Response.Write("<div class=\"col-sm-5 text-right\">");
			Response.Write("<p>");
			WriteThingLink(relationship.RightLink.Thing);
			Response.Write(relationship.RightLink);
			Response.Write("</a>");
			Response.Write("</p>");
			Response.Write("</div>");

			Response.Write("</div>");
			Response.Write("</div>");

			Response.Write("</div>");
			Response.Write("</div>");
		}

		protected void WriteThingLink(Thing thing)
		{
			Response.Write("<a href=\"../");
			Response.Write(thing.GetType().Name);
			Response.Write("/");
			Response.Write(thing.Name);
			Response.Write("\">");
		}
	}
}