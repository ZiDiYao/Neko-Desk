#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Windows 鼠标相关底层工具：
/// - 获取鼠标在虚拟桌面坐标系中的位置（物理像素）。
/// </summary>
public static class WindowsMouse
{
    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;
    }

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    /// <summary>
    /// 获取鼠标在“虚拟桌面”中的坐标（物理像素）。
    /// 可能为负数（在主屏左边/上边的屏幕上）。
    /// </summary>
    public static Vector2Int GetCursorPositionVirtual()
    {
        if (!GetCursorPos(out POINT p))
        {
            return Vector2Int.zero;
        }

        return new Vector2Int(p.X, p.Y);
    }
}

#endif
