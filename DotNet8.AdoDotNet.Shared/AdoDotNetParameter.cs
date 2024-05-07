namespace DotNet8.AdoDotNet.Shared;

public  class AdoDotNetParameter
{
    public AdoDotNetParameter()
    {
    }

    public AdoDotNetParameter(string name, object value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; set; }
    public object Value { get; set; }
}