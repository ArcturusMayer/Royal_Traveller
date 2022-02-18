using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variable
{
    private string name;
    private int value;

    private static List<Variable> variables = new List<Variable>() { new Variable(0, "Ответ") };

    public Variable(int value, string name)
    {
        this.value = value;
        this.name = name;
    }

    public void writeDown()
    {
        variables.Add(this);
    }

    public string getName()
    {
        return name;
    }

    public int getValue()
    {
        return value;
    }

    public void assign(int value)
    {
        this.value = value;
    }

    // Returns false if var dont exist, true if exist
    public static bool checkExistance(string name)
    {
        bool isExist = false;
        foreach(Variable var in variables)
        {
            if(var.getName() == name)
            {
                isExist = true;
            }
        }
        return isExist;
    }

    public static Variable getByName(string name)
    {
        foreach(Variable var in variables)
        {
            if(var.getName() == name)
            {
                return var;
            }
        }
        return null;
    }

    public static void assignByName(string name, int value)
    {
        foreach (Variable var in variables)
        {
            if (var.getName() == name)
            {
                var.assign(value);
            }
        }
    }

    public static void clearVariables()
    {
        variables = new List<Variable>() { new Variable(0, "Ответ") };
    }
}
