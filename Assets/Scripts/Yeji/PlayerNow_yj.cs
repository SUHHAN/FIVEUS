using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNow_yj : MonoBehaviour
{
    // 참고로 수치 끝에 붙은 py는 player의 약자. 

    public int hp_py; // 체력
    // 체력은 최대치인 100을 넘을 수 없도록 제한됨
    // (이후 코드에서 나옴)

    public int tired_py; // 피로도
    public int money_py; // 재화
    public int hint_py; // 단서
    public int team_py; // 단합력
                        // 단합력은 각 파티원들의 어느 수치들의 합이다.

    public int day_py; // 오늘 날짜 변수(1일부터 15일까지)
    // 15일이 되면 마왕 잡으러 간다

    public int howtoday_py = 0; // 하루에 3개까지인 기본활동을 얼마나 했는지 변수
    // 매일 0에서 시작하고, 한 활동이 끝나면 1씩 증가한다.
    // 즉, 하루가 끝날 때 2까지 증가해야 한다. 그리고 하루가 지나가면 다시 0으로 초기화

    public int howtrain_py; // 능력치를 증가시키는 "훈련"을 몇번 했는지 저장되는 변수
    // 즉, 하루가 지나도 초기화되지 않는다.
    // 아마 마왕 잡으러 갈 떄나 파티 수치 계산할 때 적용될 듯?

    public PlayerNow_yj(int hp_py, int tired_py, int money_py, int hint_py, int team_py, int day_py, int howtoday_py, int howtrain_py)
    {
        this.hp_py = hp_py;
        this.tired_py = tired_py;
        this.money_py = money_py;
        this.hint_py = hint_py;
        this.team_py = team_py;
        this.day_py = day_py;
        this.howtoday_py = howtoday_py;
        this.howtrain_py = howtrain_py;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
