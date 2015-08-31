using ItemLoader;
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

namespace ItemLibGen
{
	class Program
	{
		public static Model Model;

		static void Main(string[] args)
		{
			Initialise();

			ItemLibGen.Templates.Thing thing = new ItemLibGen.Templates.Thing(Model);
			string output = thing.TransformText();
			File.WriteAllText("output.cs", output);
			CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.OutputAssembly = "output.exe";
			compilerParameters.GenerateInMemory = false;
			compilerParameters.TreatWarningsAsErrors = true;
			CompilerResults compilerResults = provider.CompileAssemblyFromFile(compilerParameters, "output.cs");
			StringBuilder errors = new StringBuilder();
			foreach(CompilerError error in compilerResults.Errors)
			{
				errors.AppendLine(error.ToString());
			}
			File.WriteAllText("errors.txt", errors.ToString());
		}

		private static void Initialise()
		{
			DatabaseModel.LoadFromDatabase(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

			Model = DatabaseModel.ConstructModel();
		}
	}
}
