using Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ItemsTests
{
	/// <summary>
	/// This is a test class for DataDefinitionTest and is intended
	/// to contain all DataDefinitionTest Unit Tests
	/// </summary>
	[TestClass()]
	public class DataDefinitionTest
	{
		private IEnumerable<DataDefinition> GetDataDefinitions()
		{
			List<DataDefinition> dataDefinitions = new List<DataDefinition>();
			dataDefinitions.Add(new DataMember("Test Member", new SystemType<int>(), NullConstraints.EmptyOrNotApplicable));
			dataDefinitions.Add(new Parameter("Test Parameter", new SystemType<int>(), NullConstraints.EmptyOrNotApplicable));
			return dataDefinitions;
		}

		/// <summary>
		///A test for Constraints
		///</summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void ConstraintsTest()
		{
			List<DataDefinition> dataDefinitions = new List<DataDefinition>();
			dataDefinitions.Add(new DataMember("Test Member", new SystemType<int>(), NullConstraints.EmptyOrNotApplicable));
			dataDefinitions.Add(new Parameter("Test Parameter", new SystemType<int>(), NullConstraints.EmptyOrNotApplicable));

			foreach (DataDefinition dataDefinition in dataDefinitions)
			{
				Assert.IsNotNull(dataDefinition.Constraints);

				IConstraint constraint = new NumericValueConstraint<int>(NumericValueComparison.GreaterThan, -1);
				dataDefinition.Constraints.Add(constraint);
				Assert.IsTrue(dataDefinition.Constraints.Contains(constraint));
			}
		}

		/// <summary>
		/// A test for DataType
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void DataTypeTest()
		{
			List<DataDefinition> dataDefinitions = new List<DataDefinition>();
			dataDefinitions.Add(new DataMember("Test Member", new SystemType<int>(), NullConstraints.EmptyOrNotApplicable));
			dataDefinitions.Add(new Parameter("Test Parameter", new SystemType<int>(), NullConstraints.EmptyOrNotApplicable));

			foreach (DataDefinition dataDefinition in dataDefinitions)
			{
				Assert.IsNotNull(dataDefinition.DataType);
			}
		}

		/// <summary>
		/// A test for Details
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void DetailsTest()
		{
			IType type = new SystemType<int>();

			List<DataDefinition> dataDefinitions = new List<DataDefinition>();
			dataDefinitions.Add(new DataMember("Test Member", type, NullConstraints.EmptyOrNotApplicable));
			dataDefinitions.Add(new Parameter("Test Parameter", type, NullConstraints.EmptyOrNotApplicable));

			foreach (DataDefinition dataDefinition in dataDefinitions)
			{
				Assert.IsNotNull(dataDefinition.DataType);
				Assert.AreEqual(type, dataDefinition.DataType);
			}
		}

		/// <summary>
		/// A test for Name
		/// </summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void NameTest()
		{
			List<DataDefinition> dataDefinitions = new List<DataDefinition>();
			dataDefinitions.Add(new DataMember("Test Definition", new SystemType<int>(), NullConstraints.EmptyOrNotApplicable));
			dataDefinitions.Add(new Parameter("Test Definition", new SystemType<int>(), NullConstraints.EmptyOrNotApplicable));

			foreach (DataDefinition dataDefinition in dataDefinitions)
			{
				Assert.AreEqual("Test Definition", dataDefinition.Name);
			}
		}

		/// <summary>
		///A test for NullConstraint
		///</summary>
		[TestMethod()]
		[DeploymentItem("Items.dll")]
		public void NullConstraintTest()
		{
			List<DataDefinition> dataDefinitions = new List<DataDefinition>();
			dataDefinitions.Add(new DataMember("Test Member", new SystemType<int>(), NullConstraints.EmptyOrNotApplicable));
			dataDefinitions.Add(new Parameter("Test Parameter", new SystemType<int>(), NullConstraints.EmptyOrNotApplicable));

			foreach (DataDefinition dataDefinition in dataDefinitions)
			{
				Assert.AreEqual(NullConstraints.EmptyOrNotApplicable, dataDefinition.NullConstraint);
			}
		}
	}
}
