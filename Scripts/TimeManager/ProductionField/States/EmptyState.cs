using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager.ProductionField
{
    public class EmptyState : ProductionFieldState
    {
        GameObject field;
        ProductionFieldUnit unit;
        public EmptyState(GameObject go)
        {
            field = go;
            unit = field.GetComponent<ProductionFieldUnit>();
        }

        public void EndState()
        {
        }

        public ProductionFieldStates GetCurStateName()
        {
            return ProductionFieldStates.EMPTY;
        }

        public void OnClick()
        {
        }

        public void StartState()
        {
            unit.timer.SetActive(false);
            unit.product_icon.SetActive(false);
            unit.ready_icon.SetActive(false);
        }

        public void Update()
        {
        }
    }
}