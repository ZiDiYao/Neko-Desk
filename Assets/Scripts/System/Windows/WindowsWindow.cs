#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

using System;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Windows 窗口相关底层工具:
/// - 获取当前活跃窗口（通常是 Unity）句柄
/// - 获取某个窗口的矩形（虚拟桌面坐标 + 物理像素）
/// </summary>
public static class WindowsWindow
{
    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    /// <summary>
    /// 获取当前活跃窗口句柄（一般就是 Unity 的窗口）。
    /// </summary>
    public static IntPtr GetActiveWindowHandle()
    {
        return GetActiveWindow();
    }

    /// <summary>
    /// 获取当前前台窗口句柄（跟 ActiveWindow 略有区别，
    /// 但在大多数情况下也会是 Unity 窗口）。
    /// </summary>
    public static IntPtr GetForegroundWindowHandle()
    {
        return GetForegroundWindow();
    }

    /// <summary>
    /// 尝试获取某个窗口在虚拟桌面坐标中的矩形（物理像素）。
    /// </summary>
    public static bool TryGetWindowRectPx(IntPtr hWnd, out RectInt rect)
    {
        rect = default;

        if (hWnd == IntPtr.Zero)
            return false;

        if (!GetWindowRect(hWnd, out RECT r))
            return false;

        int width = r.Right - r.Left;
        int height = r.Bottom - r.Top;

        rect = new RectInt(r.Left, r.Top, width, height);
        return true;
    }

    /// <summary>
    /// 尝试获取“Unity 窗口”的矩形（物理像素）。
    /// 注意：这里假设当前 ActiveWindow 即 Unity。
    /// </summary>
    public static bool TryGetUnityWindowRectPx(out RectInt rect)
    {
        IntPtr hwnd = GetActiveWindowHandle();
        return TryGetWindowRectPx(hwnd, out rect);
    }
}

#endif
