namespace PI6.Components.Shared.Objects;

public class QuestionOption
{
    private int _id;
    private int _lp;
    private int _questionId;
    private string _text;

    public QuestionOption(int id, int lp, int questionId, string text)
    {
        _id = id;
        _lp = lp;
        _questionId = questionId;
        _text = text;
    }

    public int GetId() => _id;
    public void SetId(int id) => _id = id;
    public int GetLp() => _lp;
    public void SetLp(int lp) => _lp = lp;
    public int GetQuestionId() => _questionId;
    public void SetQuestionId(int id) => _questionId = id;
    public string GetText() => _text;
    public void SetText(string text) => _text = text;
}