using UnityEngine;


// 挂在 Cat GameObject 上， 管理当前状态和切换逻辑 
public class CatStateMachine : MonoBehaviour
{

    public CatStateId CurrentStateId { get; private set; }

    private ICatState _currentState;

    // 普通切换，尊重 CanInterrupt

    public void SwitchState(CatStateId newState)
    {
        

    }

    // 强制切换 

    public void ForceSwitchState(CatStateId newState)
    {
        

    }

     private ICatState CreateState(CatStateId id)
    {
        
    }




    
}
