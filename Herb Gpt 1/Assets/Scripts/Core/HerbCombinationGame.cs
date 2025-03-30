using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HerbCombinationGame : MonoBehaviour
{
    public Transform herbButtonContainer; // 药材按钮的父物体
    public Button herbButtonPrefab; // 药材按钮预制体
    public Text feedbackText; // 反馈文本

    private List<string> selectedHerbs = new List<string>(); // 选中的药材
    private List<Herb> herbList; // 药材数据列表
    private CombinationChecker combinationChecker; // 配伍检查器

    void Start()
    {
        combinationChecker = GetComponent<CombinationChecker>();
        LoadHerbData();
        CreateHerbButtons();
    }

    // 加载药材数据
    void LoadHerbData()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("herbs");
        if (jsonData != null)
        {
            HerbArray herbArray = JsonUtility.FromJson<HerbArray>(jsonData.text);
            herbList = new List<Herb>(herbArray.herbs);
        }
        else
        {
            Debug.LogError("未找到 herbs.json 文件，请检查路径！");
        }
    }

    // 动态生成药材按钮
    void CreateHerbButtons()
    {
        foreach (var herb in herbList)
        {
            Button herbButton = Instantiate(herbButtonPrefab, herbButtonContainer);
            herbButton.GetComponentInChildren<Text>().text = herb.name;
            herbButton.onClick.AddListener(() => OnHerbButtonClick(herb.name));
        }
    }

    // 药材按钮点击事件
    void OnHerbButtonClick(string herbName)
    {
        if (selectedHerbs.Contains(herbName))
        {
            feedbackText.text = herbName + " 已选，请选择其他药材！";
            return;
        }

        selectedHerbs.Add(herbName);

        if (selectedHerbs.Count == 2)
        {
            string result = combinationChecker.CheckCombination(selectedHerbs, herbList);
            feedbackText.text = result;

            selectedHerbs.Clear(); // 重置选择
        }
        else
        {
            feedbackText.text = herbName + " 已选，请选择第二种药材！";
        }
    }
}