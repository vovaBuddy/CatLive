using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager.ProductionField
{
    public class ProcessState : ProductionFieldState
    {
        GameObject field;
        ProductionFieldUnit unit;
        float timer;
        Utilits.Timer timer_view;
        float wait_time;

        public ProcessState(GameObject go, float t)
        {
            field = go;
            unit = field.GetComponent<ProductionFieldUnit>();
            timer = t;
            wait_time = t;

            unit.product_icon.GetComponent<SpriteRenderer>().sprite =
                ResourcesController.get_instance().product_resources.get_big_by_type(unit.cur_type);


            timer_view = unit.timer.GetComponent<Utilits.Timer>();
            timer_view.InitFull();
        }

        public void EndState()
        {

        }

        public ProductionFieldStates GetCurStateName()
        {
            return ProductionFieldStates.PROCESS;
        }

        public void OnClick()
        {
        }

        public void StartState()
        {
            unit.timer.SetActive(true);

            unit.product_icon.SetActive(true);
            unit.product_icon.GetComponent<SpriteRenderer>().sprite =
                ResourcesController.get_instance().product_resources.get_lock_by_type(unit.cur_type);
        }

        public void Update()
        { 
            if (timer >= 0)
            {
                timer_view.TickUp(wait_time, Time.deltaTime);
                timer -= Time.deltaTime;
            }
            else
            {
                unit.Ready();
            }
        }
    }
}
