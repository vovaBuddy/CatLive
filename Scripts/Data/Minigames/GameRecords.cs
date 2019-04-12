using System;

namespace Minigames
{
    [Serializable]
    public class GameRecords 
    {
        public int last_value;
        public int best_value;
        public int last_coins;

        public GameRecords()
        {
            last_value = 0;
            best_value = 0;
        }
    }
}
