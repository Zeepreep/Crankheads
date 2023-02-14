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
    float gravity = 0.2f;
    float jumpStrength = 3;
    int _score;
    Sound jumpSound;
    float vy;
    bool isMoving;

    /// <summary>
    /// Player with a pre-defined sprite.
    /// </summary>
    /// <param name="obj"></param>
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
    /// <param name="FileName"></param>
    /// <param name="cols"></param>
    /// <param name="rows"></param>
    /// <param name="obj"></param>
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
        vy += gravity;
        if (Input.GetKeyDown(Key.SPACE))
        {
            vy = -jumpStrength;
            jumpSound.Play();
        }
    }

    /// <summary>
    /// Checks for collision with pipes and promptly kills the player.
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
        Collision colY = MoveUntilCollision(0, vy);
        if (colY != null)
        {
            Console.WriteLine("Pipe Touched on Y-axis!");
            ((MyGame)game).GameOver();
        }
    }

    /// <summary>
    /// Handles the invisible pickup between the pipes.
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

                ((MyGame)game).currentLevel.pipeLoader.createPipe();

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


        /*
        counter++;
        if (counter == 10) {
            counter = 0;
            SetFrame(currentFrame + 1);
            if (currentFrame == frameCount) {
               currentFrame = 0;
            }
        } */
    }
}

