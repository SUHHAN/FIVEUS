using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAction_yj : MonoBehaviour
{
    public float Speed_yj;
    public bool isAction_yj;

    Rigidbody2D rigid_yj;
    Animator play_anim_yj;
    Vector3 dirVec_yj;

    float h_yj;
    float v_yj;
    bool isHorizonMove_yj;

    private FixedJoystick joystick; // sw추가. 조이스틱

    public GameManager_bin manager_bin;
    GameObject ScanObject_bin;
    private bool collisionInfdoor = false;
    private static PlayerAction_yj instance;
    private bool isInputEnabled = true; // 입력 활성화 상태를 제어하는 변수
    void Awake()
    {
        // 초기화
        rigid_yj = GetComponent<Rigidbody2D>();
        play_anim_yj = GetComponent<Animator>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        FindJoystick();
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void Update()
    {
        if (!isInputEnabled) return;
        // sw 추가. 조이스틱 입력 받기
        if (joystick != null)
        {
            h_yj = isAction_yj ? 0 : joystick.Horizontal;
            v_yj = isAction_yj ? 0 : joystick.Vertical;
        }
        // sw 추가. 입력값이 특정 기준 이상인지 확인
        bool hDown_yj = isAction_yj ? false : Mathf.Abs(joystick.Horizontal) > 0.1f;
        bool vDown_yj = isAction_yj ? false : Mathf.Abs(joystick.Vertical) > 0.1f;
        bool hUp_yj = isAction_yj ? false : Mathf.Abs(joystick.Horizontal) <= 0.1f;
        bool vUp_yj = isAction_yj ? false : Mathf.Abs(joystick.Vertical) <= 0.1f;

        if (hDown_yj || vDown_yj)
            isHorizonMove_yj = Mathf.Abs(h_yj) > Mathf.Abs(v_yj);
        else if (hUp_yj || vUp_yj)
        {
            isHorizonMove_yj = h_yj != 0;
        }
        else
    {
        // Joystick이 null인 경우, 기본값을 사용하거나 예외 처리
        h_yj = 0;
        v_yj = 0;
        isHorizonMove_yj = false;
    }

        // 애니메이션
        if (play_anim_yj.GetInteger("hAxisRaw_yj") != (int)h_yj)
        {
            play_anim_yj.SetBool("isChange_yj", true);
            play_anim_yj.SetInteger("hAxisRaw_yj", (int)h_yj);
        }
        else if (play_anim_yj.GetInteger("vAxisRaw_yj") != (int)v_yj)
        {
            play_anim_yj.SetBool("isChange_yj", true);
            play_anim_yj.SetInteger("vAxisRaw_yj", (int)v_yj);
        }
        else
            play_anim_yj.SetBool("isChange_yj", false);

        dirVec_yj = new Vector3(h_yj, v_yj).normalized; // sw추가. 대각선 이동도 가능

        if (Input.GetButtonDown("Jump") && ScanObject_bin != null)
            manager_bin.Action(ScanObject_bin);

        if (collisionInfdoor && Input.GetButtonDown("Jump"))
        {
            StartCoroutine(LoadSceneCoroutine("inf_guild", 7));
            collisionInfdoor = false;
        }
    }

    // 물리 연산 업데이트a
    void FixedUpdate()
    {
        if (!isInputEnabled)
        {
            rigid_yj.velocity = Vector2.zero; // 씬 로드 중 플레이어 속도 0으로 설정
            return;
        }

        rigid_yj.velocity = dirVec_yj * Speed_yj;

        //ray
        Debug.DrawRay(rigid_yj.position, dirVec_yj * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit_bin = Physics2D.Raycast(rigid_yj.position, dirVec_yj, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit_bin.collider != null)
            ScanObject_bin = rayHit_bin.collider.gameObject;
        else
            ScanObject_bin = null;
    }
    public int LoadedScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "main_indoor")
        {
            StartCoroutine(LoadSceneCoroutine("main_house", 1));
        }
        if (collision.gameObject.name == "main_outdoor")
        {
            LoadedScene = 1;
            StartCoroutine(LoadSceneCoroutine("main_map", 0));
        }
        if (collision.gameObject.name == "store_indoor")
        {
            StartCoroutine(LoadSceneCoroutine("store", 2));
        }
        if (collision.gameObject.name == "store_outdoor")
        {
            LoadedScene = 2;
            StartCoroutine(LoadSceneCoroutine("main_map", 0));
        }
        if (collision.gameObject.name == "bar_indoor")
        {
            StartCoroutine(LoadSceneCoroutine("bar", 3));
        }
        if (collision.gameObject.name == "bar_outdoor")
        {
            LoadedScene = 3;
            StartCoroutine(LoadSceneCoroutine("main_map", 0));
        }
        if (collision.gameObject.name == "hotel_indoor")
        {
            StartCoroutine(LoadSceneCoroutine("hotel", 4));
        }
        if (collision.gameObject.name == "hotel_outdoor")
        {
            LoadedScene = 4;
            StartCoroutine(LoadSceneCoroutine("main_map", 0));
        }

        if (collision.gameObject.name == "hotel_hall_indoor")
        {
            StartCoroutine(LoadSceneCoroutine("hotel_hall", 11));
        }
        if (collision.gameObject.name == "hotel_hall_outdoor")
        {
            LoadedScene = 11;
            StartCoroutine(LoadSceneCoroutine("hotel", 4));
        }
        if (collision.gameObject.name == "hotel_room1_indoor")
        {
            StartCoroutine(LoadSceneCoroutine("hotel_room1", 12));
        }
        if (collision.gameObject.name == "hotel_room1_outdoor")
        {
            LoadedScene = 12;
            StartCoroutine(LoadSceneCoroutine("hotel_hall", 11));
        }
        if (collision.gameObject.name == "hotel_room2_indoor")
        {
            StartCoroutine(LoadSceneCoroutine("hotel_room2", 13));
        }
        if (collision.gameObject.name == "hotel_room2_outdoor")
        {
            LoadedScene = 13;
            StartCoroutine(LoadSceneCoroutine("hotel_hall", 11));
        }
        if (collision.gameObject.name == "sub1house_indoor")
        {
            StartCoroutine(LoadSceneCoroutine("sub1_house", 5));
        }
        if (collision.gameObject.name == "sub1house_outdoor")
        {
            LoadedScene = 5;
            StartCoroutine(LoadSceneCoroutine("main_map", 0));
        }
        if (collision.gameObject.name == "magichouse_indoor")
        {
            StartCoroutine(LoadSceneCoroutine("magic_house", 6));
        }
        if (collision.gameObject.name == "magichouse_outdoor")
        {
            LoadedScene = 6;
            StartCoroutine(LoadSceneCoroutine("main_map", 0));
        }
        if ((collision.gameObject.name == "infguild_indoor"))
        {
            collisionInfdoor = true;

        }
        if (collision.gameObject.name == "infguild_outdoor")
        {
            LoadedScene = 7;
            StartCoroutine(LoadSceneCoroutine("main_map", 0));
        }

        if (collision.gameObject.name == "inf_indoor")
        {
            StartCoroutine(LoadSceneCoroutine("inf_guild_in", 8));
        }
        if (collision.gameObject.name == "inf_outdoor")
        {
            LoadedScene = 8;
            StartCoroutine(LoadSceneCoroutine("inf_guild", 7));
        }
        if (collision.gameObject.name == "camping_indoor")
        {
            StartCoroutine(LoadSceneCoroutine("camping", 9));
        }
        if (collision.gameObject.name == "camping_outdoor")
        {
            LoadedScene = 9;
            StartCoroutine(LoadSceneCoroutine("main_map", 0));
        }
        if (collision.gameObject.name == "training_indoor")
        {
            StartCoroutine(LoadSceneCoroutine("training", 10));
        }
        if (collision.gameObject.name == "training_outdoor")
        {
            LoadedScene = 10;
            StartCoroutine(LoadSceneCoroutine("main_map", 0));
        }

    }

    private IEnumerator LoadSceneCoroutine(string sceneName, int loadedScene)
    {
        isInputEnabled = false; // 입력 비활성화
        rigid_yj.velocity = Vector2.zero; // 플레이어 속도 0으로 설정
        yield return SceneManager.LoadSceneAsync(sceneName);
        LoadedScene = loadedScene;
        isInputEnabled = true; // 입력 활성화
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Speed_yj = 0;
        Debug.Log("Loaded scene: " + scene.name);

        if (scene.name == "main_house")
        {
            transform.position = new Vector3(2, -2, 0);
        }
        if (scene.name == "main_map" && LoadedScene == 1)
        {
            transform.position = new Vector3(-20, 8, 0);
        }
        if (scene.name == "store")
        {
            transform.position = new Vector3(0, -3, 0);
        }
        if (scene.name == "main_map" && LoadedScene == 2)
        {
            transform.position = new Vector3(-23, -5, 0);
        }
        if (scene.name == "bar")
        {
            transform.position = new Vector3(0, -3, 0);
        }
        if (scene.name == "main_map" && LoadedScene == 3)
        {
            transform.position = new Vector3(-9, -14, 0);
        }
        if (scene.name == "hotel")
        {
            transform.position = new Vector3(0, -3, 0);
        }
        if (scene.name == "main_map" && LoadedScene == 4)
        {
            transform.position = new Vector3(17, 9, 0);
        }
        if (scene.name == "hotel_hall")
        {
            transform.position = new Vector3(1, -1, 0);
        }
        if (scene.name == "hotel" && LoadedScene == 11)
        {
            transform.position = new Vector3(1, 2, 0);
        }
        if (scene.name == "hotel_room1")
        {
            transform.position = new Vector3(1, 1, 0);
        }
        if (scene.name == "hotel_hall" && LoadedScene == 12)
        {
            transform.position = new Vector3(2, 1, 0);
        }
        if (scene.name == "hotel_room2")
        {
            transform.position = new Vector3(-1, 1, 0);
        }
        if (scene.name == "hotel_hall" && LoadedScene == 13)
        {
            transform.position = new Vector3(-4, 1, 0);
        }
        if (scene.name == "sub1_house")
        {
            transform.position = new Vector3(2, -2, 0);
        }
        if (scene.name == "main_map" && LoadedScene == 5)
        {
            transform.position = new Vector3(-9, 6, 0);
        }
        if (scene.name == "magic_house")
        {
            transform.position = new Vector3(2, -3, 0);
        }
        if (scene.name == "main_map" && LoadedScene == 6)
        {
            transform.position = new Vector3(-21, -14, 0);
        }
        if (scene.name == "inf_guild")
        {
            transform.position = new Vector3(2, -2, 0);
        }
        if (scene.name == "main_map" && LoadedScene == 7)
        {
            transform.position = new Vector3(19, -14, 0);
        }
        if (scene.name == "inf_guild_in")
        {
            transform.position = new Vector3(1, -2, 0);
        }
        if (scene.name == "inf_guild" && LoadedScene == 8)
        {
            transform.position = new Vector3(3, 4, 0);
        }
        if (scene.name == "camping")
        {
            transform.position = new Vector3(3, -1, 0);
        }
        if (scene.name == "main_map" && LoadedScene == 9)
        {
            transform.position = new Vector3(-9, -18, 0);
        }
        if (scene.name == "training")
        {
            transform.position = new Vector3(8, 1, 0);
        }
        if (scene.name == "main_map" && LoadedScene == 10)
        {
            transform.position = new Vector3(-26, 4, 0);
        }

        FindJoystick();

        Speed_yj = 3; // 이동 속도 복원
    }

    void FindJoystick()
    {
        joystick = FindObjectOfType<FixedJoystick>();
        if(joystick==null)
            Debug.LogError("FixedJoystick이 씬에 없습니다.");
    }
}