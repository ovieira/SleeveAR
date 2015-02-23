using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    public Texture2D nameForeground;
    private Texture blackTexture;
    public Texture2D blobTex;
    public Vector3 zeroTarg;
    public Transform cube;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
     //transform.LookAt(zeroTarg);
     //   transform.Rotate(new Vector3(0, 0, 180));
    }

    void OnGUI()
    {
        showFloorMap();
    }

    private void showFloorMap() {
        float projection_width = 4.34f;
        float projection_depth = 3.1f;
        float projection_width_offset = 0.25f;
        float projection_width_offset_px = Screen.width / projection_width * projection_width_offset;
        float projection_depth_offset = /*-0.6f*/-0.45f;

        //float blobSize = Screen.width / projection_width * 0.85f;
        //float half_blob = blobSize * 0.5f;

        //GUIStyle name_style = new GUIStyle();
        //name_style.normal.background = nameForeground;
        //name_style.alignment = TextAnchor.MiddleCenter;

        //GUI.color = Color.black;
        //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackTexture);

        if (true) {
            int center_size = 20; // px
            float wall_width = 4; // m
            float wall_depth = 4; // m

            float c_x = 0.5f * wall_width / projection_width *
                (Screen.width - projection_width_offset_px) - center_size / 2.0f + projection_width_offset_px;
            float c_y = (0.5f * wall_depth + projection_depth_offset) /
                projection_depth * Screen.height - center_size / 2.0f;

            GUI.color = Color.white;
            GUI.DrawTexture(new Rect(c_x, c_y, center_size, center_size), blobTex);
        }
    }
}
