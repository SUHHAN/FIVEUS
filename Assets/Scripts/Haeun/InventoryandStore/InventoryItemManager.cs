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
    

    void Start()
    {
        
        // Additively load the GUI scene
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);

        LoadItem();
        Debug.Log($"Loaded {AllItemList.Count} items.");

        if (SelectItemInfor == null || itemNameText == null || itemDescriptionText == null)
        {
            
            if (SelectItemInfor == null) {
                Debug.LogError("SelectItemInfor 연결되지 않았습니다.");
            }
            if (itemNameText == null) {
                Debug.LogError("itemNameText 연결되지 않았습니다.");
            }
            if (itemDescriptionText == null)
            {
                Debug.LogError("itemDescriptionText 연결되지 않았습니다.");
            }
            return;
            
        }

        SelectItemInfor.SetActive(false); // 설명 창 비활성화

        // 처음 시작할 때 "장비" 탭을 선택하도록 설정
        TapClick(curType);

        // MyItemList의 내용을 확인하기 위한 디버그 로그
        Debug.Log("MyItemList 내용 로드 후:");
        foreach (var item in MyItemList)
        {
            Debug.Log($"ID: {item.Id}, Name: {item.Name}, Description: {item.Description}, isUsing: {item.isUsing}");
        }
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

        // 슬롯과 텍스트를 보일 수 있도록 만들기
        for (int i = 0; i < slot.Length; i++)
        {
            bool active = i < CurItemList.Count;
            slot[i].SetActive(active);

            if (active)
            {
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
            if(item.Type == "단서") {
                SelectButton.gameObject.SetActive(false);
            }
            if(item.Type == "기타") {
                // 근데 이건 물어봐야 할 듯? 기타에 선물하기 말고도 사용하기가 있어야 하니까
                Buttontext.text = "선물하기";
                SelectButton.gameObject.SetActive(true);
                SelectButton.onClick.AddListener(() => {
                    AudioManager.Instance.PlaySfx(AudioManager.Sfx.ButtonClick);
                    //UseItem_inv(item);
                });
            }
            if(item.Type == "물약") {
                Buttontext.text = "사용하기";
                SelectButton.gameObject.SetActive(true);
                SelectButton.onClick.AddListener(() => {
                    AudioManager.Instance.PlaySfx(AudioManager.Sfx.ButtonClick);
                    //UseItem_inv(item);
                });
            }
            if(item.Type == "장비") {
                Buttontext.text = "착용하기";
                SelectButton.gameObject.SetActive(true);
                SelectButton.onClick.AddListener(() => {
                    AudioManager.Instance.PlaySfx(AudioManager.Sfx.ButtonClick);
                    //WearItem_inv(item);
                });
            }
            SelectItemInfor.SetActive(true); // 설명 창 활성화
        }
    }

    public void GetItem_inv(Item item, int quantity)
    {
        Character player = DataManager.instance.nowPlayer.characters.Find(x => x.Id == "0");
        Item NowItem = DataManager.instance.nowPlayer.Items.Find(x => x.Name == item.Name);
        
        // 수량을 더해주기
        int NowItem_Quantity = int.Parse(NowItem.quantity);
        NowItem_Quantity += quantity;

        NowItem.quantity = NowItem_Quantity.ToString();
        
        // 장비를 구매했다면, 전직을 위해 바로 장착되도록 구현
        if (item.Type == "장비")
        {
            Item changeJobItem = AllItemList.Find(x => x.isUsing == true);
            NowItem.isUsing = true;

            switch (item.Id)
            {
                case "0":
                    player.Type = "검사"; break;
                case "1":
                    player.Type = "궁수"; break;
                case "2":
                    player.Type = "마법사"; break;
                case "3":
                    player.Type = "힐러"; break;
                case "4":
                    player.Type = "방패병"; break;
                case "5":
                    player.Type = "암살자"; break;
                default:
                    Debug.LogWarning($"Unknown item.Id: {item.Id}"); break;
            }
        }

        DataManager.instance.SaveData();
    }
    // public static void BuyItem_inv(Item item){
    //     int itemQuantity = int.Parse(item.quantity);
        
    // };
    // public void UseItem_inv(Item item){
    //     int itemQuantity = int.Parse(item.quantity);

    // };
    // public void WearItem_inv(Item item){
    //     int itemQitemQuantityuen = int.Parse(item.quantity);
        
    // };

    public void GetHint_inv() {
        // AllHint 리스트에 모든 단서를 필터링
        List<Item> AllHint = DataManager.instance.nowPlayer.Items.FindAll(x => x.quantity == "0" && x.Type == "단서");

        // AllHint 리스트가 비어있는지 확인
        if (AllHint.Count == 0) {
            Debug.Log("단서 아이템이 없습니다.");
            return;
        }

        // AllHint 리스트에서 랜덤한 아이템 선택
        int hintRandom = UnityEngine.Random.Range(0, AllHint.Count); // 명시적으로 UnityEngine.Random 사용
        Item selectedHint = AllHint[hintRandom];

        // 선택된 아이템의 quantity를 문자열에서 정수로 변환하여 1 증가시키고 다시 문자열로 변환
        int currentQuantity;
        if (int.TryParse(selectedHint.quantity, out currentQuantity)) {
            currentQuantity += 1;
            selectedHint.quantity = currentQuantity.ToString();
        } else {
            Debug.LogError("다 찾았습니다. 더 찾을 필요가 없어요~");
            return;
        }

        // 변경된 내용을 저장
        DataManager.instance.SaveData();

        Debug.Log($"아이템 {selectedHint.Name}의 quantity가 1증가했습니다 . 현재 quantity: {selectedHint.quantity}");
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
