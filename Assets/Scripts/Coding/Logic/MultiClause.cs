using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MultiClause
{
    private string rawClause;

    public MultiClause(string rawClause)
    {
        this.rawClause = rawClause;
        this.rawClause = this.rawClause.LastIndexOf('\r') > 0 ? this.rawClause.Substring(0, this.rawClause.LastIndexOf('\r')) : this.rawClause;
        rawClause += " ";
    }

    public bool doClause(bool isItElse)
    {
        Debug.Log(isItElse ? !parseStatement(parseOr(parseAnd(parseNot(removeSpacesIfNeeded(rawClause))))) : parseStatement(parseOr(parseAnd(parseNot(removeSpacesIfNeeded(rawClause))))));
        return isItElse ? !parseStatement(parseOr(parseAnd(parseNot(removeSpacesIfNeeded(rawClause))))) : parseStatement(parseOr(parseAnd(parseNot(removeSpacesIfNeeded(rawClause)))));
    }

    private static string removeSpacesIfNeeded(string statement)
    {
        int index = 0;
        while ((index = statement.IndexOf("==", index + 1)) != -1)
        {
            while (statement[index - 1] == ' ')
            {
                statement = statement.Remove(index - 1, 1);
                index = statement.IndexOf("==", index - 1);
            }
            index = statement.IndexOf("==", index - 1);
            while (statement[index + 2] == ' ')
            {
                statement = statement.Remove(index + 2, 1);
                index = statement.IndexOf("==", index - 1);
            }
        }
        index = 0;
        while ((index = statement.IndexOf('>', index + 1)) != -1)
        {
            while (statement[index - 1] == ' ')
            {
                statement = statement.Remove(index - 1, 1);
                index = statement.IndexOf('>', index - 1);
            }
            index = statement.IndexOf('>', index - 1);
            while (statement[index + 1] == ' ')
            {
                statement = statement.Remove(index + 1, 1);
                index = statement.IndexOf('>', index - 1);
            }
        }
        index = 0;
        while ((index = statement.IndexOf('<', index + 1)) != -1)
        {
            while (statement[index - 1] == ' ')
            {
                statement = statement.Remove(index - 1, 1);
                index = statement.IndexOf('<', index - 1);
            }
            index = statement.IndexOf('<', index - 1);
            while (statement[index + 1] == ' ')
            {
                statement = statement.Remove(index + 1, 1);
                index = statement.IndexOf('<', index - 1);
            }
        }
        return statement;
    }

    private string parseNot(string rawClause)
    {
        int startIndex = 0;
        int endIndex = 0;
        string subClause = "";
        while ((startIndex = rawClause.IndexOf('!', startIndex)) != -1)
        {
            if ((endIndex = rawClause.IndexOf(' ', startIndex + 1)) != -1) { }
            else
            {
                endIndex = rawClause.Length - 1;
            }
            subClause = rawClause.Substring(startIndex + 1, endIndex - startIndex);
            if (subClause == "сверху—вободно"){
                rawClause = rawClause.Replace("!" + subClause, (!SimpleClause.isTopFree()) ? "true" : "false");
            } else if (subClause == "справа—вободно") {
                rawClause = rawClause.Replace("!" + subClause, (!SimpleClause.isRightFree()) ? "true" : "false");
            } else if (subClause == "снизу—вободно") {
                rawClause = rawClause.Replace("!" + subClause, (!SimpleClause.isBottomFree()) ? "true" : "false");
            } else if (subClause == "слева—вободно") {
                rawClause = rawClause.Replace("!" + subClause, (!SimpleClause.isLeftFree()) ? "true" : "false");
            }else if(subClause == "true") {
                rawClause = rawClause.Replace("!" + subClause, "false");
            }else if (subClause == "false"){
                rawClause = rawClause.Replace("!" + subClause, "true");
            }else
            {
                rawClause = rawClause.Replace("!" + subClause, varClause(subClause, false));
            }
        }
        return rawClause;
    }

    private string varClause(string rawClause, bool isStraight)
    {
        Regex numberTemplate = new Regex(@"^\d+$");
        string varName1 = "";
        string varName2 = "";
        int varValue1 = 0;
        int varValue2 = 0;
        int index = rawClause.IndexOf("==");
        if(index != -1)
        {
            varName1 = rawClause.Substring(0, index);
            if (numberTemplate.IsMatch(varName1))
            {
                varValue1 = int.Parse(varName1);
            }
            else if (Variable.checkExistance(varName1))
            {
                varValue1 = Variable.getByName(varName1).getValue();
            }
            else
            {
                varValue1 = 0;
            }

            varName2 = rawClause.Substring(index + 2, rawClause.Length - index - 2);
            if (numberTemplate.IsMatch(varName2))
            {
                varValue2 = int.Parse(varName2);
            }
            else if (Variable.checkExistance(varName2))
            {
                varValue2 = Variable.getByName(varName2).getValue();
            }
            else
            {
                varValue2 = 0;
            }
            return (!((varValue1 == varValue2) ^ isStraight)) ? "true" : "false";
        }
        index = rawClause.IndexOf('>');
        if (index != -1)
        {
            varName1 = rawClause.Substring(0, index);
            if (numberTemplate.IsMatch(varName1))
            {
                varValue1 = int.Parse(varName1);
            }
            else if (Variable.checkExistance(varName1))
            {
                varValue1 = Variable.getByName(varName1).getValue();
            }
            else
            {
                varValue1 = 0;
            }

            varName2 = rawClause.Substring(index + 1, rawClause.Length - index - 1);
            if (numberTemplate.IsMatch(varName2))
            {
                varValue2 = int.Parse(varName2);
            }
            else if (Variable.checkExistance(varName2))
            {
                varValue2 = Variable.getByName(varName2).getValue();
            }
            else
            {
                varValue2 = 0;
            }
            return (!((varValue1 > varValue2) ^ isStraight)) ? "true" : "false";
        }
        index = rawClause.IndexOf('<');
        if (index != -1)
        {
            varName1 = rawClause.Substring(0, index);
            if (numberTemplate.IsMatch(varName1))
            {
                varValue1 = int.Parse(varName1);
            }
            else if (Variable.checkExistance(varName1))
            {
                varValue1 = Variable.getByName(varName1).getValue();
            }
            else
            {
                varValue1 = 0;
            }

            varName2 = rawClause.Substring(index + 1, rawClause.Length - index - 1);
            if (numberTemplate.IsMatch(varName2))
            {
                varValue2 = int.Parse(varName2);
            }
            else if (Variable.checkExistance(varName2))
            {
                varValue2 = Variable.getByName(varName2).getValue();
            }
            else
            {
                varValue2 = 0;
            }
            return (!((varValue1 < varValue2) ^ isStraight)) ? "true" : "false";
        }
        return "false";
    }

    private string parseAnd(string rawClause)
    {
        int startIndex = 0;
        int leftStart = 0;
        int leftEnd = 0;
        int rightStart = 0;
        int rightEnd = 0;
        string leftClause = "";
        string rightClause = "";
        while ((startIndex < rawClause.Length - 1) ? (startIndex = rawClause.IndexOf('и', startIndex + 1)) != -1 : false)
        {
            leftEnd = (startIndex - 2 > -1) ? startIndex - 2 : 0;
            rightStart = startIndex + 2;
            if ((leftStart = rawClause.Substring(0, leftEnd).LastIndexOf(' ')) != -1) {
                leftStart++;
            }
            else
            {
                leftStart = 0;
            }
            if ((rightEnd = rawClause.IndexOf(' ', startIndex + 2)) != -1) {
                rightEnd++;
            }
            else
            {
                rightEnd = rawClause.Length - 1;
            }
            leftClause = rawClause.Substring(leftStart, leftEnd - leftStart + 1);
            rightClause = rawClause.Substring(rightStart, rightEnd - rightStart + 1);
            bool left = false;
            bool right = false;
            if (leftClause == "сверху—вободно")
            {
                left = SimpleClause.isTopFree();
            }
            else if (leftClause == "справа—вободно")
            {
                left = SimpleClause.isRightFree();
            }
            else if (leftClause == "снизу—вободно")
            {
                left = SimpleClause.isBottomFree();
            }
            else if (leftClause == "слева—вободно")
            {
                left = SimpleClause.isLeftFree();
            }
            else if (leftClause == "true")
            {
                left = true;
            }
            else if (leftClause == "false")
            {
                left = false;
            }
            else
            {
                left = bool.Parse(varClause(leftClause, true));
            }

            if (rightClause == "сверху—вободно")
            {
                right = SimpleClause.isTopFree();
            }
            else if (rightClause == "справа—вободно")
            {
                right = SimpleClause.isRightFree();
            }
            else if (rightClause == "снизу—вободно")
            {
                right = SimpleClause.isBottomFree();
            }
            else if (rightClause == "слева—вободно")
            {
                right = SimpleClause.isLeftFree();
            }
            else if (rightClause == "true")
            {
                right = true;
            }
            else if (rightClause == "false")
            {
                right = false;
            }
            else
            {
                right = bool.Parse(varClause(rightClause, true));
            }
            rawClause = rawClause.Replace(leftClause + " и " + rightClause, (left && right) ? "true" : "false");
        }
        return rawClause;
    }

    private string parseOr(string rawClause)
    {
        int startIndex = 0;
        int leftStart = 0;
        int leftEnd = 0;
        int rightStart = 0;
        int rightEnd = 0;
        string leftClause = "";
        string rightClause = "";
        while ((startIndex < rawClause.Length - 1) ? (startIndex = rawClause.IndexOf("или", startIndex + 1)) != -1 : false)
        {
            leftEnd = (startIndex - 2 > -1) ? startIndex - 2 : 0;
            rightStart = startIndex + 4;
            if ((leftStart = rawClause.Substring(0, leftEnd).LastIndexOf(' ')) != -1)
            {
                leftStart++;
            }
            else
            {
                leftStart = 0;
            }
            if ((rightEnd = rawClause.IndexOf(' ', startIndex + 4)) != -1)
            {
                rightEnd++;
            }
            else
            {
                rightEnd = rawClause.Length - 1;
            }
            leftClause = rawClause.Substring(leftStart, leftEnd - leftStart + 1);
            rightClause = rawClause.Substring(rightStart, rightEnd - rightStart + 1);
            bool left = false;
            bool right = false;
            if (leftClause == "сверху—вободно")
            {
                left = SimpleClause.isTopFree();
            }
            else if (leftClause == "справа—вободно")
            {
                left = SimpleClause.isRightFree();
            }
            else if (leftClause == "снизу—вободно")
            {
                left = SimpleClause.isBottomFree();
            }
            else if (leftClause == "слева—вободно")
            {
                left = SimpleClause.isLeftFree();
            }
            else if (leftClause == "true")
            {
                left = true;
            }
            else if (leftClause == "false")
            {
                left = false;
            }
            else
            {
                left = bool.Parse(varClause(leftClause, true));
            }
            if (rightClause == "сверху—вободно")
            {
                right = SimpleClause.isTopFree();
            }
            else if (rightClause == "справа—вободно")
            {
                right = SimpleClause.isRightFree();
            }
            else if (rightClause == "снизу—вободно")
            {
                right = SimpleClause.isBottomFree();
            }
            else if (rightClause == "слева—вободно")
            {
                right = SimpleClause.isLeftFree();
            }
            else if (rightClause == "true")
            {
                right = true;
            }
            else if (rightClause == "false")
            {
                right = false;
            }
            else
            {
                right = bool.Parse(varClause(rightClause, true));
            }
            rawClause = rawClause.Replace(leftClause + " или " + rightClause, (left || right) ? "true" : "false");
        }
        return rawClause;
    }

    private bool parseStatement(string rawClause)
    {
        if (rawClause == "сверху—вободно") {
            rawClause = rawClause.Replace(rawClause, (SimpleClause.isTopFree()) ? "true" : "false");
        } else if (rawClause == "справа—вободно") {
            rawClause = rawClause.Replace(rawClause, (SimpleClause.isRightFree()) ? "true" : "false");
        } else if (rawClause == "снизу—вободно") {
            rawClause = rawClause.Replace(rawClause, (SimpleClause.isBottomFree()) ? "true" : "false");
        } else if (rawClause == "слева—вободно") {
            rawClause = rawClause.Replace(rawClause, (SimpleClause.isLeftFree()) ? "true" : "false");
        }else if(rawClause == "true"){
            return true;
        }else if (rawClause == "false"){
            return false;
        }
        else
        {
            rawClause = rawClause.Replace(rawClause, varClause(rawClause, true));
        }
        return (rawClause == "true") ? true : false;
    }
}
