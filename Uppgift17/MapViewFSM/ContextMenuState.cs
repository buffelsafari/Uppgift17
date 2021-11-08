using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift17.MapViewFSM
{
    public class ContextMenuState:BaseState
    {

        private static ContextMenuState Instance;
        private ContextMenuState()
        {

        }

        public static ContextMenuState GetInstance()
        {
            if (ContextMenuState.Instance == null)
            {
                Instance = new ContextMenuState();
            }
            return Instance;

        }

        protected override void EnterState(StateChangeArgs args)
        {
            

        }

        protected override void LeaveState(StateChangeArgs args)
        {
            
        }

        
    }
}
