#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Windows 多显示器相关底层工具：
/// - 枚举所有监视器
/// - 获取每个监视器的物理区域/工作区域/是否主屏
/// </summary>
public static class WindowsMonitors
{
    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct MONITORINFOEX
    {
        public int cbSize;
        public RECT rcMonitor;    // 整个显示器区域（物理像素）
        public RECT rcWork;       // 工作区域（任务栏除外）
        public uint dwFlags;      // 1 = 主显示器

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szDevice;
    }

    private const uint MONITORINFOF_PRIMARY = 0x00000001;

    private delegate bool MonitorEnumProc(
        IntPtr hMonitor,
        IntPtr hdc,
        ref RECT lprcMonitor,
        IntPtr dwData);

    [DllImport("user32.dll")]
    private static extern bool EnumDisplayMonitors(
        IntPtr hdc,
        IntPtr lprcClip,
        MonitorEnumProc lpfnEnum,
        IntPtr dwData);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool GetMonitorInfo(
        IntPtr hMonitor,
        ref MONITORINFOEX lpmi);

    /// <summary>
    /// C# 侧用的封装结构体。
    /// </summary>
    public struct MonitorInfo
    {
        public RectInt Bounds;   // 显示器物理区域（虚拟桌面坐标）
        public RectInt WorkArea; // 工作区域（通常扣掉任务栏）
        public bool IsPrimary;
        public string DeviceName;

        public override string ToString()
        {
            return $"{DeviceName} Bounds={Bounds} Work={WorkArea} Primary={IsPrimary}";
        }
    }

    /// <summary>
    /// 获取所有显示器的信息列表。
    /// </summary>
    public static List<MonitorInfo> GetAllMonitors()
    {
        var result = new List<MonitorInfo>();

        // 回调函数，每枚举到一个显示器调用一次
        bool Callback(IntPtr hMonitor, IntPtr hdc, ref RECT lprcMonitor, IntPtr dwData)
        {
            var info = new MONITORINFOEX();
            info.cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));

            if (!GetMonitorInfo(hMonitor, ref info))
                return true; // 继续下一块

            RectInt bounds = new RectInt(
                info.rcMonitor.Left,
                info.rcMonitor.Top,
                info.rcMonitor.Right - info.rcMonitor.Left,
                info.rcMonitor.Bottom - info.rcMonitor.Top
            );

            RectInt work = new RectInt(
                info.rcWork.Left,
                info.rcWork.Top,
                info.rcWork.Right - info.rcWork.Left,
                info.rcWork.Bottom - info.rcWork.Top
            );

            bool isPrimary = (info.dwFlags & MONITORINFOF_PRIMARY) != 0;

            result.Add(new MonitorInfo
            {
                Bounds = bounds,
                WorkArea = work,
                IsPrimary = isPrimary,
                DeviceName = info.szDevice
            });

            return true; // 返回 true 表示继续枚举
        }

        EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, Callback, IntPtr.Zero);

        return result;
    }

    /// <summary>
    /// 尝试获取主显示器信息。
    /// </summary>
    public static bool TryGetPrimaryMonitor(out MonitorInfo monitor)
    {
        var all = GetAllMonitors();
        foreach (var m in all)
        {
            if (m.IsPrimary)
            {
                monitor = m;
                return true;
            }
        }

        monitor = default;
        return false;
    }
}

#endif
