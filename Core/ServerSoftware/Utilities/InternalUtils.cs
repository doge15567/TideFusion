using System;
using System.Runtime.InteropServices;

using Mono.Nat;
using ServerSoftware;

namespace ServerSoftware.Utilities

{
    public class InternalUtils
    {
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GetConsoleWindow();

        private static Mono.Nat.INatDevice upnpDevice;
        private static Mono.Nat.Mapping mapping;

        //OS Check before removing the X button, ends up breaking Linux if we dont :P
        public static void OSCheck()
        {
            if (IsWindows())
            {
                DeleteMenu(GetSystemMenu(GetConsoleWindow(), false),SC_CLOSE, MF_BYCOMMAND);
            }
        }

        public static bool IsWindows()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
        }

        public static void OpenUPnPPort(int port = 7777)
        {
            mapping = new(Protocol.Udp, port, port);

            NatUtility.DeviceFound += OnDeviceFound;
            Mono.Nat.NatUtility.StartDiscovery();
        }

        public static void ClosePort(int port = 7777)
        {
            if (upnpDevice != null)
            {
                try
                {
                    upnpDevice.DeletePortMap(mapping);
                }
                catch (Exception ex)
                {
                    ServerClass.UpdateWindow($"Failed to delete port mapping with error: {ex}");
                }
            }
            else
            {
                ServerClass.UpdateWindow("Can't delete port mapping as the device is null!");
            }
        }

        private static void OnDeviceFound(object sender, DeviceEventArgs device)
        {
            NatUtility.StopDiscovery();
            try
            {
                ServerClass.hasPortForwarded = true;
                device.Device.CreatePortMap(mapping);
                upnpDevice = device.Device;
            }
            catch (Exception ex)
            {
                ServerClass.UpdateWindow($"Failed creating port map with exception: {ex}");
            }
        }
    }
}
