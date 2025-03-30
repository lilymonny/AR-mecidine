using UnityEngine;
using System.Collections.Generic;

public class CombinationChecker : MonoBehaviour
{
    public string CheckCombination(List<string> herbs, List<Herb> herbList)
    {
        if (herbs.Count != 2) return "请同时选择两种药材";

        Herb herb1 = herbList.Find(h => h.name == herbs[0]);
        Herb herb2 = herbList.Find(h => h.name == herbs[1]);

        if (herb1 == null || herb2 == null) return "药材数据缺失";

        if (herb1.combinations.ContainsKey(herb2.name))
        {
            return herb1.combinations[herb2.name];
        }
        else if (herb2.combinations.ContainsKey(herb1.name))
        {
            return herb2.combinations[herb1.name];
        }

        return "两种药材无特殊配伍效果";
    }
}