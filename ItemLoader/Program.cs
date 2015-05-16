using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Items;
using ItemSerialiser;

namespace ItemLoader
{
    class Program
    {
        private const String CONNECTION_STRING = @"Data Source=BENSDESKTOP\SQLEXPRESS;Initial Catalog=ItemsDB;Integrated Security=True";

        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            Loader loader = new Loader();

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                loader.Load(connection);
            }

            XmlCreator creator = new XmlCreator(loader.Model);
            string output = creator.TransformText();
        }
    }
}
