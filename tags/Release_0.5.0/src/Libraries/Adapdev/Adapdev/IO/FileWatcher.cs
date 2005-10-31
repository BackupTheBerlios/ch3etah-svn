// Original Copyright (c) 2004 Brad Vincent - http://www.codeproject.com/csharp/FileWatcherWrapper.asp
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
SMM	121604	Added IncludeSubDirectories property and functionality
*/
#endregion
using System;
using System.IO;

namespace Adapdev.IO
{
	/// <summary>
	///		arguments sent through when an event is fired
	/// </summary>
	public class FileWatcherEventArgs : EventArgs
	{
		private string mFileName = "";
		private string mPath = "";
		private string mOldPath = "";
		private string mOldName = "";

		private FileWatcher.FileWatcherChangeType mChangeType;

		public FileWatcherEventArgs(string fileName, string path, string oldPath, string oldName, FileWatcher.FileWatcherChangeType changeType)
		{
			mFileName = fileName;
			mPath = path;
			mOldName = oldName;
			mOldPath = oldPath;
			mChangeType = changeType;
		}

		public string FileName 
		{
			get 
			{
				return mFileName;
			}
		}

		public string OldFileName 
		{
			get 
			{
				return mOldName;
			}
		}

		public string Path 
		{
			get 
			{
				return mPath;
			}
		}
		public string OldPath 
		{
			get 
			{
				return mOldPath;
			}
		}

		public FileWatcher.FileWatcherChangeType ChangeType
		{
			get 
			{
				return mChangeType;
			}
		}

	}

	
	/// <summary>
	/// monitors a folder for file system changes
	/// </summary>
	public class FileWatcher
	{
		#region enums, constants & fields
		public enum FileWatcherChangeType
		{
			FileAdded,
			FileDeleted,
			FileRenamed,
			Filechanged
		}

		private string mPath = ".";		//path to watch
		private string mFilter = "*.*";	//filter to watch
		private FileSystemWatcher mFsw = new FileSystemWatcher();
		private string mCurrentFileName = "";
		private string mCurrentPath = "";
		private string mCurrentOldPath = "";
		private string mCurrentOldName = "";
		private bool includeSub = false;
		private FileWatcherChangeType mChangeType;
		#endregion

		#region events and delegates
		public delegate void ChangedEventHandler(object sender, FileWatcherEventArgs args);

		public event ChangedEventHandler Changed;
		#endregion

		#region constructors
		public FileWatcher()
		{
			CreateFileWatcher();
		}

		public FileWatcher(string path, string filter) : this()
		{
			mPath = path;
			mFilter = filter;
		}

		public FileWatcher(string path) : this()
		{
			mPath = path;
		}
		#endregion

		#region Properties

		public string Path
		{
			get
			{
				return mPath;
			}
			set
			{
				mPath = value;
			}
		}

		public string Filter
		{
			get
			{
				return mFilter;
			}
			set
			{
				mFilter = value;
			}
		}

		public bool isStarted
		{
			get
			{
				return mFsw.EnableRaisingEvents;
			}
		}

		public string CurrentFileName 
		{
			get 
			{
				return mCurrentFileName;
			}
		}
		public string CurrentOldFileName 
		{
			get 
			{
				return mCurrentOldName;
			}
		}
		public string CurrentPath 
		{
			get 
			{
				return mCurrentPath;
			}
		}
		public string CurrentOldPath 
		{
			get 
			{
				return mCurrentOldPath;
			}
		}

		public FileWatcher.FileWatcherChangeType CurrentChangeType
		{
			get 
			{
				return mChangeType;
			}
		}

		public bool IncludeSubDirectories
		{
			get
			{
				return includeSub;
			}
			set
			{
				includeSub = value;
			}
		}
		#endregion

		#region public methods
		/// <summary>
		///		start the watcher for a specific folder with a specific filter
		/// </summary>
		public void StartWatcher()
		{
			mFsw.Path = mPath;
			if(this.includeSub) mFsw.IncludeSubdirectories = true;
			else mFsw.IncludeSubdirectories = false;
			mFsw.Filter = mFilter;
			mFsw.EnableRaisingEvents = true;
		}

		/// <summary>
		///		to stop the folder watcher from raising events
		/// </summary>
		public void StopWatcher()
		{
			mFsw.EnableRaisingEvents = false;
		}

		#endregion

		#region file watcher engine
		/// <summary>
		///		the heart of the file watcher engine
		/// </summary>
		private void CreateFileWatcher()
		{
			mFsw = new FileSystemWatcher(mPath,mFilter);
			mFsw.NotifyFilter = NotifyFilters.LastWrite | 
				NotifyFilters.DirectoryName | NotifyFilters.FileName;
			mFsw.Changed += new FileSystemEventHandler(OnChanged);
			mFsw.Created += new FileSystemEventHandler(OnCreated);
			mFsw.Deleted += new FileSystemEventHandler(OnDeleted);
			mFsw.Renamed += new RenamedEventHandler(OnRenamed);
		}

		protected virtual void OnChanged(FileWatcherEventArgs e)
		{
			//raises the event to say that a file has changed
			Changed(this,e);
		}
		#endregion

		#region private file-change methods
		private void OnCreated(object source, FileSystemEventArgs args)
		{	
			mCurrentFileName = args.Name;
			mCurrentPath = args.FullPath;
			mCurrentOldName = "";
			mCurrentOldPath = "";
			mChangeType = FileWatcherChangeType.FileAdded;
			OnChanged(new FileWatcherEventArgs(mCurrentFileName,mCurrentPath,mCurrentOldPath,mCurrentOldName,mChangeType));
		}

		private void OnRenamed(object source, RenamedEventArgs args)
		{	
			mCurrentFileName = args.Name;
			mCurrentPath = args.FullPath;
			mCurrentOldName = args.OldFullPath;
			mCurrentOldPath = args.OldName;		
			mChangeType = FileWatcherChangeType.FileRenamed;
			OnChanged(new FileWatcherEventArgs(mCurrentFileName,mCurrentPath,mCurrentOldPath,mCurrentOldName,mChangeType));
	
		}

		private void OnDeleted(object source, FileSystemEventArgs args)
		{	
			mCurrentFileName = args.Name;
			mCurrentPath = args.FullPath;
			mCurrentOldName = "";
			mCurrentOldPath = "";
			mChangeType = FileWatcherChangeType.FileDeleted;
			OnChanged(new FileWatcherEventArgs(mCurrentFileName,mCurrentPath,mCurrentOldPath,mCurrentOldName,mChangeType));
		}

		private void OnChanged(object source, FileSystemEventArgs args)
		{	
			mCurrentFileName = args.Name;
			mCurrentPath = args.FullPath;
			mCurrentOldName = "";
			mCurrentOldPath = "";
			mChangeType = FileWatcherChangeType.Filechanged;
			OnChanged(new FileWatcherEventArgs(mCurrentFileName,mCurrentPath,mCurrentOldPath,mCurrentOldName,mChangeType));
		}

		#endregion
	}
}
