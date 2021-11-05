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
    public class ZoomState :BaseState
    {
        //MapView mapView;
        //IJSRuntime JS;
        //ElementReference canvas;       

        const double MIN_ZOOM = 0.25;
        const double MAX_ZOOM = 2.0;
        const double ZOOM_SPEED = 0.0005;

        private static ZoomState Instance;

        private ZoomState()
        { 
        
        }

        public static ZoomState GetInstance()
        {
            if (ZoomState.Instance == null)
            {
                Instance = new ZoomState();
            }
            return Instance;
        }



        protected override void EnterState()
        {
            mapView.OnMouseWheel = this.OnMouseWheel;
            mapView.OnMouseDown = this.OnMouseDown;
        }

        protected override void LeaveState()
        {
            mapView.OnMouseWheel = null;
            mapView.OnMouseDown = null;
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
                ChangeState(RotateState.GetInstance());
            }             
        }

        
    }
}
