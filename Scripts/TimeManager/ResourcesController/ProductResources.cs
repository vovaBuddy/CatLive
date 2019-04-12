using System.Collections;
using System.Collections.Generic;
using TimeManager.Level;
using UnityEngine;

namespace TimeManager.Product
{
    [System.Serializable]
    public class ProductSprite
    {
        public ProductType product;
        public Sprite sprite;
    }


    [System.Serializable]
    public class NonProductSprite
    {
        public NonProductsGoal product;
        public Sprite sprite;
    }

    [CreateAssetMenu(fileName = "NewResources", menuName = "Resources")]
    public class ProductResources : ScriptableObject
    {
        public List<ProductSprite> large_sprites;
        public List<ProductSprite> lock_sprites;
        public List<ProductSprite> small_sprites;
        public List<ProductSprite> provider_sprites;
        public List<ProductSprite> production_unit_sprites;
        public List<Sprite> customer_sprites_1;
        public List<Sprite> customer_sprites_2;
        public List<Sprite> customer_sprites_3;
        public List<Sprite> customer_sprites_4;

        public Sprite get_customer_sprite(int type, int index)
        {
            if(type == 1)
            {
                return customer_sprites_1[index];
            }
            else if(type == 2)
            {
                return customer_sprites_2[index];
            }
            else if (type == 3)
            {
                return customer_sprites_3[index];
            }
            else
            {
                return customer_sprites_4[index];
            }
        }

        public List<NonProductSprite> non_product_sprites;

        public Sprite get_by_non_product_type(NonProductsGoal type)
        {
            return search_sprite(type, non_product_sprites);
        }

        Sprite search_sprite(NonProductsGoal type, List<NonProductSprite> list)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i].product == type)
                    return list[i].sprite;
            }

            return null;
        }

        Sprite search_sprite(ProductType type, List<ProductSprite> list)
        {
            for(int i = 0; i < list.Count; ++i)
            {
                if (list[i].product == type)
                    return list[i].sprite;
            }

            return null;
        }

        public Sprite get_provider_by_type(ProductType type)
        {
            return search_sprite(type, provider_sprites);
        }

        public Sprite get_small_by_type(ProductType type)
        {
            return search_sprite(type, small_sprites);
        }

        public Sprite get_big_by_type(ProductType type)
        {
            return search_sprite(type, large_sprites);
        }

        public Sprite get_lock_by_type(ProductType type)
        {
            return search_sprite(type, lock_sprites);
        }

        public Sprite get_production_unit_sprite_by_type(ProductType type)
        {
            return search_sprite(type, production_unit_sprites);
        }
    }
}