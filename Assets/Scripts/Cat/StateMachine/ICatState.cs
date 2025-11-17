using UnityEngine;

public interface ICatState
{

    // run when enter this state 

    void Enter();

    // 每帧调用, 用来更新状态逻辑

    void Tick();

    // run this when leave the state 

    void Exit();

     bool CanInterrupt(); // 是否允许被中断， 比如跳跃跳到一半肯定是不允许的
    
}
