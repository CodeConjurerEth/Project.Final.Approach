using System;
using GXPEngine;

public class MapScreen : GameObject
{

	public MapScreen()
	{
        Sprite background = new Sprite("mapPrototype.png", false);
        AddChild(background);

        Player player = new Player();
        AddChild(player);
    }
}
