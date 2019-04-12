using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.Storage;
using System;

namespace Main.MinigamesScreen
{
    public class MinigamesScreenData
    {
        [Serializable]
        class MinigamesScreenEntity
        {
            public bool show_coins_game;

            public MinigamesScreenEntity()
            {
                show_coins_game = false;
            }
        }

        StorableData<MinigamesScreenEntity> entity;

        public MinigamesScreenData()
        {
            entity = new StorableData<MinigamesScreenEntity>("MinigamesScreenEntity");
        }

        public bool ShowCoinsGame
        {
            get { return entity.content.show_coins_game; }
            set { entity.content.show_coins_game = value; entity.Store(); }
        }
    }
}