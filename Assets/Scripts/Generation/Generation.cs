using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Generation
{
    protected int answer = 0;

    public abstract void generateMatrix(ref List<List<int>> list);

    protected void clearMatrix(ref List<List<int>> list)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                list[i][j] = 1;
            }
        }
    }

    public int getAnswer()
    {
        return answer;
    }
}
