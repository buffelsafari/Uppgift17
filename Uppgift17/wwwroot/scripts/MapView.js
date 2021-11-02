
function AddResizeListener(instance, method)
{
    window.addEventListener("resize", (event) =>
    {
        instance.invokeMethodAsync(method);
    });

}


function RequestAnimationFrame(instance, method, canvas)
{ 
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;

    window.requestAnimationFrame((event) =>
    {
        instance.invokeMethodAsync(method);
    });
}


function createTransform(originX, originY, scale, rotate, context)
{
    const xAxisX = Math.cos(rotate) * scale;
    const xAxisY = Math.sin(rotate) * scale;
    context.setTransform(xAxisX, xAxisY, -xAxisY, xAxisX, originX, originY);
}

function Draw(canvas, list, scale, transX, transY, rotation)
{
    //console.log(list);
    let context = canvas.getContext("2d");
    //console.log("matrix----");
    //console.log(matrix);

    //context.translate(1000, 1000);
    //context.scale(0.5, 0.5);
    //context.translate(-1000,-1000);
    //context.setTransform(matrix.m11, matrix.m12, matrix.m21, matrix.m22, matrix.m31, matrix.m32);

    let offsetX = canvas.width/2;
    let offsetY = canvas.height/2;

    

    //context.translate(-offsetX, -offsetY);

    //context.rotate(0.78);


    createTransform(offsetX, offsetY, scale, rotation, context);

    //context.setTransform(1, 0, 0, 1, 0, 0);
    context.translate(transX, transY);
    
    
    //context.rotate(0.1);
    //context.scale(1.1, 1.1);
    //context.transform(1, 0, 0, 1, offsetX, offsetY);
    

    list.forEach(item =>
    {
        
        console.log(item);


        switch (item.operation)
        {
            case "clear":                
                MapView.Clear(canvas, context);
                break;
            case "beginPath":                
                context.beginPath();
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
            case "lineWidth":                
                context.lineWidth = item.data[0];
            case "arcCW":                
                context.arc(item.data[0], item.data[1], item.data[2], item.data[3] * Math.PI/180 , item.data[4] * Math.PI/180, false);
                break;
            case "arcCCW":                
                context.arc(item.data[0], item.data[1], item.data[2], item.data[3] * Math.PI / 180, item.data[4] * Math.PI / 180, true);
                break;
            

        }

        

    });

    

    context.beginPath();
    context.rect(20, 20, 150, 100);
    context.fillStyle = "red";
    context.fill();
    context.stroke();
   
    context.font = "30px Arial";
    context.fillText("Hello World", 800, 50);


    

    
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
        context.clearRect(0, 0, canvas.width, canvas.height);
    }

    

}

