using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine.TextCore.Text;

public class ItemButton : MonoBehaviour
{
    public Item Item { get; set; }
}

public class InventoryItemManager : MonoBehaviour
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
    public Image itemImage; // 설명창에 아이템 이미지 표시
    public Image XitemImage; // 설명창에 비활성화 아이템 이미지 표시

    public TextMeshProUGUI itemNameText; // 설명창에 아이템 이름 표시
    public TextMeshProUGUI itemDescriptionText; // 설명창에 아이템 설명 표시
    public TextMeshProUGUI itemIdText; // 설명창에 아이템 아이디 표시
    public TextMeshProUGUI itemQuantityText; // 설명창에 아이템 타입 표시

    public Button SelectButton; // 탭마다 다른 버튼 형식 만들기
    public Button SelectButton_gi; // 탭마다 다른 버튼 형식 만들기
    public Button GiftButton_gi;
    
    public string npcName;

    void Start()
    {
        
        // GUI 씬을 추가로 로드합니다.
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);

        // 씬이 로드된 후 PlayerPrefs 값을 확인
        if (PlayerPrefs.HasKey("NpcType"))
        {
            string npcType = PlayerPrefs.GetString("NpcType");
            Debug.Log($"NpcType in InventoryMain: {npcType}");
        }
        else
        {
            Debug.LogWarning("NpcType key not found in PlayerPrefs.");
        }
    
        LoadItem();

        Debug.Log($"Loaded {AllItemList.Count} items.");

        SelectItemInfor.SetActive(false); // 설명 창 비활성화
        
        // 선물하기 버튼은 일단 비활성화, 그리고 기타 탭에서만 사용되는 두개의 버튼은 일단 비활성화
        GiftButton_gi.gameObject.SetActive(false);
        SelectButton_gi.gameObject.SetActive(false);


        // 처음 시작할 때 "장비" 탭을 선택하도록 설정
        TapClick(curType);
    }

    // 탭에 따른 탭 선택 변경
    public void TapClick(string tabName)
    {
        // 현재 아이템 리스트에 클릭한 타입 + 양이 1개보다 더 많은 경우 표시
        curType = tabName;
        //CurItemList = AllItemList.FindAll(x => x.Type == tabName);
        // -> 현재 탭이 단서이면, 단서 전부를, 현재 탭이 단서가 아니면 현재탭 이름과 수량이 1 이상인 것만 출력
        if (tabName == "단서") {
                CurItemList = AllItemList.FindAll(x => x.Type == tabName);
        }else{
            CurItemList = AllItemList.FindAll(x => x.Type == tabName && int.Parse(x.quantity) > 0);
        }

        // 만약 탭이 "기타" - 두개 버튼 활성화 및 원래 버튼 비활성화
        if (tabName == "기타") {
            GiftButton_gi.gameObject.SetActive(true);
            SelectButton_gi.gameObject.SetActive(true);
            SelectButton.gameObject.SetActive(false);

            UpdateGiftButton();
        }
        else if (tabName == "단서") {
            GiftButton_gi.gameObject.SetActive(false);
            SelectButton_gi.gameObject.SetActive(false);
            SelectButton.gameObject.SetActive(false);
        }
        else {
            GiftButton_gi.gameObject.SetActive(false);
            SelectButton_gi.gameObject.SetActive(false);
            SelectButton.gameObject.SetActive(true);
        }


        // 슬롯과 텍스트를 보일 수 있도록 만들기
        for (int i = 0; i < slot.Length; i++)
        {
            bool active = i < CurItemList.Count;
            slot[i].SetActive(active);

            if (active)
            {

                // 체크 이미지 장착 중일때만, 보이도록 설정하기
                Transform checkImage = slot[i].transform.Find("isUsing");
                if(CurItemList[i].Type == "장비" && CurItemList[i].isUsing == true) {
                    checkImage.gameObject.SetActive(true);
                }else{
                    checkImage.gameObject.SetActive(false);
                }

                // 텍스트 변경
                TextMeshProUGUI textComponent = slot[i].GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = CurItemList[i].Name;
                }

                // 이미지 설정
                Transform imageTransform = slot[i].transform.Find("Image");
                Transform XimageTransform = slot[i].transform.Find("X Image");
                
                if (imageTransform != null)
                {
                    Image imageComponent = imageTransform.GetComponent<Image>();
                    Image XimageComponent = XimageTransform.GetComponent<Image>();

                    if (imageComponent != null && XimageComponent != null)
                    {
                        // 현재 아이템의 Id를 기준으로 이미지를 설정
                        int itemId = int.Parse(CurItemList[i].Id);
                        if (itemId >= 0 && itemId < itemSprites.Length)
                        {
                            XimageTransform.gameObject.SetActive(false);
                            imageComponent.sprite = itemSprites[itemId];

                            // 기본적으로는, [비활성화 사진]은 비활성화 / 그러나 단서이고 수량이 0이면 비활성화 사진을 출력
                            if (curType == "단서" && int.Parse(CurItemList[i].quantity) == 0) 
                            {
                                XimageComponent.sprite = itemSprites[itemId];
                                XimageTransform.gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            Debug.LogError($"Item Id {itemId}에 맞는 이미지가 없습니다.");
                        }
                    }
                }

                // 수량 panel 설정
                Transform quantityTransform = slot[i].transform.Find("Quantity");
                TextMeshProUGUI quantityText = quantityTransform.GetComponentInChildren<TextMeshProUGUI>();
                quantityText.text = CurItemList[i].quantity;


                // 버튼에 아이템 정보 추가 및 클릭 이벤트 연결
                ItemButton itemButton = slot[i].GetComponent<ItemButton>();
                if (itemButton == null)
                {
                    itemButton = slot[i].AddComponent<ItemButton>();
                }
                itemButton.Item = CurItemList[i];
                Button button = slot[i].GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.RemoveAllListeners(); // 기존 이벤트 제거
                    button.onClick.AddListener(() => SlotClick(itemButton.Item));
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
        // 선택 버튼의 텍스트를 변경하기 : 선물하기 창착하기 사용하기 등
        TextMeshProUGUI Buttontext = SelectButton.GetComponentInChildren<TextMeshProUGUI>();
        
        // 단서이고, 수량이 0인 경우
        if(item.Type == "단서" && int.Parse(item.quantity) == 0){
            int itemId = int.Parse(item.Id);
            if (itemId >= 0 && itemId < itemSprites.Length)
            {
                XitemImage.gameObject.SetActive(false);
                itemImage.sprite = itemSprites[itemId];
                if (curType == "단서" && int.Parse(item.quantity) == 0) 
                {
                    XitemImage.sprite = itemSprites[itemId];
                    XitemImage.gameObject.SetActive(true);
                }
            }

            itemNameText.text = item.Name;
            itemDescriptionText.text = "";
            itemQuantityText.text = item.quantity + " 개";
            itemIdText.text = "No. " + item.Id;
            SelectButton.gameObject.SetActive(false);

            SelectItemInfor.SetActive(true); // 설명 창 활성화
        }
        else // 그외의 경우
        {
            int itemId = int.Parse(item.Id);
            if (itemId >= 0 && itemId < itemSprites.Length)
            {
                XitemImage.gameObject.SetActive(false);
                itemImage.sprite = itemSprites[itemId];
            }

            itemNameText.text = item.Name;
            itemDescriptionText.text = item.Description;
            itemQuantityText.text = item.quantity + " 개";
            itemIdText.text = "No. " + item.Id;

            SelectButton.onClick.RemoveAllListeners();
            SelectButton_gi.onClick.RemoveAllListeners();
            GiftButton_gi.onClick.RemoveAllListeners();

            if(item.Type == "단서") {
                SelectButton.gameObject.SetActive(false);
            }
            if(item.Type == "기타") {
                SelectButton_gi.onClick.AddListener(() => {
                    AudioManager.Instance.PlaySfx(AudioManager.Sfx.ButtonClick);
                    onUseButtonClick(item);
                });
                GiftButton_gi.onClick.AddListener(() => {
                    AudioManager.Instance.PlaySfx(AudioManager.Sfx.ButtonClick);
                    onGiftButtonClick(item.Name);
                });
            }
            if(item.Type == "물약") {
                Buttontext.text = "사용하기";
                SelectButton.onClick.AddListener(() => {
                    AudioManager.Instance.PlaySfx(AudioManager.Sfx.ButtonClick);
                    onUseButtonClick(item);
                });
            }
            if(item.Type == "장비") {
                Buttontext.text = "장착하기";
                SelectButton.onClick.AddListener(() => {
                    AudioManager.Instance.PlaySfx(AudioManager.Sfx.ButtonClick);
                                    onWearButtonClick(item);
                    TapClick(curType);
                });
            }
            SelectItemInfor.SetActive(true); // 설명 창 활성화
        }
    }

    // 선물하기
    public void onGiftButtonClick(string giftName) {
        npcName = PlayerPrefs.GetString("NpcType");
        ItemManager.instance.Gift_inv(npcName, giftName);
        
        PlayerPrefs.DeleteKey("NpcType");
        PlayerPrefs.Save();

        LoadItem();

        SceneManager.LoadScene("IngameEx");
    }

    // 선택 버튼 UI 업데이트 메서드
    void UpdateGiftButton()
    {
        if (PlayerPrefs.HasKey("NpcType"))
        {
            GiftButton_gi.interactable = true;
        }
        else
        {
            GiftButton_gi.interactable = false;
        }
    }
    


    // 장착하기
    public void onWearButtonClick(Item item) {
        ItemManager.instance.WearItem_inv(item);
        SaveItem();

        LoadItem();

        // UI 즉시 업데이트
        TapClick(curType);
    }


    // 사용하기
    public void onUseButtonClick(Item item) {
        ItemManager.instance.UseItem_inv(item);
        SaveItem();
        LoadItem();
    }

    void SaveItem()
    {
        DataManager.instance.nowPlayer.Items = MyItemList;
        DataManager.instance.nowPlayer.Items = AllItemList;
        DataManager.instance.SaveData();
    }

    void LoadItem()
    {
        MyItemList = DataManager.instance.nowPlayer.Items;
        AllItemList = DataManager.instance.nowPlayer.Items;

        TapClick(curType);
        UpdateGiftButton();
    }
}

