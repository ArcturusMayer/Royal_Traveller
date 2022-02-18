using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFunctions
{


    public static void moveUp()
    {
        GenerateLevel.moveHero(0);
    }

    public static void moveRight()
    {
        GenerateLevel.moveHero(1);
    }

    public static void moveDown()
    {
        GenerateLevel.moveHero(2);
    }

    public static void moveLeft()
    {
        GenerateLevel.moveHero(3);
    }
}
