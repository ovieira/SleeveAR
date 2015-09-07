using System;

public class ServiceSection {

    #region Section
    public enum Section {
        NIL,
        LEARNING,
        TEACHING,
        TUTORIAL
    }


    public event EventHandler<EventArgs> onSectionChanged; 
    protected Section _selected;

    public Section selected
    {
        get { return this._selected; }
        set
        {
            if (this._selected != value)
            {
                this._selected = value;
                Utils.LaunchEvent(this, onSectionChanged);
            }

        }
    }

    #endregion

    #region Singleton

    private static ServiceSection _instance;

    public static ServiceSection instance {
        get {
            if (_instance == null) {
                _instance = new ServiceSection();
            }
            return _instance;
        }
    }

    #endregion

}
