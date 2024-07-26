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
    public int sfxChannels;
    AudioSource[] sfxPlayers;
    int sfxChannelIndex;
    
    public enum Sfx {ButtonClick, DataSaveEnd, Get};

    // 환경음 변수
    [Header("#ENV")]
    public AudioClip[] envClips;
    public float envVolume = 0.5f;

    // 동시다발적으로 많은 효과음을 내기 위해서 channel 분리
    public int envChannels;
    AudioSource[] envPlayers;
    int envChannelIndex;
    
    public enum Env {ButtonClick, DataSaveEnd, Get};

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
        // 1. 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.loop = true;
        bgmPlayer.clip = bgmClip;

        // 2. 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;

        sfxPlayers = new AudioSource[sfxChannels];

        for (int index = 0; index < sfxPlayers.Length; index++) {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>(); // component로 audioSource를 추가하면서 배열을 각각 형성
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }

        // 2. 효과음 플레이어 초기화
        GameObject envObject = new GameObject("EnvPlayer");
        envObject.transform.parent = transform;

        envPlayers = new AudioSource[envChannels];

        for (int index = 0; index < envPlayers.Length; index++) {
            envPlayers[index] = envObject.AddComponent<AudioSource>(); // component로 audioSource를 추가하면서 배열을 각각 형성
            envPlayers[index].playOnAwake = false;
            envPlayers[index].volume = envVolume;
        }

        // 3. 볼륨 로드 및 설정
        bgmVolume = PlayerPrefs.GetFloat("BgmVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("EnvVolume", 1f);
        bgmPlayer.volume = bgmVolume;

        
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

    public void SetEnvVolume(float volume) {
        envVolume = volume;
        foreach (var envPlayer in envPlayers) {
            envPlayer.volume = envVolume;
        }
        PlayerPrefs.SetFloat("EnvVolume", envVolume); // 볼륨 값 저장
    }

    public void PlaySfx(Sfx sfx) {
        sfxPlayers[sfxChannelIndex].clip = sfxClips[(int)sfx];
        sfxPlayers[sfxChannelIndex].Play();
        sfxChannelIndex = (sfxChannelIndex + 1) % sfxChannels;
    }

    public void PlayEnv(Env env) {
        envPlayers[envChannelIndex].clip = envClips[(int)env];
        envPlayers[envChannelIndex].Play();
        envChannelIndex = (envChannelIndex + 1) % envChannels;
    }
}
