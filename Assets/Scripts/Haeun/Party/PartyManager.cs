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
    public TextMeshProUGUI CharName_T,CharDescription_T,CharLove_T;
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
        //SceneManager.LoadScene("UI", LoadSceneMode.Additive);

        LoadCharacter();

        SelectCharInfor.SetActive(true);
        DesWindow.SetActive(false);
        

        // 기본 선택된 캐릭터 설정 (id가 "0"인 캐릭터 - 주인공)
        Character selectedCharacter = AllCharacterList.Find(x => x.Id == "0");
        if (selectedCharacter != null)
        {
            SlotClick(selectedCharacter);
        }

        UpdatePartyInfo();
    }

    // 창 생성
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
                    
                    // 이름 텍스트 변경
                    TextMeshProUGUI NameTextComponent = panelTransform.Find("NameText").GetComponentInChildren<TextMeshProUGUI>();
                    if (NameTextComponent != null)
                    {
                        NameTextComponent.text = CurCharacterList[i].Name;
                    }
                    
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
                    }
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

                        if(CurCharacterList[i].Success){    // 추가: 슬롯 자체 이미지 및 패널 이미지 설정
                            if (CurCharacterList[i].isUsing)
                            {
                                // 선택된 경우의 이미지 및 색상 설정 => 초록색 설정
                                if (slotimageComponent != null)
                                {
                                    slotimageComponent.color = SlotSelectColor; 
                                }
                                if (slotpanelTransform != null)
                                {
                                    Image panelImageComponent = slotpanelTransform.GetComponent<Image>();
                                    if (panelImageComponent != null)
                                    {
                                        panelImageComponent.color = SlotSelectColor; 
                                    }
                                }
                            }
                            else
                            {
                                // 선택되지 않은 경우의 이미지 및 색상 설정 => 흰색 설정
                                slotimageComponent.color = SlotIdleColor; 
                                Image panelImageComponent = slotpanelTransform.GetComponent<Image>();
                                panelImageComponent.color = SlotIdleColor; 
                                    
                            }
                        }
                        else
                        {
                            // 선택되지 않은 경우의 이미지 및 색상 설정 => 흰색 설정
                            slotimageComponent.color = new Color32(201,199,199,255); 
                            Image panelImageComponent = slotpanelTransform.GetComponent<Image>();
                            panelImageComponent.color = new Color32(201,199,199,255); 
                        }
                    } 
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
            }
        }
    }

    // 정렬 숫자 텍스트 입력해주는 매소드
    void SortNum() {
        List<Character> SuccessCharacterList = AllCharacterList.FindAll(x => x.Success == true);
        TextMeshProUGUI SortTextComponent = SortPanel.GetComponentInChildren<TextMeshProUGUI>();
        SortTextComponent.text = SuccessCharacterList.Count + "/" + AllCharacterList.Count;

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
        if(chra.Success == true)
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

            CharLove_T.text = chra.Love;

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
        else {
            DesWindow.SetActive(false);

            CharName_T.text = chra.Type + " < " + chra.Name + " >";
            CharDescription_T.text = "...";
            CharHP_T.text = "체력 : .";
            CharSTR_T.text = "공격 : .";
            CharDEX_T.text = "민첩 : .";
            CharINT_T.text = "지능 : .";
            CharCON_T.text = "치유 : .";
            CharDEF_T.text = "방어 : .";
            CharATK_T.text = "총 능력치 : .";

            CharLove_T.text = chra.Love;

            // 선택 버튼 설정
            SelectButton.onClick.RemoveAllListeners();
            SelectButton.interactable = false;

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
        SaveCharacter();
        UpdatePartyInfo(); // 캐릭터 변경 시 파티 정보 업데이트
    }

    // 슬롯 UI 업데이트 메서드
    void UpdateSlotsUI()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (i < CurCharacterList.Count)
            {
                Character chra = CurCharacterList[i];

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

                        if(CurCharacterList[i].Success){    // 추가: 슬롯 자체 이미지 및 패널 이미지 설정
                            if (CurCharacterList[i].isUsing)
                            {
                                // 선택된 경우의 이미지 및 색상 설정 => 초록색 설정
                                if (slotimageComponent != null)
                                {
                                    slotimageComponent.color = SlotSelectColor; 
                                }
                                if (slotpanelTransform != null)
                                {
                                    Image panelImageComponent = slotpanelTransform.GetComponent<Image>();
                                    if (panelImageComponent != null)
                                    {
                                        panelImageComponent.color = SlotSelectColor; 
                                    }
                                }
                            }
                            else
                            {
                                // 선택되지 않은 경우의 이미지 및 색상 설정 => 흰색 설정
                                slotimageComponent.color = SlotIdleColor; 
                                Image panelImageComponent = slotpanelTransform.GetComponent<Image>();
                                panelImageComponent.color = SlotIdleColor; 
                                    
                            }
                        }
                        else
                        {
                            // 선택되지 않은 경우의 이미지 및 색상 설정 => 흰색 설정
                            slotimageComponent.color = new Color32(201,199,199,255); 
                            Image panelImageComponent = slotpanelTransform.GetComponent<Image>();
                            panelImageComponent.color = new Color32(201,199,199,255); 
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

        SelectButton.interactable = true;

        if(chra.Success == true){    
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
        else
        {
            SelectButton.interactable = false;
            selectText.text = "선택불가";
            buttonColor.color = new Color32(93, 86, 84, 255);
        }
    }

    // 파티원 확인창을 업데이트하는 메서드
    void UpdatePartyInfo()
    {
        // 플레이어 이름 텍스트 변경
        playerNameText.text = $"[{DataManager.instance.nowPlayer.Player_name}]";
        teamText.text = $"총 단합력 : {DataManager.instance.nowPlayer.Player_team}";

        // isUsing이 true인 캐릭터만 선택
        List<Character> selectedCharacters = MyCharacterList.FindAll(ch => ch.isUsing && ch.Id != "0");

        // 파티 텍스트 및 이미지 업데이트
        for (int i = 0; i < PartyText.Length; i++)
        {
            Image slotPanelImage = PartyChraSlot[i].GetComponent<Image>();
            Transform panelTransform = PartyChraSlot[i].transform.Find("Select");
            panelTransform.gameObject.SetActive(true);

            Transform imageTransform = PartyChraSlot[i].transform.Find("Image");
            imageTransform.gameObject.SetActive(true);

            Transform textTransform = PartyChraSlot[i].transform.Find("Text");

            if (i < selectedCharacters.Count)
            {
                // 슬롯 색상 노란색으로 변경
                slotPanelImage.color = new Color32(251, 243, 195, 255); // 노란색

                // 파티원 텍스트 변경
                PartyText[i].text = $"{selectedCharacters[i].Name} ({selectedCharacters[i].Type})";

                // 캐릭터 이미지 설정
                if (imageTransform != null)
                {
                    Image imageComponent = imageTransform.GetComponent<Image>();
                    if (imageComponent != null)
                    {
                        int itemId = int.Parse(selectedCharacters[i].Id);
                        if (itemId >= 0 && itemId < itemSprites.Length)
                        {
                            imageComponent.sprite = itemSprites[itemId];
                        }
                    }
                }
                // 캐릭터 슬롯 텍스트 설정
                TextMeshProUGUI textComponent = textTransform.GetComponent<TextMeshProUGUI>();
                textComponent.text = $"<{selectedCharacters[i].Name}> 선택중";
            }
            else
            {
                //PartyText[i].text = "선택한 용병" + (i + 1).ToString();
                PartyText[i].text = "---------";

                // 슬롯 색상 회갈색으로 변경
                slotPanelImage.color = new Color32(130, 120, 120, 255); // 회갈색

                // 판넬도 비활성화
                panelTransform.gameObject.SetActive(false);

                // 이미지 비우기 또는 기본 이미지 설정
                imageTransform.gameObject.SetActive(false);

                // 캐릭터 슬롯 텍스트 설정
                TextMeshProUGUI textComponent = textTransform.GetComponent<TextMeshProUGUI>();
                textComponent.text = "";
            }
        }
    }

    void SaveCharacter()
    {

        DataManager.instance.nowPlayer.characters = MyCharacterList;
        DataManager.instance.nowPlayer.characters = AllCharacterList;
        
        DataManager.instance.SaveData();
    }

    void LoadCharacter()
    {
        DataManager.instance.LoadData();

        MyCharacterList = DataManager.instance.nowPlayer.characters;
        AllCharacterList = DataManager.instance.nowPlayer.characters;

        IdleClick();
        UpdatePartyInfo(); // 로드 후 파티 정보 업데이트
        

    }
}
