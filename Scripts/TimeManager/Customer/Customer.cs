using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager.Product;

namespace TimeManager.Customer
{
    public class Customer : MonoBehaviour
    {
        public CustomerEntity entity;
        public GameObject Up;
        public GameObject Down;

        public int type;
        CustomerState cur_state;
        public float wait_time;
        public float def_wait_time;
        public int cat_success_state;
        public ProductType need_product;
        public float cash;

        public int cur_place_index;
        public bool successable;

        public void ShowNeedArea(bool show)
        {
            for(int i = 0; i < entity.transform.childCount; ++i)
            {
                entity.transform.GetChild(i).gameObject.SetActive(show);
            }
        }

        public void Init(ProductType p, float t, float c, bool s)
        {
            need_product = p;
            wait_time = t;
            def_wait_time = t;
            cash = c;
            cat_success_state = 1;
            type = UnityEngine.Random.Range(1, 5);
            successable = s;
        }

        // Use this for initialization
        void Start()
        {

        }

        public bool isLeave()
        {
            return cur_state.GetCurStateName() == CustomerStates.LEAVE_BAD ||
                cur_state.GetCurStateName() == CustomerStates.LEAVE_SUCCESS;
        }

        public void Go()
        {
            cur_state = new ComeState(this);
            cur_state.StartState();
        }

        public bool GiveProduct()
        {
            return cur_state.GiveProduct();
        }

        public void StartWait()
        {
            cur_state = new WaitState(this, wait_time);
            cur_state.StartState();
        }

        public void LeaveBad()
        {
            cur_state = new LeaveBadState(this);
            cur_state.StartState();
        }

        public void LeaveSuccess()
        {
            cur_state = new LeaveSuccesState(this);
            cur_state.StartState();
        }

        //ToDo: on tap 
        public void OnMouseDown()
        {
            cur_state.OnClick();
        }

        // Update is called once per frame
        void Update()
        {
            if(cur_state!=null)
                cur_state.Update();
        }
    }
}