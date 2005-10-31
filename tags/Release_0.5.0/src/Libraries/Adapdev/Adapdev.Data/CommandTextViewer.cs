namespace Adapdev.Data
{
	using System;
	using System.Data;
	using System.Text;

	/// <summary>
	/// CommandStringBuilder takes an IDbCommand and creates a fully filled in sql text, replacing
	/// parameter designations with their actual values
	/// </summary>
	public class CommandTextViewer
	{
		private static readonly ProviderInfoManager dp = ProviderInfoManager.GetInstance();

		/// <summary>
		/// Returns the fully filled-in sql text for a specified IDbCommand
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="providerType"></param>
		/// <returns></returns>
		public static string Parse(IDbCommand cmd, DbProviderType providerType)
		{
			if (providerType == DbProviderType.OLEDB) 
			{
				return ParseOleDbCommand(cmd);
			}
			else if (providerType == DbProviderType.SQLSERVER)
			{
				return ParseSqlCommand(cmd);
			}
			else if (providerType == DbProviderType.ORACLE) {
				return ParseOracleCommand(cmd);
			}
			else 
				throw new NotImplementedException(providerType.ToString() + " is not currently supported.");
		}

		/// <summary>
		/// This is a worker method, which takes an OleDbCommand and builds the sql text, with the parameter values added into it.
		/// </summary>
		/// <param name="cmd">The command.</param>
		/// <returns>A sql string with all of the parameter values filled in.</returns>
		public static string ParseOleDbCommand(IDbCommand cmd)
		{
			string cmds = cmd.CommandText.ToString().Trim();
			StringBuilder scmds = new StringBuilder(cmds);

			if (cmd.CommandType == CommandType.Text)
			{
				for (int i = cmd.Parameters.Count - 1; i >= 0; i--)
				{
					string pvalue = "";

					string pname = ((IDataParameter) cmd.Parameters[i]).ParameterName;
					if (((IDataParameter) cmd.Parameters[i]).Value == null)
					{
						pvalue = DBNull.Value.ToString();
					}
					else
					{
						pvalue = ((IDataParameter) cmd.Parameters[i]).Value.ToString();
					}
					string ptype = ((IDataParameter) cmd.Parameters[i]).DbType.ToString();

					string prefix = dp.GetPrefixByName("dbtype_oledb", ptype);
					string newValue = prefix + pvalue + prefix;
					//					Console.WriteLine(newValue);

					int qindex = scmds.ToString().LastIndexOf("?");
					//Console.WriteLine(qindex + " : " + scmds.ToString().Length);
					//if(qindex == scmds.ToString().Length - 1) qindex = qindex - 1;
					if (qindex > 0) scmds.Replace("?", newValue, qindex, 1);
				}

				cmds = scmds.ToString();
			}
			else if (cmd.CommandType == CommandType.StoredProcedure)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("{call ");
				sb.Append(cmds + "(");
				for (int i = 0; i < cmd.Parameters.Count; i++)
				{
					string pvalue = "";

					string pname = ((IDataParameter) cmd.Parameters[i]).ParameterName;
					if (((IDataParameter) cmd.Parameters[i]).Value == null)
					{
						pvalue = DBNull.Value.ToString();
					}
					else
					{
						pvalue = ((IDataParameter) cmd.Parameters[i]).Value.ToString();
					}
					string ptype = ((IDataParameter) cmd.Parameters[i]).DbType.ToString();

					string prefix = dp.GetPrefixByName("dbtype_oledb", ptype);
					string newValue = prefix + pvalue + prefix;
					string comma = ",";

					if (i >= cmd.Parameters.Count - 1)
					{
						comma = "";
					}

					sb.Append(" " + newValue + comma);
				}
				sb.Append(" )}");
				cmds = sb.ToString();
			}

			return cmds;
		}

		/// <summary>
		/// This is a worker method, which takes an SqlCommand and builds the sql text, with the parameter values added into it.
		/// </summary>
		/// <param name="cmd">The command.</param>
		/// <returns>A sql string with all of the parameter values filled in.</returns>
		public static string ParseSqlCommand(IDbCommand cmd)
		{
			string cmds = cmd.CommandText.ToString().Trim();

			if (cmd.CommandType == CommandType.Text)
			{
				for (int i = 0; i < cmd.Parameters.Count; i++)
				{
					string pvalue = "";

					string pname = ((IDataParameter) cmd.Parameters[i]).ParameterName;
					if (((IDataParameter) cmd.Parameters[i]).Value == null)
					{
						pvalue = DBNull.Value.ToString();
					}
					else
					{
						pvalue = ((IDataParameter) cmd.Parameters[i]).Value.ToString();
					}
					string ptype = ((IDataParameter) cmd.Parameters[i]).DbType.ToString();

					string prefix = dp.GetPrefixByName("dbtype_sqlclient", ptype);
					string newValue = prefix + pvalue + prefix;

					cmds = cmds.Replace(pname, newValue);
				}
			}
			else if (cmd.CommandType == CommandType.StoredProcedure)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("EXEC ");
				sb.Append(cmds);
				for (int i = 0; i < cmd.Parameters.Count; i++)
				{
					string pvalue = "";

					string pname = ((IDataParameter) cmd.Parameters[i]).ParameterName;
					if (((IDataParameter) cmd.Parameters[i]).Value == null)
					{
						pvalue = DBNull.Value.ToString();
					}
					else
					{
						pvalue = ((IDataParameter) cmd.Parameters[i]).Value.ToString();
					}
					string ptype = ((IDataParameter) cmd.Parameters[i]).DbType.ToString();

					string prefix = dp.GetPrefixByName("dbtype_sqlclient", ptype);
					string newValue = prefix + pvalue + prefix;
					string comma = ",";

					if (i >= cmd.Parameters.Count - 1)
					{
						comma = "";
					}

					sb.Append(" " + newValue + comma);
				}
				cmds = sb.ToString();
			}

			return cmds;
		}

		/// <summary>
		/// This is a worker method, which takes an OracleCommand and builds the sql text, 
		/// with the parameter values added into it.
		/// </summary>
		/// <param name="cmd">The command.</param>
		/// <returns>A sql string with all of the parameter values filled in.</returns>
		public static string ParseOracleCommand(IDbCommand cmd) {
			string cmds = cmd.CommandText.ToString().Trim();

			if (cmd.CommandType == CommandType.Text) {
				for (int i = 0; i < cmd.Parameters.Count; i++) {
					string pvalue = "";

					string pname = ((IDataParameter) cmd.Parameters[i]).ParameterName;
					if (((IDataParameter) cmd.Parameters[i]).Value == null) {
						pvalue = DBNull.Value.ToString();
					}
					else {
						pvalue = ((IDataParameter) cmd.Parameters[i]).Value.ToString();
					}
					string ptype = ((IDataParameter) cmd.Parameters[i]).DbType.ToString();

					string prefix = dp.GetPrefixByName("oracle", ptype);
					string newValue = prefix + pvalue + prefix;

					cmds = cmds.Replace(pname, newValue);
				}
			}
			else if (cmd.CommandType == CommandType.StoredProcedure) {
				StringBuilder sb = new StringBuilder();
				sb.Append("CREATE OR REPLACE");
				sb.Append(cmds);
				for (int i = 0; i < cmd.Parameters.Count; i++) {
					string pvalue = "";

					string pname = ((IDataParameter) cmd.Parameters[i]).ParameterName;
					if (((IDataParameter) cmd.Parameters[i]).Value == null) {
						pvalue = DBNull.Value.ToString();
					}
					else {
						pvalue = ((IDataParameter) cmd.Parameters[i]).Value.ToString();
					}
					string ptype = ((IDataParameter) cmd.Parameters[i]).DbType.ToString();

					string prefix = dp.GetPrefixByName("oracle", ptype);
					string newValue = prefix + pvalue + prefix;
					string comma = ",";

					if (i >= cmd.Parameters.Count - 1) {
						comma = "";
					}
					sb.Append(" " + newValue + comma);
				}
				cmds = sb.ToString();
			}
			return cmds;
		}

	}
}