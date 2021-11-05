using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift17.MapViewFSM
{
    public class TranslateState : BaseState
    {
        private double mouseDownX = 0;
        private double mouseDownY = 0;

        private static TranslateState Instance;
        private TranslateState()
        {

        }

        public static TranslateState GetInstance()
        {
            if (TranslateState.Instance == null)
            {
                Instance = new TranslateState();
            }
            return Instance;

        }

        protected override void EnterState(StateChangeArgs args)
        {
            mapView.OnMouseMove = this.OnMouseMove;
            mapView.OnMouseUp = this.OnMouseUp;
            mapView.IsMoving = true;


            mouseDownX = (args.X / mapView.Zoom - mapView.TransX);
            mouseDownY = (args.Y / mapView.Zoom - mapView.TransY);
        }

        protected override void LeaveState(StateChangeArgs args)
        {
            mapView.OnMouseMove = null;
            mapView.OnMouseUp = null;
        }


        private async Task OnMouseMove(MouseEventArgs args)
        {
            
            mapView.TransX = (args.OffsetX / mapView.Zoom - mouseDownX);
            mapView.TransY = (args.OffsetY / mapView.Zoom - mouseDownY);

            await JS.InvokeVoidAsync("RequestAnimationFrame", DotNetObjectReference.Create(mapView), "Redraw", canvas);
            
        }

        private async Task OnMouseUp(MouseEventArgs args)
        {
            ChangeState(IdleState.GetInstance(), new StateChangeArgs { X = 0, Y = 0 });
        }
    }
}
