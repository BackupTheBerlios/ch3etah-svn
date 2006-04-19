/*   Copyright 2004 Jacob Eggleston
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 *
 *   ========================================================================
 *
 *   File Created using SharpDevelop.
 *   User: Jacob Eggleston
 *   Date: 17/12/2004
 */

using System;

namespace Ch3Etah.Core.ProjectLib
{
	/// <summary>
	/// Description of MetadataFileObserverCollection.
	/// </summary>
	public class MetadataFileObserverCollection : Ch3Etah.Core.ProjectLib.Generated.MetadataFileObserverCollection
	{
		public new int Add(IMetadataFileObserver val) {
			if (!this.Contains(val)) {
				return base.Add(val);
			}
			else {
				return -1;
			}
		}
	}
}
