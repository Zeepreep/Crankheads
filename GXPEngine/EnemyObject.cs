using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

class EnemyObject : Pivot
{

    Pickup pickup;
    public static int gap;

    /// <summary>
    /// Makes the preset for the EnemySpawner to use.
    /// </summary>
    public EnemyObject()
    {
        pickup = new Pickup();
        pickup.visible = true;

        
        AddChild(pickup);

        pickup.SetOrigin(pickup.width / 2, pickup.height / 2);

        pickup.SetXY(0, 0);
        //pickup.scaleX = 0.4f;
        pickup.scaleY = (float)gap / (float)pickup.height;
    }  

}
