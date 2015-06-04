using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Items;
using System.IO;
using ItemSerialiser;

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

            File.WriteAllText("Output.xml", new XmlCreator(Model).TransformText());
            System.Diagnostics.Process.Start("Output.xml");
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

            DataMember containers = new DataMember("Containers", new ItemType("Container"), NullConstraints.Empty);
            kitchen.Attributes.Add(containers);

            DataMember people = new DataMember("Persons", new ItemType("Person"), NullConstraints.Empty);
            kitchen.Attributes.Add(people);

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

            // Kitchen foreign key
            // This is the equivalent of having a kitchenID DataAttribute which is constrained by saying it exists in the KitchenID column of the kitchenitem
            DataMember kitchen = new DataMember("Kitchen", new ItemType("Kitchen"), NullConstraints.None);
            container.Attributes.Add(kitchen);

            DataMember category = new DataMember("Category", new CategoryType("ContainerCategory"), NullConstraints.None);
            container.Attributes.Add(category);

            DataMember owners = new DataMember("Persons", new ItemType("Person"), NullConstraints.Empty);
            container.Attributes.Add(owners);

            return container;

            //// Declare some static fridge behavior which applies to all fridges globally but is tied to none individually
            //container.Behaviors.Add(new Behavior("OpenAll", BehaviorLevel.Static));

            //// Declare some behavior which is tied to the particular instance
            //container.Behaviors.Add(new Behavior("Close", BehaviorLevel.Instance));

            //// Add some conditions to the behavior 
            //container.Behaviors["Close"].Conditions.Add(new Condition()); // DoorIsOpen must be true
            //container.Behaviors["Close"].Conditions.Add(new Condition()); // User must have arms
            //container.Behaviors["Close"].Conditions.Add(new Condition()); // time parameter must be daytime

            //// Add some parameters to the behavior 
            //container.Behaviors["Close"].Parameters.Add(new DefaultedParameter()); // Closing Speed (optional, default to "slow")
            //container.Behaviors["Close"].Parameters.Add(new RequiredParameter()); // User closing the fridge, some are faster
            //container.Behaviors["Close"].Parameters.Add(new OptionalParameter()); // Who to notify
            //// how to pass the current time in? as a param? as a system value available to the actions?

            //container.Behaviors["Close"].Actions.Add(new Action()); // Close the fridge
            //container.Behaviors["Close"].Actions.Add(new Action()); // Decreast the temperature of any food items
            //container.Behaviors["Close"].Actions.Add(new Action()); // Custom code, partial class?
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

            DataMember containers = new DataMember("Containers", new ItemType("Container"), NullConstraints.Empty);
            person.Attributes.Add(containers);

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
