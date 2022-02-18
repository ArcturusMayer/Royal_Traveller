using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Width : Generation
{
    public override void generateMatrix(ref List<List<int>> list)
    {
        clearMatrix(ref list);
        generatePassage(ref list);
    }

    protected void generatePassage(ref List<List<int>> list)
    {
        int topY = Random.Range(0, 3);
        int passageWidth = Random.Range(2, 5);

        int prevTop = 0;
        int prevBottom = 0;
        int currentTop = 0;
        int currentBottom = 0;

        int maxTop = 0;
        int minBottom = 7;

        for(int i = 0; i < 10; i++)
        {
            prevTop = currentTop;
            prevBottom = currentBottom;

            currentTop = Random.Range(0, topY + 1);
            if(maxTop < currentTop)
            {
                maxTop = currentTop;
            }
            currentBottom = Random.Range(topY + passageWidth + 1, 8);
            if(minBottom > currentBottom)
            {
                minBottom = currentBottom;
            }

            list[currentTop][i] = 2;
            list[currentBottom][i] = 2;

            if (i > 0)
            {
                //Top wall fix
                if(currentTop - prevTop > 1)
                {
                    for (int j = 1; j < currentTop - prevTop; j++) {
                        list[currentTop - j][i] = 2;
                    }
                }else if(prevTop - currentTop > 1)
                {
                    for (int j = 1; j < prevTop - currentTop; j++)
                    {
                        list[prevTop - j][i - 1] = 2;
                    }
                }
                //Bottom wall fix
                if (currentBottom - prevBottom > 1)
                {
                    for (int j = 1; j < currentBottom - prevBottom; j++)
                    {
                        list[currentBottom - j][i - 1] = 2;
                    }
                }
                else if (prevBottom - currentBottom > 1)
                {
                    for (int j = 1; j < prevBottom - currentBottom; j++)
                    {
                        list[prevBottom - j][i] = 2;
                    }
                }
            }
        }

        list[Random.Range(topY + 1, topY + passageWidth)][Random.Range(0, 10)] = 3;

        answer = minBottom - maxTop - 1;
        Debug.Log(answer);
    }
}
