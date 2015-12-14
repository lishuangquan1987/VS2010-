using System;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
public class PrincipalClass : Attribute
{
    private string name;

    public PrincipalClass(string ClassName)
    {
        name = ClassName;
    }  

    public string ClassName
    {
        get { return name; }
    }
}

[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
public class RelativeModule : Attribute
{
    private string uipath;
    private string docpath;

    public RelativeModule(string ui, string doc)
    {
        uipath = ui;
        docpath = doc;
    }

    public string UiPath
    {
        get { return uipath; }
    }

    public string DocPath
    {
        get { return docpath; }
    }
}
