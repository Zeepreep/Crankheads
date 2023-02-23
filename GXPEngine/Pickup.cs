using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class Pickup : AnimationSprite {

        Sound pickupSound;
    public Pickup(TiledObject obj=null) : base("square.png", 1, 1) {
        collider.isTrigger = true;

        pickupSound = new Sound("sfx_point.wav", false, false);
    }

    /// <summary>
    /// Handles audio and destruction for pickups.
    /// </summary>
    public void Grab()
        {
           /* pickupSound.Play();
            Console.WriteLine("Pickup Grabbed!"); */
            LateDestroy();
        }

    /// <summary>
    /// Creates the pickups used in Tiled.
    /// </summary>

    public Pickup(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, rows, cols) {
        collider.isTrigger = true;
    }

    //void Update()
    //{

    //}
}
