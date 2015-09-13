using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FullSerializer;

public class Entry
{
    public Entry(JointsGroup jointsGroup, Vector3 floorArcPosition)
    {
        this.jointsGroup = jointsGroup;
        this.floorArcPosition = floorArcPosition;
    }

    public Entry(JointsGroup jointsGroup)
    {
        this.jointsGroup = jointsGroup;
    }

    [fsProperty]
    public JointsGroup jointsGroup { get; set; }

    [fsProperty]
    public Vector3 floorArcPosition { get; set; }

    public void print()
    {
        //Debug.Log(floorArcPosition);
    }
}

public class Log : List<Entry>
{

    public string logID { get; set; }

    public int validCount { get; set; }

    public int invalidCount { get; set; }

    public float time { get; set; }

    public void AddEntry(Entry e)
    {
        this.Add(e);
    }

    public void AddEntry(JointsGroup jg, Vector3 floorArcPos)
    {
        var entry = new Entry(jg,floorArcPos);
        AddEntry(entry);
    }

    public void AddEntry(JointsGroup jg)
    {
        var entry = new Entry(jg);
        AddEntry(entry);
    }
    public void print()
    {
        Debug.Log("log " + logID + ":" + this.Count + " entries");
    }

    private void printEntry(Entry obj) {
        obj.print();
    }
}


public class Session : List<Log>
{
    public string sessionID { get; set;}
    public string exerciseID { get; set;}
    public float score { get; set; }

    public void print()
    {
        ForEach(printLog);
    }

    private void printLog(Log obj) {
        obj.print();
    }

   
}




