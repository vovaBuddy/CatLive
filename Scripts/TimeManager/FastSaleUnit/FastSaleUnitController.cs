using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;

namespace TimeManager.FastSale
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class FastSaleUnitController : ExtendedBehaviour
    {
        public FastSaleUnit unit;

        public override void ExtendedStart()
        {

        }

        [Subscribe(FastSaleUnitAPI.Messages.FAST_SALE_PRODUCT)]
        public void FastSale(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<FastSaleUnitAPI.ProductParams>(msg.parametrs);

            MessageBus.Instance.SendMessage(new Message(CustomerAPI.Messages.ADD_MONEY,
                new CustomerAPI.AddMoneyParametrs(unit.conf.get_price(param.product)))); 
            //hard linking
            if (unit.GiveProduct(param.product))
            {
                var item_fu = param.pr_unit.GetComponent<ProductionField.ProductionFieldUnit>();

                if(item_fu != null)
                {
                    item_fu.Start();
                    return;
                }

                var item_pu = param.pr_unit.GetComponent<ProductionUnit.ProductionUnit>();

                if (item_pu != null)
                {
                    item_pu.Start();
                    return;
                }
            }

        }

    }
}