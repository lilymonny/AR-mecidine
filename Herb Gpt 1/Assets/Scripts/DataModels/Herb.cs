using System.Collections.Generic;

[System.Serializable]
public class Herb
{
    public string name; // Ò©²ÄÃû³Æ
    public string description; // Ò©²ÄÃèÊö
    public Dictionary<string, string> combinations; // ÅäÎé¹ØÏµ
}

[System.Serializable]
public class HerbArray
{
    public Herb[] herbs; // Ò©²ÄÊı×é
}