using System;

namespace Yaga
{
    class TestParametrs : MessageParametrs
    {
        public TestParametrs(string n, int nm)
        {
            name = n;
            nmbr = nm;
            
        }
        public string name;
        public int nmbr;
    }
}
