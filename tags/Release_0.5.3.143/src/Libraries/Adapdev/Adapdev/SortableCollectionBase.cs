// Copyright 2004 J. Ambrose - http://dotnettemplar.net/PermaLink.aspx?guid=1d32654f-9ed5-46e2-a815-4f70b762f734
using System;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp; 

namespace Adapdev.Collections
{
	/// <summary>
	/// Base class that provides T-SQL-like sorting capabilities.
	/// </summary>
	/// <remarks>This class is not thread safe; it depends on the 
	/// underlying collection to remain static during sorting 
	/// operations, therefore, use the <see cref="GetNewView"/> 
	/// methods to get a shallow copy of the collection before 
	/// attempting sort operations if the collection instance
	/// could be accessed by more than one thread in your 
	/// application.</remarks>
	public class SortableCollectionBase : System.Collections.CollectionBase
	{
		#region Static Caches
		/// <summary>
		/// Comparer cache that stores instances of dynamically-created comparers.
		/// </summary>
		protected static System.Collections.Hashtable comparers = 
			new System.Collections.Hashtable(100);
		/// <summary>
		/// Table for ensuring that we only generate a unique comparer once.
		/// </summary>
		protected static System.Collections.Hashtable comparerLocks =
			new System.Collections.Hashtable(10);
		/// <summary>
		/// Assembly path cache that stores the paths to the referenced assemblies
		/// to be used when compiling dynamically-created comparers.
		/// </summary>
		protected static System.Collections.Hashtable assemblyPaths = 
			new System.Collections.Hashtable(10);
		#endregion

		#region GetNewView
		/// <summary>
		/// Gets a new <see cref="SortableCollectionBase"/> to
		/// provide a unique view of the underlying collection.
		/// </summary>
		/// <overload>Gets a view of the current collection.</overload>
		/// <returns>A new view of the underlying collection.</returns>
		/// <remarks>This would typically be used when needing to modify
		/// the collection for display purposes, such as sorting and the like.
		/// <p>It performs a shallow copy of the collection.</p></remarks>
		public SortableCollectionBase GetNewView()
		{
			SortableCollectionBase view =
				new SortableCollectionBase();
			for (int i = 0; i < this.List.Count; i++)
			{
				view.List.Add(this.List[i]);
			}
			return view;
		}

		/// <summary>
		/// Gets a view of the current collection as
		/// the collection type specified.
		/// </summary>
		/// <overload>Gets a view of the current collection.</overload>
		/// <param name="collectionType">The type of collection to return.</param>
		/// <returns>A collection of the specified <i>collectionType</i>
		/// that contains a shallow copy of the items in this collection.</returns>
		/// <remarks>
		/// Constraints: A <see cref="NotSupportedException"/> will 
		/// be thrown if any are not met.
		/// <ul>
		///		<li>The <i>collectionType</i> must implement a 
		///			default constructor ("new" constraint).</li>
		///		<li>The <i>collectionType</i> must implement the 
		///			<see cref="System.Collections.IList"/> interface.</li>
		/// </ul>
		/// </remarks>
		public System.Collections.IList GetNewView(System.Type collectionType)
		{
			return this.GetNewView(collectionType, null);
		}

		/// <summary>
		/// Gets a view of the current collection, sorted using the given <i>sortExpression</i>.
		/// </summary>
		/// <overload>Gets a view of the current collection.</overload>
		/// <param name="collectionType">The type of collection to return.</param>
		/// <param name="sortExpression"></param>
		/// <returns>A collection of the specified <i>collectionType</i>
		/// that contains a shallow copy of the items in this collection.</returns>
		/// <remarks>
		/// Constraints: A <see cref="NotSupportedException"/> will 
		/// be thrown if any are not met.
		/// <ul>
		///		<li>The <i>collectionType</i> must implement a 
		///			default constructor ("new" constraint).</li>
		///		<li>The <i>collectionType</i> must inherit from the 
		///			<see cref="SortableCollectionBase"/> class.</li>
		/// </ul>
		/// </remarks>
		public System.Collections.IList GetNewView(System.Type collectionType, string sortExpression)
		{
			System.Reflection.ConstructorInfo ctor;
			// retrieve the property info for the specified property
			ctor = collectionType.GetConstructor(new System.Type[0]);
			if (ctor == null)
				throw new NotSupportedException(
					String.Format("The '{0}' type must have a public default constructor.", 
					collectionType.FullName));
			// get an instance of the collection type
			SortableCollectionBase view =
				ctor.Invoke(new object[] {}) as SortableCollectionBase;
			if (view == null)
				throw new NotSupportedException(
					String.Format("The '{0}' type must inherit from SortableCollectionBase.", 
					collectionType.FullName));
			for (int i = 0; i < this.List.Count; i++)
				view.InnerList.Add(base.List[i]);
			if (sortExpression != null)
				view.Sort(sortExpression);
			return view;
		}
		#endregion

		#region Sort
		/// <summary>
		/// Sorts the list by the given expression.
		/// </summary>
		/// <param name="sortExpression">A sort expression, e.g., MyProperty ASC, MyProp2 DESC.</param>
		/// <remarks>
		/// Valid sort expressions are comma-delimited lists of properties where each property
		/// can optionally be followed by a sort direction (ASC or DESC).<br />
		/// Sort order is from left to right, i.e., properties to the left are sorted on first.
		/// <p>All properties sorted on must implement <see cref="IComparable"/> and meaningfully
		/// override <see cref="Object.Equals"/>.</p>
		/// <p>If <i>sortExpression</i> is null, an <see cref="ArgumentNullException"/> is thrown.<br />
		/// If <i>sortExpression</i> is empty, an <see cref="ArgumentException"/> is thrown.<br />
		/// Throws an <see cref="ApplicationException"/> if there are errors compiling the comparer 
		/// or if a new instance of a dynamically-created comparer cannot be created 
		/// for any of the sort properties.</p>
		/// <p>It is assumed that all objects in the collection are of the same type.</p>
		/// </remarks>
		public void Sort(string sortExpression)
		{
			#region Parameter Checks
			if (sortExpression == null)
				throw new ArgumentNullException("sortExpression", "Argument cannot be null.");
			#endregion
			this.InnerList.Sort(this.GetMultiComparer(this.List[0].GetType(), sortExpression));
		}

		/// <summary>
		/// Gets an <see cref="System.Collections.IComparer"/> for the given type and sort expression.
		/// </summary>
		/// <param name="type">The type of object to be sorted.</param>
		/// <param name="sortExpression">A sort expression, e.g., MyProperty ASC, MyProp2 DESC.</param>
		/// <returns>An <see cref="System.Collections.IComparer"/> for the given type and sort expression.</returns>
		protected System.Collections.IComparer GetMultiComparer(System.Type type, string sortExpression)
		{
			// split the sort expression string by the commas
			string[] sorts = sortExpression.Split(',');
			// if no sorts are present, throw an exception
			if (sorts.Length == 0)
				throw new ArgumentException("No sorts were passed in the sort expression.", "sortExpression");
			string typeName = type.FullName;
			// create a unique type name for the comparer based on the type and sort expression
			string comparerName = sortExpression.Replace(",", "").Replace(" ", "") + "Comparer",
				dynamicTypeName = typeName + "DynamicComparers." + comparerName;
			System.Collections.IComparer comparer = null;
			// check the comparers table for an existing comparer for this type and property
			if (!comparers.ContainsKey(dynamicTypeName)) // not found
			{
				object comparerLock;
				bool generate = false;
				// let threads pile up here waiting their turn to look at this collection
				lock (comparerLocks.SyncRoot)
				{
					// First, we see if the comparer lock for this
					// dynamicTypeName exists.
					comparerLock = comparerLocks[dynamicTypeName];
					if (comparerLock == null)
					{
						// This is the first thread in here looking for this
						// dynamicTypeName, so it gets to be the one to 
						// create the comparer for the dynamicTypeName.
						generate = true;
						// Add lock object for any future threads to see and 
						// know that they won't need to generate the comparer.
						comparerLock = new object();
						comparerLocks.Add(dynamicTypeName, comparerLock);
					}
				}
				// comparerLock will be unique per dynamicTypeName and, consequently,
				// per unique comparer.  However, the code above only ensures
				// that only one thread will do the generation of the comparer.
				// We now need to lock the comparerLock for each dynamicTypeName to
				// make non generating threads wait on the thread that is doing the 
				// generation, so we ensure that the comparer is generated for all 
				// threads by the time we exit the following block.
				lock (comparerLock)
				{
					// This ensures only that first thread in actually does any generation.
					if (generate)
					{
						// declare the source code for the dynamic assembly
						string compareCode = @"
using System;
namespace [TypeName]DynamicComparers
{
	public sealed class [ComparerName] : System.Collections.IComparer
	{
		public int Compare(object first, object second)
		{
			[TypeName] firstInstance = first as [TypeName];
			if (firstInstance == null)
				throw new ArgumentNullException(""First object cannot be null."", ""first"");
			[TypeName] secondInstance = second as [TypeName];
			if (secondInstance == null)
				throw new ArgumentNullException(""Second object cannot be null."", ""second"");
			int result = 0;
			[CompareCode]
		}
	}
}

";
						System.Text.StringBuilder compareBuilder = new System.Text.StringBuilder();
						string propertyName;
						bool desc;
						for (int sortIndex = 0; sortIndex < sorts.Length; sortIndex++)
						{
							// check current sort for null, continue if null
							if (sorts[sortIndex] == null)
								continue;
							// split current sort by space to distinguish property from asc/desc
							string[] sortValues = sorts[sortIndex].Trim().Split(' ');
							// if there are no values after split, leave -- should have at least the prop name
							if (sortValues.Length == 0)
								continue;
							// prop name will be first value
							propertyName = sortValues[0].Trim();
							// ensure property exists on specified type
							PropertyInfo prop = type.GetProperty(propertyName);
							if (prop == null)
								throw new ArgumentException(
									String.Format("Specified property '{0}' is not a property of type '{1}'.", 
									propertyName, type.FullName), "sortExpression");
							// if there, the second will be asc/desc; compare to get whether or not this
							// sort is descending
							desc = (sortValues.Length > 1) ? (sortValues[1].Trim().ToUpper() == "DESC") : false;
							// if property type is reference type, we need to do null checking in the compare code
							bool checkForNull = !prop.PropertyType.IsValueType;
							// check for null if the property is a reference type
							if (checkForNull) 
								compareBuilder.Append("\t\t\tif (firstInstance." + propertyName + " != null)\n\t");
							// compare the property
							compareBuilder.Append("\t\t\tresult = firstInstance." + propertyName + 
								".CompareTo(secondInstance." + propertyName + ");");
							// check for null on the second type, if necessary
							if (checkForNull)
							{
								// if second type is also null, return true; otherwise, the first instance is 
								// less than the second because it is null
								compareBuilder.Append("\t\t\telse if (secondInstance." + propertyName + " == null)\n");
								compareBuilder.Append("\t\t\t\tresult = 0;  else result = -1;\n");
							}
							// if the two are not equal, no further comparison is needed, just return the 
							// current compare value and flip the sign, if the current sort is descending
							compareBuilder.Append("\t\t\tif (result !=0) return " + (desc ? "-" : "") + "(result);\n");
						}
						// if all comparisons were equal, we'll need the next line to return that result
						compareBuilder.Append("\t\t\treturn result;");
						// replace the type and comparer name placeholders in the source with real values
						// and insert the property comparisons
						compareCode = compareCode.Replace("[TypeName]", typeName).Replace("[ComparerName]", comparerName)
							.Replace("[CompareCode]", compareBuilder.ToString());
#if TRACE
						System.Diagnostics.Debug.WriteLine(compareCode);
#endif
						// create a C# compiler instance
						ICodeCompiler compiler = new CSharpCodeProvider().CreateCompiler();
						// create a compiler parameters collection
						CompilerParameters parameters = new CompilerParameters();
						// add necessary assembly references for the source to compile
						string primeAssemblyPath = type.Assembly.Location.Replace("file:///", "").Replace("/", "\\");
						parameters.ReferencedAssemblies.Add(primeAssemblyPath);
						foreach (System.Reflection.AssemblyName asm in type.Assembly.GetReferencedAssemblies())
							parameters.ReferencedAssemblies.Add(this.GetAssemblyPath(asm));
						// tell the compiler to generate the IL in memory
						parameters.GenerateInMemory = true;
						// compile the new dynamic assembly using the parameters and source from above
						CompilerResults compiled =
							compiler.CompileAssemblyFromSource(parameters, compareCode);
						// check for compiler errors
						if (compiled.Errors.HasErrors)
						{
							// build error message from compiler errors
							string message = "Could not generate a comparer for '{0}'.  Errors:\n";
							for (int i = 0; i < compiled.Errors.Count; i++)
								message += compiled.Errors[i].ErrorText + "\n";
							// throw an exception with the relevant information
							throw new ApplicationException(
								String.Format(message, dynamicTypeName));
						}
						// get an instance of the new type as IComparer
						comparer = compiled.CompiledAssembly.CreateInstance(dynamicTypeName)
							as System.Collections.IComparer;
						// throw an exception if getting the new instance fails
						if (comparer == null)
							throw new ApplicationException(
								String.Format("Could not instantiate the dynamic type '{0}'.", dynamicTypeName));
						// add the new comparer to the comparers table
						lock (comparers)
							comparers.Add(dynamicTypeName, comparer);
					} // (generate)
				} // comparer lock
			} // comparers cache check

			// At this point, we should be sure that a comparer has been generated for the
			// requested dynamicTypeName and stuck in the cache.  If we're the thread that 
			// did the generating, comparer will not be null, and we can just return it. 
			// If comparer hasn't been assigned (via generating it above), get it from the cache.
			if (comparer == null)
			{
				// get the comparer from the cache
				comparer = comparers[dynamicTypeName] as System.Collections.IComparer;
				// throw an exception if the comparer cannot be retrieved
				if (comparer == null)
					throw new ApplicationException(
						String.Format("Could not retrieve the dynamic type '{0}'.", dynamicTypeName));
			}
			// return the comparer
			return comparer;
		}


		/// <summary>
		/// Loads the given assembly to get its path (for dynamic compilation reference).
		/// </summary>
		/// <param name="assembly">AssemblyName instance to load assembly from.</param>
		/// <returns>The path for the given assembly or an empty 
		/// string if it can't load/locate it.</returns>
		protected string GetAssemblyPath(System.Reflection.AssemblyName assembly)
		{
			string assemblyFullName = assembly.FullName;
			string path = string.Empty;
			path = (string)assemblyPaths[assemblyFullName];
			if (path == null)
			{
				lock (assemblyPaths.SyncRoot)
				{
					path = (string)assemblyPaths[assemblyFullName];
					if (path == null)
					{
						System.Reflection.Assembly asm =
							System.Reflection.Assembly.Load(assembly);
						if (asm != null)
						{
							path = asm.Location.Replace("file:///", "").Replace("/", "\\");
						}
						assemblyPaths.Add(assemblyFullName, path);
					}
				}
			}
			return path;
		}
		#endregion

	}
}
