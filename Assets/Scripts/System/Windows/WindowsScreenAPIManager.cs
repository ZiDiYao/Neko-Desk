#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

using System;
using UnityEngine;

/// <summary>
/// 对外的统一入口，封装常用 Windows 屏幕/窗口/鼠标信息。
/// 上层代码可以只依赖这个类，而不用关心具体 Win32 细节。
/// </summary>
public static class WindowsScreenAPIManager
{
    /// <summary>DPI 数值，例如 96 / 120 / 144 / 192 等。</summary>
    public static uint Dpi => WindowsDPI.GetDPI();

    /// <summary>DPI 缩放比例，例如 1.0 / 1.25 / 1.5 / 2.0 等。</summary>
    public static float DpiScale => WindowsDPI.GetDPIScale();

    /// <summary>主屏幕分辨率（物理像素）。</summary>
    public static Vector2Int PrimaryResolutionPx => WindowsScreen.GetPrimaryScreenSizePx();

    /// <summary>虚拟桌面区域（所有显示器，物理像素）。</summary>
    public static RectInt VirtualDesktopRectPx => WindowsScreen.GetVirtualScreenRectPx();

    /// <summary>Unity 窗口逻辑大小（不含 DPI）。</summary>
    public static Vector2Int UnityWindowLogicalSize => WindowsScreen.GetUnityWindowSizeLogical();

    /// <summary>Unity 窗口物理大小（已经乘上 DPI）。</summary>
    public static Vector2Int UnityWindowPhysicalSize => WindowsScreen.GetUnityWindowSizePhysical();

    /// <summary>尝试获取 Unity 窗口在虚拟桌面里的矩形（物理像素）。</summary>
    public static bool TryGetUnityWindowRectPx(out RectInt rect)
    {
        return WindowsWindow.TryGetUnityWindowRectPx(out rect);
    }

    /// <summary>获取鼠标在虚拟桌面坐标中的位置（物理像素）。</summary>
    public static Vector2Int GetMousePositionVirtualPx()
    {
        return WindowsMouse.GetCursorPositionVirtual();
    }
}

#endif
