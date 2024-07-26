using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using TMPro;

[System.Serializable]
public class SerializationParty<T>
{
    public SerializationParty(List<T> _target) => target = _target;
    public List<T> target;
}

[System.Serializable]
public class PartyCharacter
{
    public PartyCharacter(string _Id, string _Name, string _Description, string _HP, string _STR, string _DEX, string _INT,string _CON, string _DEF,string _ATK, bool _isUsing, string _Type)
    {
        Id = _Id; Name = _Name; Description = _Description; 
        HP = _HP; STR = _STR; DEX = _DEX; 
        INT = _INT; CON = _CON; DEF = _DEF; ATK = _ATK;
        isUsing = _isUsing; Type = _Type;
    }

    
    // 캐릭터 관련 변수들
    public string Id, Name, Description, HP, STR, DEX, INT, CON, DEF, ATK;
    public string Type;
    public bool isUsing;
}

public class PartyButton : MonoBehaviour
{
    public PartyCharacter Character { get; set; }
}

public class PartyManager : MonoBehaviour
{
    [SerializeField] private List<PartyCharacter> AllCharacterList = new List<PartyCharacter>();
    [SerializeField] private List<PartyCharacter> MyCharacterList = new List<PartyCharacter>();
    [SerializeField] private List<PartyCharacter> CurCharacterList = new List<PartyCharacter>();
    public GameObject[] slot;
    
    public Sprite[] itemSprites;
    public Color SlotSelectColor = new Color32(225, 255, 225, 255);
    public Color SlotIdleColor = new Color32(255, 255, 255, 255);

    

    // 설정창 select 패널 연결
    public GameObject SelectCharInfor;

    // 캐릭터 소개 윈도우 뜰 수 있도록 하기
    public Button DesButton;
    public GameObject DesWindow;

    // 설명창에 캐릭터 정보를 띄우기 위한 변수들.
    public TextMeshProUGUI CharName_T,CharDescription_T;
    public TextMeshProUGUI CharHP_T,CharSTR_T,CharDEX_T,CharINT_T,CharCON_T,CharDEF_T,CharATK_T;


    // 정렬 버튼 연결
    public GameObject SortPanel;
    private string filePath;
    

    void Start()
    {
        // GUI 씬을 위에 추가해주기
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);

        // 나의 캐릭터 보유 목록
        filePath = Application.persistentDataPath + "/MyCharctertext.txt";
        print(filePath);

        LoadItemsFromCSV("CharacterSong"); // CSV 파일 이름, 확장자는 제외

        Debug.Log($"Loaded {AllCharacterList.Count} Charactor.");

        LoadItem();


        SelectCharInfor.SetActive(true);
        DesWindow.SetActive(false);
        

        // 기본 선택된 캐릭터 설정 (id가 "0"인 캐릭터 - 주인공)
        PartyCharacter selectedCharacter = AllCharacterList.Find(x => x.Id == "0");
        if (selectedCharacter != null)
        {
            SlotClick(selectedCharacter);
        }
        else
        {
            Debug.LogError("id가 0인 아이템을 찾을 수 없습니다.");
        }

        

        // MyItemList의 내용을 확인하기 위한 디버그 로그
        Debug.Log("MyItemList 내용 로드 후:");
        foreach (var item in MyCharacterList)
        {
            Debug.Log($"ID: {item.Id}, Name: {item.Name}, Description: {item.Description}, Price: {item.HP}");
        }
    }

    void LoadItemsFromCSV(string fileName)
    {
        var data = CSVReader.Read(fileName);

        if (data != null)
        {
            foreach (var entry in data)
            {
                //Id, Name, Description, HP, STR, DEX, INT, CON, DEF, ATK, isUsing, Type;
                string id = entry["id"].ToString();
                string name = entry["name"].ToString();
                string description = entry["description"].ToString();
                string HP = entry["HP"].ToString();
                string STR = entry["STR"].ToString();
                string DEX = entry["DEX"].ToString();
                string INT = entry["INT"].ToString();
                string CON = entry["CON"].ToString();
                string DEF = entry["DEF"].ToString();
                string ATK = entry["ATK"].ToString();
                string type = entry["type"].ToString();
                bool isUsing;
                
                if (!bool.TryParse(entry["isUsing"].ToString(), out isUsing))
                {
                    isUsing = false; // 파싱 실패 시 기본값 설정
                }
                

                AllCharacterList.Add(new PartyCharacter(id, name, description, HP, STR, DEX, INT, CON, DEF, ATK, isUsing, type));
            }
        }
        else
        {
            Debug.LogError("CSV 데이터를 불러오지 못했습니다.");
        }
    }


    public void IdleClick()
    {
        // 슬롯에 넣을 현재 아이템 리스트를 입력하기
        CurCharacterList = AllCharacterList;

        //CurCharacterList = AllCharacterList.FindAll(x => x.Type == tabName);
        // 정렬 패널의 텍스트 변경
        SortNum();

        // 슬롯과 텍스트를 보일 수 있도록 만들기
        for (int i = 0; i < slot.Length; i++)
        {
            bool active = i < CurCharacterList.Count;
            slot[i].SetActive(active);

            if (active)
            {
                // 1. 해당 용병의 직업 텍스트 변경하기
                TextMeshProUGUI JobTextComponent = slot[i].GetComponentInChildren<TextMeshProUGUI>();
                Transform panelTransform = slot[i].transform.Find("Panel");
                if (JobTextComponent != null)
                {
                    // 직업(타입) 텍스트 변경
                    if (JobTextComponent != null)
                    {
                        // 이거 나중에 Type으로 바꿔주기 -> 직업을 표시해야 함.
                        JobTextComponent.text = CurCharacterList[i].Type;
                    }
                    else
                    {
                        Debug.LogError("JobText가 Panel의 자식 구성원이 아닙니다.");
                    }
                    

                    // 이름 텍스트 변경
                    TextMeshProUGUI NameTextComponent = panelTransform.Find("NameText").GetComponentInChildren<TextMeshProUGUI>();
                    if (NameTextComponent != null)
                    {
                        NameTextComponent.text = CurCharacterList[i].Name;
                    }
                    else
                    {
                        Debug.LogError("NameText가 Panel의 자식 구성원이 아닙니다.");
                    }
                }
                else
                {
                    Debug.LogError("Panel이 Slot의 자식 구성원이 아닙니다.");
                }

                // 2. 캐릭터의 이미지 설정
                Transform imageTransform = slot[i].transform.Find("Char Image");
                if (imageTransform != null)
                {
                    Image imageComponent = imageTransform.GetComponent<Image>();
                    if (imageComponent != null)
                    {
                        // 현재 아이템의 Id를 기준으로 이미지를 설정
                        int itemId = int.Parse(CurCharacterList[i].Id);
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

                
            // 3. 선택중/선택 아님 설정 바꾸기 체크 이미지 설정 바꾸기
                Transform checkimageTransform = slot[i].transform.Find("Check Image");
                // 슬롯 자체 이미지 변수 slotimageComponent
                Image slotimageComponent = slot[i].GetComponent<Image>();
                // 슬롯 속 패널의 이미지
                Transform slotpanelTransform = slot[i].transform.Find("Panel");

                if (checkimageTransform != null)
                {
                    Image checkimageComponent = checkimageTransform.GetComponent<Image>();
                    if (checkimageComponent != null)
                    {
                        // isUsing이 true이면 체크 이미지를 활성화하고, false이면 비활성화
                        checkimageTransform.gameObject.SetActive(CurCharacterList[i].isUsing);

                        // 추가: 슬롯 자체 이미지 및 패널 이미지 설정
                        if (CurCharacterList[i].isUsing)
                        {
                            // 선택된 경우의 이미지 및 색상 설정
                            if (slotimageComponent != null)
                            {
                                slotimageComponent.color = SlotSelectColor; // 예시로 설정한 흰색
                            }
                            if (slotpanelTransform != null)
                            {
                                Image panelImageComponent = slotpanelTransform.GetComponent<Image>();
                                if (panelImageComponent != null)
                                {
                                    panelImageComponent.color = SlotSelectColor; // 예시로 설정한 회색
                                }
                            }
                        }
                        else
                        {
                            // 선택되지 않은 경우의 이미지 및 색상 설정
                            if (slotimageComponent != null)
                            {
                                slotimageComponent.color = SlotIdleColor; // 예시로 설정한 반투명 흰색
                            }
                            if (slotpanelTransform != null)
                            {
                                Image panelImageComponent = slotpanelTransform.GetComponent<Image>();
                                if (panelImageComponent != null)
                                {
                                    panelImageComponent.color = SlotIdleColor; // 예시로 설정한 흰색
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Image component not found in checkimageTransform's children.");
                    }
                }
                else
                {
                    Debug.LogError("Check Image Transform not found in slot's children.");
                }


                // 버튼에 아이템 정보 추가 및 클릭 이벤트 연결
                PartyButton partyButton = slot[i].GetComponent<PartyButton>();
                if (partyButton == null)
                {
                    partyButton = slot[i].AddComponent<PartyButton>();
                }
                partyButton.Character = CurCharacterList[i];
                Button button = slot[i].GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.RemoveAllListeners(); // 기존 이벤트 제거
                    button.onClick.AddListener(() => SlotClick(partyButton.Character));
                }
                else
                {
                    Debug.LogError("Button component not found in slot.");
                }
            }
        }
    }

    
    void SortNum() {
        TextMeshProUGUI SortTextComponent = SortPanel.GetComponentInChildren<TextMeshProUGUI>();
        SortTextComponent.text = AllCharacterList.Count + "/" + CurCharacterList.Count;

    }

    // 소개창 버튼 클릭시, 윈도우 띄우기
    public void OnDesWindow(string nametext, string destext) {
        TextMeshProUGUI DesTest = DesWindow.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI NaneTest = DesWindow.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        DesTest.text = destext;
        NaneTest.text = nametext;

        DesWindow.SetActive(true);
    }

    // 소개창 속 닫기 버튼 누르면, 창 꺼지도록 만들기
    public void ClickCloseButton() {
        DesWindow.SetActive(false);
    }


    // 슬롯 버튼 클릭 시 아이템 정보 표시
    public void SlotClick(PartyCharacter chra)
    {
        DesWindow.SetActive(false);
    
        CharName_T.text = chra.Type + " < " + chra.Name + " >";
        CharDescription_T.text = chra.Description;
        CharHP_T.text = "체력 : " + chra.HP;
        CharSTR_T.text = "공격 : " + chra.STR;
        CharDEX_T.text = "민첩 : " + chra.DEX;
        CharINT_T.text = "지능 : " + chra.INT;
        CharCON_T.text = "치유 : " + chra.CON;
        CharDEF_T.text = "방어 : " + chra.DEF;
        CharATK_T.text = "총 능력치 : " + chra.ATK;

        // 설명 버튼 누르면, 설명창 상호작용 할 수 있도록
        DesButton.onClick.AddListener(() => OnDesWindow(CharName_T.text,chra.Description));
        Button CloseButton = DesWindow.GetComponentInChildren<Button>();
        CloseButton.onClick.AddListener(ClickCloseButton);

        SelectCharInfor.SetActive(true); // 설명 창 활성화
    }

    
    // 리셋 버튼을 눌렀을 경우
    public void ReSetItemClick()
    {
        PartyCharacter BasicItem = AllCharacterList.Find(x => x.Id == "0");
        if (BasicItem != null)
        {
            MyCharacterList = new List<PartyCharacter>() { BasicItem };
        }
        else
        {
            Debug.LogError("id가 0인 아이템을 찾을 수 없습니다.");
            MyCharacterList.Clear();  // 빈 리스트로 설정하여 빈 값을 저장하지 않도록 합니다.
        }
        SaveItem();
    }

    void SaveItem()
    {
        string jdata = JsonUtility.ToJson(new SerializationParty<PartyCharacter>(MyCharacterList));
        File.WriteAllText(filePath, jdata);
    }

    void LoadItem()
    {
        if (!File.Exists(filePath))
        {
            ReSetItemClick(); // 초기 파일 생성 및 저장
            return;
        }
        
        string jdata = File.ReadAllText(filePath);
        MyCharacterList = JsonUtility.FromJson<SerializationParty<PartyCharacter>>(jdata).target;

        // Inspector에서 리스트가 업데이트되도록 합니다.
        // UnityEditor.EditorUtility.SetDirty(this);

        IdleClick();
    }

}
