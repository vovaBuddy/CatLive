using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;

namespace TimeManager.ProductionUnit
{
    class ReadyState : ProductionUnitState
    {
        ProductionUnit unit;

        public ReadyState(ProductionUnit u)
        {
            unit = u;
        }

        public void EndState()
        {
        }

        public ProductionUnitStates GetCurStateName()
        {
            return ProductionUnitStates.READY;
        }

        public void OnClick()
        {
            MessageBus.Instance.SendMessage(new Message(FastSaleUnitAPI.Messages.FAST_SALE_PRODUCT,
                 new FastSaleUnitAPI.ProductParams(unit.result_product, unit.gameObject)));
        }

        public void StartState()
        {
            unit.ready_icon.SetActive(true);
            unit.product_icon.SetActive(true);
            unit.product_icon.GetComponent<SpriteRenderer>().sprite =
                ResourcesController.get_instance().product_resources.get_big_by_type(unit.result_product);

            unit.timer.SetActive(false);
        }


        float shake_time = 0;
        public void Update()
        {
            shake_time -= Time.deltaTime;

            if (shake_time <= 0)
            {
                int t = UnityEngine.Random.Range(1, 3);

                if (t == 1)
                {
                    unit.product_icon.transform.DOShakePosition(1, 0.3f);
                }
                else
                {
                    unit.product_icon.transform.DOShakeScale(1);
                }

                shake_time = UnityEngine.Random.Range(4.0f, 5.0f);
            }
        }
    }
}
