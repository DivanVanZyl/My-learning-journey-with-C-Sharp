﻿using System.Text;

public class HtmlElement
{
    public string Name, Text;
    public List<HtmlElement> Elements = new List<HtmlElement>();
    private const int indentSize = 2;

    public HtmlElement()
    { }

    public HtmlElement(string name, string text)
    {
        Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
        Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
    }

    private string ToStringImpl(int indent)
    {
        var sb = new StringBuilder();
        var i = new string(' ', indentSize * indent);
        sb.AppendLine($"{i}<{Name}>");

        if (!string.IsNullOrEmpty(Text))
        {
            sb.Append(new string(' ', indentSize * (indent + 1)));
            sb.AppendLine(Text);
        }

        foreach (var element in Elements)
        {
            sb.Append(element.ToStringImpl(indent + 1));
        }
        sb.AppendLine($"{i}</{Name}>");
        return sb.ToString();
    }

    public override string ToString()
    {
        return ToStringImpl(0);
    }
}

//HTML Builder with is a convenient API for constructing a tree of HTML elements.
public class HtmlBuilder
{
    private readonly string rootName;
    //Start the builder by specifying the root tag.
    HtmlElement root = new HtmlElement();

    public HtmlBuilder(string rootName)
    {
        this.rootName = rootName;
        root.Name = rootName;
    }

    public void AddChild(string childName, string childText)
    {
        var e = new HtmlElement(childName, childText);
        root.Elements.Add(e);
    }

    public override string ToString()
    {
        return root.ToString();
    }

    public void Clear()
    {
        root = new HtmlElement() { Name = rootName };
    }
}