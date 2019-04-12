using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;
using Yaga;
using DG.Tweening;

namespace TimeManager.ProductionField
{
    using TimeManager;


    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class ProductionFieldsController : ExtendedBehaviour
    {
        public List<ProductionFieldUnit> fields;

        public override void ExtendedStart()
        {
            fields = new List<ProductionFieldUnit>();
        }

        [Subscribe(ProductionFieldAPI.Messages.INIT_PRODUCTION_FIELDS)]
        public void init(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<ProductionFieldAPI.InitPFParams>(msg.parametrs);

            int i = 0;
            foreach (var conf in param.configs)
            {
                var go = Instantiate(ResourcesController.get_instance().prefabs_resources.ProductionFieldUnit,
                    ResourcesController.get_instance().ProductionFieldUnitsConteiner.transform);

                go.transform.position = ResourcesController.get_instance().production_fuild_slots[i].position;

                fields.Add(go.GetComponent<ProductionFieldUnit>());
                ++i;
            }
        }

        [Subscribe(ProductionUnitAPI.Messages.NEED_INGREDIENT)]
        public void NeedIngredient(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<ProductionUnitAPI.NeedIngredientParams>(msg.parametrs);
            var finded = false;

            for (int i = 0; i < fields.Count; ++i)
            {
                if (finded)
                    continue;

                if (fields[i].cur_type == param.ingredient && fields[i].IsReady())
                {

                    var go = new GameObject();
                    var sr = go.AddComponent<SpriteRenderer>();
                    var image = fields[i].GetComponent<ProductionFieldUnit>().product_icon.GetComponent<SpriteRenderer>();
                    sr.sortingOrder = 2000;
                    go.transform.position = image.transform.position;
                    sr.sprite = image.sprite;

                    var destination_obj = param.prod_unit.GetComponent<ProductionUnit.ProductionUnit>();
                    destination_obj.wait_img_anim = true;
                    go.transform.DOMove(destination_obj.product_icon.transform.position, 0.25f, false).onComplete = (() =>
                    {
                        param.prod_unit.GetComponent<ProductionUnit.ProductionUnit>().GiveIngredient(param.ingredient);
                        Destroy(go);
                    });

                    fields[i].Start();
                    finded = true;
                }
            }
        }

        [Subscribe(CustomerAPI.Messages.NEED_PRODUCT)]
        public void NeedProduct(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<CustomerAPI.NeedProductParametrs>(msg.parametrs);

            for (int i = 0; i < fields.Count; ++i)
            {
                if (fields[i].cur_type == param.type && fields[i].IsReady())
                {
                    if (param.customer.GetComponent<Customer.Customer>().GiveProduct())
                    {
                        var go = new GameObject();
                        var sr = go.AddComponent<SpriteRenderer>();
                        var image = fields[i].GetComponent<ProductionFieldUnit>().product_icon;
                        sr.sortingOrder = 2000;
                        go.transform.position = image.transform.position;
                        sr.sprite = image.GetComponent<SpriteRenderer>().sprite;

                        go.transform.DOMove(param.customer.GetComponent<Customer.Customer>().
                            entity.product_small_icon.transform.position, 0.4f, false).onComplete = (() =>
                        {
                            Destroy(go);
                        });

                        fields[i].Start();
                    }
                    break;
                }
            }
        }

        [Subscribe(ProductProviderAPI.Messages.SEND_PRODUCT_TO_PRODUCTION)]
        public void despatch_sending_to_production(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<ProductProviderAPI.SendToProductionParams>(msg.parametrs);
            for(int i = 0; i < fields.Count; ++i)
            {
                if(fields[i].IsEmpty())
                {
                    //new obj - not good
                    var go = new GameObject();
                    var sr = go.AddComponent<SpriteRenderer>();
                    var image = param.provider.GetComponent<ProductProvider.ProductProvider>().product_icon;
                    sr.sortingOrder = 2000;
                    go.transform.position = image.transform.position;
                    sr.sprite = image.sprite;
                    var pf = fields[i].GetComponent<ProductionFieldUnit>();
                    pf.wait_icon_anim = true;
                    go.transform.DOMove(pf.product_icon.transform.position, 0.3f, false).onComplete = (() =>
                    {
                        fields[i].StartProduce(param.time, param.type);
                        Destroy(go);
                    });
                    
                    break;
                }
            }
        }
    }
}