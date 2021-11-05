using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift17.MapViewFSM
{
    public class IdleState : BaseState
    {
        const double MIN_ZOOM = 0.25;
        const double MAX_ZOOM = 2.0;
        const double ZOOM_SPEED = 0.0005;


        private static IdleState Instance;
        private IdleState()
        {

        }

        public static IdleState GetInstance()
        {
            if (IdleState.Instance == null)
            {
                Instance = new IdleState();
            }
            return Instance;

        }


        protected override void EnterState(StateChangeArgs args)
        {
            mapView.OnMouseWheel = this.OnMouseWheel;
            mapView.OnMouseDown = this.OnMouseDown;
        }

        protected override void LeaveState(StateChangeArgs args)
        {
            mapView.OnMouseWheel = null;
        }



        private async Task OnMouseWheel(WheelEventArgs args)
        {
            float deltaZoom = (float)(args.DeltaY * ZOOM_SPEED);
            mapView.Zoom += deltaZoom;

            if (mapView.Zoom < MIN_ZOOM)
            {
                mapView.Zoom = MIN_ZOOM;
            }
            if (mapView.Zoom > MAX_ZOOM)
            {
                mapView.Zoom = MAX_ZOOM;
            }


            mapView.IsMoving = false;


            await JS.InvokeVoidAsync("RequestAnimationFrame", DotNetObjectReference.Create(mapView), "Redraw", canvas);

            Console.WriteLine("the buttons in the mouse wheeler" + args.Buttons);
        }


        private async Task OnMouseDown(MouseEventArgs args)
        {
            if (args.Buttons == 4)
            {
                ChangeState(RotateState.GetInstance(), new StateChangeArgs { X = 0, Y = 0 });
                return;
            }
            if (args.Buttons == 1)
            {

                ChangeState(TranslateState.GetInstance(), new StateChangeArgs { X = args.OffsetX, Y = args.OffsetY });
                return;
            }
        }
    }
}
