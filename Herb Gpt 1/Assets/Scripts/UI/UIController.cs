using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    // UI Elements
    public Button[] herbButtons; // 8��ҩ�İ�ť
    public Text resultText; // �������ı�
    public Text herbDescriptionText; // ҩ�������ı�

    // ҩ������
    [System.Serializable]
    public class Herb
    {
        public string name;
        public string description;
        public List<string> combinations; // ��Ϲ���
    }

    public Herb[] herbs;

    private Button firstSelectedButton = null;
    private Button secondSelectedButton = null;

    void Start()
    {
        // Ϊÿ��ҩ�İ�ť��ӵ���¼�
        for (int i = 0; i < herbButtons.Length; i++)
        {
            int index = i; // ����ǰ����
            herbButtons[i].onClick.AddListener(() => OnHerbButtonClicked(index));
        }
    }

    // ����ҵ��ҩ�İ�ťʱ����
    void OnHerbButtonClicked(int index)
    {
        Herb selectedHerb = herbs[index];
        DisplayHerbDescription(selectedHerb); // ��ʾҩ������

        // ��һ��ҩ��δѡ��ʱ
        if (firstSelectedButton == null)
        {
            firstSelectedButton = herbButtons[index]; // ѡ�е�һ��ҩ��
            firstSelectedButton.GetComponent<Image>().color = Color.cyan; // ������ʾ
            return;
        }

        // �ڶ���ҩ��δѡ��ʱ
        if (secondSelectedButton == null && herbButtons[index].interactable)
        {
            secondSelectedButton = herbButtons[index]; // ѡ�еڶ���ҩ��
            secondSelectedButton.GetComponent<Image>().color = Color.green; // ������ʾ
            CheckCombination(firstSelectedButton, secondSelectedButton); // �������
        }
    }

    // ��ʾҩ������
    void DisplayHerbDescription(Herb selectedHerb)
    {
        herbDescriptionText.text = $"ҩ�ģ�{selectedHerb.name}\n{selectedHerb.description}";
    }

    // ���ҩ������Ч��
    void CheckCombination(Button firstButton, Button secondButton)
    {
        string firstHerbName = firstButton.GetComponentInChildren<Text>().text;
        string secondHerbName = secondButton.GetComponentInChildren<Text>().text;

        Herb firstHerb = GetHerbByName(firstHerbName);
        Herb secondHerb = GetHerbByName(secondHerbName);

        if (firstHerb == null || secondHerb == null)
        {
            resultText.text = "������Ч��δ�ҵ�ҩ�ġ�";
            return;
        }

        string result = CheckCombinationResult(firstHerb, secondHerb);
        resultText.text = result;

        // ��������ɺ�
        if (result.Contains("����Ч������"))
        {
            // ����ɹ�����ť��ʧ
            firstButton.gameObject.SetActive(false);
            secondButton.gameObject.SetActive(false);
        }

        // ����ѡ��״̬���������ѡ��
        ResetSelection();
    }

    // ����ҩ�����ƻ�ȡҩ������
    Herb GetHerbByName(string herbName)
    {
        foreach (var herb in herbs)
        {
            if (herb.name == herbName)
            {
                return herb;
            }
        }
        return null; // δ�ҵ�ҩ��
    }

    // ���ҩ������Ч��
    string CheckCombinationResult(Herb firstHerb, Herb secondHerb)
    {
        // ���������򣺼���һ��ҩ���ܷ���ڶ���ҩ�����飬��֮��Ȼ
        bool isValidCombination = false;

        if (firstHerb.combinations.Contains(secondHerb.name) || secondHerb.combinations.Contains(firstHerb.name))
        {
            isValidCombination = true;
        }

        if (isValidCombination)
        {
            return $"{firstHerb.name} �� {secondHerb.name} ����Ч�����ã�";
        }
        else
        {
            return $"{firstHerb.name} �� {secondHerb.name} �����н��ɣ�";
        }
    }

    // ���ѡ��״̬
    void ResetSelection()
    {
        // ���õڶ�����ť
        if (secondSelectedButton != null)
        {
            secondSelectedButton.GetComponent<Image>().color = Color.white; // �ָ���ɫ
        }

        secondSelectedButton = null; // ���õڶ���ҩ�İ�ť

        // ������һ����ť����ҿ����ٴ�������ҩ�����
    }
}