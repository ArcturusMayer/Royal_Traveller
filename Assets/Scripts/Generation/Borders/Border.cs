using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : Generation
{

    public override void generateMatrix(ref List<List<int>> list)
    {
        clearMatrix(ref list);
    }

    protected void generateWall(ref List<List<int>> list, int pointOne, int pointTwo)
    {
        int pointOneX = Random.Range((pointOne == 1) ? 2 : ((pointOne == 2) ? 9 : ((pointOne == 3) ? 5 : ((pointOne == 4) ? 0 : 0))), (pointOne == 1) ? 5 : ((pointOne == 2) ? 10 : ((pointOne == 3) ? 8 : ((pointOne == 4) ? 1 : 0))));
        int pointOneY = Random.Range((pointOne == 1) ? 0 : ((pointOne == 2) ? 4 : ((pointOne == 3) ? 7 : ((pointOne == 4) ? 2 : 5))), (pointOne == 1) ? 1 : ((pointOne == 2) ? 6 : ((pointOne == 3) ? 8 : ((pointOne == 4) ? 4 : 3))));
        int pointTwoX = Random.Range((pointTwo == 1) ? 2 : ((pointTwo == 2) ? 9 : ((pointTwo == 3) ? 5 : ((pointTwo == 4) ? 0 : 0))), (pointTwo == 1) ? 5 : ((pointTwo == 2) ? 10 : ((pointTwo == 3) ? 8 : ((pointTwo == 4) ? 1 : 0))));
        int pointTwoY = Random.Range((pointTwo == 1) ? 0 : ((pointTwo == 2) ? 4 : ((pointTwo == 3) ? 7 : ((pointTwo == 4) ? 2 : 5))), (pointTwo == 1) ? 1 : ((pointTwo == 2) ? 6 : ((pointTwo == 3) ? 8 : ((pointTwo == 4) ? 4 : 3))));

        int currentX = pointOneX;
        int currentY = pointOneY;

        int width = Mathf.Abs(pointOneX - pointTwoX);
        int height = Mathf.Abs(pointOneY - pointTwoY);
        int length = width + height;

        list[pointOneY][pointOneX] = 2;

        for (int i = 0; i < length; i++)
        {
            int choice = Random.Range(1, width + height + 1);
            if(choice >= 1 && choice <= width)
            {
                if(pointOneX < pointTwoX)
                {
                    currentX++;
                }
                else if(pointOneX > pointTwoX)
                {
                    currentX--;
                }
                list[currentY][currentX] = 2;
                --width;
            }else if(choice >= width + 1 && choice <= width + height)
            {
                if (pointOneY < pointTwoY)
                {
                    currentY++;
                }
                else if (pointOneY > pointTwoY)
                {
                    currentY--;
                }
                list[currentY][currentX] = 2;
                --height;
            }
        }

        answer = generateAnswer(list, pointOne, pointTwo);
        Debug.Log(answer);
    }

    private int generateAnswer(List<List<int>> list, int pointOne, int pointTwo)
    {
        int offsetX = ((pointOne + pointTwo == 3) ? 1 : ((pointOne + pointTwo == 7) ? 1 : ((pointOne + pointTwo == 4) ? 1 : ((pointOne + pointTwo == 6) ? 1 : ((pointTwo == 3 || pointTwo == 2) ? -1 : ((pointTwo == 1 || pointTwo == 4) ? 1 : 0 ))))));
        int offsetY = ((pointOne + pointTwo == 3) ? -1 : ((pointOne + pointTwo == 7) ? -1 : ((pointOne + pointTwo == 4) ? -1 : ((pointOne + pointTwo == 6) ? -1 : ((pointTwo == 3 || pointTwo == 2) ? -1 : ((pointTwo == 1 || pointTwo == 4) ? 1 : 0 ))))));
        int result = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (list[i][j] == 2)
                {
                    try
                    {
                        if (list[i + offsetX][j] != 2)
                        {
                            list[i + offsetX][j] = 4;
                        }
                    }
                    catch { }
                    try
                    {
                        if (list[i][j + offsetY] != 2)
                        {
                            list[i][j + offsetY] = 4;
                        }
                    }
                    catch { }
                    try
                    {
                        if (list[i + offsetX][j + offsetY] != 2)
                        {
                            list[i + offsetX][j + offsetY] = 4;
                        }
                    }
                    catch { }
                }
            }
        }

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if(list[i][j] == 4)
                {
                    result++;
                }
            }
        }

        list[7][0] = 3;

        return result;
    }
}
