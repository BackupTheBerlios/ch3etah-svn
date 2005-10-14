#region Original Copyright (c) 2002-2003, James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole, Philip A. Craig

/************************************************************************************
	'
	' Copyright © 2002-2003 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole
	' Copyright © 2000-2003 Philip A. Craig
	'
	' This software is provided 'as-is', without any express or implied warranty. In no 
	' event will the authors be held liable for any damages arising from the use of this 
	' software.
	' 
	' Permission is granted to anyone to use this software for any purpose, including 
	' commercial applications, and to alter it and redistribute it freely, subject to the 
	' following restrictions:
	'
	' 1. The origin of this software must not be misrepresented; you must not claim that 
	' you wrote the original software. If you use this software in a product, an 
	' acknowledgment (see the following) in the product documentation is required.
	'
	' Portions Copyright © 2003 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole
	' or Copyright © 2000-2003 Philip A. Craig
	'
	' 2. Altered source versions must be plainly marked as such, and must not be 
	' misrepresented as being the original software.
	'
	' 3. This notice may not be removed or altered from any source distribution.
	'
	'***********************************************************************************/

#endregion
#region Modified Copyright / License Information
/*

   Copyright 2004 - 2005 Adapdev Technologies, LLC

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

============================
Author Log
============================
III	Full Name
SMM	Sean McCormack (Adapdev)


============================
Change Log
============================
III	MMDDYY	Change

*/
#endregion
using System;

namespace Adapdev
{
	/// <summary>
	/// All objects which are marshalled by reference
	/// and whose lifetime is manually controlled by
	/// the app, should derive from this class rather
	/// than MarshalByRefObject.
	/// 
	/// This includes the remote test domain objects
	/// which are accessed by the client and those
	/// client objects which are called back by the
	/// remote test domain.
	/// 
	/// Objects in this category that already inherit
	/// from some other class (e.g. from TextWriter)
	/// which in turn inherits from MarshalByRef object 
	/// should override InitializeLifetimeService to 
	/// return null to obtain the same effect.
	/// </summary>
	public class LongLivingMarshalByRefObject : MarshalByRefObject
	{
		public override Object InitializeLifetimeService()
		{
			return null;
		}
	}
}
