using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using GXPEngine;

class PipeLoader : AnimationSprite
{
    static int pipeDistance;
    static int pipeToCreate;
    int pipesCreated = 0;
    public static bool infinitePipes = false;
    public static int winScore = 100;
    int resizeScore;
    int resizeGap; 
    int originalGap;
    public PipeLoader(string fileName, int cols, int rows, TiledObject obj=null) : base(fileName, cols, rows, addCollider:false)
    {
        Initialize(obj);

        this.visible = false;

        this.scale = 0;
    }

    /// <summary>
    /// Sets up the customizable properties for use in Tiled.
    /// </summary>
    void Initialize(TiledObject obj=null)
    {
        if (obj != null)
        {
            pipeToCreate = obj.GetIntProperty("PipeAmount", 10);
            infinitePipes = obj.GetBoolProperty("InfinitePipes", true);
            winScore = obj.GetIntProperty("WinScore", 10);
            pipeDistance = obj.GetIntProperty("BetweenPipeDistance", 175);
            originalGap = obj.GetIntProperty("PipeGap", 75);
            resizeScore = obj.GetIntProperty("GapResizeScore", 10);
            resizeGap = obj.GetIntProperty("PipeGapResized", 50);

        }
    }

    /// <summary>
    /// Creates the pipes.
    /// </summary>
    public void createPipe()
    {
        Pipe pipe = new Pipe();
        parent.AddChild(pipe);
        pipe.SetXY(x + (pipesCreated * pipeDistance), (game.height / 2) + Utils.Random(-50, 50));
        pipesCreated++;
    }

    /// <summary>
    /// Allows the gap between the pipes to be resized after a certain score.
    /// </summary>
    void pipeResize()
    {
        if (pipesCreated < resizeScore + 1 || pipeToCreate < resizeScore + 1)
        {
            Pipe.gap = originalGap;
        }
        else
        {
            Pipe.gap = resizeGap;
        }
    }


    void Update()
    {
        pipeResize();
        
        while (pipeToCreate > 0) 
        {
            createPipe();
            pipeToCreate--;
        }
    }
}
