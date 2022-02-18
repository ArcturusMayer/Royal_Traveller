using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
    public static GameObject popUpTemplate;
    public static GameObject currentPopUp;
    public GameObject bufferPopUp;

    private static List<string> messages = new List<string>()
    { 
        "ƒобро пожаловать в игру  оролевский землемер! —ейчас расскажем основы работы в этой среде.  ликните по сообщению, чтобы продолжить.",
        "—лева вы видите большое текстовое поле ввода. ¬ нем необходимо писать инструкции (программы), которые будет исполн€ть землемер.",
        "«емлемер на карте справа представлен как значок компаса на одной из клеток.",
        "√лавна€ задача на уровень написана в верхней части экрана.",
        " нопка —оздать пересоздает уровень случайным образом.",
        " нопка «апуск начинает исполнение кода на карте справа.",
        "Ќа панели между этими кнопками отображаетс€ текущий статус исполнени€ кода.",
        "¬ игре предусмотрено три уровн€ сложности - Ћегкий, —редний и ¬ысокий.",
        "Ќа Ћегком у вас неограниченное число попыток решить задачу, можно поэкспериментировать.",
        "Ќа —реднем попытка одна, и после неудачи уровень пересоздаетс€ - вам придетс€ все сделать правильно с первого раза!",
        "Ќа ¬ысоком уровень пересоздаетс€ при запуске скрипта, так что вы не будете знать, на каком поле окажетесь - пишите универсальное дл€ данного типа уровн€ решение!",
        "ѕопробуем решить текущую задачу. Ќапишите слева:\nƒлина = 0\nћы объ€вили переменную дл€ хранени€ ответа. Ќажмите «апуск дл€ продолжени€.",
        "ƒалее допишите:\nидти¬низ\nƒлина = ƒлина + 1\nѕерва€ из этих строк - команда движени€ дл€ исполнител€, их четыре на каждую сторону, а втора€ - увеличение переменной на единицу, обычна€ арифметика.",
        "ѕовторите предыдущие две строчки еще три раза и запустите, должно быть четыре повторени€ этих строк.",
        "–ассмотрим услови€. ¬ игре один оператор ветвлени€ - если-иначе, и четыре однотипных команды, провер€ющих доступность клетки.   тому же можно сравнивать переменные с помощью \">\", \"<\", \"==\". Ќапишите:\nесли сверху—вободно\n\tидти¬право\n\tƒлина = ƒлина + 1\n¬ложенные команды отбиваютс€ отступами с помощью табов (Tab), как в Python.",
        "“еперь циклы. ¬ игре есть один вид циклов - пока с предусловием. Ќапишите:\nпока !сверху—вободно\n\tƒлина = ƒлина + 1\n\tидти¬право\n ак видите, услови€ можно инвертировать с помощью оператора \"!\"",
        "¬ системную переменную \"ќтвет\" надо всегда заносить результат программы.\nћы закончили написание программы за вас. «апустите и посмотрите на результат.",
        "ѕоздравл€ем! “еперь вы знакомы с основными механиками игры. ћожете попробовать силы в остальных уровн€х.  ликните сообщение, чтобы выйти, или воспользуйтесь кнопкой выхода."
    };
    private static List<string> templates = new List<string>()
    {
        "ƒлина = 0",
        "ƒлина = 0\nидти¬низ\nƒлина = ƒлина + 1",
        "ƒлина = 0\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1",
        "ƒлина = 0\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1\nесли сверху—вободно\n\tидти¬право\n\tƒлина = ƒлина + 1",
        "ƒлина = 0\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1\nесли сверху—вободно\n\tидти¬право\n\tƒлина = ƒлина + 1\nпока !сверху—вободно\n\tƒлина = ƒлина + 1\n\tидти¬право",
        "ƒлина = 0\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1\nесли сверху—вободно\n\tидти¬право\n\tƒлина = ƒлина + 1\nпока !сверху—вободно\n\tƒлина = ƒлина + 1\n\tидти¬право\nидти¬верх\nƒлина = ƒлина + 1\nпока !слева—вободно\n\tидти¬верх\n\tƒлина = ƒлина + 1\nидти¬лево\nƒлина = ƒлина + 1\nпока !снизу—вободно\n\tидти¬лево\n\tƒлина = ƒлина + 1\nќтвет = ƒлина",
        "ƒлина = 0\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1\nидти¬низ\nƒлина = ƒлина + 1\nесли сверху—вободно\n\tидти¬право\n\tƒлина = ƒлина + 1\nпока !сверху—вободно\n\tƒлина = ƒлина + 1\n\tидти¬право\nидти¬верх\nƒлина = ƒлина + 1\nпока !слева—вободно\n\tидти¬верх\n\tƒлина = ƒлина + 1\nидти¬лево\nƒлина = ƒлина + 1\nпока !снизу—вободно\n\tидти¬лево\n\tƒлина = ƒлина + 1\nќтвет = ƒлина"
    };
    private static int counter = 0;
    private static int skipCount = 12;
    private static bool isNextAllowed = false;

    private static void showPopUp(string text, float x = 300, float y = 0)
    {
        currentPopUp = Instantiate(popUpTemplate, new Vector3(x, y, -0.1f), Quaternion.Euler(0, 0, 0));
        currentPopUp.transform.SetParent((GameObject.Find("Canvas")).transform);
        currentPopUp.transform.localScale = Vector3.one;
        currentPopUp.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        currentPopUp.GetComponentInChildren<Text>().text = text;
        currentPopUp.GetComponentInChildren<Button>().onClick.AddListener(onPopUpClick);
        counter++;
    }

    private static void removePopUp()
    {
        Destroy(currentPopUp.gameObject);
    }

    public static void onRunClick()
    {
        if (counter >= skipCount)
        {
            CodeExecutor.setTextTemplate(templates[counter - skipCount]);
            removePopUp();
        }
    }

    public static void onGenClick()
    {

    }

    public static void onExecutionFailed()
    {
        counter--;
        showPopUp(messages[counter]);
    }

    public static void onExecutionSucceed()
    {
        if (counter == 16)
        {
            CodeExecutor.setCodeManually(templates[5]);
        }
        //CodeExecutor.setCodeManually(templates[counter - skipCount + 1]);
        showPopUp(messages[counter]);
    }

    public static void onPopUpClick()
    {
        if(counter < skipCount)
        {
            removePopUp();
            showPopUp(messages[counter]);
        }
        else if (counter == 18)
        {
            clearTutorial();
            CodeExecutor.clearLevel();
            SceneManager.LoadScene("Menu");
        }
    }

    public static void clearTutorial()
    {
        counter = 0;
    }

    public static string rollBack()
    {
        if (counter > skipCount)
        {
            return templates[counter - skipCount - 1];
        }
        else
        {
            return "";
        }
    }

    void Start()
    {
        popUpTemplate = bufferPopUp;
        if (CodeExecutor.isTutorial)
        {
            showPopUp(messages[counter]);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
