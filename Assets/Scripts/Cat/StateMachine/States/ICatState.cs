using UnityEngine;

namespace NekoDesk.Cat
{
    public interface ICatState
    {
        void Enter();
        void Tick();
        void Exit();
        bool CanInterrupt();
    }
}
