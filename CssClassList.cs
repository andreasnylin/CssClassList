public class CssClassList
{

    private readonly HashSet<string> _classes;

    public CssClassList()
    {
        _classes = new HashSet<string>();
    }

    public CssClassList(string cssClass)
    {
        _classes = new HashSet<string>();

        Add(cssClass);
    }

    public void AddIf(bool condition, string truthyCssClass, string falsyCssClass = null)
    {
        if (condition)
        {
            Add(truthyCssClass);
        }
        else if (falsyCssClass != null)
        {
            Add(falsyCssClass);
        }
    }

    public void Add(string cssClass)
    {
        if (cssClass.Contains(" "))
        {
            foreach (var cssClassPart in cssClass.Split(' '))
            {
                Add(cssClassPart);
            }
        }
        else
        {
            _classes.Add(cssClass);
        }
    }

    public void Add(IEnumerable<string> classes)
    {
        foreach (var cssClass in classes)
        {
            _classes.Add(cssClass);
        }
    }

    public bool HasClass(string cssClass)
    {
        return _classes.Contains(cssClass);
    }

    public void Remove(string cssClass)
    {
        _classes.Remove(cssClass);
    }

    public void Remove(IEnumerable<string> classes)
    {
        foreach (var cssClass in classes)
        {
            _classes.Remove(cssClass);
        }
    }

    public string RenderList()
    {
        return !_classes.Any() ? null : string.Join(" ", _classes);
    }

    public string RenderClassAttribute(bool renderIfEmpty = false)
    {
        if (!renderIfEmpty && !_classes.Any())
            return null;

        return string.Format("class=\"{0}\"", RenderList());
    }
}