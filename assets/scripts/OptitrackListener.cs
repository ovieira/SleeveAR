using UnityEngine;
using System.Collections;
using OptitrackManagement;
using UnityEngine.UI;

public class OptitrackListener : MonoBehaviour
{
    public GameObject[] _GameObjects;
	
	//escala por causa do Unity
	public float optiTrackPosMultiplyer = 10.0f;
    public bool canTrack;

    public Toggle _toggle;
    private Vector3 before;


    #region Life Cycle
    ~OptitrackListener() {
        OptitrackManagement.DirectMulticastSocketClient.Close();
    }
    // Use this for initialization
    public void Start() {
        OptitrackManagement.DirectMulticastSocketClient.Close();
        _toggle.onValueChanged.AddListener(setTracking);
    }

    private void setTracking(bool b) {
        canTrack = b;
        if (canTrack) {
            OptitrackManagement.DirectMulticastSocketClient.Start();
        }
        else {
            OptitrackManagement.DirectMulticastSocketClient.Close();
        }
    }

    // Update is called once per frame
    void Update() {
        if (canTrack) {
            UpdateRigidBodies();
        }
    } 
    #endregion

    #region GameObjects Update
    private void UpdateRigidBodies() {
        OptitrackManagement.DirectMulticastSocketClient.Update();

        OptitrackManagement.RigidBody[] rigidBodies =
            OptitrackManagement.DirectMulticastSocketClient.GetStreemData()._rigidBody;

        //assigning rigidbodies

        for (int i = 0; i < OptitrackManagement.DirectMulticastSocketClient.GetStreemData()._nRigidBodies; i++) {
            if (i > rigidBodies.Length) return;

            if (rigidBodies[i].RigidBodyGameObject == null) {
                rigidBodies[i].RigidBodyGameObject = _GameObjects[i];
            }
            before = rigidBodies[i].pos;
            //Debug.Log("antes" + rigidBodies[i].pos);
            rigidBodies[i].RigidBodyGameObject.transform.position =
                Vector3.Scale(rigidBodies[i].pos, new Vector3(-1, 1, 1)) * optiTrackPosMultiplyer;
            //Debug.Log("depois" + rigidBodies[i].RigidBodyGameObject.transform.position);
            rigidBodies[i].RigidBodyGameObject.transform.rotation = Quaternion.Inverse(rigidBodies[i].ori);
        }

        /*for (int i = 0; i < OptitrackManagement.DirectMulticastSocketClient.GetStreemData()._nRigidBodies; i++)
        {
            //Assign _RigidBodies
            //Debug.Log("entrei");
            if (i == 0)
            {

                //Debug.Log("Assigned");
                if (rigidBodies[i].RigidBodyGameObject == null)
                {
                    //associa o objecto a este rigidbody
                    rigidBodies[i].RigidBodyGameObject = _GameObjects[i];
                }
                before = rigidBodies[i].pos;
                //Debug.Log("antes" + rigidBodies[i].pos);
                rigidBodies[i].RigidBodyGameObject.transform.position =
                    Vector3.Scale(rigidBodies[i].pos, new Vector3(-1, 1, 1))*optiTrackPosMultiplyer;
                //Debug.Log("depois" + rigidBodies[i].RigidBodyGameObject.transform.position);
                rigidBodies[i].RigidBodyGameObject.transform.rotation = Quaternion.Inverse(rigidBodies[i].ori);
                
                
                //Debug.Log(rigidBodies[i].RigidBodyGameObject.transform.position);
            }

            if (i == 1) {

                //Debug.Log("Assigned");
                if (rigidBodies[i].RigidBodyGameObject == null) {
                    //associa o objecto a este rigidbody
                    rigidBodies[i].RigidBodyGameObject = _GameObjects[i];
                }
                before = rigidBodies[i].pos;
                //Debug.Log("antes" + rigidBodies[i].pos);
                rigidBodies[i].RigidBodyGameObject.transform.position =
                    Vector3.Scale(rigidBodies[i].pos, new Vector3(-1, 1, 1)) * optiTrackPosMultiplyer;
                //Debug.Log("depois" + rigidBodies[i].RigidBodyGameObject.transform.position);
                rigidBodies[i].RigidBodyGameObject.transform.rotation = Quaternion.Inverse(rigidBodies[i].ori);


                //Debug.Log(rigidBodies[i].RigidBodyGameObject.transform.position);
            }
        }*/
        //DONE :)

    } 
    #endregion

    #region GUI
    void OnGUI() {
        //Vector3 p = Camera.main.WorldToScreenPoint(_RigidBodies.transform.position);
        //GUI.Label(new Rect(p.x - 50, Screen.height - p.y - 60, 150f, 50f), _RigidBodies.transform.position.x + "," + _RigidBodies.transform.position.y + " , " + _RigidBodies.transform.position.z);
        //GUI.Label(new Rect(p.x - 50, Screen.height - p.y + 40, 150f, 50f), _RigidBodies.transform.position.x + "," + _RigidBodies.transform.position.y + " , " + _RigidBodies.transform.position.z);
    } 
    #endregion
}
