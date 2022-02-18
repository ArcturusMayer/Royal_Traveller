using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public static int level = 0;
    public static int difficulty = 0;
    public static int user_id = 0;
    public int currentLevel = 0;
    private int currentDifficulty = 0;

    public Text levelName;
    public Text difName;

    public StarHandler star_1;
    public StarHandler star_2;
    public StarHandler star_3;

    public static int[] starValue;

    public string name = "Площадь 1";
    public string[] difficulties = { "Легкий", "Средний", "Высокий" };

    // Start is called before the first frame update
    void Start()
    {
        user_id = StartMenuHandler.user_id;
        levelName.text = name;
        difName.text = difficulties[currentDifficulty];
        star_1.setStarMode(getStarData(0));
        star_2.setStarMode(getStarData(1));
        star_3.setStarMode(getStarData(2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickLevel()
    {
        level = currentLevel;
        difficulty = currentDifficulty;
        starValue = DBHandler.getStars(user_id, level);
        SceneManager.LoadScene("MainGame");
    }

    public void OnClickLeft()
    {
        currentDifficulty = (currentDifficulty + 2) % 3;
        difName.text = difficulties[currentDifficulty];
    }

    public void OnClickRight()
    {
        currentDifficulty = (currentDifficulty + 1) % 3;
        difName.text = difficulties[currentDifficulty];
    }

    private bool getStarData(int number)
    {
        return (DBHandler.getStars(user_id, currentLevel)[number] == 1) ? true : false;
    }
}
