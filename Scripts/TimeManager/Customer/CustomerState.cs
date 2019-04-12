using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager.Customer
{
    public enum CustomerStates
    {
        COME,
        WAIT,
        LEAVE_SUCCESS,
        LEAVE_BAD
    }

    public interface CustomerState
    {
        void StartState();
        void EndState();
        void Update();
        void OnClick();
        bool GiveProduct();
        CustomerStates GetCurStateName();
    }
}