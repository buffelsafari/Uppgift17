using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift17.Models;
using static Uppgift17.Pages.MapView;

namespace Uppgift17.MapViewFSM
{
    public class IdleState : BaseState
    {
        const double MIN_ZOOM = 0.25;
        const double MAX_ZOOM = 2.0;
        const double ZOOM_SPEED = 0.0005;

        private bool isContextButtonDown = false;

        private bool isDown=false;
        private string selectedId = "";

        private double trX;
        private double trY;
        private double angle;

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
            isDown = false;
            isContextButtonDown = false;
            mapView.OnMouseWheel = this.OnMouseWheel;
            mapView.OnMouseDown = this.OnMouseDown;
            mapView.OnMapClicked = this.ClickCallback;
            mapView.OnMouseMove = this.OnMouseMove;
            mapView.OnMouseUp = this.OnMouseUp;
        }

        protected override void LeaveState(StateChangeArgs args)
        {
            mapView.OnMouseWheel = null;
            mapView.OnMouseDown = null;
            mapView.OnMapClicked = null;
            mapView.OnMouseMove = null;
            mapView.OnMouseUp = null;
            isContextButtonDown = false;
            isDown = false;
            selectedId = "";
        }

        private async Task OnMouseMove(MouseEventArgs args)
        {
            if (isDown)
            {
                if (selectedId == "none")
                {
                    ChangeState(TranslateState.GetInstance(), new StateChangeArgs { X = args.OffsetX, Y = args.OffsetY });
                    return;
                }

                if (!String.IsNullOrEmpty(selectedId))
                {                    
                    ChangeState(TranslateEntityState.GetInstance(), new StateChangeArgs { X = trX, Y = trY, TargetId=selectedId, Angle=angle});
                    return;
                }
            }
            
            

            

        }

        private async Task OnMouseUp(MouseEventArgs args)
        {

            if (isContextButtonDown)
            {
                if (selectedId == "none")
                {
                    Console.WriteLine("context outside");
                    //ChangeState(TranslateState.GetInstance(), new StateChangeArgs { X = args.OffsetX, Y = args.OffsetY });
                    return;
                }

                if (!String.IsNullOrEmpty(selectedId))
                {
                    
                    mapView.OnOpenContextMenu(selectedId, args.OffsetX, args.OffsetY);

                    //ChangeState(ContextMenuState.GetInstance(), new StateChangeArgs { X = trX, Y = trY, TargetId = selectedId, Angle = angle });
                    return;
                }
            }




            isContextButtonDown = false;
            isDown = false;
            selectedId = "";

            
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

            await RequestRedraw(false);            

            Console.WriteLine("the buttons in the mouse wheeler" + args.Buttons);
        }


        private async Task OnMouseDown(MouseEventArgs args)
        {
            //mapView.IsClicked = true;
            mapView.ClickX = args.OffsetX;
            mapView.ClickY = args.OffsetY;


            await RequestRedraw(true);            

            if (args.Buttons == 4)
            {
                ChangeState(RotateState.GetInstance(), new StateChangeArgs { X = 0, Y = 0 });
                return;
                
            }
            if (args.Buttons == 1)
            {
                isDown = true;
                //ChangeState(TranslateState.GetInstance(), new StateChangeArgs { X = args.OffsetX, Y = args.OffsetY });
                return;
                
            }
            if (args.Buttons == 2)
            {
                isContextButtonDown = true;
                Console.WriteLine("button 2 is pressed");
            }

        }

        private async Task ClickCallback(string id, TransformMatrix matrix)
        {            
            selectedId = id;
            //Console.WriteLine("e:" + matrix.e);
            //Console.WriteLine("f:" + matrix.f);
            trX = matrix.e;
            trY = matrix.f;

            angle=Math.Atan2(matrix.c, matrix.a);
            //Console.WriteLine("angle="+angle);

            //Console.WriteLine("you pressed in IdleState "+id);

            //console
        }

                

    }
}
