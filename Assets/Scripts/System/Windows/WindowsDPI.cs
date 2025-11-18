using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class WindowsDPI
{

    [DllImport("user32.dll")] 
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern uint GetDpiForWindow(IntPtr hwnd);

    public static float GetDPIScale()
    {
        IntPtr hwnd = GetActiveWindow(); // 拿到 Unity 窗口的句柄

        if (hwnd == IntPtr.Zero)
        {
            Debug.LogWarning("WindowsDPI: 无法获取窗口句柄，返回默认 DPI=1.0");
            return 1f; // 默认无缩放
        }

        uint dpi = 96; // 默认值，避免旧系统报错

        try
        {
            dpi = GetDpiForWindow(hwnd);
        }
        catch (EntryPointNotFoundException)
        {
            // 说明系统太旧（Win7/Win8），没有 GetDpiForWindow
            Debug.LogWarning("WindowsDPI: 当前系统不支持 GetDpiForWindow，使用默认 DPI=96");
        }
        
        return dpi / 96f;
    }
       

    public static uint GetDPI()
    {
        IntPtr hwnd = GetActiveWindow();

        if (hwnd == IntPtr.Zero)
            return 96; // Safe fallback

        try
        {
            return GetDpiForWindow(hwnd);
        }
        catch
        {
            return 96;
        }
    }   
}