using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsualCommand : Instruction
{
    private string rawCommand;

    public UsualCommand(string rawCommand)
    {
        this.rawCommand = rawCommand;
        this.rawCommand = this.rawCommand.LastIndexOf('\r') > 0 ? this.rawCommand.Substring(0, this.rawCommand.LastIndexOf('\r')) : this.rawCommand;
    }

    public override void execute()
    {
        parseCommand();
    }

    public override Instruction goNext()
    {
        return base.goNext();
    }

    private void parseCommand()
    {
        if(rawCommand == "���������")
        {
            SimpleFunctions.moveUp();
        }
        else if (rawCommand == "����������")
        {
            SimpleFunctions.moveRight();
        }
        else if (rawCommand == "��������")
        {
            SimpleFunctions.moveDown();
        }
        else if(rawCommand == "���������")
        {
            SimpleFunctions.moveLeft();
        }
        else
        {
            Arifmetics.doCount(rawCommand.Trim());
        }
    }
}
