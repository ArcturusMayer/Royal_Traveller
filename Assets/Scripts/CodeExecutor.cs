using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CodeExecutor : MonoBehaviour
{
    public int targetFrameRate = 30;

    public static InputField codeInput;
    public static Text result;

    private static string rawCode;

    public const float tickTime = 0.1f;
    private static float timeBuffer = 0;

    public static bool isExecute = false;
    public static bool isTutorial = false;
    private static string textTemplate = "";

    private static Instruction commands;

    private static int levelDifficulty = 0;
    private static int level = 0;
    private static int user_id;
    private static int star = 0;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
        result = GameObject.Find("Result").GetComponentInChildren<Text>();
        codeInput = GameObject.Find("InputField").GetComponent<InputField>();
        levelDifficulty = ButtonHandler.difficulty;
        level = ButtonHandler.level;
        user_id = ButtonHandler.user_id;
        star = ButtonHandler.starValue[levelDifficulty];
        if (!isTutorial)
        {
            setCodeManually(DBHandler.getCode(user_id, level, levelDifficulty));
        }
        generateButtonAction();
    }

    // Update is called once per frame
    void Update()
    {
        if (isExecute)
        {
            if(Time.time - timeBuffer > tickTime)
            {
                commands = commands.goNext();
                timeBuffer = Time.time;
            }
        }
    }

    private void OnDestroy()
    {
        clearLevel();
    }

    public static void setTutorial(bool isTutorial)
    {
        CodeExecutor.isTutorial = isTutorial;
    }

    public static void setTextTemplate(string text)
    {
        textTemplate = text;
    }

    public static void setCodeManually(string code)
    {
        codeInput.text = code;
    }

    public void runActionButton()
    {
        Variable.clearVariables();
        if (isTutorial)
        {
            TutorialHandler.onRunClick();
            if(codeInput.text.Trim() != textTemplate)
            {
                codeInput.text = TutorialHandler.rollBack();
                TutorialHandler.onExecutionFailed();
                return;
            }
            GameObject.Find("GameManager").GetComponent<GenerateLevel>().GenerateButtonAction();
        }
        if (levelDifficulty == 2 && !isTutorial)
        {
            GameObject.Find("GameManager").GetComponent<GenerateLevel>().GenerateButtonAction();
        }
        generateButtonAction();
        rawCode = codeInput.text;
        if(rawCode[rawCode.Length - 1] == '\n')
        {
            rawCode = rawCode.Remove(rawCode.Length - 1, 1);
        }
        parseRawCode(rawCode);
        turnOn();
        commands.execute();
        timeBuffer = Time.time;
    }

    public void generateButtonAction()
    {
        result.text = "Ждем...";
        if (isTutorial)
        {
            TutorialHandler.onGenClick();
        }
    }

    public void exitActionButton()
    {
        clearLevel();
        SceneManager.LoadScene("Menu");
    }

    public static void clearLevel()
    {
        DBHandler.updateStarNCode(user_id, level, levelDifficulty, codeInput.text, star);
        if (isTutorial)
        {
            isTutorial = false;
            TutorialHandler.clearTutorial();
        }
        Variable.clearVariables();
        timeBuffer = 0;
        isExecute = false;
        commands = null;
        textTemplate = "";
    }

    private void parseRawCode(string raw)
    {
        int i = 0;
        List<string> commandsArray = splitLine(raw);
        List<Instruction> instructionsArray = new List<Instruction>();
        string previousCommand = "";
        Instruction instruction = null;
        Instruction previousInstruction = null;
        foreach (string command in commandsArray)
        {
            if(command.LastIndexOf('\t') > previousCommand.LastIndexOf('\t'))
            {
                string cleanedCommand = command.Replace("\t", "");
                if (cleanedCommand.StartsWith("если"))
                {
                    instruction = new IfElseCommand(cleanedCommand.Substring(5, cleanedCommand.Length - 5), false);
                }
                else if (cleanedCommand.StartsWith("иначе"))
                {
                    string necessaryIf = commandsArray[commandsArray.GetRange(0, i).FindLastIndex(item => item.StartsWith(Multiply("\t", command.LastIndexOf('\t') + 1) + "если"))];
                    instruction = new IfElseCommand(necessaryIf.Substring(5, necessaryIf.Length - 5), true);
                }
                else if (cleanedCommand.StartsWith("пока"))
                {
                    instruction = new WhileCommand(cleanedCommand.Substring(5, cleanedCommand.Length - 5));
                }
                else
                {
                    instruction = new UsualCommand(cleanedCommand);
                }

                if (previousInstruction != null)
                {
                    previousInstruction.setChild(instruction);
                }
                else
                {
                    commands = instruction;
                }
            }
            else if(command.LastIndexOf('\t') == previousCommand.LastIndexOf('\t'))
            {
                string cleanedCommand = command.Replace("\t", "");
                if (cleanedCommand.StartsWith("если"))
                {
                    instruction = new IfElseCommand(cleanedCommand.Substring(5, cleanedCommand.Length - 5), false);
                }
                else if (cleanedCommand.StartsWith("иначе"))
                {
                    string necessaryIf = commandsArray[commandsArray.GetRange(0, i).FindLastIndex(item => item.StartsWith(Multiply("\t", command.LastIndexOf('\t') + 1) + "если"))];
                    instruction = new IfElseCommand(necessaryIf.Substring(5, necessaryIf.Length - 5), true);
                }
                else if (cleanedCommand.StartsWith("пока"))
                {
                    instruction = new WhileCommand(cleanedCommand.Substring(5, cleanedCommand.Length - 5));
                }
                else
                {
                    instruction = new UsualCommand(cleanedCommand);
                }

                if (previousInstruction != null)
                {
                    previousInstruction.setNext(instruction);
                }
                else
                {
                    commands = instruction;
                }
            }
            else if(command.LastIndexOf('\t') < previousCommand.LastIndexOf('\t'))
            {
                int prevCommandId = commandsArray.GetRange(0, i).FindLastIndex(item => (command.LastIndexOf('\t') + 1 > 0) ? item.LastIndexOf('\t') == command.LastIndexOf('\t') : item.IndexOf('\t') < 0);
                string cleanedCommand = command.Replace("\t", "");
                if (cleanedCommand.StartsWith("если"))
                {
                    instruction = new IfElseCommand(cleanedCommand.Substring(5, cleanedCommand.Length - 5), false);
                }
                else if (cleanedCommand.StartsWith("иначе"))
                {
                    string necessaryIf = commandsArray[commandsArray.GetRange(0, i).FindLastIndex(item => item.StartsWith(Multiply("\t", command.LastIndexOf('\t') + 1) + "если"))];
                    instruction = new IfElseCommand(necessaryIf.Substring(5, necessaryIf.Length - 5), true);
                }
                else if (cleanedCommand.StartsWith("пока"))
                {
                    instruction = new WhileCommand(cleanedCommand.Substring(5, cleanedCommand.Length - 5));
                }
                else
                {
                    instruction = new UsualCommand(cleanedCommand);
                }

                if (previousInstruction != null)
                {
                    instructionsArray[prevCommandId].setNext(instruction);
                }
                else
                {
                    commands = instruction;
                }

            }
            i++;
            previousCommand = command;
            previousInstruction = instruction;
            instructionsArray.Add(instruction);
        }
    }

    private List<string> splitLine(string raw)
    {
        return new List<string>(raw.Split('\n'));
    }

    public static void turnOff()
    {
        isExecute = false;
        if (Variable.getByName("Ответ").getValue() == GenerateLevel.generation.getAnswer())
        {
            if (rawCode.Length > 20)
            {
                result.text = "Верно!";
                star = 1;
            }
            else
            {
                result.text = "Мухлюем?";
            }
        }
        else
        {
            result.text = "Неверно!";
            if (levelDifficulty == 1 && !isTutorial)
            {
                GameObject.Find("GameManager").GetComponent<GenerateLevel>().GenerateButtonAction();
            }
        }
        if (isTutorial)
        {
            TutorialHandler.onExecutionSucceed();
        }
        Debug.Log(Variable.getByName("Ответ").getValue());
    }

    public static void turnOn()
    {
        isExecute = true;
    }

    public static string Multiply(string source, int multiplier)
    {
        StringBuilder sb = new StringBuilder(multiplier * source.Length);
        for (int i = 0; i < multiplier; i++)
        {
            sb.Append(source);
        }

        return sb.ToString();
    }
}
