using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Instruction
{
    protected Instruction parent;
    protected Instruction child;
    protected Instruction next;

    public static int goal;

    public abstract void execute();

    public virtual void sendMessage(string message)
    {

    }

    public virtual Instruction goNext()
    {
        if (child != null)
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

    public void setChild(Instruction child)
    {
        this.child = child;
        child.parent = this;
    }

    public void setNext(Instruction next)
    {
        this.next = next;
        next.parent = this.parent;
    }
}
