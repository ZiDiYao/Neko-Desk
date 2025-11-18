using UnityEngine;


// This is only for unity windows ( can not map to the real windows/mac os windows)

public class DesktopWorldManager : MonoBehaviour
{

    public static DesktopWorldManager Instance {get; private set;}

    private Camera _cam;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _cam = Camera.main;
        if (_cam == null)
        {
            Debug.LogError("DesktopWorldManager: Main Camera not found!");
        }
    }


    // public Vector3 GetMouseWorldPosition()
    // {
        
    // }


    // public Bounds GetWorldBounds()
    // {
        
    // }

    // public float ClampXToWorld(float x, float margin = 0f)
    // {
        
    // }

    

}