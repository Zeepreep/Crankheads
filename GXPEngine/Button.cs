using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class Button : GameObject
{
    Sprite visualButton;
    string filename;

    public Button(Sprite visualButton, TiledObject obj)
    {
        this.visualButton = visualButton;
        filename = obj.GetStringProperty("load", "map1");
    }


    /// <summary>
    /// Sets up the clickable text for the main menu.
    /// </summary>
    void Update()
    {
        if (visualButton.HitTestPoint(Input.mouseX, Input.mouseY))
        {
            visualButton.SetColor(1, 1, 1);
            if (Input.GetMouseButtonDown(0))
            {
                ((MyGame)game).LoadLevel(filename + ".tmx");
            }
        } else {
            visualButton.SetColor(0.7f, 0.7f, 0.7f);
        }
    }
}

