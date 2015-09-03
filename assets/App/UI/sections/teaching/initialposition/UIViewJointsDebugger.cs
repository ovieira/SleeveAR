using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIViewJointsDebugger : UIView  {

    void Update()
    {
        foretext.text = "Fore: " + fore;
        uppertext.text = "Upper: " + upper;


    }


    public override void show()
    {
        throw new System.NotImplementedException();
    }

    public override void hide()
    {
        throw new System.NotImplementedException();
    }

    public override void onUpdate(float progress)
    {
        throw new System.NotImplementedException();
    }

    public override void onShowCompleted()
    {
        throw new System.NotImplementedException();
    }

    public override void onHideCompleted()
    {
        throw new System.NotImplementedException();
    }

    public float upper, fore;

    public Text uppertext, foretext;
}
