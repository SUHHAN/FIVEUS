using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    [SerializeField] private List<Item> AllItemList = new List<Item>();
    [SerializeField] private List<Character> AllCharacterList = new List<Character>();
    private GiftManager GiftManager;   // 선물 관리 스크립트


    void Awake() {
        // 싱글톤 인스턴스 생성
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 자신을 파괴
            return;
        }
    }

    void Start()
    {
        LoadItem();
        GiftManager = new GiftManager();  // GiftManager 인스턴스 생성
    }

    // 확정 구매 및 판매 Listener -> 인벤토리에도 저장하기
    public void ConfirmItemQuantity(Item item, string sort, int currentQuantity) {
        if (item == null) {
            Debug.LogError("Item is null.");
            return;
        }

        if (instance == null) {
            Debug.LogError("InventoryItemManager.instance is null.");
            return;
        }
        
        if (sort == "구매") {
            GetItem_inv(item,sort,currentQuantity);
            int result = -1 * int.Parse(item.Price) * currentQuantity;
            IncreaseMoney(result);
            SaveData();

        }
        else if (sort == "판매") {
            GetItem_inv(item,sort,currentQuantity);
            int result = (int)(int.Parse(item.Price) * -1 * currentQuantity * 0.8);
            IncreaseMoney(result);
            SaveData();
        }
    }

    // 아이탬을 얻었을 경우, 수량을 바꿔주는 메서드
    public void GetItem_inv(Item item,string sort,int quantity)
    {
        Character player = DataManager.instance.nowPlayer.characters.Find(x => x.Id == "0");
        Item NowItem = DataManager.instance.nowPlayer.Items.Find(x => x.Name == item.Name);
        
        // 수량을 더해주기
        int NowItem_Quantity = int.Parse(NowItem.quantity);
        NowItem_Quantity += quantity;
        
        if (NowItem_Quantity < 0) { // 혹시라도 수량이 -가 되지 않도록 관리하기
            IncreaseMoney(int.Parse(item.Price) * (-1) * NowItem_Quantity);
            NowItem_Quantity = 0;
        }

        NowItem.quantity = NowItem_Quantity.ToString();           

        if (item.Type == "장비" && sort == "구매")
        {
            // 전직템 구매시, 구매한 전직템과 관련된 직업으로 바로 변경 및 장착됨.
            List<Item> AllJobItem = DataManager.instance.nowPlayer.Items.FindAll(x => x.Type == "장비");
            foreach(var ii in AllJobItem) {
                    ii.isUsing = false;
            } 
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
                    player.Type = "검사"; 
                    NowItem.isUsing = false;
                    AllJobItem.Find(x => x.Name == "장검").isUsing = true;
                    break;
            }
        }

        // 변경된 내용을 저장
        SaveData();
    }

    // 힌트를 찾았을 경우, 힌트를 랜덤으로 획득하게 하는 매서드
    public void GetHint_inv() {
        // AllHint 리스트에 모든 단서를 필터링
        List<Item> AllHint = DataManager.instance.nowPlayer.Items.FindAll(x => x.quantity == "0" && x.Type == "단서");

        // AllHint 리스트가 비어있는지 확인
        if (AllHint.Count == 0) {
            Debug.Log("다 찾았습니다. 더 찾을 필요가 없어요~");
            return;
        }

        // AllHint 리스트에서 랜덤한 아이템 선택
        int hintRandom = Random.Range(0, AllHint.Count); // 명시적으로 UnityEngine.Random 사용
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
        SaveData();

        Debug.Log($"아이템 {selectedHint.Name}의 quantity가 1증가했습니다 . 현재 quantity: {selectedHint.quantity}");
    }

    // 선물하기 -> 버튼 클릭 시
    public void Gift_inv(string charType, string giftName) {
        // 호감도를 가장 좋아하는 선물 / 그저 그런 선물 / 싫어하는 선물로 나눠서 배열로 저장.
        int[] loveNum = { 5, 0, -5 };

        // 선물 관리 매니저를 통해서 선물의 반응을 0, 1, 2로 가지고 오기
        if (GiftManager == null) {
            Debug.LogError("GiftManager is not initialized.");
            return;
        }

        int response = GiftManager.GetGiftResponse(charType, giftName);

        // 특정 조건에 맞는 캐릭터를 찾기
        Character giveChar = DataManager.instance.nowPlayer.characters.FindAll(x => x.Type == charType).Find(x => x.Id != "0");
        if (giveChar == null) {
            Debug.LogError($"Character of type {charType} not found.");
            return;
        }

        // 특정 이름에 맞는 아이템을 찾기
        Item giveItem = DataManager.instance.nowPlayer.Items.Find(x => x.Name == giftName);
        if (giveItem == null) {
            Debug.LogError($"Item with name {giftName} not found.");
            return;
        }

        // 일단 선물한 물건의 수량을 하나 감소 시키기
        int quantity;
        if (int.TryParse(giveItem.quantity, out quantity)) {
            quantity -= 1;
            if (quantity < 0) quantity = 0;  // 수량이 0 미만이 되지 않도록 방지
            giveItem.quantity = quantity.ToString();
        } else {
            Debug.LogError("Invalid item quantity.");
            return;
        }

        // 호감 대상의 호감도와 올릴 수치를 int로 선언
        int giveChar_love;
        if (int.TryParse(giveChar.Love, out giveChar_love)) {
            int giveItem_num = loveNum[response];

            // 호감 대상의 호감도 올리기
            giveChar_love += giveItem_num;
            giveChar.Love = giveChar_love.ToString();
        } else {
            Debug.LogError("Invalid character love value.");
            return;
        }

        SaveData();
    }


    // 사용하기
    public void UseItem_inv(Item item)
    {
        Character player = DataManager.instance.nowPlayer.characters.Find(x => x.Id == "0");
        Item NowItem = DataManager.instance.nowPlayer.Items.Find(x => x.Name == item.Name);
        
        // 1개 빼주기
        int NowItem_Quantity = int.Parse(item.quantity);
        NowItem_Quantity -= 1;
        
        if (NowItem_Quantity < 0) { // 혹시라도 수량이 -가 되지 않도록 관리하기
            NowItem_Quantity = 0;
        }

        item.quantity = NowItem_Quantity.ToString();           

        if (item.Type == "물약")
        {
            if (item.Id == "6" || item.Id == "7" || item.Id == "8") {
                IncreaseHealth(int.Parse(item.value));
            }
            else if (item.Id == "9" || item.Id == "10" || item.Id == "11") {
                DecreaseTiredness(int.Parse(item.value));
            }
        }
        else if (item.Type == "기타") {
            // 에시로 아무거나
            IncreaseHealth(0);
        }

        // 변경된 내용을 저장
        SaveData();
    }

    // 착용하기
    public void WearItem_inv(Item item)
    {
        Character player = DataManager.instance.nowPlayer.characters.Find(x => x.Id == "0");
        Item NowItem = DataManager.instance.nowPlayer.Items.Find(x => x.Name == item.Name);
         
        if (item.Type == "장비")
        {
            // 전직템 구매시, 구매한 전직템과 관련된 직업으로 바로 변경 및 장착됨.
            List<Item> AllJobItem = DataManager.instance.nowPlayer.Items.FindAll(x => x.Type == "장비");
            foreach(var ii in AllJobItem) {
                    ii.isUsing = false;
            } 
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
                    player.Type = "검사"; 
                    NowItem.isUsing = false;
                    AllJobItem.Find(x => x.Name == "장검").isUsing = true;
                    break;
            }
        }

        // 변경된 내용을 저장
        SaveData();

        // 변경된 내용을 저장
        SaveData();
    }

    // 플레이어 체력 증가
    public void IncreaseHealth(int amount)
    {
        DataManager.instance.nowPlayer.Player_hp += amount;
        if (DataManager.instance.nowPlayer.Player_hp > 100)
            DataManager.instance.nowPlayer.Player_hp = 100; // 최대 체력은 100으로 제한

    }

    // 플레이어 피로도 감소
    public void DecreaseTiredness(int amount)
    {
        DataManager.instance.nowPlayer.Player_tired -= amount;
        if (DataManager.instance.nowPlayer.Player_tired < 0)
            DataManager.instance.nowPlayer.Player_tired = 0; // 최소 피로도는 0으로 제한
    }

    // 플레이어 재화 증가
    public void IncreaseMoney(int amount)
    {
        DataManager.instance.nowPlayer.Player_money += amount;
    }


    void SaveData()
    {
        DataManager.instance.SaveData();
    }

    void LoadItem()
    {
        AllItemList = DataManager.instance.nowPlayer.Items;
        AllCharacterList = DataManager.instance.nowPlayer.characters;
    }
}

