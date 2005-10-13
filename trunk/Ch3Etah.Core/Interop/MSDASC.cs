//using System;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;
//
//namespace Ch3Etah.Core.Interop
//{
//	
//	[StructLayout(LayoutKind.Explicit, Size=0x10, Pack=4)]
//	public struct __MIDL_DBStructureDefinitions_0001
//	{
//	}
//
//	[StructLayout(LayoutKind.Explicit, Size=4, Pack=4)]
//	public struct __MIDL_DBStructureDefinitions_0002
//	{
//	}
//
//	[StructLayout(LayoutKind.Explicit, Pack=4)]
//	public struct __MIDL_IWinTypes_0009
//	{
//		// Fields
//		[FieldOffset(0)]
//		public int hInproc;
//		[FieldOffset(0)]
//		public int hRemote;
//	}
//
//	[StructLayout(LayoutKind.Sequential, Pack=4), ComConversionLoss]
//	public struct _COAUTHIDENTITY
//	{
//		[ComConversionLoss]
//		public IntPtr User;
//		public uint UserLength;
//		[ComConversionLoss]
//		public IntPtr Domain;
//		public uint DomainLength;
//		[ComConversionLoss]
//		public IntPtr Password;
//		public uint PasswordLength;
//		public uint Flags;
//	}
//
//	[StructLayout(LayoutKind.Sequential, Pack=4), ComConversionLoss]
//	public struct _COAUTHINFO
//	{
//		public uint dwAuthnSvc;
//		public uint dwAuthzSvc;
//		[MarshalAs(UnmanagedType.LPWStr)]
//		public string pwszServerPrincName;
//		public uint dwAuthnLevel;
//		public uint dwImpersonationLevel;
//		[ComConversionLoss]
//		public IntPtr pAuthIdentityData;
//		public uint dwCapabilities;
//	}
//
//	[StructLayout(LayoutKind.Sequential, Pack=4), ComConversionLoss]
//	public struct _COSERVERINFO
//	{
//		public uint dwReserved1;
//		[MarshalAs(UnmanagedType.LPWStr)]
//		public string pwszName;
//		[ComConversionLoss]
//		public IntPtr pAuthInfo;
//		public uint dwReserved2;
//	}
//
//	[StructLayout(LayoutKind.Sequential, Pack=4)]
//	public struct _FILETIME
//	{
//		public uint dwLowDateTime;
//		public uint dwHighDateTime;
//	}
//
//	[StructLayout(LayoutKind.Sequential, Pack=8)]
//	public struct _LARGE_INTEGER
//	{
//		public long QuadPart;
//	}
//
//	[StructLayout(LayoutKind.Sequential, Pack=4)]
//	public struct _RemotableHandle
//	{
//		public int fContext;
//		public __MIDL_IWinTypes_0009 u;
//	}
//
//	[StructLayout(LayoutKind.Sequential, Pack=8)]
//	public struct _ULARGE_INTEGER
//	{
//		public ulong QuadPart;
//	}
//
//	[ComImport, CoClass(typeof(DataLinksClass)), Guid("2206CCB2-19C1-11D1-89E0-00C04FD7A829")]
//	public interface DataLinks : IDataSourceLocator
//	{
//	}
//
//	[ComImport, Guid("2206CDB2-19C1-11D1-89E0-00C04FD7A829"), TypeLibType(2), ClassInterface((short) 0), ComConversionLoss]
//	public class DataLinksClass : IDataSourceLocator, DataLinks, IDBPromptInitialize, IDataInitialize
//	{
//		// Methods
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void CreateDBInstance([In] ref Guid clsidProvider, [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] uint dwClsCtx, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszReserved, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppDataSource);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void GetDataSource([In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] uint dwClsCtx, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszInitializationString, [In] ref Guid riid, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object ppDataSource);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void GetInitializationString([In, MarshalAs(UnmanagedType.IUnknown)] object pDataSource, [In] sbyte fIncludePassword, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszInitString);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void LoadStringFromStorage([In, MarshalAs(UnmanagedType.LPWStr)] string pwszFileName, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszInitializationString);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void PromptDataSource([In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In, ComAliasName("MSDASC.wireHWND")] ref _RemotableHandle hWndParent, [In] uint dwPromptOptions, [In] uint cSourceTypeFilter, [In] ref uint rgSourceTypeFilter, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszszzProviderFilter, [In] ref Guid riid, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object ppDataSource);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime), DispId(0x60020003)]
//		public virtual extern bool PromptEdit([In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object ppADOConnection);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void PromptFileName([In, ComAliasName("MSDASC.wireHWND")] ref _RemotableHandle hWndParent, [In] uint dwPromptOptions, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszInitialDirectory, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszInitialFile, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszSelectedFile);
//		[return: MarshalAs(UnmanagedType.IDispatch)]
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime), DispId(0x60020002)]
//		public virtual extern object PromptNew();
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void RemoteCreateDBInstanceEx([In] ref Guid clsidProvider, [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] uint dwClsCtx, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszReserved, [In] ref _COSERVERINFO pServerInfo, [In] uint cmq, [In] IntPtr rgpIID, [MarshalAs(UnmanagedType.IUnknown)] out object rgpItf, [MarshalAs(UnmanagedType.Error)] out int rghr);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void WriteStringToStorage([In, MarshalAs(UnmanagedType.LPWStr)] string pwszFileName, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszInitializationString, [In] uint dwCreationDisposition);
//
//		// Properties
//		[DispId(0x60020000)]
//		public virtual extern int hWnd { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime), DispId(0x60020000)] get; [param: In] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime), DispId(0x60020000)] set; }
//	}
//
//	[ComImport, Guid("79EAC9D0-BAF9-11CE-8C82-00AA004BA90B"), InterfaceType(1), ComConversionLoss]
//	public interface IAuthenticate
//	{
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void Authenticate([Out, ComAliasName("MSDASC.wireHWND")] IntPtr phwnd, [MarshalAs(UnmanagedType.LPWStr)] out string pszUsername, [MarshalAs(UnmanagedType.LPWStr)] out string pszPassword);
//	}
//
//	[ComImport, Guid("0C733AB1-2A1C-11CE-ADE5-00AA0044773D"), InterfaceType(1)]
//	public interface IBindResource
//	{
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteBind([In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL, [In] uint dwBindURLFlags, [In] ref Guid rguid, [In] ref Guid riid, [In, MarshalAs(UnmanagedType.Interface)] IAuthenticate pAuthenticate, [In, MarshalAs(UnmanagedType.IUnknown)] object pSessionUnkOuter, [In] ref Guid piid, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object ppSession, [In, Out] ref uint pdwBindStatus, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);
//	}
//
//	[ComImport, Guid("0C733AB2-2A1C-11CE-ADE5-00AA0044773D"), InterfaceType(1)]
//	public interface ICreateRow
//	{
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteCreateRow([In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL, [In] uint dwBindURLFlags, [In] ref Guid rguid, [In] ref Guid riid, [In, MarshalAs(UnmanagedType.Interface)] IAuthenticate pAuthenticate, [In, MarshalAs(UnmanagedType.IUnknown)] object pSessionUnkOuter, [In] ref Guid piid, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object ppSession, [In, Out] ref uint pdwBindStatus, [In, Out, MarshalAs(UnmanagedType.LPWStr)] ref string ppwszNewURL, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);
//	}
//
//	[ComImport, Guid("2206CCB1-19C1-11D1-89E0-00C04FD7A829"), InterfaceType(1), ComConversionLoss]
//	public interface IDataInitialize
//	{
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void GetDataSource([In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] uint dwClsCtx, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszInitializationString, [In] ref Guid riid, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object ppDataSource);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void GetInitializationString([In, MarshalAs(UnmanagedType.IUnknown)] object pDataSource, [In] sbyte fIncludePassword, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszInitString);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void CreateDBInstance([In] ref Guid clsidProvider, [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] uint dwClsCtx, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszReserved, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppDataSource);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteCreateDBInstanceEx([In] ref Guid clsidProvider, [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] uint dwClsCtx, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszReserved, [In] ref _COSERVERINFO pServerInfo, [In] uint cmq, [In] IntPtr rgpIID, [MarshalAs(UnmanagedType.IUnknown)] out object rgpItf, [MarshalAs(UnmanagedType.Error)] out int rghr);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void LoadStringFromStorage([In, MarshalAs(UnmanagedType.LPWStr)] string pwszFileName, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszInitializationString);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void WriteStringToStorage([In, MarshalAs(UnmanagedType.LPWStr)] string pwszFileName, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszInitializationString, [In] uint dwCreationDisposition);
//	}
//
//	[ComImport, TypeLibType(0x1040), Guid("2206CCB2-19C1-11D1-89E0-00C04FD7A829")]
//	public interface IDataSourceLocator
//	{
//		[DispId(0x60020000)]
//		int hWnd { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime), DispId(0x60020000)] get; [param: In] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime), DispId(0x60020000)] set; }
//		[return: MarshalAs(UnmanagedType.IDispatch)]
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime), DispId(0x60020002)]
//		object PromptNew();
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime), DispId(0x60020003)]
//		bool PromptEdit([In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object ppADOConnection);
//	}
//
//	[ComImport, Guid("0C733AB3-2A1C-11CE-ADE5-00AA0044773D"), InterfaceType(1)]
//	public interface IDBBinderProperties : IDBProperties
//	{
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteGetProperties([In] uint cPropertyIDSets, [In] ref tagDBPROPIDSET rgPropertyIDSets, [In, Out] ref uint pcPropertySets, [Out] IntPtr prgPropertySets, [MarshalAs(UnmanagedType.Interface)] out IErrorInfo ppErrorInfoRem);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteGetPropertyInfo([In] uint cPropertyIDSets, [In] ref tagDBPROPIDSET rgPropertyIDSets, [In, Out] ref uint pcPropertyInfoSets, [Out] IntPtr prgPropertyInfoSets, [In, Out] ref uint pcOffsets, [Out] IntPtr prgDescOffsets, [In, Out] ref uint pcbDescBuffer, [In, Out] IntPtr ppDescBuffer, [MarshalAs(UnmanagedType.Interface)] out IErrorInfo ppErrorInfoRem);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteSetProperties([In] uint cPropertySets, [In] ref tagDBPROPSET rgPropertySets, [In] uint cTotalProps, out uint rgPropStatus, [MarshalAs(UnmanagedType.Interface)] out IErrorInfo ppErrorInfoRem);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void Reset();
//	}
//
//	[ComImport, TypeLibType(0x200), Guid("2206CCB0-19C1-11D1-89E0-00C04FD7A829"), InterfaceType(1)]
//	public interface IDBPromptInitialize
//	{
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void PromptDataSource([In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In, ComAliasName("MSDASC.wireHWND")] ref _RemotableHandle hWndParent, [In] uint dwPromptOptions, [In] uint cSourceTypeFilter, [In] ref uint rgSourceTypeFilter, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszszzProviderFilter, [In] ref Guid riid, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object ppDataSource);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void PromptFileName([In, ComAliasName("MSDASC.wireHWND")] ref _RemotableHandle hWndParent, [In] uint dwPromptOptions, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszInitialDirectory, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszInitialFile, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszSelectedFile);
//	}
//
//	[ComImport, InterfaceType(1), ComConversionLoss, Guid("0C733A8A-2A1C-11CE-ADE5-00AA0044773D")]
//	public interface IDBProperties
//	{
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteGetProperties([In] uint cPropertyIDSets, [In] ref tagDBPROPIDSET rgPropertyIDSets, [In, Out] ref uint pcPropertySets, [Out] IntPtr prgPropertySets, [MarshalAs(UnmanagedType.Interface)] out IErrorInfo ppErrorInfoRem);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteGetPropertyInfo([In] uint cPropertyIDSets, [In] ref tagDBPROPIDSET rgPropertyIDSets, [In, Out] ref uint pcPropertyInfoSets, [Out] IntPtr prgPropertyInfoSets, [In, Out] ref uint pcOffsets, [Out] IntPtr prgDescOffsets, [In, Out] ref uint pcbDescBuffer, [In, Out] IntPtr ppDescBuffer, [MarshalAs(UnmanagedType.Interface)] out IErrorInfo ppErrorInfoRem);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteSetProperties([In] uint cPropertySets, [In] ref tagDBPROPSET rgPropertySets, [In] uint cTotalProps, out uint rgPropStatus, [MarshalAs(UnmanagedType.Interface)] out IErrorInfo ppErrorInfoRem);
//	}
//
//	[ComImport, InterfaceType(1), Guid("1CF2B120-547D-101B-8E65-08002B2BD119")]
//	public interface IErrorInfo
//	{
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void GetGUID(out Guid pguid);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void GetSource([MarshalAs(UnmanagedType.BStr)] out string pBstrSource);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void GetDescription([MarshalAs(UnmanagedType.BStr)] out string pBstrDescription);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void GetHelpFile([MarshalAs(UnmanagedType.BStr)] out string pBstrHelpFile);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void GetHelpContext(out uint pdwHelpContext);
//	}
//
//	[ComImport, ComConversionLoss, InterfaceType(1), Guid("00000003-0000-0000-C000-000000000046")]
//	public interface IMarshal
//	{
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void GetUnmarshalClass([In] ref Guid riid, [In] IntPtr pv, [In] uint dwDestContext, [In] IntPtr pvDestContext, [In] uint mshlflags, out Guid pCid);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void GetMarshalSizeMax([In] ref Guid riid, [In] IntPtr pv, [In] uint dwDestContext, [In] IntPtr pvDestContext, [In] uint mshlflags, out uint pSize);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void MarshalInterface([In, MarshalAs(UnmanagedType.Interface)] IStream pstm, [In] ref Guid riid, [In] IntPtr pv, [In] uint dwDestContext, [In] IntPtr pvDestContext, [In] uint mshlflags);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void UnmarshalInterface([In, MarshalAs(UnmanagedType.Interface)] IStream pstm, [In] ref Guid riid, out IntPtr ppv);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void ReleaseMarshalData([In, MarshalAs(UnmanagedType.Interface)] IStream pstm);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void DisconnectObject([In] uint dwReserved);
//	}
//
//	[ComImport, Guid("0000010C-0000-0000-C000-000000000046"), InterfaceType(1)]
//	public interface IPersist
//	{
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void GetClassID(out Guid pClassID);
//	}
//
//	[ComImport, Guid("0000010B-0000-0000-C000-000000000046"), InterfaceType(1)]
//	public interface IPersistFile : IPersist
//	{
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void GetClassID(out Guid pClassID);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void IsDirty();
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void Load([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [In] uint dwMode);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void Save([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [In] int fRemember);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void SaveCompleted([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
//	}
//
//	[ComImport, Guid("0C733AB9-2A1C-11CE-ADE5-00AA0044773D"), InterfaceType(1)]
//	public interface IRegisterProvider
//	{
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteGetURLMapping([In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL, [In] uint dwReserved, out Guid pclsidProvider);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void SetURLMapping([In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL, [In] uint dwReserved, [In] ref Guid rclsidProvider);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void UnregisterProvider([In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL, [In] uint dwReserved, [In] ref Guid rclsidProvider);
//	}
//
//	[ComImport, Guid("0C733A30-2A1C-11CE-ADE5-00AA0044773D"), InterfaceType(1)]
//	public interface ISequentialStream
//	{
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteRead(out byte pv, [In] uint cb, out uint pcbRead);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteWrite([In] ref byte pv, [In] uint cb, out uint pcbWritten);
//	}
//
//	[ComImport, Guid("0000000C-0000-0000-C000-000000000046"), InterfaceType(1)]
//	public interface IStream : ISequentialStream
//	{
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteRead(out byte pv, [In] uint cb, out uint pcbRead);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteWrite([In] ref byte pv, [In] uint cb, out uint pcbWritten);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteSeek([In] _LARGE_INTEGER dlibMove, [In] uint dwOrigin, out _ULARGE_INTEGER plibNewPosition);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void SetSize([In] _ULARGE_INTEGER libNewSize);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void RemoteCopyTo([In, MarshalAs(UnmanagedType.Interface)] IStream pstm, [In] _ULARGE_INTEGER cb, out _ULARGE_INTEGER pcbRead, out _ULARGE_INTEGER pcbWritten);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void Commit([In] uint grfCommitFlags);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void Revert();
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void LockRegion([In] _ULARGE_INTEGER libOffset, [In] _ULARGE_INTEGER cb, [In] uint dwLockType);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void UnlockRegion([In] _ULARGE_INTEGER libOffset, [In] _ULARGE_INTEGER cb, [In] uint dwLockType);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void Stat(out tagSTATSTG pstatstg, [In] uint grfStatFlag);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		void Clone([MarshalAs(UnmanagedType.Interface)] out IStream ppstm);
//	}
//
//	[ComImport, CoClass(typeof(MSDAINITIALIZEClass)), Guid("2206CCB1-19C1-11D1-89E0-00C04FD7A829")]
//	public interface MSDAINITIALIZE : IDataInitialize
//	{
//	}
//
//	[ComImport, Guid("2206CDB0-19C1-11D1-89E0-00C04FD7A829"), TypeLibType(2), ComConversionLoss, ClassInterface((short) 0)]
//	public class MSDAINITIALIZEClass : IDataInitialize, MSDAINITIALIZE
//	{
//		// Methods
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void CreateDBInstance([In] ref Guid clsidProvider, [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] uint dwClsCtx, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszReserved, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppDataSource);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void GetDataSource([In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] uint dwClsCtx, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszInitializationString, [In] ref Guid riid, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object ppDataSource);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void GetInitializationString([In, MarshalAs(UnmanagedType.IUnknown)] object pDataSource, [In] sbyte fIncludePassword, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszInitString);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void LoadStringFromStorage([In, MarshalAs(UnmanagedType.LPWStr)] string pwszFileName, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszInitializationString);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void RemoteCreateDBInstanceEx([In] ref Guid clsidProvider, [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] uint dwClsCtx, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszReserved, [In] ref _COSERVERINFO pServerInfo, [In] uint cmq, [In] IntPtr rgpIID, [MarshalAs(UnmanagedType.IUnknown)] out object rgpItf, [MarshalAs(UnmanagedType.Error)] out int rghr);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void WriteStringToStorage([In, MarshalAs(UnmanagedType.LPWStr)] string pwszFileName, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszInitializationString, [In] uint dwCreationDisposition);
//	}
//
//	[ComImport, Guid("0000010B-0000-0000-C000-000000000046"), CoClass(typeof(PDPOClass))]
//	public interface PDPO : IPersistFile
//	{
//	}
//
//	[ComImport, Guid("CCB4EC60-B9DC-11D1-AC80-00A0C9034873"), ClassInterface((short) 0), TypeLibType(2)]
//	public class PDPOClass : IPersistFile, PDPO
//	{
//		// Methods
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void GetClassID(out Guid pClassID);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void IsDirty();
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void Load([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [In] uint dwMode);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void Save([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [In] int fRemember);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void SaveCompleted([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName);
//	}
//
//	[ComImport, Guid("0C733AB1-2A1C-11CE-ADE5-00AA0044773D"), CoClass(typeof(RootBinderClass))]
//	public interface RootBinder : IBindResource
//	{
//	}
//
//	[ComImport, Guid("FF151822-B0BF-11D1-A80D-000000000000"), ComConversionLoss, TypeLibType(2), ClassInterface((short) 0)]
//	public class RootBinderClass : IBindResource, RootBinder, ICreateRow, IRegisterProvider, IDBBinderProperties, IMarshal
//	{
//		// Methods
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void DisconnectObject([In] uint dwReserved);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void GetMarshalSizeMax([In] ref Guid riid, [In] IntPtr pv, [In] uint dwDestContext, [In] IntPtr pvDestContext, [In] uint mshlflags, out uint pSize);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void GetUnmarshalClass([In] ref Guid riid, [In] IntPtr pv, [In] uint dwDestContext, [In] IntPtr pvDestContext, [In] uint mshlflags, out Guid pCid);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void MarshalInterface([In, MarshalAs(UnmanagedType.Interface)] IStream pstm, [In] ref Guid riid, [In] IntPtr pv, [In] uint dwDestContext, [In] IntPtr pvDestContext, [In] uint mshlflags);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void ReleaseMarshalData([In, MarshalAs(UnmanagedType.Interface)] IStream pstm);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void RemoteBind([In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL, [In] uint dwBindURLFlags, [In] ref Guid rguid, [In] ref Guid riid, [In, MarshalAs(UnmanagedType.Interface)] IAuthenticate pAuthenticate, [In, MarshalAs(UnmanagedType.IUnknown)] object pSessionUnkOuter, [In] ref Guid piid, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object ppSession, [In, Out] ref uint pdwBindStatus, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void RemoteCreateRow([In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL, [In] uint dwBindURLFlags, [In] ref Guid rguid, [In] ref Guid riid, [In, MarshalAs(UnmanagedType.Interface)] IAuthenticate pAuthenticate, [In, MarshalAs(UnmanagedType.IUnknown)] object pSessionUnkOuter, [In] ref Guid piid, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object ppSession, [In, Out] ref uint pdwBindStatus, [In, Out, MarshalAs(UnmanagedType.LPWStr)] ref string ppwszNewURL, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void RemoteGetProperties([In] uint cPropertyIDSets, [In] ref tagDBPROPIDSET rgPropertyIDSets, [In, Out] ref uint pcPropertySets, [Out] IntPtr prgPropertySets, [MarshalAs(UnmanagedType.Interface)] out IErrorInfo ppErrorInfoRem);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void RemoteGetPropertyInfo([In] uint cPropertyIDSets, [In] ref tagDBPROPIDSET rgPropertyIDSets, [In, Out] ref uint pcPropertyInfoSets, [Out] IntPtr prgPropertyInfoSets, [In, Out] ref uint pcOffsets, [Out] IntPtr prgDescOffsets, [In, Out] ref uint pcbDescBuffer, [In, Out] IntPtr ppDescBuffer, [MarshalAs(UnmanagedType.Interface)] out IErrorInfo ppErrorInfoRem);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void RemoteGetURLMapping([In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL, [In] uint dwReserved, out Guid pclsidProvider);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void RemoteSetProperties([In] uint cPropertySets, [In] ref tagDBPROPSET rgPropertySets, [In] uint cTotalProps, out uint rgPropStatus, [MarshalAs(UnmanagedType.Interface)] out IErrorInfo ppErrorInfoRem);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void Reset();
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void SetURLMapping([In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL, [In] uint dwReserved, [In] ref Guid rclsidProvider);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void UnmarshalInterface([In, MarshalAs(UnmanagedType.Interface)] IStream pstm, [In] ref Guid riid, out IntPtr ppv);
//		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
//		public virtual extern void UnregisterProvider([In, MarshalAs(UnmanagedType.LPWStr)] string pwszURL, [In] uint dwReserved, [In] ref Guid rclsidProvider);
//	}
//
//	[StructLayout(LayoutKind.Sequential, Pack=4)]
//	public struct tagDBID
//	{
//		public __MIDL_DBStructureDefinitions_0001 uGuid;
//		public uint eKind;
//		public __MIDL_DBStructureDefinitions_0002 uName;
//	}
//
//	[StructLayout(LayoutKind.Sequential, Pack=8)]
//	public struct tagDBPROP
//	{
//		public uint dwPropertyID;
//		public uint dwOptions;
//		public uint dwStatus;
//		public tagDBID colid;
//		[MarshalAs(UnmanagedType.Struct)]
//		public object vValue;
//	}
//
//	[StructLayout(LayoutKind.Sequential, Pack=4), ComConversionLoss]
//	public struct tagDBPROPIDSET
//	{
//		[ComConversionLoss]
//		public IntPtr rgPropertyIDs;
//		public uint cPropertyIDs;
//		public Guid guidPropertySet;
//	}
//
//	[StructLayout(LayoutKind.Sequential, Pack=8)]
//	public struct tagDBPROPINFO
//	{
//		[MarshalAs(UnmanagedType.LPWStr)]
//		public string pwszDescription;
//		public uint dwPropertyID;
//		public uint dwFlags;
//		public ushort vtType;
//		[MarshalAs(UnmanagedType.Struct)]
//		public object vValues;
//	}
//
//	[StructLayout(LayoutKind.Sequential, Pack=4), ComConversionLoss]
//	public struct tagDBPROPINFOSET
//	{
//		[ComConversionLoss]
//		public IntPtr rgPropertyInfos;
//		public uint cPropertyInfos;
//		public Guid guidPropertySet;
//	}
//
//	[StructLayout(LayoutKind.Sequential, Pack=4), ComConversionLoss]
//	public struct tagDBPROPSET
//	{
//		[ComConversionLoss]
//		public IntPtr rgProperties;
//		public uint cProperties;
//		public Guid guidPropertySet;
//	}
//
//	[StructLayout(LayoutKind.Sequential, Pack=8)]
//	public struct tagSTATSTG
//	{
//		[MarshalAs(UnmanagedType.LPWStr)]
//		public string pwcsName;
//		public uint type;
//		public _ULARGE_INTEGER cbSize;
//		public _FILETIME mtime;
//		public _FILETIME ctime;
//		public _FILETIME atime;
//		public uint grfMode;
//		public uint grfLocksSupported;
//		public Guid clsid;
//		public uint grfStateBits;
//		public uint reserved;
//	}
//
//
//}