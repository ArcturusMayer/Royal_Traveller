using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLevel : MonoBehaviour
{
    public float width = 1.2f;
    public float height = 1.2f;

    public float cenrterOfLeftTopCellX = -2.625f;
    public float cenrterOfLeftTopCellY = 4.215f;

    public static List<List<int>> matrixOfLevel;

    public GameObject hillCell;
    public GameObject hillCell2;
    public GameObject mountCell;
    public GameObject hero;

    public static GameObject actualHero;
    private static int heroX;
    private static int heroY;

    public static Generation generation;
    public static bool isTutorial = false;
    public Text task;

    // Start is called before the first frame update
    void Start()
    {
        matrixOfLevel = new List<List<int>>();
        fillMatrix(ref matrixOfLevel);
        chooseGeneration(ButtonHandler.level);
        GenerateButtonAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateButtonAction()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);

        }
        generation.generateMatrix(ref matrixOfLevel);
        generateLevel(ref matrixOfLevel);
    }

    private void chooseGeneration(int numberOfGen)
    {
        switch (numberOfGen)
        {
            case 1:
                generation = new Tutorial();
                CodeExecutor.setTutorial(true);
                task.text = "Ќайдите количество окружающих эти горы клеток";
                break;
            case 2:
                generation = new Square();
                task.text = "Ќайдите внутреннюю площадь этой долины меж скал";
                break;
            case 3:
                generation = new Width();
                task.text = "Ќайдите минимальную ширину этого перевала";
                break;
            case 4:
                generation = new BorderHorizontal();
                task.text = "ѕосчитайте количество клеток с воротами";
                break;
            case 5:
                generation = new BorderVertical();
                task.text = "ѕосчитайте количество клеток с воротами";
                break;
            case 6:
                generation = new BorderTopLeft();
                task.text = "ѕосчитайте количество клеток с воротами";
                break;
            case 7:
                generation = new BorderBottomRight();
                task.text = "ѕосчитайте количество клеток с воротами";
                break;
            case 8:
                generation = new BorderTopRight();
                task.text = "ѕосчитайте количество клеток с воротами";
                break;
        }
    }

    private void generateLevel(ref List<List<int>> list)
    {
        float currentCellCenterX = cenrterOfLeftTopCellX;
        float currentCellCenterY = cenrterOfLeftTopCellY;
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                if(list[i][j] == 1)
                {
                    Instantiate(hillCell, new Vector3(currentCellCenterX, currentCellCenterY, 0f), Quaternion.Euler(0, 0, 0), gameObject.transform);
                }
                else if(list[i][j] == 2)
                {
                    Instantiate(mountCell, new Vector3(currentCellCenterX, currentCellCenterY, 0f), Quaternion.Euler(0, 0, 0), gameObject.transform);
                }
                else if(list[i][j] == 3)
                {
                    Instantiate(hillCell, new Vector3(currentCellCenterX, currentCellCenterY, 0f), Quaternion.Euler(0, 0, 0), gameObject.transform);
                    actualHero = Instantiate(hero, new Vector3(currentCellCenterX, currentCellCenterY, -0.1f), Quaternion.Euler(0, 0, 0), gameObject.transform);
                    heroX = j;
                    heroY = i;
                }
                else if (list[i][j] == 4)
                {
                    Instantiate(hillCell2, new Vector3(currentCellCenterX, currentCellCenterY, 0f), Quaternion.Euler(0, 0, 0), gameObject.transform);
                }
                currentCellCenterX += width;
            }
            currentCellCenterX = cenrterOfLeftTopCellX;
            currentCellCenterY -= height;
        }
    }

    private void fillMatrix(ref List<List<int>> list)
    {
        for (int i = 0; i < 8; i++)
        {
            List<int> buffer = new List<int>();
            for (int j = 0; j < 10; j++)
            {
                buffer.Add(1);
            }
            list.Add(buffer);
        }
    }

    public static int getHeroX()
    {
        return heroX;
    }

    public static int getHeroY()
    {
        return heroY;
    }

    public static void moveHero(int direction)
    {
        switch (direction)
        {
            case 0:
                if (matrixOfLevel[(heroY > 0) ? heroY - 1 : heroY][heroX] != 2 && heroY > 0)
                {
                    matrixOfLevel[heroY][heroX] = 1;
                    actualHero.transform.position = new Vector3(actualHero.transform.position.x, actualHero.transform.position.y + 1.2f, actualHero.transform.position.z);
                    heroY--;
                    matrixOfLevel[heroY][heroX] = 3;
                }
                break;
            case 1:
                if (matrixOfLevel[heroY][(heroX < 9) ? heroX + 1 : heroX] != 2 && heroX < 9)
                {
                    matrixOfLevel[heroY][heroX] = 1;
                    actualHero.transform.position = new Vector3(actualHero.transform.position.x + 1.2f, actualHero.transform.position.y, actualHero.transform.position.z);
                    heroX++;
                    matrixOfLevel[heroY][heroX] = 3;
                }
                break;
            case 2:
                if (matrixOfLevel[(heroY < 7) ? heroY + 1 : heroY][heroX] != 2 && heroY < 7)
                {
                    matrixOfLevel[heroY][heroX] = 1;
                    actualHero.transform.position = new Vector3(actualHero.transform.position.x, actualHero.transform.position.y - 1.2f, actualHero.transform.position.z);
                    heroY++;
                    matrixOfLevel[heroY][heroX] = 3;
                }
                break;
            case 3:
                if (matrixOfLevel[heroY][(heroX > 0) ? heroX - 1 : heroX] != 2 && heroX > 0)
                {
                    matrixOfLevel[heroY][heroX] = 1;
                    actualHero.transform.position = new Vector3(actualHero.transform.position.x - 1.2f, actualHero.transform.position.y, actualHero.transform.position.z);
                    heroX--;
                    matrixOfLevel[heroY][heroX] = 3;
                }
                break;
        }
    }
}
