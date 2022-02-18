using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderTopRight : Border
{
    public override void generateMatrix(ref List<List<int>> list)
    {
        base.generateMatrix(ref list);
        generateWall(ref list, 1, 2);
    }
}
