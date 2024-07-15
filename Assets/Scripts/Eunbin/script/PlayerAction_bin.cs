using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAction_bin : MonoBehaviour
{
    public float Speed_bin;
    public GameManager_bin manager_bin;

    Rigidbody2D rigid_bin;
    Vector3 dirVec_bin;
    GameObject ScanObject_bin;
    private bool collisionInfdoor = false;

    float h_bin;
    float v_bin;
    bool isHorizonMove_bin;

    private static PlayerAction_bin instance;
    private bool isInputEnabled = true; // 입력 활성화 상태를 제어하는 변수

    void Awake()
    {
        rigid_bin = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (!isInputEnabled) return; // 입력이 비활성화된 경우 업데이트를 멈춥니다

        h_bin = manager_bin.isAction_bin ? 0 : Input.GetAxisRaw("Horizontal");
        v_bin = manager_bin.isAction_bin ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown_bin = manager_bin.isAction_bin ? false : Input.GetButtonDown("Horizontal");
        bool vDown_bin = manager_bin.isAction_bin ? false : Input.GetButtonDown("Vertical");
        bool hUp_bin = manager_bin.isAction_bin ? false : Input.GetButtonUp("Horizontal");
        bool vUp_bin = manager_bin.isAction_bin ? false : Input.GetButtonUp("Vertical");

        if (hDown_bin)
            isHorizonMove_bin = true;
        else if (vDown_bin)
            isHorizonMove_bin = false;
        else if (hUp_bin || vUp_bin)
            isHorizonMove_bin = h_bin != 0;

        // direction
        if (vDown_bin && v_bin == 1)
            dirVec_bin = Vector3.up;
        else if (vDown_bin && v_bin == -1)
            dirVec_bin = Vector3.down;
        else if (hDown_bin && h_bin == -1)
            dirVec_bin = Vector3.left;
        else if (hDown_bin && h_bin == 1)
            dirVec_bin = Vector3.right;

        // scan object
        if (Input.GetButtonDown("Jump") && ScanObject_bin != null)
            manager_bin.Action(ScanObject_bin);

        if(collisionInfdoor && Input.GetButtonDown("Jump"))
            StartCoroutine(LoadSceneCoroutine("inf_guild", 7));

    }

    void FixedUpdate()
    {
        if (!isInputEnabled) return; // 입력이 비활성화된 경우 업데이트를 멈춥니다

        Vector2 moveVec = isHorizonMove_bin ? new Vector2(h_bin, 0) : new Vector2(0, v_bin);
        rigid_bin.velocity = moveVec * Speed_bin;

        //ray
        Debug.DrawRay(rigid_bin.position, dirVec_bin * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit_bin = Physics2D.Raycast(rigid_bin.position, dirVec_bin, 0.7f, LayerMask.GetMask("Object"));

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
        if (collision.gameObject.name == "bighouse_indoor")
        {
            StartCoroutine(LoadSceneCoroutine("big_house", 4));
        }
        if (collision.gameObject.name == "bighouse_outdoor")
        {
            LoadedScene = 4;
            StartCoroutine(LoadSceneCoroutine("main_map", 0));
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
        if (collision.gameObject.name == "sub2house_indoor")
        {
            StartCoroutine(LoadSceneCoroutine("sub2_house", 6));
        }
        if (collision.gameObject.name == "sub2house_outdoor")
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

    }

    private IEnumerator LoadSceneCoroutine(string sceneName, int loadedScene)
    {
        isInputEnabled = false; // 입력 비활성화
        rigid_bin.velocity = Vector2.zero; // 플레이어 속도 0으로 설정
        yield return SceneManager.LoadSceneAsync(sceneName);
        LoadedScene = loadedScene;
        isInputEnabled = true; // 입력 활성화
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Speed_bin = 0;
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
        if (scene.name == "big_house")
        {
            transform.position = new Vector3(0, -3, 0);
        }
        if (scene.name == "main_map" && LoadedScene == 4)
        {
            transform.position = new Vector3(17, 9, 0);
        }
        if (scene.name == "sub1_house")
        {
            transform.position = new Vector3(2, -2, 0);
        }
        if (scene.name == "main_map" && LoadedScene == 5)
        {
            transform.position = new Vector3(-9, 6, 0);
        }
        if (scene.name == "sub2_house")
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
        if (scene.name == "main_map" && LoadedScene ==7)
        {
            transform.position = new Vector3(19, -14, 0);
        }
        Speed_bin = 3; // 이동 속도 복원
    }
}
