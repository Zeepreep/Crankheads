using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Hosting;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

class Button : GameObject
{
    Sprite visualButton;
    string filename;
    bool activateHUD;

    public Button(Sprite visualButton, TiledObject obj)
    {
        this.visualButton = visualButton;
        filename = obj.GetStringProperty("load", "map1");
        activateHUD = obj.GetBoolProperty("hud", false);
    }


    /// <summary>
    /// Sets up the clickable text for the main menu.
    /// Makes sure the text is clickable and loads the level when clicked.
    /// </summary>
    void Update()
    {
        if (visualButton.HitTestPoint(Input.mouseX, Input.mouseY))
        {
            visualButton.SetColor(0.9f, 0.9f, 0.9f);
            if (Input.GetMouseButtonDown(0))
            {
                ((MyGame)game).LoadLevel(filename + ".tmx");
                HUD.hudNeeded = activateHUD;
                EnemyObject.gap = 75;
            }
        } else {
            visualButton.SetColor(1, 1, 1);
        }
    }
}

