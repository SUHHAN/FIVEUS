using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    [SerializeField] private List<Item> AllItemList = new List<Item>();
    

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
            GetItem_inv(item, currentQuantity);
            // 예시: PlayerManager_yj.instance.IncreaseMoney(item.Price * currentQuantity);

        }
        else if (sort == "판매") {
            GetItem_inv(item, -1 * currentQuantity);
            // 예시: PlayerManager_yj.instance.IncreaseMoney(item.Price * currentQuantity * 0.8);

        }
    }

    // 아이탬을 얻었을 경우, 수량을 바꿔주는 메서드
    public void GetItem_inv(Item item, int quantity)
    {
        Character player = DataManager.instance.nowPlayer.characters.Find(x => x.Id == "0");
        Item NowItem = DataManager.instance.nowPlayer.Items.Find(x => x.Name == item.Name);
        
        // 수량을 더해주기
        int NowItem_Quantity = int.Parse(NowItem.quantity);
        NowItem_Quantity += quantity;
        
        if (NowItem_Quantity < 0) {
            //PlayerManager_yj.instance.IncreaseMoney(item.Price * -1 * NowItem_Quantity);
            NowItem_Quantity = 0;
        }

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

        // 변경된 내용을 저장
        SaveData();
    }

    // 힌트를 찾았을 경우, 힌트를 랜덤으로 획득하게 하는 매서드
    public void GetHint_inv() {
        // AllHint 리스트에 모든 단서를 필터링
        List<Item> AllHint = DataManager.instance.nowPlayer.Items.FindAll(x => x.quantity == "0" && x.Type == "단서");

        // AllHint 리스트가 비어있는지 확인
        if (AllHint.Count == 0) {
            Debug.Log("단서 아이템이 없습니다.");
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

    void SaveData()
    {
        DataManager.instance.SaveData();
    }

    void LoadItem()
    {
        AllItemList = DataManager.instance.nowPlayer.Items;
    }
}
