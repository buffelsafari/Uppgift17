using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift17.Pages;


namespace Uppgift17.MapViewFSM
{
    public interface IMapViewState
    {        
        void EnterState(MapView mapView, IJSRuntime JS, ElementReference canvas);
    }
}
