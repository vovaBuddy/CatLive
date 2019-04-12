using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class AdvEntity
{
    public bool adv_scanning;
    public int sequential_games;
    public float last_rew;

    public AdvEntity()
    {
        adv_scanning = false;
        sequential_games = 0;
        last_rew = 0;
    }
}


