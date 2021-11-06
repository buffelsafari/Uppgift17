using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift17.MapViewFSM
{
    public class TranslateEntityState : BaseState
    {
        private string selectedId;

        private double mouseDownX = 0;
        private double mouseDownY = 0;

        private double angle=0;

        private static TranslateEntityState Instance;
        private TranslateEntityState()
        {

        }

        public static TranslateEntityState GetInstance()
        {
            if (TranslateEntityState.Instance == null)
            {
                Instance = new TranslateEntityState();
            }
            return Instance;

        }



        protected override void EnterState(StateChangeArgs args)
        {
            selectedId=args.TargetId;
            mapView.OnMouseUp = this.OnMouseUp;
            mapView.OnMouseMove = this.OnMouseMove;
            

            mouseDownX = args.X;
            mouseDownY = args.Y;
            angle = args.Angle;
        }

        protected override void LeaveState(StateChangeArgs args)
        {
            mapView.OnMouseUp = null;
            mapView.OnMouseMove = null;
        }


        private async Task OnMouseUp(MouseEventArgs args)
        {
            mapView.OnStopTranslateEntity();
            ChangeState(IdleState.GetInstance(), new StateChangeArgs { X = args.OffsetX, Y = args.OffsetY });
        }

        private async Task OnMouseMove(MouseEventArgs args)
        {
            //Console.WriteLine("move in translate entity state ");
            double x = (args.OffsetX-mouseDownX)/mapView.Zoom;
            double y = (args.OffsetY-mouseDownY)/mapView.Zoom;


            double rx = x * Math.Cos(angle) - y * Math.Sin(angle);
            double ry = x * Math.Sin(angle) + y * Math.Cos(angle);

            mapView.OnTranslateEntity(selectedId, rx, ry);  // transform coordinates
            //mapView.TransX = (args.OffsetX / mapView.Zoom - mouseDownX);
            //mapView.TransY = (args.OffsetY / mapView.Zoom - mouseDownY);

            await RequestRedraw(false);

        }

        
    }
}
