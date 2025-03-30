using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HerbCombinationGame : MonoBehaviour
{
    public Transform herbButtonContainer; // ҩ�İ�ť�ĸ�����
    public Button herbButtonPrefab; // ҩ�İ�ťԤ����
    public Text feedbackText; // �����ı�

    private List<string> selectedHerbs = new List<string>(); // ѡ�е�ҩ��
    private List<Herb> herbList; // ҩ�������б�
    private CombinationChecker combinationChecker; // ��������

    void Start()
    {
        combinationChecker = GetComponent<CombinationChecker>();
        LoadHerbData();
        CreateHerbButtons();
    }

    // ����ҩ������
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
            Debug.LogError("δ�ҵ� herbs.json �ļ�������·����");
        }
    }

    // ��̬����ҩ�İ�ť
    void CreateHerbButtons()
    {
        foreach (var herb in herbList)
        {
            Button herbButton = Instantiate(herbButtonPrefab, herbButtonContainer);
            herbButton.GetComponentInChildren<Text>().text = herb.name;
            herbButton.onClick.AddListener(() => OnHerbButtonClick(herb.name));
        }
    }

    // ҩ�İ�ť����¼�
    void OnHerbButtonClick(string herbName)
    {
        if (selectedHerbs.Contains(herbName))
        {
            feedbackText.text = herbName + " ��ѡ����ѡ������ҩ�ģ�";
            return;
        }

        selectedHerbs.Add(herbName);

        if (selectedHerbs.Count == 2)
        {
            string result = combinationChecker.CheckCombination(selectedHerbs, herbList);
            feedbackText.text = result;

            selectedHerbs.Clear(); // ����ѡ��
        }
        else
        {
            feedbackText.text = herbName + " ��ѡ����ѡ��ڶ���ҩ�ģ�";
        }
    }
}