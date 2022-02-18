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
        if(rawCommand == "идти¬верх")
        {
            SimpleFunctions.moveUp();
        }
        else if (rawCommand == "идти¬право")
        {
            SimpleFunctions.moveRight();
        }
        else if (rawCommand == "идти¬низ")
        {
            SimpleFunctions.moveDown();
        }
        else if(rawCommand == "идти¬лево")
        {
            SimpleFunctions.moveLeft();
        }
        else
        {
            Arifmetics.doCount(rawCommand.Trim());
        }
    }
}
