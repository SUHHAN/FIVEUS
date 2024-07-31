using System.Collections.Generic;
using UnityEngine;

public class GiftManager
{
    private Dictionary<string, List<string>> preferredGifts = new Dictionary<string, List<string>>() {
        { "힐러", new List<string> { "회중시계" } },
        { "검사", new List<string> { "고급탈취제" } },
        { "탱커", new List<string> { "가죽장갑" } },
        { "궁수", new List<string> { "바람석" } },
        { "암살자", new List<string> { "빨간색 손수건" } },
        { "마법사", new List<string> { "보존 화관" } }
    };

    private List<string> allGifts = new List<string> {
        "회중시계", "고급탈취제", "가죽장갑", "바람석", "빨간색 손수건", "보존 화관"
    };

    public int GetGiftResponse(string charType, string giftName)
    {
        if (preferredGifts.ContainsKey(charType) && preferredGifts[charType].Contains(giftName))
        {
            return 0;  // 가장 좋아하는 선물
        }
        else if (allGifts.Contains(giftName))
        {
            return 1;  // 그저 그런 선물
        }
        else
        {
            return 2;  // 싫어하는 선물
        }
    }
}