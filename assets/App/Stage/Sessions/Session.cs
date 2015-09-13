using System.Collections.Generic;
using UnityEngine;

public class Entry
{
    public Entry(JointsGroup jointsGroup)
    {
        this.jointsGroup = jointsGroup;
    }

    public JointsGroup jointsGroup { get; set; }

    public void print()
    {
        //Debug.Log(floorArcPosition);
    }
}

public class Log
{
    public List<Entry> entries = new List<Entry>();
    public string logID;
    public float totaltime;
    public float initialPositionTime;
    public int validCount;
    public int invalidCount;
    public float logScore;

    public void AddEntry(Entry e)
    {
        entries.Add(e);
    }

    public void AddEntry(JointsGroup jg)
    {
        var entry = new Entry(jg);
        AddEntry(entry);
    }

    public void print()
    {
        Debug.Log("log " + logID + ":" + entries.Count + " entries");
    }

    private void printEntry(Entry obj)
    {
        obj.print();
    }
}

public class Session
{
    public string exerciseID;
    public List<Log> logs = new List<Log>(3);
    public float sessionScore;
    public string sessionID;

    public void print()
    {
        //ForEach(printLog);
    }

    private void printLog(Log obj)
    {
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

    public void calculateSessionScore()
    {
        float i = 0;

        foreach (var log in logs)
        {
            i += log.logScore;
        }

        this.sessionScore = i/logs.Count;
    }
}