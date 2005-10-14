namespace Adapdev.Data.Sql
{
	using System.Collections;

	/// <summary>
	/// Summary description for ICriteria.
	/// </summary>
	public interface ICriteria
	{
		void AddAnd();
		void AddAndCriteria(ICriteria pc);
		void AddCriteriaSeparator(CriteriaType ct);
		void AddBetween(string columnName, object value1, object value2);
		void AddEqualTo(string columnName, object columnValue);
		void AddEqualTo(string tableName, string columnName, object columnValue);
		void AddEqualTo(string columnName);
		void AddExists(IQuery subQuery);
		void AddGreaterThanOrEqualTo(string columnName, object columnValue);
		void AddGreaterThan(string columnName, object columnValue);
		void AddIn(string columnName, IQuery subQuery);
		void AddIn(string columnName, ICollection values);
		void AddIsNull(string columnName);
		void AddLessThanOrEqualTo(string columnName, object columnValue);
		void AddLessThan(string columnName, object columnValue);
		void AddLike(string columnName, object columnValue);
		void AddNotBetween(string columnName, object value1, object value2);
		void AddNotEqualTo(string columnName, object columnValue);
		void AddNotExists(IQuery subQuery);
		void AddNotIn(string columnName, ICollection values);
		void AddNotIn(string columnName, IQuery subQuery);
		void AddNotLike(string columnName, object columnValue);
		void AddNotNull(string columnName);
		void AddOr();
		void AddOrCriteria(ICriteria pc);
		void AddSql(string sql);
		string GetText();
		DbProviderType DbProviderType { get; set; }
	}
}