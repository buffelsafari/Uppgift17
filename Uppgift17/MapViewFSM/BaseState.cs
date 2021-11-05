using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift17.Pages;

namespace Uppgift17.MapViewFSM
{
    public abstract class BaseState 
    {
        protected static BaseState currentState;
        protected static MapView mapView;
        protected static IJSRuntime JS;
        protected static ElementReference canvas;

        protected abstract void EnterState();
        protected abstract void LeaveState();

        public static void InitFSM(BaseState state, MapView mapView, IJSRuntime JS, ElementReference canvas)
        {
            BaseState.currentState = state;
            BaseState.mapView = mapView;
            BaseState.JS = JS;
            BaseState.canvas = canvas;

            BaseState.currentState.EnterState();
        }

        public static void ChangeState(BaseState nextState)
        {
            BaseState.currentState.LeaveState();
            BaseState.currentState = nextState;
            BaseState.currentState.EnterState();
        }

                
        
    }
}
