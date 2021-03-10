using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    public delegate void ActionDelegate();

    private ActionDelegate action;

    public ActionNode(ActionDelegate action)
    {
        this.action = action;
    }

    public override void Execute()
    {
        action();
    }
}
