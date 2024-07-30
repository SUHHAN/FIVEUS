using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class eightbuttons_yj : MonoBehaviour
{
    /*
    public talkwithjjang_yj talkScript_yj;
    // 기본 활동 버튼들
    public Button trainingButton_yj; // 1. 훈련 시도 버튼 연결
    public Button campingButton_yj; // 2. 단합 시도 버튼 연결
    public Button findhintButton_yj; // 3. 단서 보기 버튼 연결
    public Button laybedButton_yj; // 4. 휴식하기 버튼 연결
    public Button noButton1; // 아니오 버튼 연결1
    public Button noButton2; // 아니오 버튼 연결2
    public Button noButton3; // 아니오 버튼 연결3
    public Button noButton4; // 아니오 버튼 연결4

    public void Start()
    {
        // talkwithjjang_yj 스크립트가 연결된 게임 오브젝트에서 talkwithjjang_yj 스크립트 가져오기
        talkScript_yj = GetComponent<talkwithjjang_yj>();

        if (talkScript_yj == null)
        {
            Debug.LogError("talkwithjjang_yj component not found or not initialized properly.");
            return;
        }


        // 각 버튼에 클릭 이벤트를 추가합니다.
        trainingButton_yj.onClick.AddListener(OnTrainButtonClick);
        campingButton_yj.onClick.AddListener(OnCampButtonClick);
        findhintButton_yj.onClick.AddListener(OnHintButtonClick);
        laybedButton_yj.onClick.AddListener(OnBedButtonClick);

        noButton1.onClick.AddListener(OnNoButtonClick); // 아니오1 버튼 클릭 이벤트 연결
        noButton2.onClick.AddListener(OnNoButtonClick); // 아니오2 버튼 클릭 이벤트 연결
        noButton3.onClick.AddListener(OnNoButtonClick); // 아니오3 버튼 클릭 이벤트 연결
        noButton4.onClick.AddListener(OnNoButtonClick); // 아니오4 버튼 클릭 이벤트 연결

    }
    // 훈련 버튼 클릭 처리
    public void OnTrainButtonClick()
    {
        // 기존의 talkwithjjang_yj 스크립트의 OntrainButtonClick 메서드 내용을 여기에 넣습니다.
        talkScript_yj.OntrainButtonClick();
    }

    // 단합 버튼 클릭 처리
    public void OnCampButtonClick()
    {
        talkScript_yj.OncampButtonClick();
    }

    // 단서 보기 버튼 클릭 처리
    public void OnHintButtonClick()
    {
        talkScript_yj.OnhintButtonClick();
    }

    // 휴식하기 버튼 클릭 처리
    public void OnBedButtonClick()
    {
        talkScript_yj.OnbedButtonClick();
    }
    public void OnNoButtonClick()
    {
        talkScript_yj.OnNoButtonClick();
    }
    */

    public talkwithjjang_yj talkScript_yj;
    public Button trainingButton_yj;
    public Button campingButton_yj;
    public Button findhintButton_yj;
    public Button laybedButton_yj;
    public Button noButton1;
    public Button noButton2;
    public Button noButton3;
    public Button noButton4;

    public void Start()
    {
        talkScript_yj = GetComponent<talkwithjjang_yj>();

        if (talkScript_yj == null)
        {
            Debug.LogError("talkwithjjang_yj component not found or not initialized properly.");
            return;
        }

        trainingButton_yj.onClick.AddListener(OnTrainButtonClick);
        campingButton_yj.onClick.AddListener(OnCampButtonClick);
        findhintButton_yj.onClick.AddListener(OnHintButtonClick);
        laybedButton_yj.onClick.AddListener(OnBedButtonClick);

        noButton1.onClick.AddListener(OnNoButtonClick);
        noButton2.onClick.AddListener(OnNoButtonClick);
        noButton3.onClick.AddListener(OnNoButtonClick);
        noButton4.onClick.AddListener(OnNoButtonClick);
    }

    public void OnTrainButtonClick()
    {
        if (talkScript_yj != null)
        {
            talkScript_yj.OntrainButtonClick();
        }
        else
        {
            Debug.LogError("talkScript_yj is not initialized.");
        }
    }

    public void OnCampButtonClick()
    {
        if (talkScript_yj != null)
        {
            talkScript_yj.OncampButtonClick();
        }
        else
        {
            Debug.LogError("talkScript_yj is not initialized.");
        }
    }

    public void OnHintButtonClick()
    {
        if (talkScript_yj != null)
        {
            talkScript_yj.OnhintButtonClick();
        }
        else
        {
            Debug.LogError("talkScript_yj is not initialized.");
        }
    }

    public void OnBedButtonClick()
    {
        if (talkScript_yj != null)
        {
            talkScript_yj.OnbedButtonClick();
        }
        else
        {
            Debug.LogError("talkScript_yj is not initialized.");
        }
    }

    public void OnNoButtonClick()
    {
        if (talkScript_yj != null)
        {
            talkScript_yj.OnNoButtonClick();
        }
        else
        {
            Debug.LogError("talkScript_yj is not initialized.");
        }
    }
}