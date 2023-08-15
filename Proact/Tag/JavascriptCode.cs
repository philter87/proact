namespace Proact.Tag;

public class JavascriptCode
{
    private readonly string _code;

    public JavascriptCode(string code)
    {
        _code = code;
    }

    public JavascriptCode Then(JavascriptCode codeAfter)
    {
        return new JavascriptCode($"{_code}.then(() => {codeAfter})");
    }

    public static implicit operator string(JavascriptCode code) => code._code;
}