using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    // UI Elements
    public Button[] herbButtons; // 8个药材按钮
    public Text resultText; // 配伍结果文本
    public Text herbDescriptionText; // 药材描述文本

    // 药材数据
    [System.Serializable]
    public class Herb
    {
        public string name;
        public string description;
        public List<string> combinations; // 组合规则
    }

    public Herb[] herbs;

    private Button firstSelectedButton = null;
    private Button secondSelectedButton = null;

    void Start()
    {
        // 为每个药材按钮添加点击事件
        for (int i = 0; i < herbButtons.Length; i++)
        {
            int index = i; // 捕获当前索引
            herbButtons[i].onClick.AddListener(() => OnHerbButtonClicked(index));
        }
    }

    // 当玩家点击药材按钮时触发
    void OnHerbButtonClicked(int index)
    {
        Herb selectedHerb = herbs[index];
        DisplayHerbDescription(selectedHerb); // 显示药材描述

        // 第一个药材未选中时
        if (firstSelectedButton == null)
        {
            firstSelectedButton = herbButtons[index]; // 选中第一个药材
            firstSelectedButton.GetComponent<Image>().color = Color.cyan; // 高亮显示
            return;
        }

        // 第二个药材未选中时
        if (secondSelectedButton == null && herbButtons[index].interactable)
        {
            secondSelectedButton = herbButtons[index]; // 选中第二个药材
            secondSelectedButton.GetComponent<Image>().color = Color.green; // 高亮显示
            CheckCombination(firstSelectedButton, secondSelectedButton); // 检查配伍
        }
    }

    // 显示药材描述
    void DisplayHerbDescription(Herb selectedHerb)
    {
        herbDescriptionText.text = $"药材：{selectedHerb.name}\n{selectedHerb.description}";
    }

    // 检查药材配伍效果
    void CheckCombination(Button firstButton, Button secondButton)
    {
        string firstHerbName = firstButton.GetComponentInChildren<Text>().text;
        string secondHerbName = secondButton.GetComponentInChildren<Text>().text;

        Herb firstHerb = GetHerbByName(firstHerbName);
        Herb secondHerb = GetHerbByName(secondHerbName);

        if (firstHerb == null || secondHerb == null)
        {
            resultText.text = "配伍无效，未找到药材。";
            return;
        }

        string result = CheckCombinationResult(firstHerb, secondHerb);
        resultText.text = result;

        // 配伍检查完成后
        if (result.Contains("配伍效果良好"))
        {
            // 配伍成功，按钮消失
            firstButton.gameObject.SetActive(false);
            secondButton.gameObject.SetActive(false);
        }

        // 重置选择状态，允许继续选择
        ResetSelection();
    }

    // 根据药材名称获取药材数据
    Herb GetHerbByName(string herbName)
    {
        foreach (var herb in herbs)
        {
            if (herb.name == herbName)
            {
                return herb;
            }
        }
        return null; // 未找到药材
    }

    // 检查药材配伍效果
    string CheckCombinationResult(Herb firstHerb, Herb secondHerb)
    {
        // 检查配伍规则：检查第一个药材能否与第二个药材配伍，反之亦然
        bool isValidCombination = false;

        if (firstHerb.combinations.Contains(secondHerb.name) || secondHerb.combinations.Contains(firstHerb.name))
        {
            isValidCombination = true;
        }

        if (isValidCombination)
        {
            return $"{firstHerb.name} 和 {secondHerb.name} 配伍效果良好！";
        }
        else
        {
            return $"{firstHerb.name} 和 {secondHerb.name} 配伍有禁忌！";
        }
    }

    // 清空选择状态
    void ResetSelection()
    {
        // 重置第二个按钮
        if (secondSelectedButton != null)
        {
            secondSelectedButton.GetComponent<Image>().color = Color.white; // 恢复颜色
        }

        secondSelectedButton = null; // 重置第二个药材按钮

        // 保留第一个按钮，玩家可以再次与其他药材组合
    }
}