using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager.Product;

namespace TimeManager.FastSale
{
    public enum FastSaleUnitStates
    {
        COME,
        WAIT,
        LEAVE,
    }

    public interface FastSaleUnitState
    {
        void StartState();
        void EndState();
        void Update();
        void OnClick();
        bool GiveProduct(ProductType product);
        FastSaleUnitStates GetCurStateName();
    }
}
