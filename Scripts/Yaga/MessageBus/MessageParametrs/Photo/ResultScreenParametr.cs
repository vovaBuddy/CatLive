using System.Collections.Generic;
using Yaga;

namespace PhotoScene
{
    class ResultScreenParametr : MessageParametrs
    {
        public string name;
        public List<string> names;
        public int opened;
        public int need_open;
        public bool new_pet;

        public ResultScreenParametr(string n)
        {
            name = n;
        }
    }
}