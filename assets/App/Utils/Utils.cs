using System;
using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour {

    #region Events
    public static void LaunchEvent(object _sender, EventHandler<EventArgs> _event) {
        if (_event != null) _event(_sender, new EventArgs());
    }
    #endregion

    #region Itween

    public static Hashtable HashValueTo(string name, float from, float to, float time, float delay, iTween.EaseType easetype,
        string onupdate, string oncomplete) {
        Hashtable hash = new Hashtable();
        hash.Add("name", name);
        hash.Add("from", from);
        hash.Add("to", to);
        hash.Add("time", time);
        hash.Add("delay", delay);
        hash.Add("easetype", easetype);
        hash.Add("onupdate", onupdate);
        hash.Add("oncomplete", oncomplete);

        return hash;
    }

    public static Hashtable HashMoveTo(string name, Vector3[] path, bool orienttopath, float time, float delay, iTween.EaseType easetype, bool islocal/*, string axis*/) {
        Hashtable hash = new Hashtable();
        hash.Add("name", name);
        hash.Add("path", path);
        //hash.Add("orienttopath", orienttopath);
        hash.Add("islocal", islocal);
        hash.Add("time", time);
        hash.Add("delay", delay);
        hash.Add("easetype", easetype);
        //hash.Add("looktime", 0f);
        hash.Add("lookahead", 0.3f);
        return hash;
    }

    #endregion

    #region Floats Comparison

    public static bool IsApproximately(float a, float b, float threshold = 0.02f) {
        return Mathf.Abs(a - b) <= threshold;
    }

    public static bool IsApproximately(Vector3 a, Vector3 b, float threshold = 0.02f) {
        return (IsApproximately(a.x, b.x, threshold) && IsApproximately(a.y, b.y, threshold) &&
                IsApproximately(a.z, b.z, threshold));
    }
    #endregion

    #region Vectors Comparison


    public static bool isEqual(Vector3 a, Vector3 b, float percentageError = 0.05f) {
        //Debug.Log("" + (a - b).sqrMagnitude + "  " + (a * percentageError).sqrMagnitude);
        if ((a - b).sqrMagnitude <= (a * percentageError).sqrMagnitude) {
            //             Debug.Log( "They are less then 1% different" );
            return true;
        }
        return false;
    }

    public static bool isEqualByAngle(Vector3 a, Vector3 b, float threshold) {
        var result = Vector3.Angle(a, b);
        return (result <= threshold);
    }
    #endregion

    #region Map
    public static float Map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
    #endregion

    #region Transform

    public static void DestroyAllChildren(Transform t) {
        foreach (Transform childTransform in t) {
            Destroy(childTransform.gameObject);
        }
    }

    public static GameObject AddChildren(Transform t, GameObject prefab, bool worldPositionStays = false) {
        GameObject ob = Instantiate(prefab);
        ob.transform.SetParent(t, worldPositionStays);
        return ob;
    }

    #endregion

    #region Angle

    public static float Angle(Vector3 a, Vector3 b) {
        var angle = Vector3.Angle(a, b);
        var cross = Vector3.Cross(a, b);
        if (cross.y >= 0) angle = -angle;
        return angle;
    }

    #endregion
}
