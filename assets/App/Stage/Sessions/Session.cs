using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FullSerializer;

public class Entry
{

    public Entry(JointsGroup jointsGroup)
    {
        this.jointsGroup = jointsGroup;
    }

    //[fsProperty]
    public JointsGroup jointsGroup { get; set; }

    public int LOL;
    public void print()
    {
        //Debug.Log(floorArcPosition);
    }
}

public class Log
{

    public List<Entry> entries = new List<Entry>();

     public string logID;

    public int validCount;

     public int invalidCount;

     public float time;

    public void AddEntry(Entry e)
    {
        this.entries.Add(e);
    }

    public void AddEntry(JointsGroup jg)
    {
        var entry = new Entry(jg);
        AddEntry(entry);
    }
    public void print()
    {
        Debug.Log("log " + logID + ":" + this.entries.Count + " entries");
    }

    private void printEntry(Entry obj) {
        obj.print();
    }
}

public class Session
{

    public List<Log> logs = new List<Log>();

    public string sessionID;

    public string exerciseID;

    public float score;

    public void print()
    {
        //ForEach(printLog);
    }

    private void printLog(Log obj) {
        obj.print();
    }

    public void Add(Log l)
    {
        logs.Add(l);
    }

    public Log Get(int i)
    {
        return logs[i];
    }
}




