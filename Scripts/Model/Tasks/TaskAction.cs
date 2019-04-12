using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasks
{
    public interface TaskAction
    {
        void FirstAction(TaskEntity info);
        void StartAction();
        void IdleAction();
        void EndAction();
        void DoneInit();
        bool CheckItDone();
    }
}
