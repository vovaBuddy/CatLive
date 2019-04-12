using System.Collections.Generic;
using Yaga;

namespace MainScene
{
    public class BuyItemParametr : MessageParametrs
    {
        public int price;
        public int beauty_value;
        public ShopItemType type;
        public UnityEngine.Texture item_texture;
        public UnityEngine.Sprite item_sprite;
    }
}