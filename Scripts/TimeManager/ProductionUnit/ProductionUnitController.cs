using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;
using Yaga;
using DG.Tweening;

namespace TimeManager.ProductionUnit
{

    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class ProductionUnitController : ExtendedBehaviour
    {
        public List<ProductionUnit> units;
        public override void ExtendedStart()
        {
            units = new List<ProductionUnit>();
        }

        [Subscribe(ProductionUnitAPI.Messages.INIT_PRODUCTION_UNITS)]
        public void init(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<ProductionUnitAPI.InitPUParams>(msg.parametrs);

            int i = 0;
            foreach (var conf in param.configs)
            {
                var go = Instantiate(ResourcesController.get_instance().prefabs_resources.ProductionUnit,
                    ResourcesController.get_instance().ProductionUnitsContainer.transform);

                go.transform.position = ResourcesController.get_instance().production_unit_slots[i].position;

                var unit = go.GetComponent<ProductionUnit>();
                unit.Init(conf.recipe, conf.result_product, conf.process_time);

                units.Add(go.GetComponent<ProductionUnit>());
                ++i;
            }
        }

        [Subscribe(ProductionUnitAPI.Messages.NEED_INGREDIENT)]
        public void NeedIngredient(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<ProductionUnitAPI.NeedIngredientParams>(msg.parametrs);

            bool finded = false;
            for (int i = 0; i < units.Count; ++i)
            {
                if (finded)
                    continue;

                if (units[i].result_product == param.ingredient && units[i].IsReady())
                {
                    var go = new GameObject();
                    var sr = go.AddComponent<SpriteRenderer>();
                    var image = units[i].GetComponent<ProductionUnit>().product_icon.GetComponent<SpriteRenderer>();
                    sr.sortingOrder = 2000;
                    go.transform.position = image.transform.position;
                    sr.sprite = image.sprite;

                    var destination_obj = param.prod_unit.GetComponent<ProductionUnit>();
                    destination_obj.wait_img_anim = true;
                    go.transform.DOMove(destination_obj.product_icon.transform.position, 0.25f, false).onComplete = (() =>
                    {
                        param.prod_unit.GetComponent<ProductionUnit>().GiveIngredient(param.ingredient);
                        Destroy(go);
                    });

                    units[i].Start();
                    finded = true;
                }
            }
        }

        [Subscribe(CustomerAPI.Messages.NEED_PRODUCT)]
        public void customer_need(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<CustomerAPI.NeedProductParametrs>(msg.parametrs);

            for (int i = 0; i < units.Count; ++i)
            {
                if (units[i].result_product == param.type && units[i].IsReady())
                {
                    if (param.customer.GetComponent<Customer.Customer>().GiveProduct())
                    {
                        var go = new GameObject();
                        var sr = go.AddComponent<SpriteRenderer>();
                        var image = units[i].GetComponent<ProductionUnit>().product_icon;
                        sr.sortingOrder = 2000;
                        go.transform.position = image.transform.position;
                        sr.sprite = image.GetComponent<SpriteRenderer>().sprite;

                        go.transform.DOMove(param.customer.GetComponent<Customer.Customer>().
                            entity.product_small_icon.transform.position, 0.4f, false).onComplete = (() =>
                        {
                            Destroy(go);
                        });

                        units[i].Start();
                    }
                    break;
                }
            }
        }
    }
}
