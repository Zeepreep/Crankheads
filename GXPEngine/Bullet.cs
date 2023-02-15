using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

    class Bullet : AnimationSprite
    {
    
    float speed;
        
    public Bullet(TiledObject obj = null) : base("Basic_Submarine.png", 1, 1) {
        collider.isTrigger = true;

    }

    public void Movement()
    {
        float dx = 0;
        dx += speed;
    }
}