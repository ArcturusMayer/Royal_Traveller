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
        "����� ���������� � ���� ����������� ��������! ������ ��������� ������ ������ � ���� �����. �������� �� ���������, ����� ����������.",
        "����� �� ������ ������� ��������� ���� �����. � ��� ���������� ������ ���������� (���������), ������� ����� ��������� ��������.",
        "�������� �� ����� ������ ����������� ��� ������ ������� �� ����� �� ������.",
        "������� ������ �� ������� �������� � ������� ����� ������.",
        "������ ������� ����������� ������� ��������� �������.",
        "������ ������ �������� ���������� ���� �� ����� ������.",
        "�� ������ ����� ����� �������� ������������ ������� ������ ���������� ����.",
        "� ���� ������������� ��� ������ ��������� - ������, ������� � �������.",
        "�� ������ � ��� �������������� ����� ������� ������ ������, ����� ��������������������.",
        "�� ������� ������� ����, � ����� ������� ������� ������������� - ��� �������� ��� ������� ��������� � ������� ����!",
        "�� ������� ������� ������������� ��� ������� �������, ��� ��� �� �� ������ �����, �� ����� ���� ��������� - ������ ������������� ��� ������� ���� ������ �������!",
        "��������� ������ ������� ������. �������� �����:\n����� = 0\n�� �������� ���������� ��� �������� ������. ������� ������ ��� �����������.",
        "����� ��������:\n��������\n����� = ����� + 1\n������ �� ���� ����� - ������� �������� ��� �����������, �� ������ �� ������ �������, � ������ - ���������� ���������� �� �������, ������� ����������.",
        "��������� ���������� ��� ������� ��� ��� ���� � ���������, ������ ���� ������ ���������� ���� �����.",
        "���������� �������. � ���� ���� �������� ��������� - ����-�����, � ������ ���������� �������, ����������� ����������� ������. � ���� �� ����� ���������� ���������� � ������� \">\", \"<\", \"==\". ��������:\n���� ��������������\n\t����������\n\t����� = ����� + 1\n��������� ������� ���������� ��������� � ������� ����� (Tab), ��� � Python.",
        "������ �����. � ���� ���� ���� ��� ������ - ���� � ������������. ��������:\n���� !��������������\n\t����� = ����� + 1\n\t����������\n��� ������, ������� ����� ������������� � ������� ��������� \"!\"",
        "� ��������� ���������� \"�����\" ���� ������ �������� ��������� ���������.\n�� ��������� ��������� ��������� �� ���. ��������� � ���������� �� ���������.",
        "�����������! ������ �� ������� � ��������� ���������� ����. ������ ����������� ���� � ��������� �������. �������� ���������, ����� �����, ��� �������������� ������� ������."
    };
    private static List<string> templates = new List<string>()
    {
        "����� = 0",
        "����� = 0\n��������\n����� = ����� + 1",
        "����� = 0\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1",
        "����� = 0\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1\n���� ��������������\n\t����������\n\t����� = ����� + 1",
        "����� = 0\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1\n���� ��������������\n\t����������\n\t����� = ����� + 1\n���� !��������������\n\t����� = ����� + 1\n\t����������",
        "����� = 0\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1\n���� ��������������\n\t����������\n\t����� = ����� + 1\n���� !��������������\n\t����� = ����� + 1\n\t����������\n���������\n����� = ����� + 1\n���� !�������������\n\t���������\n\t����� = ����� + 1\n���������\n����� = ����� + 1\n���� !�������������\n\t���������\n\t����� = ����� + 1\n����� = �����",
        "����� = 0\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1\n��������\n����� = ����� + 1\n���� ��������������\n\t����������\n\t����� = ����� + 1\n���� !��������������\n\t����� = ����� + 1\n\t����������\n���������\n����� = ����� + 1\n���� !�������������\n\t���������\n\t����� = ����� + 1\n���������\n����� = ����� + 1\n���� !�������������\n\t���������\n\t����� = ����� + 1\n����� = �����"
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
