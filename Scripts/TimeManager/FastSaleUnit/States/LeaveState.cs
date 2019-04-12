using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TimeManager.Product;

namespace TimeManager.FastSale
{
    public class LeaveState : FastSaleUnitState
    {
        FastSaleUnit unit;

        public LeaveState(FastSaleUnit u)
        {
            unit = u;
        }

        public void EndState()
        {
        }

        public void OnClick()
        {
        }

        public void StartState()
        {
            unit.unit.SetActive(true);
            foreach (var place in unit.product_places)
            {
                place.place.SetActive(false);
            }

            unit.transform.DOMoveY(unit.transform.position.y + 6, 5.0f).onComplete =
                () => { unit.Start(); };

        }

        public bool GiveProduct(ProductType product)
        {
            return false;
        }

        public void Update()
        {
        }

        FastSaleUnitStates FastSaleUnitState.GetCurStateName()
        {
            return FastSaleUnitStates.LEAVE;
        }
    }
}