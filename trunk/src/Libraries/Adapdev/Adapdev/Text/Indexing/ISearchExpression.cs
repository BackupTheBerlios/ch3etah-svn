#region license
// Bamboo.Prevalence - a .NET object prevalence engine
// Copyright (C) 2004 Rodrigo B. de Oliveira
//
// Based on the original concept and implementation of Prevayler (TM)
// by Klaus Wuestefeld. Visit http://www.prevayler.org for details.
//
// Permission is hereby granted, free of charge, to any person 
// obtaining a copy of this software and associated documentation 
// files (the "Software"), to deal in the Software without restriction, 
// including without limitation the rights to use, copy, modify, merge, 
// publish, distribute, sublicense, and/or sell copies of the Software, 
// and to permit persons to whom the Software is furnished to do so, 
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included 
// in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY 
// CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
// OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 
// Contact Information
//
// http://bbooprevalence.sourceforge.net
// mailto:rodrigobamboo@users.sourceforge.net
#endregion

using System;

namespace Adapdev.Text.Indexing
{
	/// <summary>
	/// Models a search over an index or a set
	/// of indexes.
	/// </summary>
	public interface ISearchExpression
	{
		/// <summary>
		/// Evaluates the expression against
		/// the specified index.<br />
		/// If the expression can not be evaluated
		/// using the specified index
		/// it should raise ArgumentException.
		/// </summary>
		/// <param name="index">the index</param>
		/// <remarks>the expression must never call
		/// <see cref="IIndex.Search"/> unless it is completely sure
		/// the IIndex implementation knows how to handle the expression without
		/// calling this method again (what whould result in a infinite recursion loop)</remarks>
		/// <exception cref="ArgumentException">when the expression can not be evaluated with the specified index</exception>
		SearchResult Evaluate(IIndex index);
	}
}
