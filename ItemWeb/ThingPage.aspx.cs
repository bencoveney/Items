using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Items;
using ItemLoader;

namespace ItemWeb
{
	public partial class ThingPage : System.Web.UI.Page
	{
		public IDbiThing Thing
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

				Thing = Global.Model.Things.Single(modelThing => modelThing.Name == thingName) as IDbiThing;

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

		protected void WriteType(IDbiType type)
		{
			Type typeType = type.GetType();

			if (typeType.IsGenericType && typeType.GetGenericTypeDefinition() == typeof(DbiSystemType<>))
			{
				String pureDefinition = String.Format("The attribute stores <strong>{0}</strong> data.", typeType.GetGenericArguments()[0].Name);
				Response.Write(pureDefinition);
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

		protected void WriteSqlDetails(DbiDataMember attribute)
		{
			// TODO make these tabular, read them sensibly instead of all these ifs with the same content

			IDbiType dbiType = attribute.DataType as IDbiType;

			if(!string.IsNullOrEmpty(attribute.SqlColumn))
			{
				Response.Write("<li>The column name is ");
				Response.Write(attribute.SqlColumn);
				Response.Write(".</li>");
			}

			if (string.IsNullOrEmpty(dbiType.SqlDataType))
			{
				Response.Write("<li>The data type is ");
				Response.Write(dbiType.SqlDataType);
				Response.Write(".</li>");
			}

			if (dbiType.SqlNumericPrecision.HasValue)
			{
				Response.Write("<li>The numeric precision is ");
				Response.Write(dbiType.SqlNumericPrecision.Value);
				Response.Write(".</li>");
			}

			if (dbiType.SqlNumericPrecisionRadix.HasValue)
			{
				Response.Write("<li>The numeric precision radix is ");
				Response.Write(dbiType.SqlNumericPrecisionRadix.Value);
				Response.Write(".</li>");
			}

			if (dbiType.SqlNumericScale.HasValue)
			{
				Response.Write("<li>The numeric scale is ");
				Response.Write(dbiType.SqlNumericScale.Value);
				Response.Write(".</li>");
			}

			if (dbiType.SqlDateTimePrecision.HasValue)
			{
				Response.Write("<li>The date time precision is ");
				Response.Write(dbiType.SqlDateTimePrecision.Value);
				Response.Write(".</li>");
			}

			if (dbiType.SqlMaxCharacters.HasValue)
			{
				Response.Write("<li>The maximum number of characters is ");
				Response.Write(dbiType.SqlMaxCharacters.Value);
				Response.Write(".</li>");
			}

			if (!string.IsNullOrEmpty(dbiType.SqlCharacterSet))
			{
				Response.Write("<li>The character set is ");
				Response.Write(dbiType.SqlCharacterSet);
				Response.Write(".</li>");
			}

			if (!string.IsNullOrEmpty(dbiType.SqlCollationName))
			{
				Response.Write("<li>The collation name is ");
				Response.Write(dbiType.SqlCollationName);
				Response.Write(".</li>");
			}
		}

		protected void WriteRelationshipSqlDetails(Relationship relationship)
		{
		}

		protected void WriteConstraints(DataMember attribute)
		{
			if(attribute.Constraints.Count == 0) return;
			foreach(IConstraint constraint in attribute.Constraints)
			{
				WriteConstraint(constraint);
			}
		}

		protected void WriteAttribute(DbiDataMember attribute, bool isIdentifier)
		{
			Response.Write("<div class=\"col-sm-6\">");
			if (isIdentifier)
			{
				Response.Write("<div class=\"panel panel-primary\">");
			}
			else
			{
				Response.Write("<div class=\"panel panel-default\">");
			}

			Response.Write("<div class=\"panel-heading\">");
			Response.Write("<h4 class=\"panel-title\">");
			Response.Write(attribute.Name);
			if (isIdentifier)
			{
				Response.Write(" <small>Identifier</small>");
			}
			Response.Write("</h4>");
			Response.Write("</div>");

			Response.Write("<ul class=\"list-group\">");

			Response.Write("<li class=\"list-group-item\">");
			Response.Write("<h5 class=\"list-group-item-heading\">Data Type</h5>");
			Response.Write("<p class=\"list-group-item-text\">");
			WriteType(attribute.DataType as IDbiType);
			Response.Write("</p>");
			Response.Write("</li>");

			Response.Write("<li class=\"list-group-item\">");
			Response.Write("<h5 class=\"list-group-item-heading\">Null Constraint</h5>");
			Response.Write("<p class=\"list-group-item-text\">");
			WriteNullability(attribute.NullConstraint);
			Response.Write("</p>");
			Response.Write("</li>");

			if (attribute.Constraints.Any())
			{
				Response.Write("<li class=\"list-group-item\">");
				Response.Write("<h5 class=\"list-group-item-heading\">Constraints</h5>");
				Response.Write("<ul class=\"list-group-item-text\">");
				WriteConstraints(attribute);
				Response.Write("</ul>");
				Response.Write("</li>");
			}

			IDbiType dbiType = attribute.DataType as IDbiType;

			Response.Write("<li class=\"list-group-item\">");
			Response.Write("<h5 class=\"list-group-item-heading\">Sql Details</h5>");
			Response.Write("<ul class=\"list-group-item-text\">");
			WriteSqlDetails(attribute as DbiDataMember);
			Response.Write("</ul>");
			Response.Write("</li>");

			Response.Write("</ul>");

			Response.Write("</div>");
			Response.Write("</div>");
		}

		protected void WriteRelationship(DbiRelationship relationship)
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
			WriteThingLink(relationship.LeftLink.Thing as IDbiThing);
			Response.Write(relationship.LeftLink);
			Response.Write("</a>");
			Response.Write("</p>");
			Response.Write("</div>");

			Response.Write("<div class=\"col-sm-2 text-center\">");
			Response.Write("<p>-</p>");
			Response.Write("</div>");

			Response.Write("<div class=\"col-sm-5 text-right\">");
			Response.Write("<p>");
			WriteThingLink(relationship.RightLink.Thing as IDbiThing);
			Response.Write(relationship.RightLink);
			Response.Write("</a>");
			Response.Write("</p>");
			Response.Write("</div>");

			Response.Write("</div>");

			Response.Write("<div class=\"row\">");
			Response.Write("<div class=\"col-sm-12\">");

			Response.Write("Represented by ");
			Response.Write(relationship.SqlCatalog);
			Response.Write(" ");
			Response.Write(relationship.SqlSchema);
			Response.Write(" ");
			Response.Write(relationship.SqlTable);

			if(!string.IsNullOrEmpty(relationship.SqlConstraint))
			{
				Response.Write(" ");
				Response.Write(relationship.SqlConstraint);
			}

			Response.Write("</div>");
			Response.Write("</div>");

			Response.Write("</div>");

			Response.Write("</div>");
			Response.Write("</div>");
		}

		protected void WriteThingLink(IDbiThing thing)
		{
			Response.Write("<a href=\"../");
			Response.Write(thing.GetType().Name);
			Response.Write("/");
			Response.Write(thing.Name);
			Response.Write("\">");
		}

		protected void WriteSqlObject()
		{
			Response.Write("This ");
			Response.Write(this.ThingType);
			Response.Write(" is represented by the database schema object ");
			Response.Write(" represents the dbo ");

			Response.Write(this.Thing.SqlCatalog);

			Response.Write(".");
			Response.Write(this.Thing.SqlSchema);

			Response.Write(".");
			Response.Write(this.Thing.SqlTable);

			if(string.IsNullOrEmpty(this.Thing.SqlConstraint))
			{
				Response.Write(".");
				Response.Write(this.Thing.SqlConstraint);
			}
		}

		protected void WriteBehaviors(IDbiThing thing)
		{
			Item item = thing as Item;

			if (item != null && item.Behaviors.Count > 0)
			{
				Response.Write("<div class=\"section\">");
				Response.Write("<h3>Behaviors</h3>");
				Response.Write("<div class=\"row\">");
				foreach (Behavior behavior in item.Behaviors)
				{
					WriteBehavior(behavior);
				}
				Response.Write("</div>");
				Response.Write("</div>");
			}
		}

		protected void WriteAttributes(IDbiThing thing)
		{
			Item item = thing as Item;

			if (item != null && item.Behaviors.Count > 0)
			{
				Response.Write("<div class=\"section\">");
				Response.Write("<h3>Attributes</h3>");
				Response.Write("<div class=\"row\">");
				foreach (DataMember attribute in this.Thing.Attributes)
				{
					WriteAttribute(attribute as DbiDataMember, this.Thing.IntegerIdentifier == attribute || this.Thing.StringIdentifier == attribute);
				}
				Response.Write("</div>");
				Response.Write("</div>");
			}
		}

		protected void WriteRelationships(IDbiThing thing)
		{
			Item item = thing as Item;

			if (item != null && item.GetReferenceRelationships(Global.Model).Count() > 0)
			{
				Response.Write("<div class=\"section\">");
				Response.Write("<h3>Relationships</h3>");
				Response.Write("<div class=\"row\">");
				foreach (Relationship relationship in this.Thing.GetReferenceRelationships(Global.Model))
				{
					WriteRelationship(relationship as DbiRelationship);
				}
				Response.Write("</div>");
				Response.Write("</div>");
			}
		}

		protected void WriteBehavior(Behavior behavior)
		{
			Response.Write("<div class=\"col-sm-12\">");
			Response.Write("<div class=\"panel panel-default\">");

			Response.Write("<div class=\"panel-heading\">");
			Response.Write("<h4 class=\"panel-title\">");
			Response.Write(behavior.Name);
			Response.Write("</h4>");
			Response.Write("</div>");

			Response.Write("<table class=\"table table-striped\">");

			Response.Write("<thead>");
			Response.Write("<tr>");
			Response.Write("<th>#</th>");
			Response.Write("<th>Name</th>");
			Response.Write("<th>Direction</th>");
			Response.Write("<th>Data Type</th>");
			Response.Write("<th>SQL Type</th>");
			Response.Write("</tr>");
			Response.Write("<thead>");

			Response.Write("<tbody>");
			foreach (DbiParameter parameter in behavior.Parameters.OfType<DbiParameter>())
			{
				Response.Write("<tr>");
				Response.Write("<td>");
				Response.Write(parameter.SqlOrdinal);
				Response.Write("</td>");
				Response.Write("<td>");
				Response.Write(parameter.Name);
				Response.Write("</td>");
				Response.Write("<td>");
				Response.Write(parameter.SqlMode);
				Response.Write("</td>");
				Response.Write("<td>");
				WriteParameterType(parameter.DataType as IDbiType);
				Response.Write("</td>");
				Response.Write("<td class=\"text-uppercase\">");
				Response.Write((parameter.DataType as IDbiType).SqlDataType);
				Response.Write("</td>");
				Response.Write("</tr>");
			}
			Response.Write("</tbody>");

			Response.Write("</table>");

			Response.Write("</div>");
			Response.Write("</div>");
		}

		protected void WriteRelationshipSides(IDbiThing thing)
		{
			if (thing.GetType() != typeof(DbiRelationship))
			{
				return;
			}

			DbiRelationship relationship = (DbiRelationship)thing;

			Response.Write("<div class=\"section\">");
			Response.Write("<div class=\"row\">");

			Response.Write("<div class=\"col-sm-6 relationship-side\">");
			Response.Write("<div class=\"well text-center\">");
			Response.Write("<h3>");
			Response.Write(relationship.LeftLink.Thing.Name);
			Response.Write("</h3>");
			Response.Write("<span class=\"big-number\">");
			Response.Write(relationship.LeftLink.AmountString);
			Response.Write("</span>");
			Response.Write("</div>");
			Response.Write("<p>");
			Response.Write(relationship.LeftLink.Thing.Description);
			Response.Write("</p>");
			Response.Write("</div>");

			Response.Write("<div class=\"col-sm-6 relationship-side\">");
			Response.Write("<div class=\"well text-center\">");
			Response.Write("<h3>");
			Response.Write(relationship.RightLink.Thing.Name);
			Response.Write("</h3>");
			Response.Write("<span class=\"big-number\">");
			Response.Write(relationship.RightLink.AmountString);
			Response.Write("</span>");
			Response.Write("</div>");
			Response.Write("<p>");
			Response.Write(relationship.RightLink.Thing.Description);
			Response.Write("</p>");
			Response.Write("</div>");

			Response.Write("</div>");
			Response.Write("</div>");
		}

		protected void WriteParameterType(IDbiType type)
		{
			Type typeType = type.GetType();

			if (typeType.IsGenericType && typeType.GetGenericTypeDefinition() == typeof(SystemType<>))
			{
				String pureDefinition =typeType.GetGenericArguments()[0].Name;
				Response.Write(pureDefinition);
			}
			else
			{
				Response.Write(String.Format("Unknown type ({0})", typeType.FullName));
			}
		}
	}
}