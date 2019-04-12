using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager.Product;
using Yaga.MessageBus;

namespace TimeManager.ProductProvider
{
    using TimeManager.ProductProviderAPI;

    public class ProductProvider : MonoBehaviour
    {
        float time;
        ProductType product_type;
        [SerializeField]
        public SpriteRenderer product_icon;

        public void Init(ProductType type, float t)
        {
            product_type = type;

            product_icon.sprite = ResourcesController.get_instance().product_resources.get_provider_by_type(type);

            time = t;
        }

        //ToDo: on tap 
        public void OnMouseDown()
        {
            MessageBus.Instance.SendMessage( new Message(ProductProviderAPI.Messages.SEND_PRODUCT_TO_PRODUCTION, 
                new ProductProviderAPI.SendToProductionParams(product_type, gameObject, time)));
        }
    }
}