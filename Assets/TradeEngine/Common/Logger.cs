using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Diagnostics;

public class Logger
{
    bool debug = true;
    public void Log(bool toLog, string message)
    {
        //Refrence of methods https://msdn.microsoft.com/en-us/library/system.diagnostics.stackframe_members(v=vs.90).aspx
        StackFrame frame = new StackFrame(1);
        var method = frame.GetMethod();
        var className = method.DeclaringType;
        var methodName = method.Name;

        if (debug && toLog)
        {
            UnityEngine.Debug.Log(className + ":" + methodName + " <" + message + ">");
        }
    }

}

