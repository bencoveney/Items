using Items;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using System.CodeDom.Compiler;
using ItemLoader;
using System.Reflection;

namespace ItemLibGen
{
	/// <summary>
	/// The ItemLibGen application
	/// </summary>
	class Program
	{
		/// <summary>
		/// The model which code will be generated for
		/// </summary>
		public static Items.Model Model;

		/// <summary>
		/// The program entry point
		/// </summary>
		/// <param name="args">The arguments.</param>
		static void Main(string[] args)
		{
			Initialise();

			// Generate the code files from the model
			DirectoryInfo outputDirectory = Directory.CreateDirectory("Output");
			GenerateCodeFiles(outputDirectory);

			// Compile a list of all code to be compiled
			IEnumerable<FileInfo> generatedCode = outputDirectory.EnumerateFiles("*.cs", SearchOption.TopDirectoryOnly);
			IEnumerable<FileInfo> contentCode = new DirectoryInfo("CodeContent").EnumerateFiles("*.cs");
			IEnumerable<FileInfo> allSourceCode = generatedCode.Concat(contentCode);

			// Compile the assembly
			Assembly generatedAssembly = GenerateAssembly(allSourceCode, outputDirectory);

			// Compile a list of all tests to run
			DirectoryInfo testScriptDirectory = new DirectoryInfo("TestScripts");
			IEnumerable<FileInfo> testScripts = testScriptDirectory.EnumerateFiles("*.cs", SearchOption.TopDirectoryOnly);

			// Run the tests
			RunTests(generatedAssembly, testScripts, outputDirectory);

			Console.ReadLine();
		}

		/// <summary>
		/// Initializes the model
		/// </summary>
		private static void Initialise()
		{
			Program.Model = Factory.ConstructModel(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
		}

		/// <summary>
		/// Generates code files for each thing in the model
		/// </summary>
		/// <param name="outputDirectory">The output directory code files will be placed in.</param>
		private static void GenerateCodeFiles(DirectoryInfo outputDirectory)
		{
			// Initialise the templates
			List<ITemplate> templates = new List<ITemplate>()
			{
				new ItemLibGen.Templates.Thing(Model),
				new ItemLibGen.Templates.Properties(Model),
				new ItemLibGen.Templates.Constructors(Model),
				new ItemLibGen.Templates.PopulateFromIdentifier(Model)
			};

			// Generate output to file
			foreach (ITemplate template in templates)
			{
				string filename = template.GetType().Name + ".cs";
				string filepath = Path.Combine(outputDirectory.FullName, filename);
				File.WriteAllText(filepath, template.TransformText());
			}
		}

		/// <summary>
		/// Compiles the specified code files into an assembly
		/// </summary>
		/// <param name="inputFiles">The code files.</param>
		/// <param name="outputDirectory">The directory the assembly will be created within.</param>
		private static Assembly GenerateAssembly(IEnumerable<FileInfo> inputFiles, DirectoryInfo outputDirectory)
		{
			// Find all cs files in the output directory
			string[] fileNames = inputFiles.Select<FileInfo, string>(file => file.FullName).ToArray();

			CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

			string outputAssemblyName = Path.Combine(outputDirectory.FullName, "output.dll");

			// Define compilation parameters
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.OutputAssembly = outputAssemblyName;
			compilerParameters.GenerateInMemory = false;
			compilerParameters.TreatWarningsAsErrors = true;
			compilerParameters.ReferencedAssemblies.Add("System.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Data.dll");

			// Attempt to compile
			CompilerResults compilerResults = provider.CompileAssemblyFromFile(compilerParameters, fileNames);

			// Output any errors to console and disk
			if (compilerResults.Errors.Count > 0)
			{
				StringBuilder errors = new StringBuilder();
				foreach (CompilerError error in compilerResults.Errors)
				{
					string errorMessage = error.ToString();

					errors.AppendLine(errorMessage);
					Console.WriteLine(errorMessage);
				}
				File.WriteAllText(Path.Combine(outputDirectory.FullName, "compile_errors.txt"), errors.ToString());
			}

			return compilerResults.CompiledAssembly;
		}

		private static void RunTests(Assembly assemblyUnderTest, IEnumerable<FileInfo> testScripts, DirectoryInfo outputDirectory)
		{
			string[] fileNames = testScripts.Select<FileInfo, string>(file => file.FullName).ToArray();

			CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

			// Define compilation parameters
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.GenerateInMemory = true;
			compilerParameters.TreatWarningsAsErrors = true;
			compilerParameters.ReferencedAssemblies.Add(assemblyUnderTest.Location);
			compilerParameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);

			// Attempt to compile
			CompilerResults compilerResults = provider.CompileAssemblyFromFile(compilerParameters, fileNames);

			// Output any errors to console and disk
			if (compilerResults.Errors.Count > 0)
			{
				StringBuilder errors = new StringBuilder();
				foreach (CompilerError error in compilerResults.Errors)
				{
					string errorMessage = error.ToString();

					errors.AppendLine(errorMessage);
					Console.WriteLine(errorMessage);
				}
				File.WriteAllText(Path.Combine(outputDirectory.FullName, "test_errors.txt"), errors.ToString());
			}

			Assembly resultantAssembly = compilerResults.CompiledAssembly;

			IEnumerable<MethodInfo> testMethods = resultantAssembly.GetTypes().SelectMany(type => type.GetMethods()).Where(method => method.GetCustomAttributes(typeof(TestAttribute), false).Length > 0);

			foreach(MethodInfo method in testMethods)
			{
				try
				{
					method.Invoke(null, null);
				}
				catch(TargetInvocationException exception)
				{
					Console.WriteLine(exception.InnerException);
				}
			}
		}
	}
}
