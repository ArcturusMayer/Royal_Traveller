using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class Arifmetics
{
    public static void doCount(string rawCommand)
    {
        rawCommand = rawCommand.Replace(" ", "");
        int index = rawCommand.IndexOf('=', 1);
        if (rawCommand[index - 1] != ' ')
        {
            rawCommand = rawCommand.Insert(index, " ");
        }
        index = rawCommand.IndexOf('=', index);
        if (rawCommand[index + 1] != ' ')
        {
            rawCommand = rawCommand.Insert(index + 1, " ");
        }
        string rightPart = rawCommand.Substring(rawCommand.IndexOf('=') + 2).Trim();
        string changingVariable = rawCommand.Substring(0, rawCommand.IndexOf(' ')).Trim();
        if (Variable.checkExistance(changingVariable))
        {
            Variable.assignByName(changingVariable, int.Parse(parseStatement(insertSpacesIfNeeded(rightPart))));
        }
        else
        {
            Variable assignableVar = new Variable(int.Parse(parseStatement(insertSpacesIfNeeded(rightPart))), changingVariable);
            assignableVar.writeDown();
        }
    }
    
    private static string insertSpacesIfNeeded(string statement)
    {
        int index = 0;
        while((index = statement.IndexOf('*', index + 1)) != -1)
        {
            if(statement[index - 1] != ' ')
            {
                statement = statement.Insert(index, " ");
            }
            index = statement.IndexOf('*', index);
            if (statement[index + 1] != ' ')
            {
                statement = statement.Insert(index + 1, " ");
            }
        }
        index = 0;
        while ((index = statement.IndexOf('/', index + 1)) != -1)
        {
            if (statement[index - 1] != ' ')
            {
                statement = statement.Insert(index, " ");
            }
            index = statement.IndexOf('/', index);
            if (statement[index + 1] != ' ')
            {
                statement = statement.Insert(index + 1, " ");
            }
        }
        index = 0;
        while ((index = statement.IndexOf('+', index + 1)) != -1)
        {
            if (statement[index - 1] != ' ')
            {
                statement = statement.Insert(index, " ");
            }
            index = statement.IndexOf('+', index);
            if (statement[index + 1] != ' ')
            {
                statement = statement.Insert(index + 1, " ");
            }
        }
        index = 0;
        while ((index = statement.IndexOf('-', index + 1)) != -1)
        {
            if (statement[index - 1] != ' ')
            {
                statement = statement.Insert(index, " ");
            }
            index = statement.IndexOf('-', index);
            if (statement[index + 1] != ' ')
            {
                statement = statement.Insert(index + 1, " ");
            }
        }
        return statement;
    }

    private static string parseStatement(string statement)
    {
        int index = 0;
        if ((index = statement.IndexOfAny(new char[]{'*', '/'})) != -1)
        {
            string resBuf = statement.Substring((statement.Substring(0, index - 1).LastIndexOf(' ') != -1 ? statement.Substring(0, index - 1).LastIndexOf(' ') + 1 : 0),
                (statement.IndexOf(' ', index + 2) != -1 ?
                statement.IndexOf(' ', index + 2) - (statement.Substring(0, index - 1).LastIndexOf(' ') != -1 ? statement.Substring(0, index - 1).LastIndexOf(' ') + 1 : 0) :
                statement.Length - (statement.Substring(0, index - 1).LastIndexOf(' ') != -1 ? statement.Substring(0, index - 1).LastIndexOf(' ') + 1 : 0))).Trim();
            return parseStatement(statement.Replace(resBuf, count(resBuf, (statement[index] == '*' ? 1 : 2))));
        }
        else if((index = statement.IndexOfAny(new char[] { '+', '-' })) != -1 && statement[index + 1] == ' ')
        {
            string resBuf = statement.Substring((statement.Substring(0, index - 1).LastIndexOf(' ') != -1 ? statement.Substring(0, index - 1).LastIndexOf(' ') + 1 : 0),
                (statement.IndexOf(' ', index + 2) != -1 ?
                statement.IndexOf(' ', index + 2) - (statement.Substring(0, index - 1).LastIndexOf(' ') != -1 ? statement.Substring(0, index - 1).LastIndexOf(' ') + 1 : 0) :
                statement.Length - (statement.Substring(0, index - 1).LastIndexOf(' ') != -1 ? statement.Substring(0, index - 1).LastIndexOf(' ') + 1 : 0))).Trim();
            return parseStatement(statement.Replace(resBuf, count(resBuf, (statement[index] == '+' ? 3 : 4))));
        }
        else
        {
            if (Variable.checkExistance(statement))
            {
                return Variable.getByName(statement).getValue().ToString();
            }
            else
            {
                return statement;
            }
        }
    }

    private static string count(string statement, int mode)
    {
        int result = 0;

        int varValue1 = 0;
        int varValue2 = 0;

        string varName1 = statement.Substring(0, statement.IndexOf(' ')).Trim();
        string varName2 = statement.Substring(statement.LastIndexOf(' ') + 1, statement.Length - 1 - statement.LastIndexOf(' ')).Trim();

        Regex numberTemplate = new Regex(@"^\d+$");

        if(numberTemplate.IsMatch(varName1))
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

        switch (mode)
        {
            case 1:
                result = varValue1 * varValue2;
                break;
            case 2:
                result = varValue1 / varValue2;
                break;
            case 3:
                result = varValue1 + varValue2;
                break;
            case 4:
                result = varValue1 - varValue2;
                break;
        }

        return result.ToString();
    }
}
