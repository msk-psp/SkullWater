/***********************************
코  드 : ScrollVeiwAdapter
수  정 : 20170530 김민수, 정지훈 작성
***********************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using FirebaseAccess;
using Furniture;
using System.Data;
using Mono.Data.Sqlite;

public class MyScrollViewAdapter : MonoBehaviour
{

    public Texture2D[] availableIcons;                  // 아이콘 이미지들
    public RectTransform prefab;                        // 아이템 prefab
    //public Text countText;                            // 개수 텍스트 여기에 큐브 개수 넣으면 됨.
    public RectTransform panel;                         // 데이터 베이스를 보여줄 Panel
    public RectTransform content;                       // content 객체
    public string userID;                               // user ID;
    private Transaction tran;                           // 파이어 베이스에서 데이터 가져올 때 사용하는 클래스
    string dbName = "burnitureDatabase.db";             // 내부 DB 파일로 저장될 이름
    string filePath;                                    // 기본 경로를 받아오기 위한 변수
    private string mCube_name = null;                   // 저장할 큐브 이름

    private float mCube_xScale;                         // 큐브의 x 크기
    private float mCube_yScale;                         // 큐브의 y 크기
    private float mCube_zScale;                         // 큐브의 z 크기

    public int switch_value = 0;                        // 스위치를 통해 View조작

    List<CubeItemView> views = new List<CubeItemView>();       // Content에 넣을 아이템들 
                                                               // Use this for initialization
    void Start()                                        // 데이터베이스창을 여는 함수
    {
        panel.gameObject.SetActive(false);              // panel 오브젝트 비활성화
    }
    public void View_On()                               // List를 보여주기 위한 함수
    {
        panel.gameObject.SetActive(true);               // panel 오브젝트 활성화
        Internal_Update_Items();                        // 내부DB를 불러오는 함수
    }
    public void View_Off()                                       // 데이터베이스창을 여는 함수
    {
        panel.gameObject.SetActive(false);              // panel 오브젝트 비활성화
    }

    public void Internal_Update_Items()                 // 내부DB 아이템 업데이트
    {  
        StartCoroutine(RecieveInternalDB(results => OnReceivedNewModels(results)));       // newCount 개수 만큼, results 에 반환 된 cube Item들을 가지고        
    }

    public void External_Update_Items()                 // 외부DB 아이템 업데이트
    {
        tran = new Transaction();                       // 임의로 만든 저장객체
        tran.RetrieveCubesByUserid(userID);             // 외부DB에서 불러오는 부분 (비동기적)
        StartCoroutine(FetchItemModelsFromServer(results => OnReceivedNewModels(results)));       // newCount 개수 만큼, results 에 반환 된 cube Item들을 가지고        
    }
    // OnreceivedNewModels에 넘겨줘 각 게임 오브젝트를 생성하고 view List에 

    void OnReceivedNewModels(CubeItem[] models)                                     // content에 data 추가하는 함수
    {
        foreach (Transform child in content.transform)                              // content 에 있는 아이템들 다 지움
        {
            Destroy(child.gameObject);
        }

        views.Clear();                                                              // views clear

        int i = 0;
        foreach (var model in models)                                               // Fetch 에서 받아온 걸로 views에 추가함 
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
            instance.transform.SetParent(content, false);                                   // parent가 content가 되므로 자동 추가 됨
            instance.SetActive(true);
            var view = InitializeItemView(instance, model);
            //GameObject.Find("ScrollItemPrefab(Clone)").GetComponent<RawImage>().color = new Color(255f, 255f, 255f, 0.7f);
            Debug.Log("foreach view " + view.titleText.text);
            views.Add(view);
            ++i;
        }
    }
    CubeItemView InitializeItemView(GameObject viewGameObject, CubeItem model)          // CubeItem View를 초기화하여 넘겨줌,
    {
        CubeItemView view = new CubeItemView(viewGameObject.transform);
        view.titleText.text = model.name;
        // view.iconImage.texture = availableIcons[model.iconIndex];                           //각 index들은 fetch에서 랜덤으로 받아옴'
        view.x.text = model.x.ToString();
        view.y.text = model.y.ToString();
        view.z.text = model.z.ToString();
        
        //view.icon2Image.texture = availableIcons[model.icon2Index];
        //view.icon3Image.texture = availableIcons[model.icon3Index];
        return view;
    }
    
    IEnumerator FetchItemModelsFromServer(Action<CubeItem[]> onDone)            // 비동기로 처리라 서버에서 값을 처리할때 까지 기다림
    {
        int count = 0;
        int delay = 0;

        Debug.Log("FetchItemModels ");
        while (true)                                                 //  서버에서 값을 받아 올 때 까지 기다림 
        {
            if (tran.isFailed || 20 <= delay++) { Debug.Log("is failed in FetchItem while"); count = 0; break; }
            else if (tran.isWaiting) { Debug.Log("is waiting in FetchItem while"); yield return new WaitForSeconds(0.5f); }
            else if (tran.isSuccess) { count = tran.cubes.Count; Debug.Log("is success in FetchItem while"); break; }
        }

        Debug.Log("after FetchItem while");

        count = tran.cubes.Count;

        var results = new CubeItem[count];

        for (int i = 0; i < count; ++i)                                 // count 만큼 results[i] 에 값을 넣어줌 그걸 리턴 함
        {                                                               //여기서 큐브의 정보를 넣어주면 됨 
            results[i] = new CubeItem();                                // X,Y,Z , 위치좌표, 큐브이름 
            results[i].name = tran.cubes[i].name;
            //results[i].iconIndex = UnityEngine.Random.Range(0, availableIcons.Length);
            results[i].x = tran.cubes[i].x.ToString();
            results[i].y = tran.cubes[i].y.ToString();
            results[i].z = tran.cubes[i].z.ToString();

            //results[i].icon2Index = UnityEngine.Random.Range(0, availableIcons.Length);
            //results[i].icon3Index = UnityEngine.Random.Range(0, availableIcons.Length);
        }
        onDone(results);
    }
    
    public class CubeItemView
    {
        public Text titleText;
        public Text x, y, z;
        public Text xAxis, yAxis, zAxis;
        //public RawImage iconImage;//, icon2Image,icon3Image;

        public CubeItemView(Transform rootView)
        {
            titleText = rootView.Find("TitlePanel/CubeName").GetComponent<Text>();
            //iconImage = rootView.Find("IconImage").GetComponent<RawImage>();
            x = rootView.Find("XText").GetComponent<Text>();
            y = rootView.Find("YText").GetComponent<Text>();
            z = rootView.Find("ZText").GetComponent<Text>();
            xAxis = rootView.Find("XAxisText").GetComponent<Text>();
            yAxis = rootView.Find("YAxisText").GetComponent<Text>();
            zAxis = rootView.Find("ZAxisText").GetComponent<Text>();
        }

    }
    public class CubeItem
    {
        public string name;
        public string x, y, z;
        public int type;
        public string rgb;
        // public int iconIndex;//, icon2Index, icon3Index;
    }
    IEnumerator RecieveInternalDB(Action<CubeItem[]> onDone)
    {
        IDbConnection dbconn;

        string color;
        string conn;
        int count;

        filePath = Application.persistentDataPath + "/" + dbName;
         conn = "URI=file:" + Application.dataPath + "/" + dbName;//test

        dbconn = new SqliteConnection(conn);                        // db 연결
        dbconn.Open();                                              //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();                  // 명령어 생성
        string sqlQuery = "SELECT * " + "FROM myBurniture";         // 쿼리문 만들기

        dbcmd.CommandText = sqlQuery;                               // 명령어 설정
        IDataReader reader = dbcmd.ExecuteReader();                 // 명령어 실행하여 데이터 리더기에 저장
        count = reader.FieldCount-1;
        var results = new CubeItem[count];
       
        if (reader.FieldCount == 0)
        {
            Debug.Log("No Data!!!!");                               // No data
        }

        for (int i = 0; i < count; ++i)                                 // count 만큼 results[i] 에 값을 넣어줌 그걸 리턴 함
        {
            if (!reader.Read()) break; ;//여기서 큐브의 정보를 넣어주면 됨 
            results[i] = new CubeItem();                                // X,Y,Z , 위치좌표, 큐브이름 
            results[i].type = reader.GetInt32(1);                       // 저장한 가구의 타입을 가져옴
            results[i].name = reader.GetString(2);                      // 저장한 가구의 이름을 가져옴
            results[i].x = reader.GetString(3);                         // 저장한 가구의 x크기 가져옴
            results[i].y = reader.GetString(4);                         // 저장한 가구의 y크기 가져옴
            results[i].z = reader.GetString(5);                         // 저장한 가구의 z크기 가져옴
            results[i].rgb = reader.GetString(6);                       // 저장한 가구의 rgb 가져옴
            Debug.Log("type= " + results[i].type + "  name =" + results[i].name);
        }
 
        dbcmd.Dispose();                                            // 커멘드 닫아주기
        dbcmd = null;
        dbconn.Close();                                             // 트랜잭션 닫아주기
        dbconn = null;

        onDone(results);

        yield return null;
    }
}
