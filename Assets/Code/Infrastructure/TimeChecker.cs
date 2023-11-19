using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChecker
{
    private DateTime TimeStamp { get; set; }

    public TimeChecker()
    {
        TimeStamp = DateTime.Now;
    }

    public bool IsTimePass(int milliseconds)
    {
        if ((DateTime.Now - TimeStamp).TotalMilliseconds >= milliseconds)
        {
            TimeStamp = DateTime.Now;
            return true;
        }
        return false;
    }
}
