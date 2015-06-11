using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Items;

namespace ItemSerialiser
{
    partial class XmlCreator
    {
        private Model _model;

        public XmlCreator(Model model)
        {
            _model = model;
        }

        private String TypeToXml(IType type)
        {
            Type typeType = type.GetType();

            if (typeType.IsGenericType && typeType.GetGenericTypeDefinition() == typeof(SystemType<>))
            {
                return String.Format("<SystemType Type=\"{0}\" />", typeType.GetGenericArguments()[0].Name);
            }

            if (typeType == typeof(Category))
            {
                return String.Format("<CategoryType Item=\"{0}\" />", type.Name);
            }

            return "<UnknownType>";
        }
    }
}
