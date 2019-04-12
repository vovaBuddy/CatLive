using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager.ProductionField
{
    public enum ProductionFieldStates
    {
        EMPTY,
        PROCESS,
        READY
    }


    public interface ProductionFieldState 
    {
        void StartState();
        void EndState();
        void Update();
        void OnClick();
        ProductionFieldStates GetCurStateName();
    }
}