using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift17.Pages;

namespace Uppgift17.MapViewFSM
{
    public class RotateState :BaseState
    {

        //MapView mapView;
        //IJSRuntime JS;
        //ElementReference canvas;        
        
        const double ROTATION_SPEED = 0.0005;


        private static RotateState Instance;

        private RotateState()
        {

        }

        public static RotateState GetInstance()
        {
            if (RotateState.Instance == null)
            {
                Instance = new RotateState();
            }
            return Instance;
        }



        protected override void EnterState(StateChangeArgs args)
        {

            mapView.OnMouseWheel = this.OnMouseWheel;
            mapView.OnMouseUp = this.OnMouseUp;
        }

        protected override void LeaveState(StateChangeArgs args)
        {
            mapView.OnMouseWheel = null;
            mapView.OnMouseUp = null;
        }
                

        


        private async Task OnMouseWheel(WheelEventArgs args)
        {                        
            
            mapView.Rotation += args.DeltaY * ROTATION_SPEED;

            if (mapView.Rotation > Math.PI * 2)
            {
                mapView.Rotation -= Math.PI * 2;
            }
            if (mapView.Rotation < 0)
            {
                mapView.Rotation += Math.PI * 2;
            }               
                        
            await RequestRedraw(false);
        }

        private async Task OnMouseUp(MouseEventArgs args)
        {
            ChangeState(IdleState.GetInstance(),new StateChangeArgs { X=0,Y=0});
        }

    }
}
