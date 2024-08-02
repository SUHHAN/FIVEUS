using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SelectSlotManager : MonoBehaviour
{
    public GameObject playernameInputPopup; // 플레이어 이름 입력창.
    public TextMeshProUGUI[] slotText;      // 슬롯 버튼에 추가될 text.
    public TMP_InputField newPlayername;    // 새로 입력된 플레이어의 닉네임.

    public bool[] savefile = new bool[3];  // 세이브 파일의 존재 유무 저장

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++) {
            // 슬롯 별로 저장된 데이터가 존재하는지 판단.
            if (File.Exists(DataManager.instance.path + $"{i}")) {
                savefile[i] = true; // 해당 슬롯 번호의 bool 배열을 true로 변환
                DataManager.instance.nowSlot = i;   // 선택한 슬롯 번호를 저장
                DataManager.instance.LoadData();    // 해당 슬롯 데이터를 불러옴
                slotText[i].text = "플레이어 이름 : " + DataManager.instance.nowPlayer.Player_name;  // 버튼에 닉네임 표시
            }
            else {
            // 해당 슬롯에 존재하는 데이터가 없으면
                slotText[i].text = "비어 있음";
            }
        }
        // 불러왔던 게임 데이터는 변수 초기화 시킴. -> 단지 버튼에 닉네임을 표현하기 위해서 가지고 온 것이므로, 변수 초기화를 해줘야 게임 데이터가 섞이지 않음.
        DataManager.instance.ClearData();
    }

    // 슬롯에 데이터가 존재하는지 확인하는 매소드
    public void CheckData() {
        for (int i = 0; i < 3; i++) {
            // 슬롯 별로 저장된 데이터가 존재하는지 판단.
            if (File.Exists(DataManager.instance.path + $"{i}")) {
                savefile[i] = true; // 해당 슬롯 번호의 bool 배열을 true로 변환
            }
            else {
                savefile[i] = false; // 해당 슬롯 번호의 bool 배열을 true로 변환
            }
        }
            // 불러왔던 게임 데이터는 변수 초기화 시킴. -> 단지 버튼에 닉네임을 표현하기 위해서 가지고 온 것이므로, 변수 초기화를 해줘야 게임 데이터가 섞이지 않음.
            DataManager.instance.ClearData();
    }


    // 슬롯 자체 기능 구현 => 이건 유니티 내에서 버튼을 지정해줬음.
    public void Slot(int number) {

        // 내가 선택한 슬롯에 대한 번호를 현재 슬롯을 저장하는 변수 nowSlot에 int로 저장.
        DataManager.instance.nowSlot = number;

        // 2. 저장된 데이터가 이미 있을 때 : 팝업을 불러오지 않고 저장된 데이터를 통해 게임씬으로 넘어가도록 하기
        if(savefile[number]) {
            DataManager.instance.LoadData();
            GoIngame();
        }
        // 1. 저장된 데이터가 없을 때 : 그냥 이름 입력 팝업을 불러오면 됨.
        else {
            NewPlayerCreate();
        }
    } 

    // 플레이어 이름을 입력하는 팝업창을 활성화 시킴
    public void NewPlayerCreate() { 
        playernameInputPopup.gameObject.SetActive(true);
    }

    // 새 플레이어 이름 저장 및 게임 시작 버튼을 누를 때 호출
    public void OnNewPlayerNameEntered() {
        if (string.IsNullOrEmpty(newPlayername.text)) {
            Debug.LogError("플레이어 이름이 입력되지 않았습니다.");
            return;
        }

        if (!savefile[DataManager.instance.nowSlot]) {
            DataManager.instance.nowPlayer.Player_name = newPlayername.text;
            DataManager.instance.LoadCharactersFromCSV("Character", newPlayername.text);
            DataManager.instance.LoadItemsFromCSV("Item");
            DataManager.instance.SaveData(); // 현재의 정보를 저장함.
        }

        SceneManager.LoadScene("main_map");
    }

    // 원하는 파일의 정보를 가지고 게임을 시작하기.
    public void GoIngame() {
        if (!savefile[DataManager.instance.nowSlot]) {
            DataManager.instance.nowPlayer.Player_name = newPlayername.text;
            DataManager.instance.LoadCharactersFromCSV("Character", newPlayername.text);
            DataManager.instance.LoadItemsFromCSV("Item");
            DataManager.instance.SaveData(); // 현재의 정보를 저장함.
        }
        SceneManager.LoadScene("main_map");
    }

    public void ReturnMain() {
        SceneManager.LoadScene("MainScene");
    }
}
