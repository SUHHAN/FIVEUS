using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    // 배경음 변수
    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume = 0.2f;
    AudioSource bgmPlayer;

    // 효과음 변수
    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume = 0.5f;

    // 동시다발적으로 많은 효과음을 내기 위해서 channel 분리
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;
    
    public enum Sfx {ButtonClick, DataSaveEnd, Get};

    void Awake() {
        // 싱글톤 인스턴스 생성
        if (Instance == null)
        {
            Instance = this;
            Init();
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 자신을 파괴
            return;
        }
    }

    void Init() {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.loop = true;
        bgmPlayer.clip = bgmClip;

        // 볼륨 로드 및 설정
        bgmVolume = PlayerPrefs.GetFloat("BgmVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);
        bgmPlayer.volume = bgmVolume;

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;

        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++) {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>(); // component로 audioSource를 추가하면서 배열을 각각 형성
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay) {
        if (isPlay) {
            bgmPlayer.Play();
        }
        else {
            bgmPlayer.Stop();
        }
    }

    public void SetBgmVolume(float volume) {
        bgmVolume = volume;
        bgmPlayer.volume = bgmVolume;
        PlayerPrefs.SetFloat("BgmVolume", bgmVolume); // 볼륨 값 저장
    }

    public void SetSfxVolume(float volume) {
        sfxVolume = volume;
        foreach (var sfxPlayer in sfxPlayers) {
            sfxPlayer.volume = sfxVolume;
        }
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume); // 볼륨 값 저장
    }

    public void PlaySfx(Sfx sfx) {
        sfxPlayers[channelIndex].clip = sfxClips[(int)sfx];
        sfxPlayers[channelIndex].Play();
        channelIndex = (channelIndex + 1) % channels;
    }
}
