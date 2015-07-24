public interface IUIView
{
    /// <summary>
    /// show behaviour
    /// </summary>
    void show();

    /// <summary>
    /// hide behaviour
    /// </summary>
    void hide();

    void onUpdate(float progress);

    void onShowCompleted();

    void onHideCompleted();
}
