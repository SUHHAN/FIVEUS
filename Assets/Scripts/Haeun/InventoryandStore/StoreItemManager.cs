using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using TMPro;

public class StoreButton : MonoBehaviour
{
    public Item Item { get; set; }
}

public class StoreItemManager : MonoBehaviour
{
    [SerializeField] private List<Item> AllItemList = new List<Item>();
    [SerializeField] private List<Item> MyItemList = new List<Item>();
    [SerializeField] private List<Item> CurItemList = new List<Item>();
    public string curType = "장비"; // 현재 고른 탭의 타입이 무엇인지
    public GameObject[] slot;
    public Image[] TabImage;
    public Sprite[] itemSprites;
    public Color TabSelectColor = new Color32(186, 227, 255, 255);
    public Color TabIdleColor = new Color32(255, 255, 255, 255);

    // 슬롯 버튼을 누르면 패널을 활성화하기 위함.
    public GameObject SelectItemInfor; // 미니 창 패널
    public Image itemImageText; // 설명창에 아이템 이미지 표시

    public TextMeshProUGUI itemNameText; // 설명창에 아이템 이름 표시
    public TextMeshProUGUI itemDescriptionText; // 설명창에 아이템 설명 표시
    public TextMeshProUGUI itemIdText; // 설명창에 아이템 아이디 표시
    public TextMeshProUGUI itemTypeText; // 설명창에 아이템 타입 표시

    private string filePath;

    void Start()
    {
        // Additively load the GUI scene
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);

        // LoadItem 호출
        LoadItem();
        Debug.Log($"Loaded {AllItemList.Count} items.");

        SelectItemInfor.SetActive(false); // 설명 창 비활성화

        // MyItemList의 내용을 확인하기 위한 디버그 로그
        Debug.Log("MyItemList 내용 로드 후:");
        foreach (var item in MyItemList)
        {
            Debug.Log($"ID: {item.Id}");
        }

        // 처음 시작할 때 "장비" 탭을 선택하도록 설정
        TapClick(curType);
    }

    // 탭에 따른 탭 선택 변경
    public void TapClick(string tabName)
    {
        // 현재 아이템 리스트에 클릭한 타입만 추가하기
        curType = tabName;
        CurItemList = MyItemList.FindAll(x => x.Type == tabName).FindAll(x => x.isSelling == true);

        // 슬롯과 텍스트를 보일 수 있도록 만들기
        for (int i = 0; i < slot.Length; i++)
        {
            bool active = i < CurItemList.Count;
            slot[i].SetActive(active);

            if (active)
            {
                // "Panel" 객체를 찾고 그 내부의 "NameText" 객체를 찾기
                TextMeshProUGUI nameTextComponent = slot[i].GetComponentInChildren<TextMeshProUGUI>();
                if (nameTextComponent != null)
                {
                    // 가격 텍스트 변경
                    nameTextComponent.text = CurItemList[i].Name;
                    Transform panelTransform = slot[i].transform.Find("Panel");

                    // 이름 텍스트 변경
                    TextMeshProUGUI priceTextComponent = panelTransform.Find("NameText").GetComponentInChildren<TextMeshProUGUI>();
                    if (priceTextComponent != null)
                    {
                        priceTextComponent.text = CurItemList[i].Price + " 원";
                    }
                    else
                    {
                        Debug.LogError("NameText component not found in Panel's children.");
                    }
                }
                else
                {
                    Debug.LogError("Panel Transform not found in slot's children.");
                }

                // 이미지 설정
                Transform imageTransform = slot[i].transform.Find("Image");
                if (imageTransform != null)
                {
                    Image imageComponent = imageTransform.GetComponent<Image>();
                    if (imageComponent != null)
                    {
                        // 현재 아이템의 Id를 기준으로 이미지를 설정
                        int itemId = int.Parse(CurItemList[i].Id);
                        if (itemId >= 0 && itemId < itemSprites.Length)
                        {
                            imageComponent.sprite = itemSprites[itemId];
                        }
                        else
                        {
                            Debug.LogError($"Item Id {itemId}에 맞는 이미지가 없습니다.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Image component not found in imageTransform's children.");
                    }
                }
                else
                {
                    Debug.LogError("Image Transform not found in slot's children.");
                }

                // 버튼에 아이템 정보 추가 및 클릭 이벤트 연결
                StoreButton storeButton = slot[i].GetComponent<StoreButton>();
                if (storeButton == null)
                {
                    storeButton = slot[i].AddComponent<StoreButton>();
                }
                storeButton.Item = CurItemList[i];
                Button button = slot[i].GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.RemoveAllListeners(); // 기존 이벤트 제거
                    button.onClick.AddListener(() => SlotClick(storeButton.Item));
                }
                else
                {
                    Debug.LogError("Button component not found in slot.");
                }
            }
        }

        int tabNum = 0;
        switch (tabName)
        {
            case "장비": tabNum = 0; break;
            case "물약": tabNum = 1; break;
            case "단서": tabNum = 2; break;
            case "기타": tabNum = 3; break;
        }
        for (int i = 0; i < TabImage.Length; i++)
        {
            TabImage[i].color = i == tabNum ? TabSelectColor : TabIdleColor;
        }
    }

    // 슬롯 버튼 클릭 시 아이템 정보 표시
    public void SlotClick(Item item)
    {
        int itemId = int.Parse(item.Id);
        if (itemId >= 0 && itemId < itemSprites.Length)
        {
            itemImageText.sprite = itemSprites[itemId];
        }
        else
        {
            Debug.LogError($"Item Id {itemId}에 맞는 이미지가 없습니다.");
        }

        itemNameText.text = item.Name;
        itemDescriptionText.text = item.Description;
        itemTypeText.text = item.Type;
        itemIdText.text = "No. " + item.Id;

        SelectItemInfor.SetActive(true); // 설명 창 활성화
    }

    void SaveItem()
    {
        DataManager.instance.SaveData();
    }

    void LoadItem()
    {
        MyItemList = DataManager.instance.nowPlayer.Items;
        AllItemList = DataManager.instance.nowPlayer.Items;
    }
}
