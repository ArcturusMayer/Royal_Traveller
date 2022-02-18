using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : Generation
{
    private TutorialHandler tutorialHandler;

    public override void generateMatrix(ref List<List<int>> list)
    {
        clearMatrix(ref list);
        generateCube(ref list);
    }

    protected void generateCube(ref List<List<int>> list)
    {
        int leftTopX = Random.Range(2, 7);
        int leftTopY = Random.Range(2, 5);
        int rightBottomX = leftTopX + 2;
        int rightBottomY = leftTopY + 2;

        for (int i = leftTopX; i <= rightBottomX; i++)
        {
            list[leftTopY][i] = 2;
            list[rightBottomY][i] = 2;
        }
        for (int i = leftTopY; i <= rightBottomY; i++)
        {
            list[i][leftTopX] = 2;
            list[i][rightBottomX] = 2;
        }

        list[leftTopY - 1][leftTopX - 1] = 3;

        answer = 16;
        Debug.Log(answer);
    }
}
