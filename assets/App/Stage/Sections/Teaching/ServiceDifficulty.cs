using System;

public class ServiceDifficulty {

    #region Difficulty
    public enum Difficulty {
        EASY,
        MEDIUM,
        HARD
    }

    public event EventHandler<EventArgs> onDifficultyChanged; 
    public Difficulty _selected;

    protected Difficulty selected
    {
        get { return this._selected; }
        set
        {
            if (this._selected == value) return;
            this._selected = value;
            Utils.LaunchEvent(this, this.onDifficultyChanged);
        }
    }
    #endregion

    #region Thresholds

    #endregion


    #region Singleton

    private static ServiceDifficulty _instance;

    public static ServiceDifficulty instance {
        get {
            if (_instance == null) {
                _instance = new ServiceDifficulty();
            }
            return _instance;
        }
    }

    #endregion
}