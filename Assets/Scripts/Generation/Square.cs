using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Generation
{
    public override void generateMatrix(ref List<List<int>> list)
    {
        clearMatrix(ref list);
        generateRect(ref list);
    }

    protected void generateRect(ref List<List<int>> list)
    {
        int leftTopX = Random.Range(0, 5);
        int leftTopY = Random.Range(0, 3);
        int rightBottomX = leftTopX + 5 + Random.Range(0, 5 - leftTopX);
        int rightBottomY = leftTopY + 5 + Random.Range(0, 3 - leftTopY);

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

        list[Random.Range(leftTopY + 1, rightBottomY)][Random.Range(leftTopX + 1, rightBottomX)] = 3;

        answer = Mathf.Abs((rightBottomX - leftTopX - 1) * (rightBottomY - leftTopY - 1));
    }
}
