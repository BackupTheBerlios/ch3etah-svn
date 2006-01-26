using System;
using System.Xml;

using CodeSmith.Engine;

using Ch3Etah.Core.Metadata;
using Ch3Etah.Core.ProjectLib;
using Ch3Etah.Metadata.OREntities;
using Ch3Etah.TemplateHelpers;

namespace Ch3Etah.CodeSmithSupport.BaseTemplates
{
	/// <summary>
	/// Provides a base for templates that generate classes
	/// in C# using the metadata objects in
	/// <see cref="Ch3Etah.Metadata.OREntities"/>.
	/// </summary>
	public class CSharpEntityTemplate : CodeTemplate
	{
		#region Private fields
		private MetadataFile _currentMetadataFile;
		private MetadataFileCollection _contextMetadataFiles;
		private StringHelper _stringHelper;
		private TypeHelper _typeHelper;
		private string _codeGenSystemLogin;
		private string _codeGenOutputPath;
		#endregion Private fields

		[CodeTemplateProperty(CodeTemplatePropertyOption.Required)]
		public MetadataFile CurrentMetadataFile
		{
			get { return _currentMetadataFile; }
			set { _currentMetadataFile = value; }
		}
		
		[CodeTemplateProperty(CodeTemplatePropertyOption.Required)]
		public MetadataFileCollection ContextMetadataFiles
		{
			get { return _contextMetadataFiles; }
			set { _contextMetadataFiles = value; }
		}

		[CodeTemplateProperty(CodeTemplatePropertyOption.Required)]
		public string CodeGenSystemLogin
		{
			get { return _codeGenSystemLogin; }
			set { _codeGenSystemLogin = value; }
		}

		[CodeTemplateProperty(CodeTemplatePropertyOption.Required)]
		public string CodeGenOutputPath
		{
			get { return _codeGenOutputPath; }
			set { _codeGenOutputPath = value; }
		}
		
		[CodeTemplateProperty(CodeTemplatePropertyOption.Required)]
		public StringHelper StringHelper
		{
			get { return _stringHelper; }
			set { _stringHelper = value; }
		}

		//[CodeTemplateProperty(CodeTemplatePropertyOption.Required)]
		public TypeHelper TypeHelper
		{
			get { return _typeHelper; }
			set { _typeHelper = value; }
		}

		
		// Gets the O/R Entity for which this template is being run
		protected Entity OREntity
		{
			get
			{
				foreach (IMetadataEntity metaentity in CurrentMetadataFile.MetadataEntities)
				{
					if (metaentity is Entity)
						return (Entity)metaentity;
				}
				return null;
			}
		}
		

		// Searches the list of selected metadata files/entities
		// for the first O/R Entity with the specified name
		protected Entity FindEntity(string name)
		{
			return FindEntity(name, "");
		}
	
		protected Entity FindEntity(string name, string searchNamespace)
		{
			foreach (MetadataFile file in ContextMetadataFiles)
			{
				foreach (IMetadataEntity metaentity in file.MetadataEntities)
				{
					if (metaentity is Entity && ((Entity)metaentity).Name == name)
						return (Entity)metaentity;
				}
			}
			return null;
		}
		
		
		protected virtual string FormatFieldName(string memberName)
		{
			return this.StringHelper.CamelCase(memberName);
		}
	
		protected virtual string FormatPropertyName(string memberName)
		{
			return this.StringHelper.PascalCase(memberName);
		}
		
		
		#region Property accessor helper methods
		protected string GetBasicColumnProperty(EntityField field, string indent)
		{
			return StringHelper.IndentBlock(
				GetBasicColumnProperty(field), 
				indent);
		}
		
		protected virtual string GetBasicColumnProperty(EntityField field)
		{
			string txt = string.Format(
				"public {0} {1} {{\r\n"
				, field.Type
				, field.Name);
			txt += StringHelper.IndentBlock(string.Format(
				"get {{{0}}}\r\n"
				, FormatPropertyAccessorText(field, GetPropertyGetterText(field))));
			txt += StringHelper.IndentBlock(string.Format(
				"set {{{0}}}\r\n"
				, FormatPropertyAccessorText(field, GetPropertySetterText(field))));
			txt += "}";

			return txt;
		}

		protected string GetBasicLinkProperty(Link link, string indent)
		{
			return StringHelper.IndentBlock(
				GetBasicLinkProperty(link), 
				indent);
		}

		protected virtual string GetBasicLinkProperty(Link link)
		{
			string txt = string.Format(
				"public {0} {1} {{\r\n"
				, this.GetLinkType(link)
				, link.Name);
			txt += StringHelper.IndentBlock(string.Format(
				"get {{{0}}}\r\n"
				, FormatPropertyAccessorText(link, GetPropertyGetterText(link))));
			txt += StringHelper.IndentBlock(string.Format(
				"set {{{0}}}\r\n"
				, FormatPropertyAccessorText(link, GetPropertySetterText(link))));
			txt += "}";

			return txt;
		}
		
		protected virtual string GetPropertyGetterText(IMetadataNode member)
		{
			return GetPropertyAccessorText(member, "Get", "return ${field);");
		}

		protected virtual string GetPropertySetterText(IMetadataNode member)
		{
			return GetPropertyAccessorText(member, "Set", "${field) = value;");
		}
		
		private string GetPropertyAccessorText(IMetadataNode member, string nodeName, string defaultCode)
		{
			string txt = "";

			XmlNode node = member.LoadedXmlNode.SelectSingleNode(nodeName);
			if (node != null && node.InnerText.Trim() != "")
				txt = node.InnerText;
			else
				txt = defaultCode;

			return txt;
		}

		private string FormatPropertyAccessorText(IMetadataNode member, string accessorText)
		{
			if (accessorText.IndexOf("\r\n") > 0)
				accessorText = "\r\n" + StringHelper.IndentBlock(accessorText) + "\r\n";

			return accessorText.Replace("${field)", FormatFieldName(GetMemberName(member)));
		}

		protected virtual string GetMemberName(IMetadataNode member)
		{
			if (member is EntityField)
				return ((EntityField)member).Name;
			if (member is IndexField)
				return ((IndexField)member).Name;
			if (member is Link)
				return ((Link)member).Name;

			throw new InvalidOperationException("Unrecognized member type: " + member.GetType().ToString());
		}
		#endregion Property accessor helper methods

		/// <summary>
		/// Returns a string.Format parameter list for the 
		/// key fields in the entity in the following format: 
		/// "{0}{1}", KeyField1, KeyField2
		/// </summary>
		protected string GetObjectIDFormatParams(Entity entity)
		{
			string comma = "";
			int formatIndex = 0;
			string formatString = "";
			string formatParams = "";

			foreach (EntityField field in entity.Fields)
			{
				if (field.KeyField)
				{
					formatString += "{" + formatIndex + "}";
					formatParams += comma + field.Name;
					comma = ", ";
					formatIndex += 1;
				}
			}
			if (formatString == "")
			{
				foreach (Index index in entity.Indexes)
				{
					if (index.PrimaryKey || index.Unique)
					{
						foreach (IndexField field in index.Fields)
						{
							formatString += "{" + formatIndex + "}";
							formatParams += comma + field.Name;
							comma = ", ";
							formatIndex += 1;
						}
						break;
					}
				}
			}
			
			if (formatString == "")
				return "";
			else
				return "\"" + formatString + "\"" + ", " + formatParams;
		}
		
		
		protected virtual string GetLinkType(Link link)
		{
			if (link.IsCollection)
				return this.FindEntity(link.TargetEntityName).CollectionName;
			else
				return link.TargetEntityName;
		}

		protected virtual string GetIndexReturnType(Index index)
		{
			if (index.PrimaryKey || index.Unique)
				return index.Entity.Name;
			else
				return index.Entity.CollectionName;
		}
		

	}
}
