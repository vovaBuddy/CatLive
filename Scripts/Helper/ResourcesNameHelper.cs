using System.Collections;
using System.Collections.Generic;
using Minigames;

namespace Helper
{
    public static class ResourcesNameHelper {

        public static string UI_ICON_NAME_BY_TASK(TaskType t)
        {
            switch (t)
            {
                case TaskType.COINS:
                    return "coin_icon";
                case TaskType.POINTS:
                    return "odo_icon";
                case TaskType.TIME:
                    return "time_icon";
            }

            return null;
        }
    }
}