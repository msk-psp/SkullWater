/***********************************
코  드 : ScrollVeiwAdapter
수  정 : 20170530 김민수, 정지훈 작성
***********************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using FirebaseAccess;
using Furniture;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;
using System.Threading;
using System.Threading.Tasks;

public class MyScrollViewAdapter : MonoBehaviour
{

    public Texture2D[] fImage = new Texture2D[9];       // 아이콘 이미지들
    public RectTransform prefab;                        // 아이템 prefab
    //public Text countText;                            // 개수 텍스트 여기에 큐브 개수 넣으면 됨.
    public RectTransform panel;                         // 데이터 베이스를 보여줄 Panel
    public RectTransform content;                       // content 객체
    public string userID;                               // user ID;
    private Transaction tran;                           // 파이어 베이스에서 데이터 가져올 때 사용하는 클래스
    string dbName = "burnitureDatabase.db";             // 내부 DB 파일로 저장될 이름
    private string mCube_name = null;                   // 저장할 큐브 이름
    private string filePath;                            // 기본 경로를 받아오기 위한 변수
    private string conn;                                // URI+ filepath
    private float mCube_xScale;                         // 큐브의 x 크기
    private float mCube_yScale;                         // 큐브의 y 크기
    private float mCube_zScale;                         // 큐브의 z 크기
    public Texture2D[] my_button = new Texture2D[2];
    public Texture2D[] web_button = new Texture2D[2];

    public int switch_value = 0;                        // 스위치를 통해 View조작

    List<CubeItemView> views = new List<CubeItemView>();       // Content에 넣을 아이템들 
                                                               // Use this for initialization
    void Start()                                        // 데이터베이스창을 여는 함수
    {
        panel.gameObject.SetActive(false);              // panel 오브젝트 비활성화
        filePath = Application.persistentDataPath + "/" + dbName; //for android
        //filePath = Application.dataPath + "/" + dbName;   // for unity editor
        if (!File.Exists(filePath))//데이터베이스가 생성이 안 되어 있다면.. jar 경로에서 DB를 불러와 어플리케이션  persistentpath에 DB를 write함
        {

            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + dbName);  // this is the path to your StreamingAssets in android
            while (!loadDB.isDone)
            {

            }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check

            // then save to Application.persistentDataPath

            File.WriteAllBytes(filePath, loadDB.bytes);
        }
        Debug.Log("DB Log : " + filePath);
    }
    public void View_On()                               // List를 보여주기 위한 함수
    {
        panel.gameObject.SetActive(true);               // panel 오브젝트 활성화
        Internal_Update_Items();                        // 내부DB를 불러오는 함수
        //Debug.Log("DB Path : " + conn);
    }
    public void View_Off()                              // 데이터베이스창을 여는 함수
    {
        panel.gameObject.SetActive(false);              // panel 오브젝트 비활성화
    }
    public void Internal_Update_Items()                 // 내부DB 아이템 업데이트
    {
        StartCoroutine(RecieveInternalDB(results => OnReceivedNewModels(results)));       // newCount 개수 만큼, results 에 반환 된 cube Item들을 가지고
        GameObject.Find("MyFurniture").GetComponent<RawImage>().texture = my_button[0];
        GameObject.Find("WeFurniture").GetComponent<RawImage>().texture = web_button[1];

    }
    public void Internal_Delete_Item(int index)
    {
        IDbConnection dbconn;

        dbconn = new SqliteConnection(conn);                        // db 연결
        dbconn.Open();                                              //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();                  // 명령어 생성
        string sqlQuery = "DELETE FROM myBurniture WHERE IndexNo=" + index + "";         // 쿼리문 만들기

        dbcmd.CommandText = sqlQuery;                               // 명령어 설정
        dbcmd.ExecuteNonQuery();

        dbcmd.Dispose();                                            // 커멘드 닫아주기
        dbcmd = null;
        dbconn.Close();                                             // 트랜잭션 닫아주기
        dbconn = null;
        Internal_Update_Items();
    }
    public void External_Update_Items()                 // 외부DB 아이템 업데이트
    {
        tran = new Transaction();                       // 임의로 만든 저장객체
        tran.RetrieveCubesByUserId(userID);             // 외부DB에서 불러오는 부분 (비동기적)
        //tran.RetrieveCubesAll();
        StartCoroutine(FetchItemModelsFromServer(results => OnReceivedNewModels(results)));       // newCount 개수 만큼, results 에 반환 된 cube Item들을 가지고  
        GameObject.Find("MyFurniture").GetComponent<RawImage>().texture = my_button[1];
        GameObject.Find("WeFurniture").GetComponent<RawImage>().texture = web_button[0];
    }
    public void External_Download_Item(int index)
    {
        Cube cube = new Cube();
        RectTransform selectedPrefab = content.GetChild(index * -1).GetComponent<RectTransform>();

        cube.type = Furniture_Choose(selectedPrefab.Find("TitlePanel/Type").GetComponent<Text>().text);
        cube.color = selectedPrefab.Find("Color").GetComponent<Text>().text;
        cube.type = int.Parse(selectedPrefab.Find("TypeNum").GetComponent<Text>().text);
        cube.name = selectedPrefab.Find("TitlePanel/CubeName").GetComponent<Text>().text;
        cube.x = float.Parse(selectedPrefab.Find("TitlePanel/XText").GetComponent<Text>().text);
        cube.y = float.Parse(selectedPrefab.Find("TitlePanel/YText").GetComponent<Text>().text);
        cube.z = float.Parse(selectedPrefab.Find("TitlePanel/ZText").GetComponent<Text>().text);
        Debug.Log("External_Download:" + selectedPrefab.Find("XAxisText").GetComponent<Text>().text);
        cube.xAxis = float.Parse(selectedPrefab.Find("XAxisText").GetComponent<Text>().text);
        cube.yAxis = float.Parse(selectedPrefab.Find("YAxisText").GetComponent<Text>().text);
        cube.zAxis = float.Parse(selectedPrefab.Find("ZAxisText").GetComponent<Text>().text);

        string conn;
        string TABLENAME = "myBurniture";

        IDbConnection dbconn;

        conn = "URI=file:" + filePath;                   // db 경로
        dbconn = new SqliteConnection(conn);                        // db 연결
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();                  // 명령어 생성

        string sqlQuery = "INSERT INTO " + TABLENAME + "(Type,Name,XLength,YLength,ZLength,XAxis,YAxis,ZAxis,RGB) VALUES ('" + cube.type + "', '" + cube.name + "', '" + cube.x.ToString() +
                     "', '" + cube.y.ToString() + "', '" + cube.z.ToString() + "', '" + cube.xAxis + "','" + cube.yAxis + "','" + cube.zAxis + "','" + cube.color + "');";        // 쿼리문 만들기
        Debug.Log("SendToDatabase sqlQuery:" + sqlQuery);
        dbcmd.CommandText = sqlQuery;                               // 명령어 설정
        if (dbcmd.ExecuteNonQuery() == -1)                          //쿼리 실행
        {
            Debug.Log("Internal DataBase Rollback or Error!!!");
        }
        else
        {
            Debug.Log("Insert Query excutes Successfully");
        }

        dbcmd.Dispose();                                            // 커멘드 닫아주기
        dbcmd = null;
        dbconn.Close();                                             // 트랜잭션 닫아주기
        dbconn = null;
        Internal_Update_Items();
    }
    public void External_Delete_Item(string cubeName)//웹에 있는 건 안 지우기로 함
    {
        /*
        int delay = 0;
        tran = new Transaction();
        tran.DeleteCubeByCubeName(cubeName);

        while (true)                                                 //  서버에서 값을 받아 올 때 까지 기다림 
        {
            if (tran.isFailed || 20 <= delay++) { Debug.Log("is failed in delete while");  break; }
            else if (tran.isWaiting) { Debug.Log("is waiting in delete while");  new WaitForSeconds(0.2f); }
            else if (tran.isSuccess) { Debug.Log("is success in delete while"); break; }
        }

        tran.RetrieveCubesByUserId(userID);             // 외부DB에서 불러오는 부분 (비동기적)

        StartCoroutine(FetchItemModelsFromServer(results => OnReceivedNewModels(results)));       // newCount 개수 만큼, results 에 반환 된 cube Item들을 가지고        
        */
    }
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
            // Debug.Log("foreach view " + view.titleText.text);
            views.Add(view);
            ++i;
        }
    }
    CubeItemView InitializeItemView(GameObject viewGameObject, CubeItem model)          // CubeItem View를 초기화하여 넘겨줌,
    {
        CubeItemView view = new CubeItemView(viewGameObject.transform);

        view.index.text = model.index.ToString();
        view.type.text = Furniture_Choose(model.type);
        view.titleText.text = model.name;
        view.color.text = ColorUtility.ToHtmlStringRGB(model.color);
        view.typeNum.text = model.type.ToString();
        Debug.Log("ModelColor:" + model.color);
        Debug.Log("MyScrollViewAdapter_Debug view.color.text :" + view.color.text);
        view.furnitureImage.texture = fImage[model.type];

        view.x.text = model.x.ToString();
        view.y.text = model.y.ToString();
        view.z.text = model.z.ToString();

        view.xAxis.text = model.xAxis.ToString();
        view.yAxis.text = model.yAxis.ToString();
        view.zAxis.text = model.zAxis.ToString();

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
        tran.isFailed = false;                                                                // 접속 실패
        tran.isWaiting = true;                                                               // 대기중
        tran.isSuccess = false;
        Debug.Log("after FetchItem while");

        count = tran.cubes.Count;

        var results = new CubeItem[count];

        for (int i = 0; i < count; ++i)                                 // count 만큼 results[i] 에 값을 넣어줌 그걸 리턴 함
        {                                                               //여기서 큐브의 정보를 넣어주면 됨 
            results[i] = new CubeItem();
            results[i].index = i * -1;                         // X,Y,Z , 위치좌표, 큐브이름 
            results[i].name = tran.cubes[i].name.Trim();
            results[i].type = int.Parse(tran.cubes[i].type.ToString().Trim());
            results[i].x = tran.cubes[i].x.ToString().Trim();
            results[i].y = tran.cubes[i].y.ToString().Trim();
            results[i].z = tran.cubes[i].z.ToString().Trim();
            results[i].xAxis = tran.cubes[i].xAxis.ToString().Trim();
            results[i].yAxis = tran.cubes[i].yAxis.ToString().Trim();
            results[i].zAxis = tran.cubes[i].zAxis.ToString().Trim();
            ColorUtility.TryParseHtmlString("#" + tran.cubes[i].color, out results[i].color);
        }
        onDone(results);
    }
    IEnumerator RecieveInternalDB(Action<CubeItem[]> onDone)
    {
        IDbConnection dbconn;

        int count;
        conn = "URI=file:" + filePath;
        //  Debug.Log("Database path : " + conn);
        dbconn = new SqliteConnection(conn);                        // db 연결
        dbconn.Open();                                              //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();                  // 명령어 생성
        string sqlQuery = "SELECT count(IndexNo) FROM myBurniture";         // 쿼리문 만들기

        dbcmd.CommandText = sqlQuery;                               // 명령어 설정       

        count = Convert.ToInt32(dbcmd.ExecuteScalar());
        dbcmd.CommandText = "SELECT * FROM myBurniture";
        IDataReader reader = dbcmd.ExecuteReader();                 // 명령어 실행하여 데이터 리더기에 저장

        var results = new CubeItem[count];

        if (count == 0)
        {
            Debug.Log("There is No Data!!!!");                               // No data
        }

        for (int i = 0; i < count; ++i)                                 // count 만큼 results[i] 에 값을 넣어줌 그걸 리턴 함
        {
            if (!reader.Read()) break; ;//여기서 큐브의 정보를 넣어주면 됨 
            results[i] = new CubeItem();                                // X,Y,Z , 위치좌표, 큐브이름 
            results[i].index = reader.GetInt32(0);
            results[i].type = reader.GetInt32(1);                       // 저장한 가구의 타입을 가져옴
            results[i].name = reader.GetString(2);                      // 저장한 가구의 이름을 가져옴
            results[i].x = reader.GetString(3);                         // 저장한 가구의 x크기 가져옴
            results[i].y = reader.GetString(4);                         // 저장한 가구의 y크기 가져옴
            results[i].z = reader.GetString(5);                         // 저장한 가구의 z크기 가져옴  
                                                                        // results[i].xAxis = reader.GetString(6);                     //x 축 y 축 z 축
                                                                        // results[i].yAxis = reader.GetString(7);
                                                                        // results[i].zAxis = reader.GetString(8);
            ColorUtility.TryParseHtmlString("#" + reader.GetString(9), out results[i].color);
        }

        dbcmd.Dispose();                                            // 커멘드 닫아주기
        dbcmd = null;
        dbconn.Close();                                             // 트랜잭션 닫아주기
        dbconn = null;

        onDone(results);

        yield return null;
    }
    public string Furniture_Choose(int num)
    {
        switch (num)
        {
            case 1:
                return "서랍장";
            case 2:
                return "에어컨";
            case 3:
                return "옷장";
            case 4:
                return "의자";
            case 5:
                return "책장";
            case 6:
                return "책상";
            case 7:
                return "침대";
            case 8:
                return "화장대";
            case 9:
                return "테이블";
            default:
                return "NO";
        }
    }
    public int Furniture_Choose(String furnitureName)
    {
        switch (furnitureName)
        {
            case "서랍장":
                return 1;
            case "에어컨":
                return 2;
            case "옷장":
                return 3;
            case "의자":
                return 4;
            case "책장":
                return 5;
            case "책상":
                return 6;
            case "침대":
                return 7;
            case "화장대":
                return 8;
            case "테이블":
                return 9;
            default:
                return 0;
        }
    }
    public class CubeItemView
    {
        public Text titleText;
        public Text x, y, z;
        public Text xAxis, yAxis, zAxis;
        public Text type;
        public Text index;
        public Text color;
        public Text typeNum;
        public RawImage furnitureImage;
        //public RawImage iconImage;//, icon2Image,icon3Image;

        public CubeItemView(Transform rootView)
        {
            index = rootView.Find("Index").GetComponent<Text>();
            furnitureImage = rootView.Find("FImage").GetComponent<RawImage>();
            type = rootView.Find("TitlePanel/Type").GetComponent<Text>();
            color = rootView.Find("Color").GetComponent<Text>();
            typeNum = rootView.Find("TypeNum").GetComponent<Text>();
            titleText = rootView.Find("TitlePanel/CubeName").GetComponent<Text>();
            x = rootView.Find("TitlePanel/XText").GetComponent<Text>();
            y = rootView.Find("TitlePanel/YText").GetComponent<Text>();
            z = rootView.Find("TitlePanel/ZText").GetComponent<Text>();
            xAxis = rootView.Find("XAxisText").GetComponent<Text>();
            yAxis = rootView.Find("YAxisText").GetComponent<Text>();
            zAxis = rootView.Find("ZAxisText").GetComponent<Text>();
        }
    }
    public class CubeItem
    {
        public string name;
        public string x, y, z;
        public string xAxis, yAxis, zAxis;
        public int type;
        public int index;
        public Color color;
        public CubeItem() { xAxis = "0"; yAxis = "0"; zAxis = "0"; }
    }


}