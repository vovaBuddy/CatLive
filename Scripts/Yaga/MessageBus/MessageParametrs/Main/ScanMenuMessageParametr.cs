using System.Collections.Generic;
using Yaga;

namespace MainScene
{
    public class ShopTypeParametr : MessageParametrs
    {
        public ShopItemType type;

        public ShopTypeParametr(ShopItemType t)
        {
            type = t;
        }
    }

    public class ScanMenuMessageParametrs : MessageParametrs
    {
        public List<string> names;
        public int max_cats;
        public int star_cnt;
        public int max_star_cnt;

        public ScanMenuMessageParametrs()
        {
        }
    }
}