// --------------------------------------------------------------------------------
//  C# implementation of the Agilent 488 API definitions.  Use this file with the
//  C# language to program GPIB I/O interfaces that support the Agilent 488 (or
//  National Instruments NI-488.2) I/O API, such as Agilent 82357A USB/GPIB 
//  converter or Agilent 82350B PCI GPIB card.  See the Agilent IO Libraries Suite 
//  documentation for more information on programming with Agilent 488.
//
//  Copyright (C) 2005 Agilent Technologies, Inc.
// --------------------------------------------------------------------------------
//  Title   : ag488.cs
//  Date    : 07-13-2005
//  Purpose : Agilent 488 definitions for C#
// -------------------------------------------------------------------------
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace GPIBͨѶ
{
	public delegate int GpibNotifyCallback_t(int localUd, int localIbsta, int localIberr, int localIbcntl, IntPtr refData);

	/// <summary>
	/// This class contains the definitions for the Agilent 488 functions and constant values
	/// for use in C#.
	/// </summary>
	
	public class Ag488Wrap
	{
		#region Implementation Fields and definitions
		protected static bool m_globalsInitialized = false;
		protected static IntPtr m_statusVariables;
		protected static IntPtr m_ibstaPtr;
		protected static IntPtr m_iberrPtr;
		protected static IntPtr m_ibcntPtr;
		protected static IntPtr m_ibcntlPtr;

		/*protected static GCHandle m_ibcntHandle;
		protected static GCHandle m_ibstaHandle;
		protected static GCHandle m_iberrHandle;
		protected static GCHandle m_ibcntlHandle; */
		[DllImportAttribute("gpib-32.dll", EntryPoint="RegisterGpibGlobalsForThread", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int RegisterGpibGlobalsForThread (IntPtr Longibsta, IntPtr Longiberr, IntPtr Longibcnt, IntPtr ibcntl);
		[DllImportAttribute("gpib-32.dll", EntryPoint="UnregisterGpibGlobalsForThread", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int UnregisterGpibGlobalsForThread();
		
		private Ag488Wrap()
		{
		}
		#endregion
		
		#region Implementation helper functions
		protected static void VerifyGpibGlobalsRegistered()
		{ 
			if (!m_globalsInitialized)
			{
				m_statusVariables = Marshal.AllocHGlobal(16);
				m_ibstaPtr = m_statusVariables;
				m_iberrPtr = new IntPtr(m_statusVariables.ToInt32() + 4);
				m_ibcntPtr = new IntPtr(m_statusVariables.ToInt32() + 8);
				m_ibcntlPtr = new IntPtr(m_statusVariables.ToInt32() + 12);
				
				int rc = RegisterGpibGlobalsForThread(
					m_ibstaPtr, m_iberrPtr, m_ibcntPtr, m_ibcntlPtr);
				if (rc == 0)
				{
					m_globalsInitialized = true;
				}
				else if (rc == 1) 
				{
					rc = UnregisterGpibGlobalsForThread();
					rc = RegisterGpibGlobalsForThread(
						m_ibstaPtr, m_iberrPtr, m_ibcntPtr, m_ibcntlPtr);
					m_globalsInitialized = true;
				}
				else if (rc == 2 || rc == 3) 
				{
					rc = UnregisterGpibGlobalsForThread();
					Marshal.FreeHGlobal(m_statusVariables);
				}
				else
				{
					Marshal.FreeHGlobal(m_statusVariables);
				}
			}
		} 
		protected static void VerifyGpibGlobalsNotRegistered()
		{
			if (m_globalsInitialized)
			{
				UnregisterGpibGlobalsForThread();
				Marshal.FreeHGlobal(m_statusVariables);
				m_ibstaPtr = IntPtr.Zero;
				m_iberrPtr = IntPtr.Zero;
				m_ibcntPtr = IntPtr.Zero;
				m_ibcntlPtr = IntPtr.Zero;
				m_statusVariables = IntPtr.Zero;
				m_globalsInitialized = false;
			}
		}
		#endregion

		#region 488.2 Import Implementation 
		/*  Agilent 488 Function Prototypes  */
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibfindA", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibfindA32 (string udname);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibbnaA", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibbnaA32 (int ud, string udname);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibrdfA", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibrdfA32 (int ud, string filename);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibwrtfA", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibwrtfA32 (int ud, string filename);

		[DllImportAttribute("gpib-32.dll", EntryPoint="ibfindW", ExactSpelling=true, CharSet=CharSet.Unicode, SetLastError=false)]
		protected static extern int ibfindW32 (string udname);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibbnaW", ExactSpelling=true, CharSet=CharSet.Unicode, SetLastError=false)]
		protected static extern int ibbnaW32 (int ud, string udname);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibrdfW", ExactSpelling=true, CharSet=CharSet.Unicode, SetLastError=false)]
		protected static extern int ibrdfW32 (int ud, string filename);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibwrtfW", ExactSpelling=true, CharSet=CharSet.Unicode, SetLastError=false)]
		protected static extern int ibwrtfW32 (int ud, string filename);

		[DllImportAttribute("gpib-32.dll", EntryPoint="ibask", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibask32 (int ud, int option, out int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibcac", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibcac32 (int ud, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibclr", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibclr32 (int ud);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibcmd", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibcmd32 (int ud, byte[] buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibcmd", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibcmd32 (int ud, short[] buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibcmd", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibcmd32 (int ud, int[] buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibcmd", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibcmd32 (int ud, float[] buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibcmd", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibcmd32 (int ud, double[] buf, int cnt);

		[DllImportAttribute("gpib-32.dll", EntryPoint="ibcmd", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibcmd32 (int ud, IntPtr buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibcmd", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibcmd32 (int ud, string buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibcmda", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibcmda32 (int ud, IntPtr /* use a GCHandle */ buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibconfig", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibconfig32 (int ud, int option, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibdev", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibdev32 (int boardID, int pad, int sad, int tmo, int eot, int eos);
		/*		[DllImportAttribute("gpib-32.dll", EntryPoint="ibdiag", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
				protected static extern int ibdiag32 (int ud, PVOID buf, int cnt); */
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibdma", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibdma32 (int ud, int v);
		/*[DllImportAttribute("gpib-32.dll", EntryPoint="ibexpert", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibexpert32 (int ud, int option, void * Input, void * Output); */
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibeos", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibeos32 (int ud, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibeot", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibeot32 (int ud, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibgts", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibgts32 (int ud, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibist", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibist32 (int ud, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="iblck", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int iblck32 (int ud, int v, uint LockWaitTime, IntPtr Reserved);
		[DllImportAttribute("gpib-32.dll", EntryPoint="iblines", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int iblines32 (int ud, out short result);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibln", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibln32 (int ud, int pad, int sad, out short listen);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibloc", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibloc32 (int ud);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibnotify", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibnotify32 (int ud, int mask, GpibNotifyCallback_t Callback, IntPtr /* use a GCHandle */ RefData);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibonl", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibonl32 (int ud, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibpad", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibpad32 (int ud, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibpct", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibpct32 (int ud);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibpoke", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibpoke32 (int ud, int option, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibppc", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibppc32 (int ud, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibrd", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibrd32 (int ud, byte[] buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibrd", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibrd32 (int ud, short[] buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibrd", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibrd32 (int ud, int[] buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibrd", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibrd32 (int ud, float[] buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibrd", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibrd32 (int ud, double[] buf, int cnt);

		[DllImportAttribute("gpib-32.dll", EntryPoint="ibrd", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibrd32 (int ud, IntPtr buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibrd", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibrd32 (int ud, StringBuilder buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibrda", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibrda32 (int ud, IntPtr /* use a GCHandle */ buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibrpp", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibrpp32 (int ud, out byte ppr);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibrsc", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibrsc32 (int ud, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibrsp", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibrsp32 (int ud, out byte spr);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibrsv", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibrsv32 (int ud, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibsad", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibsad32 (int ud, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibsic", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibsic32 (int ud);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibsre", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibsre32 (int ud, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibstop", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibstop32 (int ud);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibtmo", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibtmo32 (int ud, int v);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibtrg", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibtrg32 (int ud);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibwait", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibwait32 (int ud, int mask);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibwrt", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibwrt32 (int ud, byte[] buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibwrt", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibwrt32 (int ud, short[] buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibwrt", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibwrt32 (int ud, int[] buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibwrt", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibwrt32 (int ud, float[] buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibwrt", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibwrt32 (int ud, double[] buf, int cnt);

		[DllImportAttribute("gpib-32.dll", EntryPoint="ibwrt", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibwrt32 (int ud, IntPtr buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibwrt", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibwrt32 (int ud, string buf, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibwrta", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibwrta32 (int ud, IntPtr /* use a GCHandle */ buf, int cnt);

		// GPIB-ENET only functions to support locking across machines
		// Deprecated - Use iblck
		[DllImportAttribute("gpib-32.dll", EntryPoint="iblock", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int iblock32 (int ud);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ibunlock", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ibunlock32 (int ud);

		//
		//  Functions to access Thread-Specific copies of the GPIB global vars 

		[DllImportAttribute("gpib-32.dll", EntryPoint="ThreadIbsta", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ThreadIbsta32 ();
		[DllImportAttribute("gpib-32.dll", EntryPoint="ThreadIberr", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ThreadIberr32 ();
		[DllImportAttribute("gpib-32.dll", EntryPoint="ThreadIbcnt", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ThreadIbcnt32 ();
		[DllImportAttribute("gpib-32.dll", EntryPoint="ThreadIbcntl", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern int ThreadIbcntl32 ();


		//
		//  Agilent 488 Function Prototypes  

		[DllImportAttribute("gpib-32.dll", EntryPoint="AllSpoll", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void AllSpoll32 (int boardID, /* Addr4882_t */ short[] addrlist, short[] results);
		[DllImportAttribute("gpib-32.dll", EntryPoint="DevClear", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void DevClear32 (int boardID, /* Addr4882_t */ short addr);
		[DllImportAttribute("gpib-32.dll", EntryPoint="DevClearList", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void DevClearList32 (int boardID, /* Addr4882_t */ short[] addrlist);
		[DllImportAttribute("gpib-32.dll", EntryPoint="EnableLocal", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void EnableLocal32 (int boardID, /* Addr4882_t */ short[] addrlist);
		[DllImportAttribute("gpib-32.dll", EntryPoint="EnableRemote", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void EnableRemote32 (int boardID, /* Addr4882_t */ short[] addrlist);
		[DllImportAttribute("gpib-32.dll", EntryPoint="FindLstn", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void FindLstn32 (int boardID, /* Addr4882_t */ short[] addrlist, /* Addr4882_t */ short[] results, int limit);
		[DllImportAttribute("gpib-32.dll", EntryPoint="FindRQS", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void FindRQS32 (int boardID, /* Addr4882_t */ short[] addrlist, short[] dev_stat);
		[DllImportAttribute("gpib-32.dll", EntryPoint="PPoll", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void PPoll32 (int boardID, out short result);
		[DllImportAttribute("gpib-32.dll", EntryPoint="PPollConfig", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void PPollConfig32 (int boardID, /* Addr4882_t */ short addr, int dataLine, int lineSense);
		[DllImportAttribute("gpib-32.dll", EntryPoint="PPollUnconfig", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void PPollUnconfig32 (int boardID, /* Addr4882_t */ short[] addrlist);
		[DllImportAttribute("gpib-32.dll", EntryPoint="PassControl", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void PassControl32 (int boardID, /* Addr4882_t */ short addr);
		[DllImportAttribute("gpib-32.dll", EntryPoint="RcvRespMsg", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void RcvRespMsg32 (int boardID, byte[] buffer, int cnt, int Termination);
		[DllImportAttribute("gpib-32.dll", EntryPoint="RcvRespMsg", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void RcvRespMsg32 (int boardID, short[] buffer, int cnt, int Termination);
		[DllImportAttribute("gpib-32.dll", EntryPoint="RcvRespMsg", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void RcvRespMsg32 (int boardID, int[] buffer, int cnt, int Termination);
		[DllImportAttribute("gpib-32.dll", EntryPoint="RcvRespMsg", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void RcvRespMsg32 (int boardID, float[] buffer, int cnt, int Termination);
		[DllImportAttribute("gpib-32.dll", EntryPoint="RcvRespMsg", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void RcvRespMsg32 (int boardID, double[] buffer, int cnt, int Termination);

		[DllImportAttribute("gpib-32.dll", EntryPoint="RcvRespMsg", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void RcvRespMsg32 (int boardID, IntPtr buffer, int cnt, int Termination);
		[DllImportAttribute("gpib-32.dll", EntryPoint="RcvRespMsg", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void RcvRespMsg32 (int boardID, StringBuilder buffer, int cnt, int Termination);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ReadStatusByte", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void ReadStatusByte32 (int boardID, /* Addr4882_t */ short addr, out short result);
		[DllImportAttribute("gpib-32.dll", EntryPoint="Receive", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void Receive32 (int boardID, /* Addr4882_t */ short addr, byte[] buffer, int cnt, int Termination);
		[DllImportAttribute("gpib-32.dll", EntryPoint="Receive", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void Receive32 (int boardID, /* Addr4882_t */ short addr, short[] buffer, int cnt, int Termination);
		[DllImportAttribute("gpib-32.dll", EntryPoint="Receive", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void Receive32 (int boardID, /* Addr4882_t */ short addr, int[] buffer, int cnt, int Termination);
		[DllImportAttribute("gpib-32.dll", EntryPoint="Receive", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void Receive32 (int boardID, /* Addr4882_t */ short addr, float[] buffer, int cnt, int Termination);
		[DllImportAttribute("gpib-32.dll", EntryPoint="Receive", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void Receive32 (int boardID, /* Addr4882_t */ short addr, double[] buffer, int cnt, int Termination);

		[DllImportAttribute("gpib-32.dll", EntryPoint="Receive", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void Receive32 (int boardID, /* Addr4882_t */ short addr, StringBuilder buffer, int cnt, int Termination);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ReceiveSetup", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void ReceiveSetup32 (int boardID, /* Addr4882_t */ short addr);
		[DllImportAttribute("gpib-32.dll", EntryPoint="ResetSys", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void ResetSys32 (int boardID, /* Addr4882_t */ short[] addrlist);
		[DllImportAttribute("gpib-32.dll", EntryPoint="Send", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void Send32 (int boardID, /* Addr4882_t */ short addr, byte[] databuf, int datacnt, int eotMode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="Send", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void Send32 (int boardID, /* Addr4882_t */ short addr, short[] databuf, int datacnt, int eotMode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="Send", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void Send32 (int boardID, /* Addr4882_t */ short addr, int[] databuf, int datacnt, int eotMode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="Send", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void Send32 (int boardID, /* Addr4882_t */ short addr, float[] databuf, int datacnt, int eotMode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="Send", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void Send32 (int boardID, /* Addr4882_t */ short addr, double[] databuf, int datacnt, int eotMode);

		[DllImportAttribute("gpib-32.dll", EntryPoint="Send", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void Send32 (int boardID, /* Addr4882_t */ short addr, IntPtr databuf, int datacnt, int eotMode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="Send", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void Send32 (int boardID, /* Addr4882_t */ short addr, string databuf, int datacnt, int eotMode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendCmds", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendCmds32 (int boardID, byte[] buffer, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendCmds", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendCmds32 (int boardID, short[] buffer, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendCmds", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendCmds32 (int boardID, int[] buffer, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendCmds", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendCmds32 (int boardID, float[] buffer, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendCmds", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendCmds32 (int boardID, double[] buffer, int cnt);

		[DllImportAttribute("gpib-32.dll", EntryPoint="SendCmds", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendCmds32 (int boardID, IntPtr buffer, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendCmds", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendCmds32 (int boardID, string buffer, int cnt);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendDataBytes", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendDataBytes32 (int boardID, byte[] buffer, int cnt, int eot_mode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendDataBytes", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendDataBytes32 (int boardID, short[] buffer, int cnt, int eot_mode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendDataBytes", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendDataBytes32 (int boardID, int[] buffer, int cnt, int eot_mode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendDataBytes", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendDataBytes32 (int boardID, float[] buffer, int cnt, int eot_mode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendDataBytes", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendDataBytes32 (int boardID, double[] buffer, int cnt, int eot_mode);

		[DllImportAttribute("gpib-32.dll", EntryPoint="SendDataBytes", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendDataBytes32 (int boardID, IntPtr buffer, int cnt, int eot_mode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendDataBytes", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendDataBytes32 (int boardID, string buffer, int cnt, int eot_mode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendIFC", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendIFC32 (int boardID);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendLLO", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendLLO32 (int boardID);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendList", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendList32 (int boardID, /* Addr4882_t */ short[] addrlist, byte[] databuf, int datacnt, int eotMode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendList", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendList32 (int boardID, /* Addr4882_t */ short[] addrlist, short[] databuf, int datacnt, int eotMode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendList", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendList32 (int boardID, /* Addr4882_t */ short[] addrlist, int[] databuf, int datacnt, int eotMode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendList", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendList32 (int boardID, /* Addr4882_t */ short[] addrlist, float[] databuf, int datacnt, int eotMode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendList", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendList32 (int boardID, /* Addr4882_t */ short[] addrlist, double[] databuf, int datacnt, int eotMode);

		[DllImportAttribute("gpib-32.dll", EntryPoint="SendList", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendList32 (int boardID, /* Addr4882_t */ short[] addrlist, IntPtr databuf, int datacnt, int eotMode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendList", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendList32 (int boardID, /* Addr4882_t */ short[] addrlist, string databuf, int datacnt, int eotMode);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SendSetup", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SendSetup32 (int boardID, /* Addr4882_t */ short[] addrlist);
		[DllImportAttribute("gpib-32.dll", EntryPoint="SetRWLS", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void SetRWLS32 (int boardID, /* Addr4882_t */ short[] addrlist);
		[DllImportAttribute("gpib-32.dll", EntryPoint="TestSRQ", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void TestSRQ32 (int boardID, out short result);
		[DllImportAttribute("gpib-32.dll", EntryPoint="TestSys", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void TestSys32 (int boardID, /* Addr4882_t */ short[] addrlist, short[] results);
		[DllImportAttribute("gpib-32.dll", EntryPoint="Trigger", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void Trigger32 (int boardID, /* Addr4882_t */ short addr);
		[DllImportAttribute("gpib-32.dll", EntryPoint="TriggerList", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void TriggerList32 (int boardID, /* Addr4882_t */ short[] addrlist);
		[DllImportAttribute("gpib-32.dll", EntryPoint="WaitSRQ", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=false)]
		protected static extern void WaitSRQ32 (int boardID, out short result);
		#endregion 

		#region 488.2 Constants
		
		//
		//    Agilent 488 constants 
		//
		/// <summary>
		/// GPIB unlisten command  
		/// </summary>
		public const byte UNL = 0x3f  ; 
		/// <summary>
		/// GPIB untalk command   
		/// </summary>
		public const byte UNT = 0x5f  ; 
		/// <summary>
		/// GPIB go to local   
		/// </summary>
		public const byte GTL = 0x01  ; 
		/// <summary>
		/// GPIB selected device clear   
		/// </summary>
		public const byte SDC = 0x04  ; 
		/// <summary>
		/// GPIB parallel poll configure     
		/// </summary>
		public const byte PPC = 0x05  ; 
		/// <summary>
		/// GPIB group execute trigger  
		/// </summary>
		public const byte GET = 0x08  ; 
		/// <summary>
		///  GPIB take control   
		/// </summary>
		public const byte TCT = 0x09  ;
		/// <summary>
		/// GPIB local lockout   
		/// </summary>
		public const byte LLO = 0x11  ; 
		/// <summary>
		/// GPIB device clear 
		/// </summary>
		public const byte DCL = 0x14  ; 
		/// <summary>
		/// GPIB parallel poll unconfigure 
		/// </summary>
		public const byte PPU = 0x15  ; 
		/// <summary>
		/// GPIB serial poll enable     
		/// </summary>
		public const byte SPE = 0x18  ; 
		/// <summary>
		/// GPIB serial poll disable  
		/// </summary>
		public const byte SPD = 0x19  ; 
		/// <summary>
		/// GPIB parallel poll enable  
		/// </summary>
		public const byte PPE = 0x60  ; 
		/// <summary>
		/// GPIB parallel poll disable   
		/// </summary>
		public const byte PPD = 0x70  ; 


		//GPIB status bits for global variable ibsta and wait mask                

		/// <summary>
		/// Error detected 
		/// </summary>
		public const int ERR = (1<<15) ; 
		/// <summary>
		/// Timeout
		/// </summary>
		public const int TIMO = (1<<14) ; 
		/// <summary>
		/// EOI or EOS detected 
		/// </summary>
		public const int END = (1<<13) ; 
		/// <summary>
		/// SRQ detected by CIC 
		/// </summary>
		public const int SRQI = (1<<12) ; 
		/// <summary>
		/// Device needs service    
		/// </summary>
		public const int RQS = (1<<11) ; 
		/// <summary>
		/// I/O completed   
		/// </summary>
		public const int CMPL = (1<<8)  ; 
		/// <summary>
		/// Local lockout state   
		/// </summary>
		public const int LOK = (1<<7)  ; 
		/// <summary>
		/// Remote state  
		/// </summary>
		public const int REM = (1<<6)  ; 
		/// <summary>
		/// Controller-in-Charge  
		/// </summary>
		public const int CIC = (1<<5)  ; 
		/// <summary>
		/// Attention asserted  
		/// </summary>
		public const int ATN = (1<<4)  ; 
		/// <summary>
		///  Talker active    
		/// </summary>
		public const int TACS = (1<<3)  ; 
		/// <summary>
		/// Listener active  
		/// </summary>
		public const int LACS = (1<<2)  ; 
		/// <summary>
		/// Device trigger state  
		/// </summary>
		public const int DTAS = (1<<1)  ; 
		/// <summary>
		/// Device clear state   
		/// </summary>
		public const int DCAS = (1<<0)  ; 

		// Error codes returned in global variable iberr         

		/// <summary>
		/// System error     
		/// </summary>
		public const int EDVR = 0  ; 
		/// <summary>
		/// Function requires GPIB board to be CIC 
		/// </summary>
		public const int ECIC = 1  ; 
		/// <summary>
		/// Write function detected no Listeners  
		/// </summary>
		public const int ENOL = 2  ; 
		/// <summary>
		/// Interface board not addressed correctly 
		/// </summary>
		public const int EADR = 3  ; 
		/// <summary>
		/// Invalid argument to function call  
		/// </summary>
		public const int EARG = 4  ; 
		/// <summary>
		/// Function requires GPIB board to be SAC  
		/// </summary>
		public const int ESAC = 5  ; 
		/// <summary>
		/// I/O operation aborted    
		/// </summary>
		public const int EABO = 6  ; 
		/// <summary>
		/// Non-existent interface board     
		/// </summary>
		public const int ENEB = 7  ; 
		/// <summary>
		/// Error performing DMA   
		/// </summary>
		public const int EDMA = 8  ; 
		/// <summary>
		///  I/O operation started before previous operation completed  
		/// </summary>
		public const int EOIP = 10  ; 
		
		/// <summary>
		/// No capability for intended operation  
		/// </summary>
		public const int ECAP = 11  ; 
		/// <summary>
		/// File system operation error     
		/// </summary>
		public const int EFSO = 12  ; 
		/// <summary>
		/// Command error during device call   
		/// </summary>
		public const int EBUS = 14  ; 
		/// <summary>
		/// Serial poll status byte lost    
		/// </summary>
		public const int ESTB = 15  ; 
		/// <summary>
		/// SRQ remains asserted   
		/// </summary>
		public const int ESRQ = 16  ; 
		/// <summary>
		/// The return buffer is full 
		/// </summary>
		public const int ETAB = 20  ;
		/// <summary>
		/// Address or board is locked 
		/// </summary>
		public const int ELCK = 21  ; 
		/// <summary>
		/// The ibnotify callback failed to rearm  
		/// </summary>
		public const int EARM = 22  ; 
		/// <summary>
		/// The input handle is invalid    
		/// </summary>
		public const int EHDL = 23  ; 
		/// <summary>
		/// Wait already in progress on input descriptor  
		/// </summary>
		public const int EWIP = 26  ; 
		/// <summary>
		/// The event notification was cancelled due to a reset of the interface 
		/// </summary>
		public const int ERST = 27  ; 

		/// <summary>
		/// The system or board has lost power or gone to standby     
		/// </summary>
		public const int EPWR = 28  ; 

		// Warning messages returned in global variable iberr       

		/// <summary>
		/// Configuration warning  
		/// </summary>
		public const int WCFG = 24  ; 
		public const int ECFG = WCFG;

		// EOS mode bits                                            

		/// <summary>
		/// Eight bit compare       
		/// </summary>
		public const int BIN = (1<<12) ; 
		/// <summary>
		/// Send END with EOS byte     
		/// </summary>
		public const int XEOS = (1<<11) ; 
		/// <summary>
		/// Terminate read on EOS   
		/// </summary>
		public const int REOS = (1<<10) ; 

		// Timeout values and meanings                            

		/// <summary>
		/// Infinite timeout (disabled)  
		/// </summary>
		public const int TNONE = 0   ; 
		/// <summary>
		/// Timeout of 10 us (ideal)   
		/// </summary>
		public const int T10us = 1   ; 
		/// <summary>
		/// Timeout of 30 us (ideal)       
		/// </summary>
		public const int T30us = 2   ; 
		/// <summary>
		/// Timeout of 100 us (ideal)   
		/// </summary>
		public const int T100us = 3   ; 
		/// <summary>
		/// Timeout of 300 us (ideal)    
		/// </summary>
		public const int T300us = 4   ; 
		/// <summary>
		/// Timeout of 1 ms (ideal)  
		/// </summary>
		public const int T1ms = 5   ; 
		/// <summary>
		/// Timeout of 3 ms (ideal)  
		/// </summary>
		public const int T3ms = 6   ; 
		/// <summary>
		/// Timeout of 10 ms (ideal)
		/// </summary>
		public const int T10ms = 7   ; 
		/// <summary>
		/// Timeout of 30 ms (ideal)
		/// </summary>
		public const int T30ms = 8   ; 
		/// <summary>
		/// Timeout of 100 ms (ideal)
		/// </summary>
		public const int T100ms = 9   ;
		/// <summary>
		/// Timeout of 300 ms (ideal)
		/// </summary>
		public const int T300ms = 10   ; 
		/// <summary>
		/// Timeout of 1 s (ideal)
		/// </summary>
		public const int T1s = 11   ; 
		/// <summary>
		/// Timeout of 3 s (ideal)
		/// </summary>
		public const int T3s = 12   ; 
		/// <summary>
		/// Timeout of 10 s (ideal)
		/// </summary>
		public const int T10s = 13   ; 
		/// <summary>
		/// Timeout of 30 s (ideal) 
		/// </summary>
		public const int T30s = 14   ; 
		/// <summary>
		/// Timeout of 100 s (ideal)   
		/// </summary>
		public const int T100s = 15   ; 
		/// <summary>
		/// Timeout of 300 s (ideal)  
		/// </summary>
		public const int T300s = 16   ; 
		/// <summary>
		/// Timeout of 1000 s (ideal)  
		/// </summary>
		public const int T1000s = 17   ; 

		//  IBLN Constants                                          
		/// <summary>
		/// Test no secondary addresses
		/// </summary>
		public const int NO_SAD = 0;
		/// <summary>
		/// Test all secondary addresses on specified primary
		/// </summary>
		public const int ALL_SAD = -1;

		// The following constants are used for the second parameter of the
		//  ibconfig function.  They are the item selection codes.
		//
		/// <summary>
		/// Primary Address   
		/// </summary>
		public const int IbcPAD = 0x0001      ; 
		/// <summary>
		/// Secondary Address       
		/// </summary>
		public const int IbcSAD = 0x0002      ; 
		/// <summary>
		/// Timeout Value  
		/// </summary>
		public const int IbcTMO = 0x0003      ; 
		/// <summary>
		/// Send EOI with last data byte?
		/// </summary>
		public const int IbcEOT = 0x0004      ; 
		/// <summary>
		/// Parallel Poll Configure
		/// </summary>
		public const int IbcPPC = 0x0005      ; 
		/// <summary>
		/// Repeat Addressing 
		/// </summary>
		public const int IbcREADDR = 0x0006      ; 
		/// <summary>
		/// Disable Auto Serial Polling
		/// </summary>
		public const int IbcAUTOPOLL = 0x0007      ; 
		/// <summary>
		/// Use the CIC Protocol? 
		/// </summary>
		public const int IbcCICPROT = 0x0008      ; 
		/// <summary>
		/// Use PIO for I/O  
		/// </summary>
		public const int IbcIRQ = 0x0009      ; 
		/// <summary>
		/// Board is System Controller? 
		/// </summary>
		public const int IbcSC = 0x000A      ; 
		/// <summary>
		/// Assert SRE on device calls?   
		/// </summary>
		public const int IbcSRE = 0x000B      ; 
		/// <summary>
		///  Terminate reads on EOS    
		/// </summary>
		public const int IbcEOSrd = 0x000C      ; 
		/// <summary>
		/// Send EOI with EOS character  
		/// </summary>
		public const int IbcEOSwrt = 0x000D      ; 
		/// <summary>
		/// Use 7 or 8-bit EOS compare: Only 8-bit compare is supported for Agilent interfaces
		/// </summary>
		public const int IbcEOScmp = 0x000E      ; 
		/// <summary>
		/// The EOS character.   
		/// </summary>
		public const int IbcEOSchar = 0x000F      ; 
		/// <summary>
		/// Use Parallel Poll Mode 2   
		/// </summary>
		public const int IbcPP2 = 0x0010      ; 
		/// <summary>
		/// NORMAL, HIGH, or VERY_HIGH timing
		/// </summary>
		public const int IbcTIMING = 0x0011      ; 
		/// <summary>
		/// Use DMA for I/O   
		/// </summary>
		public const int IbcDMA = 0x0012      ; 
		/// <summary>
		/// Swap bytes during an ibrd   
		/// </summary>
		public const int IbcReadAdjust = 0x0013      ; 
		/// <summary>
		///  Swap bytes during an ibwrt 
		/// </summary>
		public const int IbcWriteAdjust = 0x014      ; 
		/// <summary>
		/// Enable/disable the sending of LLO 
		/// </summary>
		public const int IbcSendLLO = 0x0017      ; 
		/// <summary>
		///  Set the timeout value for serial polls
		/// </summary>
		public const int IbcSPollTime = 0x0018      ; 
		/// <summary>
		///  Set the parallel poll length 
		/// </summary>
		public const int IbcPPollTime = 0x0019      ; 
		/// <summary>
		/// Remove EOS from END bit of ibsta 
		/// </summary>
		public const int IbcEndBitIsNormal = 0x001A  ; 
		/// <summary>
		/// Enable/disable device unaddressing 
		/// </summary>
		public const int IbcUnAddr = 0x001B  ;
		/// <summary>
		/// Set UNIX signal number: Unsupported 
		/// </summary>
		public const int IbcSignalNumber = 0x001C  ; 
		/// <summary>
		/// Enable/disable blocking for locked boards
		/// </summary>
		public const int IbcBlockIfLocked = 0x001D  ; 
		/// <summary>
		/// Length of cable specified for high speed timing: Not supported for Agilent interfaces
		/// </summary>
		public const int IbcHSCableLength = 0x001F  ; 
		/// <summary>
		/// Set the IST bit   
		/// </summary>
		public const int IbcIst = 0x0020      ; 
		/// <summary>
		/// Set the RSV byte 
		/// </summary>
		public const int IbcRsv = 0x0021      ; 
		/// <summary>
		/// Enter listen only mode  
		/// </summary>
		public const int IbcLON = 0x0022      ; 

		//
		//    Constants to be used when calling ibask() 
		//

		public const int IbaPAD = IbcPAD;
		public const int IbaSAD = IbcSAD;
		public const int IbaTMO = IbcTMO;
		public const int IbaEOT = IbcEOT;
		public const int IbaPPC = IbcPPC;
		public const int IbaREADDR = IbcREADDR;
		public const int IbaAUTOPOLL = IbcAUTOPOLL;
		public const int IbaCICPROT = IbcCICPROT;
		public const int IbaIRQ = IbcIRQ;
		public const int IbaSC = IbcSC;
		public const int IbaSRE = IbcSRE;
		public const int IbaEOSrd = IbcEOSrd;
		public const int IbaEOSwrt = IbcEOSwrt;
		public const int IbaEOScmp = IbcEOScmp;
		public const int IbaEOSchar = IbcEOSchar;
		public const int IbaPP2 = IbcPP2;
		public const int IbaTIMING = IbcTIMING;
		public const int IbaDMA = IbcDMA;
		public const int IbaReadAdjust = IbcReadAdjust;
		public const int IbaWriteAdjust = IbcWriteAdjust;
		public const int IbaSendLLO = IbcSendLLO;
		public const int IbaSPollTime = IbcSPollTime;
		public const int IbaPPollTime = IbcPPollTime;
		public const int IbaEndBitIsNormal = IbcEndBitIsNormal;
		public const int IbaUnAddr = IbcUnAddr;
		public const int IbaSignalNumber = IbcSignalNumber;
		public const int IbaBlockIfLocked = IbcBlockIfLocked;
		public const int IbaHSCableLength = IbcHSCableLength;
		public const int IbaIst = IbcIst;
		public const int IbaRsv = IbcRsv;
		public const int IbaLON = IbcLON;
		public const int IbaSerialNumber = 0x0023;

		public const int IbaBNA = 0x0200   ; 


		// Values used by the Send command. 
		/// <summary>
		/// Do nothing at the end of a transfer
		/// </summary>
		public const int NULLend = 0x00  ; 
		/// <summary>
		/// Send NL with EOI after a transfer
		/// </summary>
		public const int NLend = 0x01  ; 
		/// <summary>
		/// Send EOI with the last DAB
		/// </summary>
		public const int DABend = 0x02  ; 

		// Value used by the Receive command.
		
		/// <summary>
		/// Stop reading when EOI asserted
		/// </summary>
		public const int STOPend = 0x0100;

		

		/// <summary>
		/// Value used as last entry to terminate an address list 
		/// </summary>

		public const short NOADDR = unchecked((short)0xFFFF);



		// iblines constants 

		public const short ValidEOI = (short)0x0080;
		public const short ValidATN = (short)0x0040;
		public const short ValidSRQ = (short)0x0020;
		public const short ValidREN = (short)0x0010;
		public const short ValidIFC = (short)0x0008;
		public const short ValidNRFD = (short)0x0004;
		public const short ValidNDAC = (short)0x0002;
		public const short ValidDAV = (short)0x0001;
		public const short BusEOI = unchecked((short)0x8000);
		public const short BusATN = (short)0x4000;
		public const short BusSRQ = (short)0x2000;
		public const short BusREN = (short)0x1000;
		public const short BusIFC = (short)0x0800;
		public const short BusNRFD = (short)0x0400;
		public const short BusNDAC = (short)0x0200;
		public const short BusDAV = (short)0x0100;
		#endregion

		#region 488.2 Status Variables
		/// <summary>
		/// Register GPIB globals for use.
		/// </summary>
		public static void RegisterGpibGlobals()
		{
			VerifyGpibGlobalsRegistered();
		}

		/// <summary>
		/// Unregister GPIB globals
		/// </summary>
		public static void UnregisterGPIBGlobals()
		{
			VerifyGpibGlobalsNotRegistered();
		}

		/// <summary>
		/// Get or set 488.2 status word
		/// </summary>
		public static int ibsta 
		{
			get
			{
				VerifyGpibGlobalsRegistered();
				if (m_globalsInitialized)
					return Marshal.ReadInt32(m_ibstaPtr);
				else
					return 0x8000;
			}
			set
			{
				VerifyGpibGlobalsRegistered();
				if (m_globalsInitialized)
					Marshal.WriteInt32(m_ibstaPtr, value);
			}
		}

		/// <summary>
		/// Get or set 488.2 error code: valid if ibsta has ERR bit set.
		/// </summary>
		public static int iberr
		{
			get
			{
				VerifyGpibGlobalsRegistered();
				if (m_globalsInitialized)
					return Marshal.ReadInt32(m_iberrPtr);
				else
					return EDVR;
			}
			set
			{
				VerifyGpibGlobalsRegistered();
				if (m_globalsInitialized)
					Marshal.WriteInt32(m_iberrPtr, value);
			}
		}

		/// <summary>
		/// Get or set 16-bit count from last operation.
		/// </summary>
		public static int ibcnt
		{
			get
			{
				VerifyGpibGlobalsRegistered();
				if (m_globalsInitialized)
					return Marshal.ReadInt32(m_ibcntPtr);
				else
					return unchecked((int)0xDEAD37F0);
			}
			set
			{
				VerifyGpibGlobalsRegistered();
				if (m_globalsInitialized)
					Marshal.WriteInt32(m_ibcntPtr, value);
			}
		}

		/// <summary>
		/// Get or set 32-bit count from last operation
		/// </summary>
		public static int ibcntl
		{
			get
			{
				VerifyGpibGlobalsRegistered();
				if (m_globalsInitialized)
					return Marshal.ReadInt32(m_ibcntlPtr);
				else
					return unchecked((int)0xDEAD37F0);
			}
			set
			{
				VerifyGpibGlobalsRegistered();
				if (m_globalsInitialized)
					Marshal.WriteInt32(m_ibcntlPtr, value);
			}
		}
		#endregion
	
		#region 488.2 Public Methods
		//  Agilent 488 Function Prototypes  

		/// <summary>
		/// Find a device or board descriptor by name
		/// </summary>
		/// <param name="udname">Name to find</param>
		/// <returns>Device or board descriptor</returns>
		public static int ibfind(string udname)
		{
			VerifyGpibGlobalsRegistered();
			return ibfindW32(udname);
		}

		public static int ibbna(int ud, string udname)
		{
			VerifyGpibGlobalsRegistered();
			return ibbnaW32(ud, udname);
		}

		/// <summary>
		/// Transfer data from GPIB to a file
		/// </summary>
		/// <param name="ud">Device or board descriptor of source</param>
		/// <param name="filename">File name to receive data</param>
		/// <returns>Status outcome</returns>
		public static int ibrdf(int ud, string filename)
		{
			VerifyGpibGlobalsRegistered();
			return ibrdfW32(ud, filename);
		}

		/// <summary>
		/// Write data from a file to the GPIB
		/// </summary>
		/// <param name="ud">Device or board descriptor of destination</param>
		/// <param name="filename">File name of the data source</param>
		/// <returns>Status outcome</returns>
		public static int ibwrtf(int ud, string filename)
		{
			VerifyGpibGlobalsRegistered();
			return ibwrtfW32(ud, filename);
		}

		/// <summary>
		/// Return the value of a configuration parameter
		/// </summary>
		/// <param name="ud">Device or board descriptor</param>
		/// <param name="option">Parameter to return (IbaXXX)</param>
		/// <param name="v">Output variable to receive value</param>
		/// <returns>Status outcome</returns>
		public static int ibask(int ud, int option, out int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibask32(ud, option, out v);
		}

		/// <summary>
		/// Make the interface Active Controller
		/// </summary>
		/// <param name="ud">Board descriptor to make active</param>
		/// <param name="v">0 for asynchronous operation, anything else for synchronous</param>
		/// <returns>Status outcome</returns>
		/// <remarks>Call ibsic before ibcac to ensure this board is Controller in Charge</remarks>
		public static int ibcac(int ud, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibcac32(ud, v);
		}

		/// <summary>
		/// Clear a device
		/// </summary>
		/// <param name="ud">Device descriptor to clear</param>
		/// <returns>Status outcome</returns>
		public static int ibclr(int ud)
		{
			VerifyGpibGlobalsRegistered();
			return ibclr32(ud);
		}

		/// <summary>
		/// Send a GPIB command
		/// </summary>
		/// <param name="ud">Board descriptor to send the command</param>
		/// <param name="buf">Buffer to send </param>
		/// <param name="cnt">Number of bytes to send from 'buf'</param>
		/// <returns>Status outcome</returns>
		public static int ibcmd(int ud, byte[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibcmd32(ud, buf, cnt);
		}

		/// <summary>
		/// Send a GPIB command
		/// </summary>
		/// <param name="ud">Board descriptor to send the command</param>
		/// <param name="buf">Buffer to send</param>
		/// <param name="cnt">Number of bytes to send from 'buf'</param>
		/// <returns>Status outcome</returns>
		public static int ibcmd(int ud, short[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibcmd32(ud, buf, cnt);
		}

		/// <summary>
		/// Send a GPIB command
		/// </summary>
		/// <param name="ud">Board descriptor to send the command</param>
		/// <param name="buf">Buffer to send</param>
		/// <param name="cnt">Number of bytes to send from 'buf'</param>
		/// <returns>Status outcome</returns>
		public static int ibcmd(int ud, int[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibcmd32(ud, buf, cnt);
		}

		/// <summary>
		/// Send a GPIB command
		/// </summary>
		/// <param name="ud">Board descriptor to send the command</param>
		/// <param name="buf">Buffer to send</param>
		/// <param name="cnt">Number of bytes to send from 'buf'</param>
		/// <returns>Status outcome</returns>
		public static int ibcmd(int ud, float[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibcmd32(ud, buf, cnt);
		}

		/// <summary>
		/// Send a GPIB command
		/// </summary>
		/// <param name="ud">Board descriptor to send the command</param>
		/// <param name="buf">Buffer to send</param>
		/// <param name="cnt">Number of bytes to send from 'buf'</param>
		/// <returns>Status outcome</returns>
		public static int ibcmd(int ud, double[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibcmd32(ud, buf, cnt);
		}

		/// <summary>
		/// Send a GPIB command
		/// </summary>
		/// <param name="ud">Board descriptor to send the command</param>
		/// <param name="buf">Buffer to send</param>
		/// <param name="cnt">Number of bytes to send from 'buf'</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// The 'buf' parameter is passed directly to .NET's unmanaged interop layer.
		/// Use GCHandle.Alloc to get a pinned pointer to a byte array, and
		/// pass handle.AddrOfPinnedObject() as 'buf'.
		/// </remarks>
		public static int ibcmd(int ud, IntPtr buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibcmd32(ud, buf, cnt);
		}

		/// <summary>
		/// Send a GPIB command
		/// </summary>
		/// <param name="ud">Board descriptor to send the command</param>
		/// <param name="buf">Buffer to send</param>
		/// <param name="cnt">Number of bytes to send from 'buf'</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// 'buf' is treated here as an ASCII string, which .NET converts to an array of bytes.
		/// Consider using a byte array for 'buf' if you want to pass binary data.
		/// </remarks>
		public static int ibcmd(int ud, string buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibcmd32(ud, buf, cnt);
		}

		/// <summary>
		/// Send a GPIB command asyncronously
		/// </summary>
		/// <param name="ud">Board descriptor to send the command</param>
		/// <param name="buf">Buffer to send</param>
		/// <param name="cnt">Number of bytes to send from 'buf'</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// The 'buf' parameter is passed directly to .NET's unmanaged interop layer.
		/// Use GCHandle.Alloc to get a pinned pointer to a byte array, and
		/// pass handle.AddrOfPinnedObject() as 'buf'.
		/// Use 'ibwait' to wait for completion of this operation.
		/// </remarks>		
		public static int ibcmda(int ud, IntPtr /* use a GCHandle */ buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibcmda32(ud, buf, cnt);
		}

		/// <summary>
		/// Set the value of a configuration parameter.
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <param name="option">Parameter to set (IbcXXX)</param>
		/// <param name="v">Value for the parameter</param>
		/// <returns>Status outcome</returns>
		public static int ibconfig(int ud, int option, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibconfig32(ud, option, v);
		}

		/// <summary>
		/// Get a device descriptor
		/// </summary>
		/// <param name="boardID">Board number to which the device is connected</param>
		/// <param name="pad">Primary address of the device</param>
		/// <param name="sad">Secondary address of the device (0 if none)</param>
		/// <param name="tmo">Timeout for the device (Txxx constants: see ibtmo reference in help)</param>
		/// <param name="eot">1 to enable end-of-transmission EOI, 0 to disable EOI</param>
		/// <param name="eos">0 to disable end-of-string termination, nonzero to enable (see ibeos)</param>
		/// <returns>Status outcome</returns>
		public static int ibdev(int boardID, int pad, int sad, int tmo, int eot, int eos)
		{
			VerifyGpibGlobalsRegistered();
			return ibdev32(boardID, pad, sad, tmo, eot, eos);
		}

		/// <summary>
		/// Enable or disable DMA (direct memory access)
		/// </summary>
		/// <param name="ud">Board (interface) descriptor</param>
		/// <param name="v">0 to disable DMA, 1 to enable</param>
		/// <returns>Status outcome</returns>
		public static int ibdma(int ud, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibdma32(ud, v);
		}


		/// <summary>
		/// Enable or disable end-of-string termination
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <param name="v">0 to disable EOS termination.  See Remarks for other values.</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// The 'v' parameter is a 16-bit value which contains a mode setting in the high byte
		/// and a EOS character in the low byte.
		/// Possible high byte values:
		///		0x04	Terminate read on detecting EOS
		///		0x08	Set EOT with EOS on write
		///		0x10	Use 8-bit EOS comparison (the fixed default for Agilent interfaces)
		///	The EOS byte is not send automatically on writes: include it at the end of written data strings. 
		/// </remarks>
		public static int ibeos(int ud, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibeos32(ud, v);
		}

		/// <summary>
		/// Enable or disable assertion of EOI at the end of writes.
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <param name="v">0 to disable EOI, nonzero to enable</param>
		/// <returns>Status outcome</returns>
		public static int ibeot(int ud, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibeot32(ud, v);
		}

		/// <summary>
		/// Make the interface Standby Controller
		/// </summary>
		/// <param name="ud">Board (interface) descriptor</param>
		/// <param name="v">Nonzero to perform acceptor handshaking (ignored by Agilent interfaces)</param>
		/// <returns>Status outcome</returns>
		public static int ibgts(int ud, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibgts32(ud, v);
		}

		/// <summary>
		/// Set or clear ist (individual status) bit
		/// </summary>
		/// <param name="ud">Board (interface) descriptor</param>
		/// <param name="v">Nonzero to set, zero to clear</param>
		/// <returns>Status outcome</returns>
		public static int ibist(int ud, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibist32(ud, v);
		}

		/// <summary>
		/// Acquire or release an interface lock
		/// </summary>
		/// <param name="ud">Board (interface) descriptor</param>
		/// <param name="v">Nonzero to acquire, zero to release</param>
		/// <param name="LockWaitTime">Time in milliseconds to wait for a lock</param>
		/// <param name="Reserved">Must be IntPtr.Zero</param>
		/// <returns>Status outcome</returns>
		public static int iblck(int ud, int v, uint LockWaitTime, IntPtr Reserved)
		{
			VerifyGpibGlobalsRegistered();
			return iblck32(ud, v, LockWaitTime, Reserved);
		}

		/// <summary>
		/// Return the state of the bus management lines
		/// </summary>
		/// <param name="ud">Board descriptor</param>
		/// <param name="result">Bus management line status information (bit per line)</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// Status line bits:
		/// Bit 7	EOI
		/// Bit 6	ATN
		/// Bit 5	SRQ
		/// Bit 4	REN
		/// Bit 3	IFC
		/// Bit 2	NRFD
		/// Bit 1	NDAC
		/// Bit 0	DAV
		/// The low byte (bits 7-0) indicates whether the interface can sense the line
		/// The high byte (bits 15-8) indicates the line status for each line (same bit order)
		/// </remarks>
		public static int iblines(int ud, out short result)
		{
			VerifyGpibGlobalsRegistered();
			return iblines32(ud, out result);
		}

		/// <summary>
		/// Test for listeners
		/// </summary>
		/// <param name="ud">Board (interface) descriptor to check</param>
		/// <param name="pad">Primary device address to test</param>
		/// <param name="sad">Secondary device address to test (or NO_SAD or ALL_SAD)</param>
		/// <param name="listen">Output, set to nonzero if listener present</param>
		/// <returns>Status outcome</returns>
		public static int ibln(int ud, int pad, int sad, out short listen)
		{
			VerifyGpibGlobalsRegistered();
			return ibln32(ud, pad, sad, out listen);
		}

		/// <summary>
		/// Place a device in local mode
		/// </summary>
		/// <param name="ud">Device or board descriptor (Agilent interfaces support only device-level)</param>
		/// <returns>Status outcome</returns>
		public static int ibloc(int ud)
		{
			VerifyGpibGlobalsRegistered();
			return ibloc32(ud);
		}

		/// <summary>
		/// Register a callback function to be called when events occur
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <param name="mask">GPIB event mask: see remarks below</param>
		/// <param name="Callback">Callback function to call on event</param>
		/// <param name="RefData">Reference data for the callback. Must be pinned GCHandle: see remarks</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// GPIB event mask bit constants:
		///		TIMO	(0x4000)
		///		END		(0x2000)
		///		SRQI	(0x1000)
		///		RQS		(0x800)
		///		CMPL	(0x100)
		///		LOK		(0x80)
		///		REM		(0x40)
		///		CIC		(0x20)
		///		ATN		(0x10)
		///		TACS	(0x8)
		///		LACS	(0x4)
		///		DTAS	(0x2)
		///		DCAS	(0x1)
		///	'RefData' must be a pinned IntPtr. Use GCHandle.Alloc to get a pinned pointer,
		///	and pass handle.AddrOfPinnedObject() as 'RefData'.
		/// </remarks>
		public static int ibnotify(int ud, int mask, GpibNotifyCallback_t Callback, IntPtr /* use a GCHandle */ RefData)
		{
			VerifyGpibGlobalsRegistered();
			return ibnotify32(ud, mask, Callback, RefData);
		}

		/// <summary>
		/// Reset an interface or device, or take it offline
		/// </summary>
		/// <param name="ud">Board (interface) or device descriptor</param>
		/// <param name="v">0 to reset device/interface and take offline, 1 to just reset</param>
		/// <returns>Status outcome</returns>
		public static int ibonl(int ud, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibonl32(ud, v);
		}

		/// <summary>
		/// Change the primary GPIB address of a board or device
		/// </summary>
		/// <param name="ud">Board or device descriptor to set</param>
		/// <param name="v">New primary address (0 to 30)</param>
		/// <returns>Status outcome</returns>
		public static int ibpad(int ud, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibpad32(ud, v);
		}

		/// <summary>
		/// Pass Controller in Charge (CIC) status to another GPIB device
		/// </summary>
		/// <param name="ud">Device descriptor to make CIC</param>
		/// <returns>Status outcome</returns>
		public static int ibpct(int ud)
		{
			VerifyGpibGlobalsRegistered();
			return ibpct32(ud);
		}

		/// <summary>
		/// Not recommended for new code.
		/// </summary>
		/// <param name="ud"></param>
		/// <param name="option"></param>
		/// <param name="v"></param>
		/// <returns></returns>
		public static int ibpoke(int ud, int option, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibpoke32(ud, option, v);
		}

		/// <summary>
		/// Configure parallel polling
		/// </summary>
		/// <param name="ud">Board or device descriptor to configure</param>
		/// <param name="v">0 to disable, 96 to 126 to enable</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// 'v' values are from 96 to 126 (0x60 to 0x7E). Bits are encoded as follows:
		/// 
		///		0 1 1 E S D2 D1 D0
		///		
		///	where E=1 disables and E=0 enables parallel polling on this device,
		///	S=1 asserts the data line when ist is 1, S=0 asserts the data line
		///	when ist is 0, and D2 - D0 indicate which of the eight lines to assert. 	 
		/// </remarks>
		public static int ibppc(int ud, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibppc32(ud, v);
		}

		/// <summary>
		/// Read data from the GPIB
		/// </summary>
		/// <param name="ud">Board or device descriptor to read from</param>
		/// <param name="buf">Buffer to read data into</param>
		/// <param name="cnt">Number of bytes to read</param>
		/// <returns>Status outcome</returns>
		public static int ibrd(int ud, byte[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibrd32(ud, buf, cnt);
		}

		/// <summary>
		/// Read data from the GPIB
		/// </summary>
		/// <param name="ud">Board or device descriptor to read from</param>
		/// <param name="buf">Buffer to read data into</param>
		/// <param name="cnt">Number of bytes to read</param>
		/// <returns>Status outcome</returns>
		public static int ibrd(int ud, short[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibrd32(ud, buf, cnt);
		}

		/// <summary>
		/// Read data from the GPIB
		/// </summary>
		/// <param name="ud">Board or device descriptor to read from</param>
		/// <param name="buf">Buffer to read data into</param>
		/// <param name="cnt">Number of bytes to read</param>
		/// <returns>Status outcome</returns>
		public static int ibrd(int ud, int[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibrd32(ud, buf, cnt);
		}

		/// <summary>
		/// Read data from the GPIB
		/// </summary>
		/// <param name="ud">Board or device descriptor to read from</param>
		/// <param name="buf">Buffer to read data into</param>
		/// <param name="cnt">Number of bytes to read</param>
		/// <returns>Status outcome</returns>
		public static int ibrd(int ud, float[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibrd32(ud, buf, cnt);
		}

		/// <summary>
		/// Read data from the GPIB
		/// </summary>
		/// <param name="ud">Board or device descriptor to read from</param>
		/// <param name="buf">Buffer to read data into</param>
		/// <param name="cnt">Number of bytes to read</param>
		/// <returns>Status outcome</returns>
		public static int ibrd(int ud, double[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibrd32(ud, buf, cnt);
		}

		/// <summary>
		/// Read data from the GPIB
		/// </summary>
		/// <param name="ud">Board or device descriptor to read from</param>
		/// <param name="buf">Buffer to read data into (must be a pinned GCHandle pointer to a byte array)</param>
		/// <param name="cnt">Number of bytes to read</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// 'buf' must be a pinned pointer to a byte array.
		/// Use GCHandle.Alloc to get a GCHandle to the byte array,
		/// and pass handle.AddrOfPinnedObject() as the 'buf' parameter.
		/// </remarks>
		public static int ibrd(int ud, IntPtr buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibrd32(ud, buf, cnt);
		}

		/// <summary>
		/// Read data from the GPIB
		/// </summary>
		/// <param name="ud">Board or device descriptor to read from</param>
		/// <param name="response">Buffer to read data into</param>
		/// <param name="cnt">Number of characters to read</param>
		/// <returns>Status outcome</returns>	
		public static int ibrd(int ud, out string response, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			StringBuilder buffer = new StringBuilder(cnt);
			int result = ibrd32(ud, buffer, cnt);
			response = buffer.ToString(0, ibcntl);
			return result;
		}

		/// <summary>
		/// Read data from the GPIB asynchronously
		/// </summary>
		/// <param name="ud">Board or device descriptor to read from</param>
		/// <param name="buf">Buffer to read data into (must be a pinned GCHandle pointer to a byte array)</param>
		/// <param name="cnt">Number of bytes to read</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// 'buf' must be a pinned pointer to a byte array.
		/// Use GCHandle.Alloc to get a GCHandle to the byte array,
		/// and pass handle.AddrOfPinnedObject() as the 'buf' parameter.
		/// </remarks>
		public static int ibrda(int ud, IntPtr /* use a GCHandle */ buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibrda32(ud, buf, cnt);
		}

		/// <summary>
		/// Parallel poll all devices on an interface
		/// </summary>
		/// <param name="ud">Board (interface) or device descriptor</param>
		/// <param name="ppr">Parallel poll assigned device bits</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// The meaning of the 'ppr' bits is determined by ibppc configuration calls.
		/// </remarks>
		public static int ibrpp(int ud, out byte ppr)
		{
			VerifyGpibGlobalsRegistered();
			return ibrpp32(ud, out ppr);
		}

		/// <summary>
		/// Request or release System Controller status
		/// </summary>
		/// <param name="ud">Board (interface) descriptor</param>
		/// <param name="v">Zero to release, nonzero to request</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// Agilent interfaces do not support changing this status.
		/// If no error occurs, iberr contains the prior setting of this state.
		/// </remarks>
		public static int ibrsc(int ud, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibrsc32(ud, v);
		}

		/// <summary>
		/// Serial poll a device
		/// </summary>
		/// <param name="ud">Device descriptor to poll</param>
		/// <param name="spr">Response byte. If 0x40 is set, the device is requesting service</param>
		/// <returns>Status outcome</returns>
		public static int ibrsp(int ud, out byte spr)
		{
			VerifyGpibGlobalsRegistered();
			return ibrsp32(ud, out spr);
		}

		/// <summary>
		/// Request service or change serial poll response byte
		/// </summary>
		/// <param name="ud">Board (interface) descriptor</param>
		/// <param name="v">0x40 set requests service, other values change response byte</param>
		/// <returns>Status outcome</returns>
		public static int ibrsv(int ud, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibrsv32(ud, v);
		}

		/// <summary>
		/// Change the secondary GPIB address
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <param name="v">Zero to disable SAD, 96 to 126 as new SAD value</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// Agilent interfaces don't support secondary addresses for themselves.
		/// Agilent Connection Expert displays device secondary addresses as 0 to 30.
		/// Add 96 to that value to get the equivalent Agilent 488 secondary address.
		/// </remarks>
		public static int ibsad(int ud, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibsad32(ud, v);
		}

		/// <summary>
		/// Make the interface the Controller in Charge
		/// </summary>
		/// <param name="ud">Board (interface) descriptor</param>
		/// <returns>Status outcome</returns>
		public static int ibsic(int ud)
		{
			VerifyGpibGlobalsRegistered();
			return ibsic32(ud);
		}

		/// <summary>
		/// Assert or deassert Remote Enable (GPIB REN line)
		/// </summary>
		/// <param name="ud">Board (interface) descriptor</param>
		/// <param name="v">Zero to deassert, nonzero to assert</param>
		/// <returns>Status outcome</returns>
		public static int ibsre(int ud, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibsre32(ud, v);
		}

		/// <summary>
		/// Stop an asynchronous I/O operation
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <returns>Status outcome</returns>
		public static int ibstop(int ud)
		{
			VerifyGpibGlobalsRegistered();
			return ibstop32(ud);
		}

		/// <summary>
		/// Change the timeout period
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <param name="v">Timeout period (Txxx constant; see IO Libraries Suite help on ibtmo)</param>
		/// <returns>Status outcome</returns>
		public static int ibtmo(int ud, int v)
		{
			VerifyGpibGlobalsRegistered();
			return ibtmo32(ud, v);
		}

		/// <summary>
		/// Trigger a device.
		/// </summary>
		/// <param name="ud">Device descriptor</param>
		/// <returns>Status outcome</returns>
		public static int ibtrg(int ud)
		{
			VerifyGpibGlobalsRegistered();
			return ibtrg32(ud);
		}

		/// <summary>
		/// Wait for an asynchronous I/O operation event
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <param name="mask">Event mask to wait for</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// Event constants for the event mask:
		///		ERR		(0x8000)
		///		TIMO	(0x4000)
		///		END		(0x2000)
		///		SRQI	(0x1000)
		///		RQS		(0x800)
		///		CMPL	(0x100)
		///		LOK		(0x80)
		///		REM		(0x40)
		///		CIC		(0x20)
		///		ATN		(0x10)
		///		TACS	(0x8)
		///		LACS	(0x4)
		///		DTAS	(0x2)
		///		DCAS	(0x1)
		///	If TIMO is not set in the mask, ibwait will wait indefinitely for one of the masked events.
		///	Set TIMO to ensure control returns no later that the ibtmo timeout period.
		///	</remarks>
		public static int ibwait(int ud, int mask)
		{
			VerifyGpibGlobalsRegistered();
			return ibwait32(ud, mask);
		}

		/// <summary>
		/// Write data to the GPIB
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <param name="buf">Buffer to write</param>
		/// <param name="cnt">Number of bytes to write</param>
		/// <returns>Status outcome</returns>
		public static int ibwrt(int ud, byte[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibwrt32(ud, buf, cnt);
		}
		
		/// <summary>
		/// Write data to the GPIB
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <param name="buf">Buffer to write</param>
		/// <param name="cnt">Number of bytes to write</param>
		/// <returns>Status outcome</returns>
		public static int ibwrt(int ud, short[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibwrt32(ud, buf, cnt);
		}

		/// <summary>
		/// Write data to the GPIB
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <param name="buf">Buffer to write</param>
		/// <param name="cnt">Number of bytes to write</param>
		/// <returns>Status outcome</returns>
		public static int ibwrt(int ud, int[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibwrt32(ud, buf, cnt);
		}

		/// <summary>
		/// Write data to the GPIB
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <param name="buf">Buffer to write</param>
		/// <param name="cnt">Number of bytes to write</param>
		/// <returns>Status outcome</returns>
		public static int ibwrt(int ud, float[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibwrt32(ud, buf, cnt);
		}

		/// <summary>
		/// Write data to the GPIB
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <param name="buf">Buffer to write</param>
		/// <param name="cnt">Number of bytes to write</param>
		/// <returns>Status outcome</returns>
		public static int ibwrt(int ud, double[] buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibwrt32(ud, buf, cnt);
		}

		/// <summary>
		/// Write data to the GPIB
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <param name="buf">Buffer to write (must be a pinned GCHandle pointer to a byte array)</param>
		/// <param name="cnt">Number of bytes to write</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// 'buf' must be a pinned pointer to a byte array.
		/// Use GCHandle.Alloc to get a GCHandle to the byte array,
		/// and pass handle.AddrOfPinnedObject() as the 'buf' parameter.
		/// </remarks>
		public static int ibwrt(int ud, IntPtr buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibwrt32(ud, buf, cnt);
		}

		/// <summary>
		/// Write data to the GPIB
		/// </summary>
		/// <param name="ud">Board or device descriptor</param>
		/// <param name="buf">Buffer to write (characters only: see remarks)</param>
		/// <param name="cnt">Number of bytes to write</param>
		/// <returns>Status outcome</returns>
		/// <remarks>
		/// 'buf' is treated as an ASCII string, converted by .NET to an array of bytes.
		/// Pass 'buf' as a byte array to send binary data.
		/// </remarks>
		public static int ibwrt(int ud, string buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibwrt32(ud, buf, cnt);
		}

		/// <summary>
		/// Write data asynchronously from a pinned GCHandle byte array to a device.
		/// </summary>
		/// <param name="ud">device or board descriptor</param>
		/// <param name="buf">GCHandle pinned pointer to byte array</param>
		/// <param name="cnt">bytes to write</param>
		/// <returns>ibsta status</returns>
		/// <remarks>
		/// To use this method, obtain a pinned GCHandle using GCHandle.Alloc for a byte array, then pass handle.AddrOfPinnedObject() as 'buf'.
		/// Use ibwait to wait for completion of this write before attempting further IO.
		/// </remarks>
		public static int ibwrta(int ud, IntPtr /* use a GCHandle */ buf, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			return ibwrta32(ud, buf, cnt);
		}

		// GPIB-ENET only functions to support locking across machines
		// Deprecated - Use iblck

		/// <summary>
		/// Not recommended for new code. Use 'iblck'
		/// </summary>
		/// <param name="ud"></param>
		/// <returns></returns>
		public static int iblock(int ud)
		{
			VerifyGpibGlobalsRegistered();
			return iblock32(ud);
		}

		/// <summary>
		/// Not recommended for new code. Use 'iblck'
		/// </summary>
		/// <param name="ud"></param>
		/// <returns></returns>
		public static int ibunlock(int ud)
		{
			VerifyGpibGlobalsRegistered();
			return ibunlock32(ud);
		}

		/**************************************************************************/
		/*  Functions to access Thread-Specific copies of the GPIB global vars */


		/// <summary>
		/// Get the thread-specific ibsta value
		/// </summary>
		/// <returns>Thread-specific ibsta status</returns>
		/// <remarks>
		/// Use this instead of ibsta in multi-threaded applications
		/// </remarks>
		public static int ThreadIbsta()
		{
			VerifyGpibGlobalsRegistered();
			return ThreadIbsta32();
		}

		/// <summary>
		/// Get the thread-specific iberr value
		/// </summary>
		/// <returns>Thread-specific iberr value</returns>
		/// <remarks>
		/// Use this instead of iberr in multi-threaded applications.
		/// </remarks>
		public static int ThreadIberr()
		{
			VerifyGpibGlobalsRegistered();
			return ThreadIberr32();
		}

		/// <summary>
		/// Get the thread-specific ibcnt value
		/// </summary>
		/// <returns>Thread-specific ibcnt value</returns>
		/// <remarks>
		/// Use this instead of ibcnt in multi-threaded applications
		/// </remarks>
		public static int ThreadIbcnt()
		{
			VerifyGpibGlobalsRegistered();
			return ThreadIbcnt32();
		}

		/// <summary>
		/// Get the thread-specific ibcntl value
		/// </summary>
		/// <returns>Thread-specific ibcntl value</returns>
		/// <remarks>
		/// Use this instead of ibcntl in multi-threaded applications
		/// </remarks>
		public static int ThreadIbcntl()
		{
			VerifyGpibGlobalsRegistered();
			return ThreadIbcntl32();
		}


		/**************************************************************************/
		/*  Agilent 488 Extended Function Prototypes  */


		/// <summary>
		/// Serial poll multiple devices
		/// </summary>
		/// <param name="boardID">Board number to poll</param>
		/// <param name="addrlist">Array of device GPIB addresses to poll, ending with NOADDR</param>
		/// <param name="results">Array of polling results, indexed like 'addrlist'</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void AllSpoll(int boardID, /* Addr4882_t */ short[] addrlist, short[] results)
		{
			VerifyGpibGlobalsRegistered();
			AllSpoll32(boardID, /* Addr4882_t */ addrlist, results);
		}

		/// <summary>
		/// Clear a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address of device</param>
		/// <remarks>
		/// GPIB address = sum of the primary (0-30) and secondary (0 or 96 - 126) addresses
		/// </remarks>
		public static void DevClear(int boardID, /* Addr4882_t */ short addr)
		{
			VerifyGpibGlobalsRegistered();
			DevClear32(boardID, /* Addr4882_t */ addr);
		}

		/// <summary>
		/// Clear multiple devices
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses to clear, terminated with NOADDR</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void DevClearList(int boardID, /* Addr4882_t */ short[] addrlist)
		{
			VerifyGpibGlobalsRegistered();
			DevClearList32(boardID, /* Addr4882_t */ addrlist);
		}

		/// <summary>
		/// Place multiple devices in local mode
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses to make local, terminated with NOADDR</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// If 'addrlist' contains only NOADDR, all devices on the interface are local enabled.
		/// </remarks>
		public static void EnableLocal(int boardID, /* Addr4882_t */ short[] addrlist)
		{
			VerifyGpibGlobalsRegistered();
			EnableLocal32(boardID, /* Addr4882_t */ addrlist);
		}

		/// <summary>
		/// Place multiple devices in remote mode
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses to make remote, terminated by NOADDR</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// If 'addrlist' contains only NOADDR, all devices on the interface are remote enabled.
		/// </remarks>
		public static void EnableRemote(int boardID, /* Addr4882_t */ short[] addrlist)
		{
			VerifyGpibGlobalsRegistered();
			EnableRemote32(boardID, /* Addr4882_t */ addrlist);
		}

		/// <summary>
		/// Find listeners on the GPIB
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of primary addresses to search, terminated with NOADDR</param>
		/// <param name="results">A list of the GPIB addresses of listening devices</param>
		/// <param name="limit">Maximum number of listeners to return (should be the size of the results parameter)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void FindLstn(int boardID, /* Addr4882_t */ short[] addrlist, /* Addr4882_t */ short[] results, int limit)
		{
			VerifyGpibGlobalsRegistered();
			FindLstn32(boardID, /* Addr4882_t */ addrlist, /* Addr4882_t */ results, limit);
		}

		/// <summary>
		/// Find a device requesting service
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses to search, terminated by NOADDR</param>
		/// <param name="dev_stat">Serial poll response byte for the device requesting service</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// 'ibcntl' contains the index of the device requesting service, or if none, the index of the terminating NOADDR.
		/// If no device requests service, 'iberr' contains ETAB.
		/// </remarks>
		public static void FindRQS(int boardID, /* Addr4882_t */ short[] addrlist, short[] dev_stat)
		{
			VerifyGpibGlobalsRegistered();
			FindRQS32(boardID, /* Addr4882_t */ addrlist, dev_stat);
		}

		/// <summary>
		/// Perform a parallel poll
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="result">8-bit poll result, with 1-bit status per device </param>
		public static void PPoll(int boardID, out short result)
		{
			VerifyGpibGlobalsRegistered();
			PPoll32(boardID, out result);
		}

		/// <summary>
		/// Configure a device for parallel poll
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address of device to configure</param>
		/// <param name="dataLine">Line number (1 - 8) assigned to this device</param>
		/// <param name="lineSense">Response sense: 1 to assert, 0 to deassert</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void PPollConfig(int boardID, /* Addr4882_t */ short addr, int dataLine, int lineSense)
		{
			VerifyGpibGlobalsRegistered();
			PPollConfig32(boardID, /* Addr4882_t */ addr, dataLine, lineSense);
		}

		/// <summary>
		/// Unconfigure devices so they don't respond to parallel polls
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses to unconfigure, terminated by NOADDR</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void PPollUnconfig(int boardID, /* Addr4882_t */ short[] addrlist)
		{
			VerifyGpibGlobalsRegistered();
			PPollUnconfig32(boardID, /* Addr4882_t */ addrlist);
		}

		/// <summary>
		/// Pass Controller in Charge status to a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void PassControl(int boardID, /* Addr4882_t */ short addr)
		{
			VerifyGpibGlobalsRegistered();
			PassControl32(boardID, /* Addr4882_t */ addr);
		}

		/// <summary>
		/// Read data from an addressed device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">Buffer to hold data</param>
		/// <param name="cnt">Maximum bytes to read</param>
		/// <param name="Termination">Either a termination character, or STOPend to terminate on EOI</param>
		/// <remarks>
		/// Use 'ReceiveSetup' to address a device before calling this method.
		/// </remarks>
		public static void RcvRespMsg(int boardID, byte[] buffer, int cnt, int Termination)
		{
			VerifyGpibGlobalsRegistered();
			RcvRespMsg32(boardID, buffer, cnt, Termination);
		}

		/// <summary>
		/// Read data from an addressed device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">Buffer to hold data</param>
		/// <param name="cnt">Maximum bytes to read</param>
		/// <param name="Termination">Either a termination character, or STOPend to terminate on EOI</param>
		/// <remarks>
		/// Use 'ReceiveSetup' to address a device before calling this method.
		/// </remarks>
		public static void RcvRespMsg(int boardID, short[] buffer, int cnt, int Termination)
		{
			VerifyGpibGlobalsRegistered();
			RcvRespMsg32(boardID, buffer, cnt, Termination);
		}

		/// <summary>
		/// Read data from an addressed device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">Buffer to hold data</param>
		/// <param name="cnt">Maximum bytes to read</param>
		/// <param name="Termination">Either a termination character, or STOPend to terminate on EOI</param>
		/// <remarks>
		/// Use 'ReceiveSetup' to address a device before calling this method.
		/// </remarks>
		public static void RcvRespMsg(int boardID, int[] buffer, int cnt, int Termination)
		{
			VerifyGpibGlobalsRegistered();
			RcvRespMsg32(boardID, buffer, cnt, Termination);
		}

		/// <summary>
		/// Read data from an addressed device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">Buffer to hold data</param>
		/// <param name="cnt">Maximum bytes to read</param>
		/// <param name="Termination">Either a termination character, or STOPend to terminate on EOI</param>
		/// <remarks>
		/// Use 'ReceiveSetup' to address a device before calling this method.
		/// </remarks>
		public static void RcvRespMsg(int boardID, float[] buffer, int cnt, int Termination)
		{
			VerifyGpibGlobalsRegistered();
			RcvRespMsg32(boardID, buffer, cnt, Termination);
		}

		/// <summary>
		/// Read data from an addressed device.
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">Buffer to hold data</param>
		/// <param name="cnt">Maximum bytes to read</param>
		/// <param name="Termination">Either a termination character, or STOPend to terminate on EOI</param>
		/// <remarks>
		/// Use 'ReceiveSetup' to address a device before calling this method.
		/// </remarks>
		public static void RcvRespMsg(int boardID, double[] buffer, int cnt, int Termination)
		{
			VerifyGpibGlobalsRegistered();
			RcvRespMsg32(boardID, buffer, cnt, Termination);
		}

		/// <summary>
		/// Read data from an addressed device.
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">GCHandle pinned pointer to a byte array to hold data</param>
		/// <param name="cnt">Maximum bytes to read</param>
		/// <param name="Termination">Either a termination character, or STOPend to terminate on EOI</param>
		/// <remarks>
		/// Use 'ReceiveSetup' to address a device before calling this method.
		/// Use GCHandle.Alloc to get a pinned pointer to the byte array, and
		/// pass handle.AddrOfPinnedObject() as 'buffer'
		/// </remarks>
		public static void RcvRespMsg(int boardID, IntPtr buffer, int cnt, int Termination)
		{
			VerifyGpibGlobalsRegistered();
			RcvRespMsg32(boardID, buffer, cnt, Termination);
		}

		/// <summary>
		/// Read data from an addressed device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="response">String to hold data</param>
		/// <param name="cnt">Maximum characters to read</param>
		/// <param name="Termination">Either a termination character, or STOPend to terminate on EOI</param>
		/// <remarks>
		/// Use 'ReceiveSetup' to address a device before calling this method.
		/// </remarks>
		public static void RcvRespMsg(int boardID, out string response, int cnt, int Termination)
		{
			VerifyGpibGlobalsRegistered();
			StringBuilder buffer = new StringBuilder(cnt);
			RcvRespMsg32(boardID, buffer, cnt, Termination);
			response = buffer.ToString(0, ibcntl);
		}

		/// <summary>
		/// Serial poll a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address of device</param>
		/// <param name="result">Serial poll response byte from the device</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void ReadStatusByte(int boardID, /* Addr4882_t */ short addr, out short result)
		{
			VerifyGpibGlobalsRegistered();
			ReadStatusByte32(boardID, /* Addr4882_t */ addr, out result);
		}

		/// <summary>
		/// Receive data from a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <param name="buffer">Buffer to receive data</param>
		/// <param name="cnt">Maximum number of bytes to receive</param>
		/// <param name="Termination">Termination character to stop after, or STOPend to stop on EOI</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void Receive(int boardID, /* Addr4882_t */ short addr, byte[] buffer, int cnt, int Termination)
		{
			VerifyGpibGlobalsRegistered();
			Receive32(boardID, /* Addr4882_t */ addr, buffer, cnt, Termination);
		}

		/// <summary>
		/// Receive data from a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <param name="buffer">Buffer to receive data</param>
		/// <param name="cnt">Maximum number of bytes to receive</param>
		/// <param name="Termination">Termination character to stop after, or STOPend to stop on EOI</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void Receive(int boardID, /* Addr4882_t */ short addr, short[] buffer, int cnt, int Termination)
		{
			VerifyGpibGlobalsRegistered();
			Receive32(boardID, /* Addr4882_t */ addr, buffer, cnt, Termination);
		}

		/// <summary>
		/// Receive data from a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <param name="buffer">Buffer to receive data</param>
		/// <param name="cnt">Maximum number of bytes to receive</param>
		/// <param name="Termination">Termination character to stop after, or STOPend to stop on EOI</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void Receive(int boardID, /* Addr4882_t */ short addr, int[] buffer, int cnt, int Termination)
		{
			VerifyGpibGlobalsRegistered();
			Receive32(boardID, /* Addr4882_t */ addr, buffer, cnt, Termination);
		}

		/// <summary>
		/// Receive data from a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <param name="buffer">Buffer to receive data</param>
		/// <param name="cnt">Maximum number of bytes to receive</param>
		/// <param name="Termination">Termination character to stop after, or STOPend to stop on EOI</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void Receive(int boardID, /* Addr4882_t */ short addr, float[] buffer, int cnt, int Termination)
		{
			VerifyGpibGlobalsRegistered();
			Receive32(boardID, /* Addr4882_t */ addr, buffer, cnt, Termination);
		}

		/// <summary>
		/// Receive data from a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <param name="buffer">Buffer to receive data</param>
		/// <param name="cnt">Maximum number of bytes to receive</param>
		/// <param name="Termination">Termination character to stop after, or STOPend to stop on EOI</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void Receive(int boardID, /* Addr4882_t */ short addr, double[] buffer, int cnt, int Termination)
		{
			VerifyGpibGlobalsRegistered();
			Receive32(boardID, /* Addr4882_t */ addr, buffer, cnt, Termination);
		}

		/// <summary>
		/// Receive data from a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <param name="response">String to receive data</param>
		/// <param name="cnt">Maximum number of characters to receive</param>
		/// <param name="Termination">Termination character to stop after, or STOPend to stop on EOI</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void Receive(int boardID, /* Addr4882_t */ short addr, out string response, int cnt, int Termination)
		{
			VerifyGpibGlobalsRegistered();
			StringBuilder buffer = new StringBuilder(cnt);
			Receive32(boardID, /* Addr4882_t */ addr, buffer, cnt, Termination);
			response = buffer.ToString(0, ibcntl);
		}

		/// <summary>
		/// Address a device to receive data
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// Use this method to address a device before using RcvRespMsg to receive the data
		/// </remarks>
		public static void ReceiveSetup(int boardID, /* Addr4882_t */ short addr)
		{
			VerifyGpibGlobalsRegistered();
			ReceiveSetup32(boardID, /* Addr4882_t */ addr);
		}

		/// <summary>
		/// Reset the GPIB system
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses to reset, terminated by NOADDR</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void ResetSys(int boardID, /* Addr4882_t */ short[] addrlist)
		{
			VerifyGpibGlobalsRegistered();
			ResetSys32(boardID, /* Addr4882_t */ addrlist);
		}

		/// <summary>
		/// Send data to a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <param name="databuf">Data to send</param>
		/// <param name="datacnt">Number of bytes to send</param>
		/// <param name="eotMode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void Send(int boardID, /* Addr4882_t */ short addr, byte[] databuf, int datacnt, int eotMode)
		{
			VerifyGpibGlobalsRegistered();
			Send32(boardID, /* Addr4882_t */ addr, databuf, datacnt, eotMode);
		}

		/// <summary>
		/// Send data to a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <param name="databuf">Data to send</param>
		/// <param name="datacnt">Number of bytes to send</param>
		/// <param name="eotMode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void Send(int boardID, /* Addr4882_t */ short addr, short[] databuf, int datacnt, int eotMode)
		{
			VerifyGpibGlobalsRegistered();
			Send32(boardID, /* Addr4882_t */ addr, databuf, datacnt, eotMode);
		}

		/// <summary>
		/// Send data to a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <param name="databuf">Data to send</param>
		/// <param name="datacnt">Number of bytes to send</param>
		/// <param name="eotMode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void Send(int boardID, /* Addr4882_t */ short addr, int[] databuf, int datacnt, int eotMode)
		{
			VerifyGpibGlobalsRegistered();
			Send32(boardID, /* Addr4882_t */ addr, databuf, datacnt, eotMode);
		}

		/// <summary>
		/// Send data to a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <param name="databuf">Data to send</param>
		/// <param name="datacnt">Number of bytes to send</param>
		/// <param name="eotMode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void Send(int boardID, /* Addr4882_t */ short addr, float[] databuf, int datacnt, int eotMode)
		{
			VerifyGpibGlobalsRegistered();
			Send32(boardID, /* Addr4882_t */ addr, databuf, datacnt, eotMode);
		}

		/// <summary>
		/// Send data to a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <param name="databuf">Data to send</param>
		/// <param name="datacnt">Number of bytes to send</param>
		/// <param name="eotMode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void Send(int boardID, /* Addr4882_t */ short addr, double[] databuf, int datacnt, int eotMode)
		{
			VerifyGpibGlobalsRegistered();
			Send32(boardID, /* Addr4882_t */ addr, databuf, datacnt, eotMode);
		}
		
		/// <summary>
		/// Send data to a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <param name="databuf">GCHandle pinned pointer to byte array with data to send</param>
		/// <param name="datacnt">Number of bytes to send</param>
		/// <param name="eotMode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// Use GCHandle.Alloc to get a pinned pointer to the byte array, and
		/// pass handle.AddrOfPinnedObject() as 'databuf'
		/// </remarks>
		public static void Send(int boardID, /* Addr4882_t */ short addr, IntPtr databuf, int datacnt, int eotMode)
		{
			VerifyGpibGlobalsRegistered();
			Send32(boardID, /* Addr4882_t */ addr, databuf, datacnt, eotMode);
		}

		/// <summary>
		/// Send data to a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <param name="databuf">Data to send</param>
		/// <param name="datacnt">Number of bytes to send</param>
		/// <param name="eotMode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// Send only character data with this method.  Send binary data using a byte array.
		/// </remarks>
		public static void Send(int boardID, /* Addr4882_t */ short addr, string databuf, int datacnt, int eotMode)
		{
			VerifyGpibGlobalsRegistered();
			Send32(boardID, /* Addr4882_t */ addr, databuf, datacnt, eotMode);
		}

		/// <summary>
		/// Send GPIB commands (interface messages) on the interface
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">Commands to send</param>
		/// <param name="cnt">Count of bytes to send</param>
		/// <remarks>
		/// Interface messages are one byte or character commands.  
		/// See online help for possible command constants and character command meanings.
		/// </remarks>
		public static void SendCmds(int boardID, byte[] buffer, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			SendCmds32(boardID, buffer, cnt);
		}

		/// <summary>
		/// Not recommended for new code.
		/// </summary>
		/// <param name="boardID"></param>
		/// <param name="buffer"></param>
		/// <param name="cnt"></param>
		public static void SendCmds(int boardID, short[] buffer, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			SendCmds32(boardID, buffer, cnt);
		}

		/// <summary>
		/// Not recommended for new code.
		/// </summary>
		/// <param name="boardID"></param>
		/// <param name="buffer"></param>
		/// <param name="cnt"></param>
		public static void SendCmds(int boardID, int[] buffer, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			SendCmds32(boardID, buffer, cnt);
		}

		/// <summary>
		/// Not recommended for new code.
		/// </summary>
		/// <param name="boardID"></param>
		/// <param name="buffer"></param>
		/// <param name="cnt"></param>
		public static void SendCmds(int boardID, float[] buffer, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			SendCmds32(boardID, buffer, cnt);
		}

		/// <summary>
		/// Not recommended for new code
		/// </summary>
		/// <param name="boardID"></param>
		/// <param name="buffer"></param>
		/// <param name="cnt"></param>
		public static void SendCmds(int boardID, double[] buffer, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			SendCmds32(boardID, buffer, cnt);
		}
		
		/* Note for Function void SendCmds(int boardID, IntPtr buffer, int cnt)
			This Method's "buffer" parameter is passed directly to .NET's unmanaged 
			interoperability layer.  You are responsible for initializing "buffer" 
			as a valid pinned pointer to managed memory when you use this 
			overload of the method. */
		/// <summary>
		/// Not recommended for new code
		/// </summary>
		/// <param name="boardID"></param>
		/// <param name="buffer"></param>
		/// <param name="cnt"></param>
		public static void SendCmds(int boardID, IntPtr buffer, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			SendCmds32(boardID, buffer, cnt);
		}

		/* Note for Function void SendCmds(int boardID, string buffer, int cnt)
			This Method's "buffer" parameter is treated as an ASCII string that 
			is converted by .NET into an array of bytes.  Consider using 
			a different version of this method for passing binary data. */
		/// <summary>
		/// Send GPIB commands (interface messages) on the interface
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">Commands to send</param>
		/// <param name="cnt">Count of characters to send</param>
		/// <remarks>
		/// Interface messages are one-byte or character commands.  
		/// See online help for possible command constants and ASCII character command meanings.
		/// </remarks>
		public static void SendCmds(int boardID, string buffer, int cnt)
		{
			VerifyGpibGlobalsRegistered();
			SendCmds32(boardID, buffer, cnt);
		}

		/// <summary>
		/// Send data over GPIB
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">Data to send</param>
		/// <param name="cnt">Number of bytes to send</param>
		/// <param name="eot_mode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// Use 'SendSetup' before this method to address the data recipient device.
		/// </remarks>
		public static void SendDataBytes(int boardID, byte[] buffer, int cnt, int eot_mode)
		{
			VerifyGpibGlobalsRegistered();
			SendDataBytes32(boardID, buffer, cnt, eot_mode);
		}

		/// <summary>
		/// Send data over GPIB
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">Data to send</param>
		/// <param name="cnt">Number of bytes to send</param>
		/// <param name="eot_mode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// Use 'SendSetup' before this method to address the data recipient device.
		/// </remarks>
		public static void SendDataBytes(int boardID, short[] buffer, int cnt, int eot_mode)
		{
			VerifyGpibGlobalsRegistered();
			SendDataBytes32(boardID, buffer, cnt, eot_mode);
		}

		/// <summary>
		/// Send data over GPIB
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">Data to send</param>
		/// <param name="cnt">Number of bytes to send</param>
		/// <param name="eot_mode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// Use 'SendSetup' before this method to address the data recipient device.
		/// </remarks>
		public static void SendDataBytes(int boardID, int[] buffer, int cnt, int eot_mode)
		{
			VerifyGpibGlobalsRegistered();
			SendDataBytes32(boardID, buffer, cnt, eot_mode);
		}

		/// <summary>
		/// Send data over GPIB
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">Data to send</param>
		/// <param name="cnt">Number of bytes to send</param>
		/// <param name="eot_mode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// Use 'SendSetup' before this method to address the data recipient device.
		/// </remarks>
		public static void SendDataBytes(int boardID, float[] buffer, int cnt, int eot_mode)
		{
			VerifyGpibGlobalsRegistered();
			SendDataBytes32(boardID, buffer, cnt, eot_mode);
		}

		/// <summary>
		/// Send data over GPIB
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">Data to send</param>
		/// <param name="cnt">Number of bytes to send</param>
		/// <param name="eot_mode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// Use 'SendSetup' before this method to address the data recipient device.
		/// </remarks>
		public static void SendDataBytes(int boardID, double[] buffer, int cnt, int eot_mode)
		{
			VerifyGpibGlobalsRegistered();
			SendDataBytes32(boardID, buffer, cnt, eot_mode);
		}

		/// <summary>
		/// Send data over GPIB
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">GCHandle pinned pointer to a byte array of data to send</param>
		/// <param name="cnt">Number of bytes to send</param>
		/// <param name="eot_mode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// Use 'SendSetup' before this method to address the data recipient device.
		/// Use GCHandle.Alloc to get a pinned pointer to a byte array, and
		/// pass handle.AddrOfPinnedObject() as 'buffer'.
		/// </remarks>
		public static void SendDataBytes(int boardID, IntPtr buffer, int cnt, int eot_mode)
		{
			VerifyGpibGlobalsRegistered();
			SendDataBytes32(boardID, buffer, cnt, eot_mode);
		}

		/// <summary>
		/// Send character data over GPIB
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="buffer">Data to send</param>
		/// <param name="cnt">Number of characters to send</param>
		/// <param name="eot_mode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// Use 'SendSetup' before this method to address the data recipient device.
		/// Use this form for character data, use a byte array for binary data.
		/// </remarks>
		public static void SendDataBytes(int boardID, string buffer, int cnt, int eot_mode)
		{
			VerifyGpibGlobalsRegistered();
			SendDataBytes32(boardID, buffer, cnt, eot_mode);
		}

		/// <summary>
		/// Clear the interface
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		public static void SendIFC(int boardID)
		{
			VerifyGpibGlobalsRegistered();
			SendIFC32(boardID);
		}

		/// <summary>
		/// Send Local Lockout to all devices on the interface
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		public static void SendLLO(int boardID)
		{
			VerifyGpibGlobalsRegistered();
			SendLLO32(boardID);
		}

		/// <summary>
		/// Send data to a list of devices
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses, terminated by NOADDR</param>
		/// <param name="databuf">Data to send</param>
		/// <param name="datacnt">Number of bytes to send</param>
		/// <param name="eotMode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void SendList(int boardID, /* Addr4882_t */ short[] addrlist, byte[] databuf, int datacnt, int eotMode)
		{
			VerifyGpibGlobalsRegistered();
			SendList32(boardID, /* Addr4882_t */ addrlist, databuf, datacnt, eotMode);
		}

		/// <summary>
		/// Send data to a list of devices
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses, terminated by NOADDR</param>
		/// <param name="databuf">Data to send</param>
		/// <param name="datacnt">Number of bytes to send</param>
		/// <param name="eotMode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void SendList(int boardID, /* Addr4882_t */ short[] addrlist, short[] databuf, int datacnt, int eotMode)
		{
			VerifyGpibGlobalsRegistered();
			SendList32(boardID, /* Addr4882_t */ addrlist, databuf, datacnt, eotMode);
		}

		/// <summary>
		/// Send data to a list of devices
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses, terminated by NOADDR</param>
		/// <param name="databuf">Data to send</param>
		/// <param name="datacnt">Number of bytes to send</param>
		/// <param name="eotMode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void SendList(int boardID, /* Addr4882_t */ short[] addrlist, int[] databuf, int datacnt, int eotMode)
		{
			VerifyGpibGlobalsRegistered();
			SendList32(boardID, /* Addr4882_t */ addrlist, databuf, datacnt, eotMode);
		}

		/// <summary>
		/// Send data to a list of devices
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses, terminated by NOADDR</param>
		/// <param name="databuf">Data to send</param>
		/// <param name="datacnt">Number of bytes to send</param>
		/// <param name="eotMode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void SendList(int boardID, /* Addr4882_t */ short[] addrlist, float[] databuf, int datacnt, int eotMode)
		{
			VerifyGpibGlobalsRegistered();
			SendList32(boardID, /* Addr4882_t */ addrlist, databuf, datacnt, eotMode);
		}

		/// <summary>
		/// Send data to a list of devices
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses, terminated by NOADDR</param>
		/// <param name="databuf">Data to send</param>
		/// <param name="datacnt">Number of bytes to send</param>
		/// <param name="eotMode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void SendList(int boardID, /* Addr4882_t */ short[] addrlist, double[] databuf, int datacnt, int eotMode)
		{
			VerifyGpibGlobalsRegistered();
			SendList32(boardID, /* Addr4882_t */ addrlist, databuf, datacnt, eotMode);
		}

		/// <summary>
		/// Send data to a list of devices
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses, terminated by NOADDR</param>
		/// <param name="databuf">GCHandle pinned pointer to byte array with data to send</param>
		/// <param name="datacnt">Number of bytes to send</param>
		/// <param name="eotMode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// Use GCHandle.Alloc to get a pinned pointer to a byte array, and
		/// pass handle.AddrOfPinnedObject() as 'databuf'.
		/// </remarks>
		public static void SendList(int boardID, /* Addr4882_t */ short[] addrlist, IntPtr databuf, int datacnt, int eotMode)
		{
			VerifyGpibGlobalsRegistered();
			SendList32(boardID, /* Addr4882_t */ addrlist, databuf, datacnt, eotMode);
		}

		/// <summary>
		/// Send data to a list of devices
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses, terminated by NOADDR</param>
		/// <param name="databuf">Data to send</param>
		/// <param name="datacnt">Number of characters to send</param>
		/// <param name="eotMode">End indicator: DABend (EOI), NLend (newline+EOI), or NULLend (none)</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// Use this for character data.  Use a byte array for binary data.
		/// </remarks>
		public static void SendList(int boardID, /* Addr4882_t */ short[] addrlist, string databuf, int datacnt, int eotMode)
		{
			VerifyGpibGlobalsRegistered();
			SendList32(boardID, /* Addr4882_t */ addrlist, databuf, datacnt, eotMode);
		}

		/// <summary>
		/// Address a device for send operations
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">GPIB address</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// Use this method a device before using 'SendCmds' or 'SendDataBytes'
		/// </remarks>
		public static void SendSetup(int boardID, /* Addr4882_t */ short[] addrlist)
		{
			VerifyGpibGlobalsRegistered();
			SendSetup32(boardID, /* Addr4882_t */ addrlist);
		}

		/// <summary>
		/// Put devices in remote state with local lockout and address them as Listeners
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses, terminated by NOADDR</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void SetRWLS(int boardID, /* Addr4882_t */ short[] addrlist)
		{
			VerifyGpibGlobalsRegistered();
			SetRWLS32(boardID, /* Addr4882_t */ addrlist);
		}

		/// <summary>
		/// Return the state of the SRQ line
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="result">1 if SRQ asserted, 0 if deasserted</param>
		public static void TestSRQ(int boardID, out short result)
		{
			VerifyGpibGlobalsRegistered();
			TestSRQ32(boardID, out result);
		}

		/// <summary>
		/// Send self-test message to listed devices and return self-test results
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses, terminated by NOADDR</param>
		/// <param name="results">Array of self-test results: usually 0 means no error</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void TestSys(int boardID, /* Addr4882_t */ short[] addrlist, short[] results)
		{
			VerifyGpibGlobalsRegistered();
			TestSys32(boardID, /* Addr4882_t */ addrlist, results);
		}

		/// <summary>
		/// Trigger a device
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addr">GPIB address</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void Trigger(int boardID, /* Addr4882_t */ short addr)
		{
			VerifyGpibGlobalsRegistered();
			Trigger32(boardID, /* Addr4882_t */ addr);
		}

		/// <summary>
		/// Trigger a list of devices
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="addrlist">Array of GPIB addresses, terminated by NOADDR</param>
		/// <remarks>
		/// GPIB address has a low byte primary address (0-30) and a high byte secondary address (0 or 96 - 126)
		/// </remarks>
		public static void TriggerList(int boardID, /* Addr4882_t */ short[] addrlist)
		{
			VerifyGpibGlobalsRegistered();
			TriggerList32(boardID, /* Addr4882_t */ addrlist);
		}

		/// <summary>
		/// Wait for SRQ to be asserted
		/// </summary>
		/// <param name="boardID">Interface board number</param>
		/// <param name="result">1 if SRQ asserted before this method returns, 0 otherwise</param>
		public static void WaitSRQ(int boardID, out short result)
		{
			VerifyGpibGlobalsRegistered();
			WaitSRQ32(boardID, out result);
		}
		#endregion

	}
}
