using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleClause
{
    public static bool isTopFree()
    {
        if(GenerateLevel.matrixOfLevel[(GenerateLevel.getHeroY() > 0) ? GenerateLevel.getHeroY() - 1 : GenerateLevel.getHeroY()][GenerateLevel.getHeroX()] != 2 && GenerateLevel.getHeroY() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool isRightFree()
    {
        if (GenerateLevel.matrixOfLevel[GenerateLevel.getHeroY()][(GenerateLevel.getHeroX() < 9) ? GenerateLevel.getHeroX() + 1 : GenerateLevel.getHeroX()] != 2 && GenerateLevel.getHeroX() < 9)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool isBottomFree()
    {
        if (GenerateLevel.matrixOfLevel[(GenerateLevel.getHeroY() < 7) ? GenerateLevel.getHeroY() + 1 : GenerateLevel.getHeroY()][GenerateLevel.getHeroX()] != 2 && GenerateLevel.getHeroY() < 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool isLeftFree()
    {
        if (GenerateLevel.matrixOfLevel[GenerateLevel.getHeroY()][(GenerateLevel.getHeroX() > 0) ? GenerateLevel.getHeroX() - 1 : GenerateLevel.getHeroX()] != 2 && GenerateLevel.getHeroX() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
