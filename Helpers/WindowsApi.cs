using System.Runtime.InteropServices;

namespace RMPortal.WebServer.Helpers
{
    public class WindowsApi
    {
        [DllImport("Wtsapi32.dll")]
        protected static extern bool WTSQuerySessionInformation(IntPtr hServer, int sessionId, WTSInfoClass wtsInfoClass, out IntPtr ppBuffer, out uint pBytesReturned);
        [DllImport("Wtsapi32.dll")]
        protected static extern void WTSFreeMemory(IntPtr pointer);
   
        public static string GetCurrentUser()
        {
            IntPtr buffer;
            uint strLen;
            int cur_session = -1;
            var username = "SYSTEM";
            if(WTSQuerySessionInformation(IntPtr.Zero,cur_session,WTSInfoClass.WTSUserName,out buffer,out strLen) && strLen > 1)
            {
                username=Marshal.PtrToStringAnsi(buffer);
                WTSFreeMemory(buffer);
                //if(WTSQuerySessionInformation(IntPtr.Zero,cur_session,WTSInfoClass.WTSDomainName,out buffer,out strLen) && strLen > 1)
                //{
                //    username=Marshal.PtrToStringAnsi(buffer)+"\\"+username;//prepend domain name
                //    WTSFreeMemory(buffer);
                //}
              
            }
            return username;
        }
    }
    public enum WTSInfoClass
    {
        WTSInitialProgram,
        WTSApplicationName,
        WTSWorkingDirectory,
        WTSOEMId,
        WTSSessionId,
        WTSUserName,
        WTSWinStationName,
        WTSDomainName,
        WTSConnectState,
        WTSClientBuildNumber,
        WTSClientName,
        WTSClientDirectory,
        WTSClientProductId,
        WTSClientHardwareId,
        WTSClientAddress,
        WTSClientDisplay,
        WTSClientProtocolType,
        WTSIdleTime,
        WTSLogonTime,
        WTSIncomingBytes,
        WTSOutgoingBytes,
        WTSIncomingFrames,
        WTSOutgoingFrames,
        WTSClientInfo,
        WTSSessionInfo
    }
}
