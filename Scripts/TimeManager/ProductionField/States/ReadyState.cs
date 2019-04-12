using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;
using DG.Tweening;

namespace TimeManager.ProductionField
{
    public class ReadyState : ProductionFieldState
    {
        GameObject field;
        ProductionFieldUnit unit;

        public ReadyState(GameObject go)
        {
            field = go;
            unit = field.GetComponent<ProductionFieldUnit>();
        }

        public void EndState()
        {
        }

        public ProductionFieldStates GetCurStateName()
        {
            return ProductionFieldStates.READY;
        }

        public void OnClick()
        {
            MessageBus.Instance.SendMessage(new Message(FastSaleUnitAPI.Messages.FAST_SALE_PRODUCT,
                new FastSaleUnitAPI.ProductParams(unit.cur_type, field)));
        }

        public void StartState()
        {
            unit.timer.SetActive(false);

            unit.product_icon.GetComponent<SpriteRenderer>().sprite =
                ResourcesController.get_instance().product_resources.get_big_by_type(unit.cur_type);

            MessageBus.Instance.SendMessage(new Message(ProductionFieldAPI.Messages.READY,
                new ProductionFieldAPI.ReadyParams(unit.cur_type)));
        }

        float shake_time = 0;
        public void Update()
        {
            shake_time -= Time.deltaTime;

            if (shake_time <= 0)
            {
                int t = Random.Range(1, 3);

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
