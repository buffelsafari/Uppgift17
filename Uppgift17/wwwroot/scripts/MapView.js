
function PositionContextMenu(contextMenu, x, y, isOpen)
{
    

    
    if (contextMenu.style)
    {        
        if ((contextMenu.offsetWidth + x+50) > window.innerWidth)
        {
            x -= (contextMenu.offsetWidth);            
        }
        if ((contextMenu.offsetHeight + y + 50) > window.innerHeight)
        {
            y -= (contextMenu.offsetHeight);            
        }

        contextMenu.style.left = x + "px";
        contextMenu.style.top = y + "px";


        if (isOpen)
        {
            contextMenu.style.display = "block";
        }
        else
        {
            contextMenu.style.display = "none";
        }

    }
    


}


function AddResizeListener(instance, method)
{
    window.addEventListener("resize", (event) =>
    {
        instance.invokeMethodAsync(method);
    });

    window.addEventListener("contextmenu", (event) =>
    {
        event.preventDefault();
    });
}



function RequestAnimationFrame(instance, method, test)
{     
    window.requestAnimationFrame((event) =>
    {
        instance.invokeMethodAsync(method, test);
    });
}



//function createTransform(originX, originY, scale, rotate, context)
//{
//    const xAxisX = Math.cos(rotate) * scale;
//    const xAxisY = Math.sin(rotate) * scale;
//    context.setTransform(xAxisX, xAxisY, -xAxisY, xAxisX, originX, originY);
//}


function Draw(canvas, list, scale, transX, transY, rotation, px, py, test, instance, testEvent)
{
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;

    let currentRotation = 0;


    let context = canvas.getContext("2d");
    
    let offsetX = canvas.width/2;
    let offsetY = canvas.height/2;    

    MapView.CreateTransform(offsetX, offsetY, scale, 0, context);

    
    context.translate(transX, transY);




    context.rotate(rotation);
    currentRotation += rotation;  // todo remove use transform

    let clickedItemId = "none";
    let clickedTransform =context.getTransform();
    
    
    

    list.forEach(item =>
    {
        
        


        switch (item.operation)
        {
            case "clear":                
                MapView.Clear(canvas, context);
                break;
            case "beginPath":                
                context.beginPath();
                break;
            case "closePath":
                context.closePath();
                break;
            case "moveTo":                
                context.moveTo(item.data[0],item.data[1]);
                break;
            case "lineTo":                
                context.lineTo(item.data[0], item.data[1]);                
                break;
            case "stroke":                
                context.stroke();                
                break;
            case "fill":                
                context.fill();
                break;
            case "strokeRGBA":                
                context.strokeStyle = "rgba(" + item.data[0] + "," + item.data[1] + "," + item.data[2] + "," + (item.data[3]/255)+")";
                break;
            case "fillRGBA":
                context.fillStyle = "rgba(" + item.data[0] + "," + item.data[1] + "," + item.data[2] + "," + (item.data[3] / 255) + ")";
                break;
            case "lineWidth":                
                context.lineWidth = item.data[0];
            case "arcCW":                
                context.arc(item.data[0], item.data[1], item.data[2], item.data[3] * Math.PI/180 , item.data[4] * Math.PI/180, false);
                break;
            case "arcCCW":                
                context.arc(item.data[0], item.data[1], item.data[2], item.data[3] * Math.PI / 180, item.data[4] * Math.PI / 180, true);
                break;
            case "testClick":                
                if (test & context.isPointInPath(px, py))
                {
                    clickedItemId = item.targetId;
                    clickedTransform = context.getTransform();
                }                                
                break;
            case "save":
                context.save();
                break;
            case "restore":
                context.restore();
                break;
            case "rotate":
                currentRotation += item.data[0];
                context.rotate((item.data[0] * Math.PI)/180);
                break;
            case "translate":                
                context.translate(item.data[0], item.data[1]);
                break;
            case "levelText":
                context.save();
                context.rotate((-currentRotation * Math.PI) / 180);
                context.font = "50px Arial";
                context.fillStyle = "white";
                context.fillText(item.targetId, 0, 0);
                context.restore();
                break;
            

        }

        

    });

    if (test)
    {
        instance.invokeMethodAsync(testEvent, clickedItemId, clickedTransform);
    }


    //createTransform(-100, -100, 1, rotation, context);

    //console.log("clickX:" + px);
    //console.log("clickY:" + py);
    context.save();
    context.rotate(1);
    context.translate(-300, -300);
    context.beginPath();    
    context.rect(-100, -100, 100, 100);    
    context.fillStyle = "red";
    context.fill();
    
    context.stroke();
    

    context.font = "30px Arial";
    context.fillStyle = "blue";
    context.fillText("Hello World", 800, 50);

    context.restore();
    

    
}

//function DrawLine(context, item)
//{
//    context.beginPath();
//    context.moveTo(item.x, item.y);
//    context.lineTo(item.x + item.width, item.y + item.height);
//    context.stroke();
//}

//function Clear(context, rect)
//{
//    context.clearRect(rect.x, rect.y, rect.x + rect.width, rect.y + rect.height);
//}

class MapView
{
    static Clear(canvas, context)
    {
        context.save();
        context.setTransform(1, 0, 0, 1, 0, 0);
        context.clearRect(0, 0, canvas.width, canvas.height);
        context.restore();
    }

    static CreateTransform(originX, originY, scale, rotate, context)
    {
        const xAxisX = Math.cos(rotate) * scale;
        const xAxisY = Math.sin(rotate) * scale;
        context.setTransform(xAxisX, xAxisY, -xAxisY, xAxisX, originX, originY);
    }

    

}

