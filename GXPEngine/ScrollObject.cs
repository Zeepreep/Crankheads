using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class ScrollObject : AnimationSprite {

    int cameraSpeed = 7;

    public ScrollObject(TiledObject obj = null) : base("Basic_Submarine.png", 1, 1)
    {
        SetXY(0, 0);
        Initialize(obj);
    }

    void Initialize(TiledObject obj = null)
    {
        this.visible = false;
        this.scale = 0;

        if (obj != null)
        {
            scale = obj.GetIntProperty("PlayerScale", 1);
        }
    }
    float GetHorizontalInput()
    {
        float dx = 0;
        dx += cameraSpeed;
        Mirror(false, false);
        //isMoving = true;
        return dx;
    }

    void Movement()
    {
        float dx = GetHorizontalInput();

        Collision colX = MoveUntilCollision(dx, 0);
    }


    void Update()
    {
        Movement();
        Initialize();
    }
}

