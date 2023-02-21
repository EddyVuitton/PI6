namespace PI6.Components.Shared.Objects;

public class Question
{
    private int _id;
    private int _lp;
    private string _title;

    public Question(int id, int lp, string title)
    {
        _id = id;
        _lp = lp;
        _title = title;
    }

    public int GetId() => _id;
    public void SetId(int id) => _id = id;
    public int GetLp() => _lp;
    public void SetLp(int lp) => _lp = lp;
    public string GetTitle() => _title;
    public void SetTitle(string title) => _title = title;
}