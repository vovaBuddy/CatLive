using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager.ProductionField
{
    using TimeManager.Product;
    public class ProductionFieldUnit : MonoBehaviour
    {
        public GameObject timer;
        public GameObject ready_icon;
        public GameObject product_icon;

        public ProductType cur_type;

        ProductionFieldState cur_state;
        public bool wait_icon_anim = false;

        public bool IsEmpty()
        {
            return cur_state.GetCurStateName() == ProductionFieldStates.EMPTY && !wait_icon_anim;
        }
        public bool InProcess()
        {
            return cur_state.GetCurStateName() == ProductionFieldStates.PROCESS;
        }
        public bool IsReady()
        {
            return cur_state.GetCurStateName() == ProductionFieldStates.READY;
        }

        public void Start()
        {
            cur_type = ProductType.NONE;
            cur_state = new EmptyState(gameObject);
            cur_state.StartState();
        }

        public void Ready()
        {
            cur_state = new ReadyState(gameObject);
            cur_state.StartState();
        }

        public void StartProduce(float t, ProductType type)
        {
            wait_icon_anim = false;
            cur_type = type;
            cur_state = new ProcessState(gameObject, t);
            cur_state.StartState();            
        }

        public void Update()
        {
            cur_state.Update();
        }

        //ToDo: on tap 
        public void OnMouseDown()
        {
            cur_state.OnClick();
        }
    }
}