using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class Player : AnimationSprite {

    float speed = 2;
    //float gravity = 0.2f;
    float jumpStrength = 2;
    int _score;
    Sound jumpSound;
    float vy;
    bool isMoving;

    /// <summary>
    /// Player with a pre-defined sprite.
    /// </summary>
    public Player(TiledObject obj = null) : base("flappy-sheet.png", 3, 1)
    {
        scale = 1;
        SetCycle(0, 2);
        SetXY(0, 0);
        _score = 0;
    }

    /// <summary>
    /// Player with parameters for tiled.
    /// </summary>

    public Player(string FileName, int cols, int rows, TiledObject obj = null) : base(FileName, cols, rows)
    {
        scale = 1;
        SetCycle(0, 3);
        SetXY(0, 0);
        jumpSound = new Sound("sfx_wing.wav", false, false);
    }

    /// <summary>
    /// Ensures that the player moves to the right.
    /// </summary>
    /// <returns></returns>
    float GetHorizontalInput()
    {
        float dx = 0;
            dx += speed;
            Mirror(false, false);
            isMoving = true;
        return dx;
    }

    /// <summary>
    /// Adds gravity and jumping mechanics.
    /// </summary>
    void Move()
    {
        //vy += gravity;
        //if (Input.GetKeyDown(Key.SPACE))
        //{
        //    vy = -jumpStrength;
        //    jumpSound.Play();
        //}

        if (Input.GetKeyDown(Key.UP)) {
            vy = +jumpStrength;
        }

        if (Input.GetKey(Key.DOWN))
        {
            vy = -jumpStrength;
        }
    }

    /// <summary>
    /// Checks for collision with pipes or ground and promptly kills the player.
    /// </summary>
    void DeathCheck()
    {
        float dx = GetHorizontalInput();

        Collision colX = MoveUntilCollision(dx, 0);
        if (colX != null)
        {
            Console.WriteLine("Pipe Touched on X-axis!");
            ((MyGame)game).GameOver();
        }
        //Collision colY = MoveUntilCollision(0, vy);
        //if (colY != null)
        //{
        //    Console.WriteLine("Pipe Touched on Y-axis!");
        //    ((MyGame)game).GameOver();
        //}
        if (y > game.height)
        {
            Console.WriteLine("Ground Touched!");
            ((MyGame)game).GameOver();
        }
    }

    /// <summary>
    /// Handles the invisible pickup between the pipes.
    /// When it is picked up there is 1 point added to the score and a new pipe is created.
    /// </summary>
    void Collisions()
    {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i] is Pickup)
            {
                ((Pickup)collisions[i]).Grab();

                _score += 1;

                if (PipeLoader.infinitePipes == true)
                {
                ((MyGame)game).currentLevel.pipeLoader.createPipe();
                }
            }
        }
    }


    void Update() {

        HUD.main.score = _score;

        if (isMoving)
        {
            Animate(0.1f);
        }

        DeathCheck();
        Collisions();
        Move();
    }
}

