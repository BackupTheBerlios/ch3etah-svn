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
 *   Date: 22/9/2004
 */

using System;
using System.IO;
using System.Xml.Serialization;

namespace Ch3Etah.Core.Config
{
	#region Ch3EtahSettingsHelper helper class
	[XmlRoot("Ch3EtahSettings")]
	public class Ch3EtahSettingsHelper
	{
		private MetadataBrandCollection _metadataBrands;
		public Ch3EtahSettingsHelper()
		{
		}
		
		[XmlArrayItem("Brand")]
		public MetadataBrandCollection MetadataBrands {
			get {
				return _metadataBrands;
			}
			set {
				_metadataBrands = value;
			}
		}
	}
	#endregion Ch3EtahSettings helper class

	/// <summary>
	/// Describes a Ch3Etah config file and the settings it contains.
	/// This class is a singleton and all settings can be accessed through
	/// static members.
	/// </summary>
	public class Ch3EtahConfig
	{
		private const string CONFIG_FILE_NAME = @".\Ch3Etah.config";

		private static Ch3EtahSettingsHelper _settings;

		private static Ch3EtahSettingsHelper Settings {
			get {
				if (_settings == null) {
					LoadSettings();
					if (_settings == null) {
						throw new NullReferenceException();
					}
				}
				return _settings;
			}
		}
		
		public static MetadataBrandCollection MetadataBrands {
			get {
				if (Settings.MetadataBrands == null) {
					throw new NullReferenceException();
				}
				return Settings.MetadataBrands;
			}
		}
		
		#region Load / Save
		public static void LoadSettings() {
			LoadSettings(CONFIG_FILE_NAME);
		}

		public static void LoadSettings(string fileName) {
			_settings = (Ch3EtahSettingsHelper)XmlSerializationHelper.LoadObject(GetFullPath(fileName), typeof(Ch3EtahSettingsHelper));
		}

		public static void SaveSettings() {
			SaveSettings(CONFIG_FILE_NAME);
		}

		public static void SaveSettings(string fileName) {
			XmlSerializationHelper.SaveObject(_settings, GetFullPath(fileName), typeof(Ch3EtahSettingsHelper));
		}
		#endregion Load / Save
		
		#region GetFullPath
		private static string GetFullPath(string fileName) {
			string oldBaseFolder = Directory.GetCurrentDirectory();
			Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
			try {
				string fullPath = Path.GetFullPath(fileName);
				return fullPath;
			}
			finally {
				Directory.SetCurrentDirectory(oldBaseFolder);
			}
		}
		#endregion GetFullPath
	}
}
