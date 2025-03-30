using System.Collections.Generic;

[System.Serializable]
public class Herb
{
    public string name; // ҩ������
    public string description; // ҩ������
    public Dictionary<string, string> combinations; // �����ϵ
}

[System.Serializable]
public class HerbArray
{
    public Herb[] herbs; // ҩ������
}