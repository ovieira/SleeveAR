using UnityEngine;
using System.Collections;
using OptitrackManagement;

public class OptitrackListener : MonoBehaviour
{
    public GameObject cube;
	
	//escala por causa do Unity
	public float optiTrackPosMultiplyer = 10.0f;
    public bool canTrack;
	
	~OptitrackListener()
	{
		OptitrackManagement.DirectMulticastSocketClient.Close();
	}
	// Use this for initialization
	void Start ()
	{
        OptitrackManagement.DirectMulticastSocketClient.Start();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (canTrack) {
            UpdateRigidBodies(); 
        }
	}

    private void UpdateRigidBodies()
    {
        OptitrackManagement.DirectMulticastSocketClient.Update();

        OptitrackManagement.RigidBody[] rigidBodies =
            OptitrackManagement.DirectMulticastSocketClient.GetStreemData()._rigidBody;

        //assigning rigidbodies

        for (int i = 0; i < OptitrackManagement.DirectMulticastSocketClient.GetStreemData()._nRigidBodies; i++)
        {
            //Assign cube
            Debug.Log("entrei");
            if (i == 0)
            {
                //Debug.Log("Assigned");
                if (rigidBodies[i].RigidBodyGameObject == null)
                {
                    //associa o objecto a este rigidbody
                    rigidBodies[i].RigidBodyGameObject = cube;
                }

                rigidBodies[i].RigidBodyGameObject.transform.position =
                    Vector3.Scale(rigidBodies[i].pos, new Vector3(-1, 1, 1))*optiTrackPosMultiplyer;
                rigidBodies[i].RigidBodyGameObject.transform.rotation = Quaternion.Inverse(rigidBodies[i].ori);
                //Debug.Log(rigidBodies[i].ID);
            }
        }
        //DONE :)
    }

    public void setTracking(bool b)
    {
        canTrack = b;
    }
}
