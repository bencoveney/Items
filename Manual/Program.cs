using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Items;
using System.IO;

namespace Manual
{
	public class Program
	{
		public static Model Model
		{
			get;
			set;
		}

		static void Main(string[] args)
		{
			Model = new Model();
			Model.AddItem(CreateKitchen());
			Model.AddItem(CreateContainer());
			Model.AddItem(CreatePerson());
			Model.AddCategory(CreateContainerCategory());
		}

		public static Item CreateKitchen()
		{
			// Declare the kitchen
			Item kitchen = new Item("Kitchen");

			// Primary key
			DataMember kitchenID = new DataMember("ID", new SystemType<Int32>(), NullConstraints.None);
			kitchenID.Constraints.Add(new NumericValueConstraint<Int32>(NumericValueComparison.GreaterThan, 0)); // Value must be greater than 0
			kitchenID.Constraints.Add(new AttributeConstraint(kitchenID, CollectionComparison.IsUniqueWithin)); // Value cannot be duplicated
			kitchen.Attributes.Add(kitchenID);
			kitchen.IntegerIdentifier = kitchenID;

			// String key
			DataMember name = new DataMember("Name", new SystemType<String>(), NullConstraints.None);
			name.Constraints.Add(StringLengthConstraint.NotEmpty); // Dont allow empty strings
			name.Constraints.Add(new StringLengthConstraint(LengthComparison.ShorterThan, 257)); // Maximum 256 chars
			name.Constraints.Add(new AttributeConstraint(name, CollectionComparison.IsUniqueWithin)); // Value cannot be duplicated
			kitchen.Attributes.Add(name);
			kitchen.StringIdentifier = name;

			return kitchen;
		}

		public static Item CreateContainer()
		{
			// Declare the kitchen
			Item container = new Item("Container");

			// Primary key
			DataMember containerID = new DataMember("ID", new SystemType<Int32>(), NullConstraints.None);
			containerID.Constraints.Add(new NumericValueConstraint<Int32>(NumericValueComparison.GreaterThan, 0)); // Value must be greater than 0
			containerID.Constraints.Add(new AttributeConstraint(containerID, CollectionComparison.IsUniqueWithin)); // Value cannot be duplicated
			container.Attributes.Add(containerID);
			container.IntegerIdentifier = containerID;

			// String key
			DataMember name = new DataMember("Name", new SystemType<String>(), NullConstraints.None);
			name.Constraints.Add(StringLengthConstraint.NotEmpty); // Dont allow empty strings
			name.Constraints.Add(new StringLengthConstraint(LengthComparison.ShorterThan, 257)); // Maximum 256 chars
			name.Constraints.Add(new AttributeConstraint(name, CollectionComparison.IsUniqueWithin)); // Value cannot be duplicated
			container.Attributes.Add(name);
			container.StringIdentifier = name;

			return container;
		}

		public static Item CreatePerson()
		{
			// Declare the kitchen
			Item person = new Item("Person");

			// Primary key
			DataMember personID = new DataMember("ID", new SystemType<Int32>(), NullConstraints.None);
			personID.Constraints.Add(new NumericValueConstraint<Int32>(NumericValueComparison.GreaterThan, 0)); // Value must be greater than 0
			personID.Constraints.Add(new AttributeConstraint(personID, CollectionComparison.IsUniqueWithin)); // Value cannot be duplicated
			person.Attributes.Add(personID);
			person.IntegerIdentifier = personID;

			// String key
			DataMember name = new DataMember("Name", new SystemType<String>(), NullConstraints.None);
			name.Constraints.Add(StringLengthConstraint.NotEmpty); // Dont allow empty strings
			name.Constraints.Add(new StringLengthConstraint(LengthComparison.ShorterThan, 257)); // Maximum 256 chars
			name.Constraints.Add(new AttributeConstraint(name, CollectionComparison.IsUniqueWithin)); // Value cannot be duplicated
			person.Attributes.Add(name);
			person.StringIdentifier = name;

			return person;
		}

		public static Category CreateContainerCategory()
		{
			Category containerCategory = new Category("ContainerCategory");

			// Primary key
			DataMember containerCategoryID = new DataMember("ID", new SystemType<Int32>(), NullConstraints.None);
			containerCategoryID.Constraints.Add(new NumericValueConstraint<Int32>(NumericValueComparison.GreaterThan, 0)); // Value must be greater than 0
			containerCategoryID.Constraints.Add(new AttributeConstraint(containerCategoryID, CollectionComparison.IsUniqueWithin)); // Value cannot be duplicated
			containerCategory.Attributes.Add(containerCategoryID);
			containerCategory.IntegerIdentifier = containerCategoryID;

			// String key
			DataMember name = new DataMember("Name", new SystemType<String>(), NullConstraints.None);
			name.Constraints.Add(StringLengthConstraint.NotEmpty); // Dont allow empty strings
			name.Constraints.Add(new StringLengthConstraint(LengthComparison.ShorterThan, 257)); // Maximum 256 chars
			name.Constraints.Add(new AttributeConstraint(name, CollectionComparison.IsUniqueWithin)); // Value cannot be duplicated
			containerCategory.Attributes.Add(name);
			containerCategory.StringIdentifier = name;

			return containerCategory;
		}
	}
}
