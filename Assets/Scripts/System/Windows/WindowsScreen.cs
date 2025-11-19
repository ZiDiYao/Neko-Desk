#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

using System;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// 跟 Windows 屏幕相关的底层工具:
/// - 主屏幕分辨率
/// - 虚拟桌面区域（多显示器拼接）
/// - Unity 窗口的逻辑/物理尺寸
/// </summary>
public static class WindowsScreen
{
    // Win32: GetSystemMetrics
    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    // 主屏幕宽/高（像素）
    private const int SM_CXSCREEN = 0;
    private const int SM_CYSCREEN = 1;

    // 虚拟桌面（包含所有显示器）
    private const int SM_XVIRTUALSCREEN = 76;
    private const int SM_YVIRTUALSCREEN = 77;
    private const int SM_CXVIRTUALSCREEN = 78;
    private const int SM_CYVIRTUALSCREEN = 79;

    /// <summary>
    /// 获取主显示器物理分辨率（像素）
    /// 例如 1920x1080 / 2560x1440 / 3840x2160
    /// </summary>
    public static Vector2Int GetPrimaryScreenSizePx()
    {
        int w = GetSystemMetrics(SM_CXSCREEN);
        int h = GetSystemMetrics(SM_CYSCREEN);
        return new Vector2Int(w, h);
    }

    /// <summary>
    /// 获取虚拟桌面区域（物理像素），包含所有显示器。
    /// 左上角可能是负数（例如左边还有一个副屏）。
    /// </summary>
    public static RectInt GetVirtualScreenRectPx()
    {
        int x = GetSystemMetrics(SM_XVIRTUALSCREEN);
        int y = GetSystemMetrics(SM_YVIRTUALSCREEN);
        int w = GetSystemMetrics(SM_CXVIRTUALSCREEN);
        int h = GetSystemMetrics(SM_CYVIRTUALSCREEN);
        return new RectInt(x, y, w, h);
    }

    /// <summary>
    /// Unity 认为的窗口逻辑尺寸（不含 DPI 缩放）
    /// 即 Screen.width / Screen.height。
    /// </summary>
    public static Vector2Int GetUnityWindowSizeLogical()
    {
        return new Vector2Int(Screen.width, Screen.height);
    }

    /// <summary>
    /// Unity 窗口在“真实物理像素”上占用的大小
    /// = 逻辑尺寸 * DPI 缩放。
    /// </summary>
    public static Vector2Int GetUnityWindowSizePhysical()
    {
        float scale = WindowsDPI.GetDPIScale();  // 例如 1.0, 1.25, 1.5, 2.0
        int w = Mathf.RoundToInt(Screen.width * scale);
        int h = Mathf.RoundToInt(Screen.height * scale);
        return new Vector2Int(w, h);
    }
}

#endif
