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
    public class NormalState : IMapViewState
    {

        MapView mapView;
        IJSRuntime JS;
        ElementReference canvas;

        private double mouseDownX = 0;
        private double mouseDownY = 0;

        const double MIN_ZOOM = 0.25;
        const double MAX_ZOOM = 2.0;
        const double ZOOM_SPEED = 0.0005;
        const double ROTATION_SPEED = 0.0005;




        public void EnterState(MapView mapView, IJSRuntime JS, ElementReference canvas)
        {
            this.mapView = mapView;
            this.JS = JS;
            this.canvas = canvas;
            mapView.OnClick = this.OnClick;
            mapView.OnMouseDown = this.OnMouseDown;
            //mapView.OnMouseUp = this.OnMouseUp;
            mapView.OnMouseMove = this.OnMouseMove;
            mapView.OnMouseWheel = this.OnMouseWheel;
        }


        private async Task OnClick(MouseEventArgs args)
        {

            mapView.IsClicked = true;
            await JS.InvokeVoidAsync("RequestAnimationFrame", DotNetObjectReference.Create(mapView), "Redraw", canvas);

        }

        private async Task OnMouseDown(MouseEventArgs args)
        {

            mouseDownX = (args.OffsetX / mapView.Zoom - mapView.TransX);
            mouseDownY = (args.OffsetY / mapView.Zoom - mapView.TransY);

            mapView.ClickX = args.OffsetX;
            mapView.ClickY = args.OffsetY;

            mapView.IsMoving = true;

            await JS.InvokeVoidAsync("RequestAnimationFrame", DotNetObjectReference.Create(mapView), "Redraw", canvas);

        }

        //private async Task OnMouseUp(MouseEventArgs args)
        //{
        //    //isMachineGrabbed = false;
        //}


        private async Task OnMouseMove(MouseEventArgs args)
        {

            if (mapView.IsMoving & args.Buttons == 1)
            {

                mapView.TransX = (args.OffsetX / mapView.Zoom - mouseDownX);
                mapView.TransY = (args.OffsetY / mapView.Zoom - mouseDownY);

                await JS.InvokeVoidAsync("RequestAnimationFrame", DotNetObjectReference.Create(mapView), "Redraw", canvas);
            }
            else
            {
                mapView.IsMoving = false;
            }


        }

        private async Task OnMouseWheel(WheelEventArgs args)
        {
            //isMachineGrabbed = false;


            if (args.Buttons != 4)
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
            }
            else
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

                mapView.IsMoving = false;
            }

            await JS.InvokeVoidAsync("RequestAnimationFrame", DotNetObjectReference.Create(mapView), "Redraw", canvas);

            Console.WriteLine("the buttons in the mouse wheeler" + args.Buttons);
        }  






    }
}










//protected async override Task OnParametersSetAsync()
//{
   
//}


//protected override async Task OnInitializedAsync()
//{

//}


//private async Task OnClick(MouseEventArgs args)
//{
  
//}

//private async Task OnMouseDown(MouseEventArgs args)
//{
  
//}

//private async void OnMouseUp(MouseEventArgs args)
//{
   
//}


//private async Task OnMouseMove(MouseEventArgs args)
//{
  

//}

//private async Task OnMouseWheel(WheelEventArgs args)
//{
   
//}




//[JSInvokable]
//public async Task Redraw()
//{
   
//}

//[JSInvokable]
//public async Task OnResize()
//{
   
//}

//[JSInvokable]
//public async Task OnMapClick(string id)
//{
   
//}

//[JSInvokable]
//public async Task OnMapDown(string id)
//{
    
//}