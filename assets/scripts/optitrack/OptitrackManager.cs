using UnityEngine;
using System;
using System.Collections;
//using NatNetML;
using OptitrackManagement;

public class OptitrackManager : MonoBehaviour
{
    public GameObject markerObject;

    public string myName;
    public string _clientIPadres ="172.20.41.25";
    public string _serverIPadres ="172.20.41.24";
    private Vector3 _moveVector;
    public bool _deinitValue = false;

    ~OptitrackManager()
    {
        Debug.Log("OptitrackManager: Destruct");
        OptitrackManagement.DirectMulticastSocketClient.Close();
    }

    void Start()
    {
        Debug.Log(myName + ": i am alive");

        OptitrackManagement.DirectMulticastSocketClient.Start();
        _moveVector = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        OptitrackManagement.DirectMulticastSocketClient.Update();
        ///_netClient.Step();

        if (OptitrackManagement.DirectMulticastSocketClient.IsInit())
        {
            StreemData networkData = OptitrackManagement.DirectMulticastSocketClient.GetStreemData();

            _moveVector = networkData._rigidBody[0].pos * 2.0f;
        }

        transform.position = _moveVector;

        if (_deinitValue)
        {
            _deinitValue = false;
            OptitrackManagement.DirectMulticastSocketClient.Close();
        }

        OtherMarker[] markers = OptitrackManagement.DirectMulticastSocketClient.GetStreemData()._otherMarkers;

        for (int i = 0; i < OptitrackManagement.DirectMulticastSocketClient.GetStreemData()._nOtherMarkers; i++)
        {
            if (markers[i].markerGameObject == null)
            {
                if (markerObject == null)
                    markerObject = GameObject.Find("Marker Prefab");
                markers[i].markerGameObject = (GameObject)Instantiate(markerObject, markers[i].pos, Quaternion.identity);
            }
            else {
                markers[i].markerGameObject.transform.position = markers[i].pos *10;
            }
        }
    }

}