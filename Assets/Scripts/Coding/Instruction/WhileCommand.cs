using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileCommand : Instruction
{
    private string rawClause;
    private MultiClause clause;

    public WhileCommand(string rawClause)
    {
        this.rawClause = rawClause;
        this.rawClause = this.rawClause.LastIndexOf('\r') > 0 ? this.rawClause.Substring(0, this.rawClause.LastIndexOf('\r')) : this.rawClause;
        clause = new MultiClause(rawClause);
    }

    public override void execute()
    {
    }

    public override Instruction goNext()
    {
        if (child != null && clause.doClause(false))
        {
            child.execute();
            return child;
        }
        else
        {
            if (next != null)
            {
                next.execute();
                return next;
            }
            else
            {
                //parent.child = null;
                if (parent != null)
                {
                    return parent.goNext();
                }
                else
                {
                    CodeExecutor.turnOff();
                    return null;
                    //print goal
                }
            }
        }
    }
}
