using System.Collections;
using System.Collections.Generic;
using TimeManager.Product;
using UnityEngine;

namespace TimeManager.FastSale
{
    public class WaitState : FastSaleUnitState
    {

        FastSaleUnit unit;

        public WaitState(FastSaleUnit u)
        {
            unit = u;
        }

        public void EndState()
        {
        }

        public FastSaleUnitStates GetCurStateName()
        {
            return FastSaleUnitStates.WAIT;
        }

        public bool GiveProduct(ProductType product)
        {
            unit.products_count++;

            unit.product_places[unit.products_count - 1].place.SetActive(true);
            unit.product_places[unit.products_count - 1].product.GetComponent<SpriteRenderer>().sprite =
                ResourcesController.get_instance().product_resources.get_small_by_type(product);

            //add coins

            if (unit.products_count == unit.max_products)
            {
                unit.Leave();
            }

            return true;
        }

        public void OnClick()
        {
        }

        public void StartState()
        {
        }

        public void Update()
        {
        }
    }
}