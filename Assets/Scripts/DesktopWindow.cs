using UnityEngine;
using Kirurobo;   // 来自插件

public class DesktopWindow : MonoBehaviour
{
    private UniWindowController win;

    private void Awake()
    {
        // 在场景中找 UniWindowController（就是你拖进来的那个 prefab 实例）
        win = FindObjectOfType<UniWindowController>();
    }

    private void Start()
    {
        if (win == null)
        {
            Debug.LogError("UniWindowController not found in scene! 请确认 Runtime/Prefabs 里的 UniWindowController 已经拖进场景。");
            return;
        }

        // 下面这些字段就是 README 里写的 

        // 透明窗口（非矩形）
        win.isTransparent = true;

        // 置顶（总在最前）
        win.isTopmost = true;

        // 是否自动做 hit test（透明处点穿）
        win.isHitTestEnabled = true;

        // 是否“点穿”窗口
        // 如果你想先能点击到猫，就先设为 false；想完全穿透就 true
        win.isClickThrough = false;

        // 设置窗口大小（注意是 Vector2）
        win.windowSize = new Vector2(400, 400);

        // 设置窗口位置（原点在主屏幕左下角，向上为正）
        // 这里举例：离左边 50px，离下边 50px
        win.windowPosition = new Vector2(50, 50);

        Debug.Log("DesktopWindow initialized UniWindowController ✅");
    }
}
