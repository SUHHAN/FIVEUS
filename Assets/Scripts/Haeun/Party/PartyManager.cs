using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using TMPro;
using UnityEditor.VersionControl;


public class PartyButton : MonoBehaviour
{
    public Character Character { get; set; }
}

public class PartyManager : MonoBehaviour
{
    [Header("#기본 데이터")]
    [SerializeField] private List<Character> AllCharacterList = new List<Character>();
    [SerializeField] private List<Character> MyCharacterList = new List<Character>();
    [SerializeField] private List<Character> CurCharacterList = new List<Character>();
    public GameObject[] slot;

    private int checkSum = 0;

    
    [Header("#슬롯마다 참조")]
    public Sprite[] itemSprites;
    public Color SlotSelectColor = new Color32(225, 255, 225, 255);
    public Color SlotIdleColor = new Color32(255, 255, 255, 255);

    
    [Header("#파티원 확인창")]
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI[] PartyText;
    public TextMeshProUGUI teamText;

    // 캐릭터 슬롯창 업데이트
    public GameObject[] PartyChraSlot;

    

    [Header("#설명창")]
    // 설정창 select 패널 연결
    public GameObject SelectCharInfor;

    // 설명창에 캐릭터 정보를 띄우기 위한 변수들.
    public TextMeshProUGUI CharName_T,CharDescription_T;
    public TextMeshProUGUI CharHP_T,CharSTR_T,CharDEX_T,CharINT_T,CharCON_T,CharDEF_T,CharATK_T;

    // 캐릭터 소개 윈도우 뜰 수 있도록 하기
    public Button DesButton;
    public GameObject DesWindow;

    // 초과 선택 시 팝업
    public GameObject PopupWindow;

    // 장착 및 장착 해제를 위한 버튼 연결
    public Button SelectButton;


    [Header("#캐릭터 목록")]
    // 정렬 버튼 연결
    public GameObject SortPanel;
    

    void Start()
    {
        // GUI 씬을 위에 추가해주기
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);

        LoadCharacter();

        SelectCharInfor.SetActive(true);
        DesWindow.SetActive(false);
        

        // 기본 선택된 캐릭터 설정 (id가 "0"인 캐릭터 - 주인공)
        Character selectedCharacter = AllCharacterList.Find(x => x.Id == "0");
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

    // 창 생성
    public void IdleClick()
    {
        // 슬롯에 넣을 현재 아이템 리스트를 입력하기
        CurCharacterList = AllCharacterList;

        //CurCharacterList = AllCharacterList.FindAll(x => x.Type == tabName);
        // 정렬 패널의 텍스트 변경
        SortNum();

        // 플레이어 이름 텍스트 변경
        playerNameText.text = DataManager.instance.nowPlayer.Player_name;

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

    // 정렬 숫자 텍스트 입력해주는 매소드
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
        PopupWindow.SetActive(false);
    }

    // 2. 슬롯 클릭 시
    // 슬롯 클릭 시 호출되는 메서드
    public void SlotClick(Character chra)
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

        // 선택 버튼 설정
        SelectButton.onClick.RemoveAllListeners();
        SelectButton.interactable = true;

        // 주인공은 무조건 선택 해제할 수 없도록 설정. -> 그 외에는 선택 및 선택 해제가 가능하도록 함.
        if(chra.Id == "0")
        {
            SelectButton.interactable = false;
            TextMeshProUGUI selectText = SelectButton.GetComponentInChildren<TextMeshProUGUI>();
            selectText.text = "해제불가";
            Image buttonColor = SelectButton.GetComponent<Image>();
            buttonColor.color = new Color32(93, 86, 84, 255);
        }
        else
        {
            SelectButton.onClick.AddListener(() => changeIsUsing(chra));
            UpdateSelectButton(chra);
        }

        DesButton.onClick.RemoveAllListeners();
        DesButton.onClick.AddListener(() => OnDesWindow(CharName_T.text, chra.Description));
        Button CloseButton = DesWindow.GetComponentInChildren<Button>();
        CloseButton.onClick.RemoveAllListeners();
        CloseButton.onClick.AddListener(ClickCloseButton);

        SelectCharInfor.SetActive(true);
    }

    // isUsing 개수 확인하기
    void checkIsUsing() {
        foreach (var ii in MyCharacterList) {
            if(ii.isUsing == true) {
                checkSum++;
            }
        }
    }

    // 캐릭터 선택 및 해제 처리
    void changeIsUsing(Character chra)
    {
        checkSum = 0;
        checkIsUsing();

        if (checkSum >= 4 && !chra.isUsing)
        {
            Debug.Log("최대 4명의 캐릭터만 선택할 수 있습니다.");
            PopupWindow.SetActive(true);
            Button CloseButton = PopupWindow.GetComponentInChildren<Button>();
            CloseButton.onClick.RemoveAllListeners();
            CloseButton.onClick.AddListener(ClickCloseButton);

        }
        else
        {
            chra.isUsing = !chra.isUsing;
            SaveCharacter();
            UpdateSelectButton(chra);
            UpdateSlotsUI(); // 슬롯 UI 업데이트 호출
        }
    }

    // 슬롯 UI 업데이트 메서드
    void UpdateSlotsUI()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (i < CurCharacterList.Count)
            {
                Character chra = CurCharacterList[i];

                // 슬롯의 선택 상태를 반영
                Transform checkimageTransform = slot[i].transform.Find("Check Image");
                Image slotimageComponent = slot[i].GetComponent<Image>();
                Transform slotpanelTransform = slot[i].transform.Find("Panel");

                if (checkimageTransform != null)
                {
                    Image checkimageComponent = checkimageTransform.GetComponent<Image>();
                    checkimageTransform.gameObject.SetActive(chra.isUsing);

                    // 슬롯 배경 및 패널 색상 설정
                    if (chra.isUsing)
                    {
                        if (slotimageComponent != null)
                            slotimageComponent.color = SlotSelectColor;

                        if (slotpanelTransform != null)
                        {
                            Image panelImageComponent = slotpanelTransform.GetComponent<Image>();
                            if (panelImageComponent != null)
                                panelImageComponent.color = SlotSelectColor;
                        }
                    }
                    else
                    {
                        if (slotimageComponent != null)
                            slotimageComponent.color = SlotIdleColor;

                        if (slotpanelTransform != null)
                        {
                            Image panelImageComponent = slotpanelTransform.GetComponent<Image>();
                            if (panelImageComponent != null)
                                panelImageComponent.color = SlotIdleColor;
                        }
                    }
                }
            }
        }
    }

    // 선택 버튼 UI 업데이트 메서드
    void UpdateSelectButton(Character chra)
    {
        Image buttonColor = SelectButton.GetComponent<Image>();
        TextMeshProUGUI selectText = SelectButton.GetComponentInChildren<TextMeshProUGUI>();

        if (chra.isUsing)
        {
            selectText.text = "선택해제";
            buttonColor.color = new Color32(90, 46, 46, 255);
        }
        else
        {
            selectText.text = "선택하기";
            buttonColor.color = new Color32(36, 66, 35, 255);
        }
    }
    void SaveCharacter()
    {
        DataManager.instance.SaveData();
    }

    void LoadCharacter()
    {
        DataManager.instance.LoadData();

        MyCharacterList = DataManager.instance.nowPlayer.characters;
        AllCharacterList = DataManager.instance.nowPlayer.characters;

        IdleClick();

    }
}
