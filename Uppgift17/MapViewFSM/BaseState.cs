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

        protected abstract void EnterState(StateChangeArgs args);
        protected abstract void LeaveState(StateChangeArgs args);

        public static void InitFSM(BaseState state, MapView mapView, IJSRuntime JS, ElementReference canvas, StateChangeArgs args)
        {
            BaseState.currentState = state;
            BaseState.mapView = mapView;
            BaseState.JS = JS;
            BaseState.canvas = canvas;

            BaseState.currentState.EnterState(args);
        }

        public static void ChangeState(BaseState nextState, StateChangeArgs args)
        {
            BaseState.currentState.LeaveState(args);
            BaseState.currentState = nextState;
            BaseState.currentState.EnterState(args);
        }

                
        
    }

    public class StateChangeArgs
    {
        public double X { get; set; }
        public double Y { get; set; }

    }
}
