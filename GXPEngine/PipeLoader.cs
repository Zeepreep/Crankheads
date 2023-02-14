using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using GXPEngine;

class PipeLoader : AnimationSprite
{
    const int pipeGap = 175;
    int pipeToCreate = 5;
    int pipesCreated = 0;

    public PipeLoader(string fileName, int cols, int rows, TiledObject obj) : base(fileName, cols, rows, addCollider:false)
    {
        this.visible = false;

        this.scale = 0;
    }

    /// <summary>
    /// Creates the pipes.
    /// </summary>
    public void createPipe()
    {
        Pipe pipe = new Pipe();
        parent.AddChild(pipe);
        pipe.SetXY(x + (pipesCreated * pipeGap), (game.height / 2) + Utils.Random(-50, 50));
        pipesCreated++;
    }

    void Update()
    {
       while (pipeToCreate > 0)
        {
            createPipe();
            pipeToCreate--;
        }

    }
}
