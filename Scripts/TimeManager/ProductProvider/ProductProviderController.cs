using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;

namespace TimeManager.ProductProvider
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class ProductProviderController : ExtendedBehaviour
    {
        public List<ProductProvider> providers;

        public override void ExtendedStart()
        {
            providers = new List<ProductProvider>();
        }

        [Subscribe(ProductProviderAPI.Messages.INIT_PRODUCT_PROVIDERS)]
        public void init(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<ProductProviderAPI.InitPPParams>(msg.parametrs);

            int i = 0;
            foreach(var conf in param.configs)
            {
                var go = Instantiate(ResourcesController.get_instance().prefabs_resources.ProductProvider,
                    ResourcesController.get_instance().ProvidersContainer.transform);

                go.transform.position = ResourcesController.get_instance().product_provider_slots[i].position;

                var provider = go.GetComponent<ProductProvider>();
                provider.Init(conf.product, conf.production_time);

                providers.Add(provider);
                ++i;
            }
        }
    }
}