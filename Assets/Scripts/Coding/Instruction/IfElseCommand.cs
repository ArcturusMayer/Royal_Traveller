using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfElseCommand : Instruction
{
    private string rawClause;
    private MultiClause clause;

    private bool isFirst = true;
    private bool isItElse;
    private bool ifElseShouldWork = true;

    public IfElseCommand(string rawClause, bool isItElse)
    {
        this.rawClause = rawClause;
        this.rawClause = this.rawClause.LastIndexOf('\r') > 0 ? this.rawClause.Substring(0, this.rawClause.LastIndexOf('\r')) : this.rawClause;
        this.isItElse = isItElse;
        clause = new MultiClause(rawClause);
    }

    public override void execute()
    {
    }

    public override void sendMessage(string message)
    {
        ifElseShouldWork = bool.Parse(message);
    }

    public override Instruction goNext()
    {
        if (child != null && clause.doClause(isItElse) && isFirst)
        {
            if (!isItElse)
            {
                ifElseShouldWork = false;
                try
                {
                    next.sendMessage(ifElseShouldWork.ToString());
                }
                catch { }
                isFirst = false;
                child.execute();
                return child;
            }
            else
            {
                if (ifElseShouldWork)
                {
                    isFirst = false;
                    child.execute();
                    return child;
                }
                else
                {
                    isFirst = true;
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
        else
        {
            if (!isItElse && isFirst)
            {
                ifElseShouldWork = true;
                try
                {
                    next.sendMessage(ifElseShouldWork.ToString());
                }
                catch { }
            }
            isFirst = true;
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
                    return this;
                    //print goal
                }
            }
        }
    }
}
