using UnityEngine;


public class WindowsScreenAPIManager : MonoBehaviour
{
    // 单例
    public static WindowsScreenAPIManager Instance { get; private set; }

    // screen info ( Rect = Rectangle)
    public Rect PrimaryScreen;

    public Rect[] AllScreens;

    // DPI （ 每英寸显示多少个像素 ) 
    public float DpiScale;

    // 鼠标绝对坐标 (鼠标坐标必须是整数)
    public Vector2Int SystemMousePos;

    // Unity 窗口系统坐标
    public Rect UnityWindowRect;

    private void Awake()
    {
        Instance = this;
        // RefreshSystemInfo();
    }

    



}