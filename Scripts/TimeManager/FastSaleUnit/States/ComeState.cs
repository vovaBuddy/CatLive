using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TimeManager.Product;

namespace TimeManager.FastSale
{
    public class ComeState : FastSaleUnitState
    {
        FastSaleUnit unit;

        public ComeState(FastSaleUnit u)
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
            foreach(var place in unit.product_places)
            {
                place.place.SetActive(false);
            }

            unit.transform.DOMoveY(unit.transform.position.y - 6, 3.0f).onComplete =
                () => { unit.StartWait(); };

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
            return FastSaleUnitStates.COME;
        }
    }
}