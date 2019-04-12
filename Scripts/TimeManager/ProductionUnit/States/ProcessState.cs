using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

namespace TimeManager.ProductionUnit
{
    using TimeManager.ProductionUnitAPI;
    public class ProcessState : ProductionUnitState
    {
        ProductionUnit unit;
        float timer;
        Utilits.Timer timer_view;

        public ProcessState(ProductionUnit u)
        {
            unit = u;
            timer = unit.process_time;

            timer_view = unit.timer.GetComponent<Utilits.Timer>();
            timer_view.InitFull();
        }

        public void EndState()
        {
        }

        public ProductionUnitStates GetCurStateName()
        {
            return ProductionUnitStates.PROCESS;
        }

        public void OnClick()
        {
        }

        public void StartState()
        {
            //unit.ready_icon.SetActive(false);
            //unit.product_icon.SetActive(false);

            foreach (var slot in unit.recipe_slots)
            {
                slot.SetActive(false);
            }

            unit.timer.SetActive(true);
        }

        public void Update()
        {
            timer -= Time.deltaTime;

            timer_view.TickUp(unit.process_time, Time.deltaTime);

            if (timer <= 0)
            {
                unit.BeReady();
            }
        }
    }
}