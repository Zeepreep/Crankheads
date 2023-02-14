using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

class Pipe : Pivot
{

    Pickup pickup;
    int gap = 80;

    /// <summary>
    /// Makes the preset for the PipeLoader to use.
    /// </summary>
    public Pipe()
    {
        Sprite TopPipe = new Sprite("pipe-top.png");
        Sprite BottomPipe = new Sprite("pipe-bottom.png");
        pickup = new Pickup();
        pickup.visible = false;
        

        AddChild(TopPipe);
        AddChild(BottomPipe);
        AddChild(pickup);

        TopPipe.SetOrigin(TopPipe.width / 2, TopPipe.height);
        BottomPipe.SetOrigin(BottomPipe.width / 2, 0);
        pickup.SetOrigin(pickup.width / 2, pickup.height / 2);

        TopPipe.SetXY(0, -gap/2f);
        BottomPipe.SetXY(0, gap/2f);
        pickup.SetXY(0, 0);
        pickup.scaleX = 0.4f;
        pickup.scaleY = (float)gap / (float)pickup.height;
        Console.WriteLine(pickup.scaleY);
    }  
}
