using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager.Product;

namespace TimeManager.FastSale
{
    [System.Serializable]
    public class ProductPlace
    {
        public GameObject place;
        public GameObject product;
    }


    public class FastSaleUnit : MonoBehaviour
    {
        public GameObject unit;
        public FastSaleUnitConfig conf;
        public List<ProductPlace> product_places;

        FastSaleUnitState cur_state;
        public int products_count;
        public int max_products = 4;

        public void StartWait()
        {
            cur_state = new WaitState(this);
            cur_state.StartState();
        }
        // Use this for initialization
        public void Start()
        {
            products_count = 0;
            cur_state = new ComeState(this);
            cur_state.StartState();
        }

        public void Leave()
        {
            cur_state = new LeaveState(this);
            cur_state.StartState();
        }

        public bool GiveProduct(ProductType product)
        {
            return cur_state.GiveProduct(product);
        }

        // Update is called once per frame
        void Update()
        {
            cur_state.Update();
        }
    }
}