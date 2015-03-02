﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

public class UdpListener : MonoBehaviour {

	public int port;
    private KinectListener _KinectListener;
	private UdpClient _udpClient;
	private IPEndPoint _anyIP;
	private List<string> _stringsToParse;

	void Start () {
		Debug.Log("[UDP Broadcast] Start");
        _udpClient = null;
		udpRestart();
	    _KinectListener = gameObject.GetComponent<KinectListener>();
	}
	
	public void udpRestart()
	{
		_stringsToParse = new List<string>();
		_anyIP = new IPEndPoint(IPAddress.Any, port);
        if (_udpClient != null)
            _udpClient.Close();
		_udpClient = new UdpClient(_anyIP);

		_udpClient.BeginReceive(new AsyncCallback(this.ReceiveCallback), null);
	}

	public void ReceiveCallback(IAsyncResult ar)
	{
		Byte[] receiveBytes = _udpClient.EndReceive(ar, ref _anyIP);
		_stringsToParse.Add(Encoding.ASCII.GetString(receiveBytes));

        _udpClient.BeginReceive(new AsyncCallback(this.ReceiveCallback), null);
	}

	void Update () 
	{
		while(_stringsToParse.Count > 0)
		{
			string stringToParse = _stringsToParse.First();
			_stringsToParse.RemoveAt(0);

            //TrackerMessage message = new TrackerMessage(stringToParse);

            //Debug.Log(stringToParse);
            _KinectListener.Parse(stringToParse);
            
			//if (message.ParseState == TrackerMessageParseState.WellFormed)
			//{
			//	GameObject.Find("CreepyTrackerGO").GetComponent<CreepyTracker>().postMessage(message);
			//}
		}
	}
	
	void OnApplicationQuit()
	{
		_udpClient.Close();
        _udpClient = null;
	}
	
	void OnQuit()
	{
		OnApplicationQuit();
	}
}
