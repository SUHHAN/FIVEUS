using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider bgmSlider; // 배경음 슬라이더
    public Slider sfxSlider; // 효과음 슬라이더

    void Start()
    {
        if (bgmSlider == null || sfxSlider == null)
        {
            Debug.LogError("슬라이더가 할당되지 않았습니다. bgmSlider: " + bgmSlider + ", sfxSlider: " + sfxSlider);
            return;
        }

        // 슬라이더 초기값 설정
        bgmSlider.value = AudioManager.Instance.bgmVolume;
        sfxSlider.value = AudioManager.Instance.sfxVolume;

        // 슬라이더의 OnValueChanged 이벤트에 메서드 연결
        bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    public void SetBgmVolume(float volume)
    {
        AudioManager.Instance.SetBgmVolume(volume);
    }

    public void SetSfxVolume(float volume)
    {
        AudioManager.Instance.SetSfxVolume(volume);
    }
}
