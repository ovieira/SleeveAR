using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


public class Entry
{
    public Entry(JointsGroup position, Vector3 floorArcPosition)
    {
        this.position = position;
        this.floorArcPosition = floorArcPosition;
    }

    public JointsGroup position { get; set; }
    public Vector3 floorArcPosition { get; set; }

    public void print()
    {
        Debug.Log(floorArcPosition);
    }
}

public class Log : List<Entry>
{
    private Log l;

    public string logID { get; set; }

    public void AddEntry(Entry e)
    {
        this.Add(e);
    }

    public void AddEntry(JointsGroup jg, Vector3 floorArcPos)
    {
        var entry = new Entry(jg,floorArcPos);
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




