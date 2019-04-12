using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager.ProductionUnit
{
    public enum ProductionUnitStates
    {
        EMPTY,
        PROCESS,
        READY
    }

    public interface ProductionUnitState 
    {
        void StartState();
        void EndState();
        void Update();
        void OnClick();
        ProductionUnitStates GetCurStateName();        
    }
}