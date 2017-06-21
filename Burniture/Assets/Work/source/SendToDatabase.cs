/*************************
* 프로그램 : 내부, 외부 데이터베이스 전송 프로그램
* 작성자   : 김민수, 정지훈
* 수정일자 : 20170527
**************************/

using UnityEngine;
using System.Collections;
using FirebaseAccess;
using Furniture;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;

public class SendToDatabase : MonoBehaviour
{
    const string COLORS = "COLORS";
    string dbName = "burnitureDatabase.db";
    string filePath;
    private GameObject BotLDSphere; //2
    private GameObject BotLUSphere; //7
    private GameObject BotRDSphere; //1
    private GameObject TopLDSphere; //3
    private GameObject oCube;
    private InputField nameField;
    private string mCube_name = null;

    private float mCube_xScale;
    private float mCube_yScale;
    private float mCube_zScale;

    private float mCube_xPosition;
    private float mCube_yPosition;
    private float mCube_zPosition;

    private Transaction tran;
    public GameObject prefab;
    public Dropdown dropdown;
    // public RectTransform prefab;
    public RectTransform content;
    public int furnitureNum;

    void Start()
    {
        #if UNITY_ANDROID
                filePath = Application.persistentDataPath + "/" + dbName; //for android
        #endif
        #if UNITY_EDITOR
                filePath = Application.dataPath + "/" + dbName;   // for unity editor
        #endif
        tran = new Transaction();
        prefab.SetActive(false);

        // Smart Phone 사용 시 코드
        if (!File.Exists(filePath))//데이터베이스가 생성이 안 되어 있다면.. jar 경로에서 DB를 불러와 어플리케이션  persistentpath에 DB를 write함
        {
            
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + dbName);  // this is the path to your StreamingAssets in android
            while (!loadDB.isDone)
            {

            }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check

            // then save to Application.persistentDataPath

            File.WriteAllBytes(filePath, loadDB.bytes);
        }
    }
    private void GetScale()
    {
        BotLDSphere = GameObject.FindWithTag("Sphere2");                                //Bottom LeftDown 왼쪽아래 구체
        BotLUSphere = GameObject.FindWithTag("Sphere7");                                //Bottom LeftUp 왼쪽아래 구체
        BotRDSphere = GameObject.FindWithTag("Sphere1");                                //Bottom RightDown 왼쪽아래 구체
        TopLDSphere = GameObject.FindWithTag("Sphere3");                                //Top LeftDown 왼쪽아래 구체

        mCube_xScale = Mathf.Abs(BotLDSphere.transform.position.x - BotRDSphere.transform.position.x);  // x 길이 구해서 저장
        mCube_yScale = Mathf.Abs(BotLDSphere.transform.position.y - TopLDSphere.transform.position.y);  // y 길이 구해서 저장
        mCube_zScale = Mathf.Abs(BotLDSphere.transform.position.z - BotLUSphere.transform.position.z);  // z 길이 구해서 저장
    }
    private void GetPosition()
    {
        mCube_xPosition = BotRDSphere.transform.position.x;                 //가구 위치
        mCube_yPosition = TopLDSphere.transform.position.y;
        mCube_zPosition = BotLUSphere.transform.position.z;
    }
    private void GetText()
    {
        nameField = prefab.GetComponentInChildren(typeof(InputField)) as InputField;    //

        Text[] textfields = prefab.GetComponentsInChildren<Text>();      //prefab 칠드런에서 텍스트 입력 필드 가져옴           
        textfields[0].text = mCube_xScale.ToString();                   //길이 띄워줌
        textfields[1].text = mCube_zScale.ToString();
        textfields[2].text = mCube_yScale.ToString();
    }

    public void OpenDialog()
    {
        GetScale();
        GetPosition();
        GetText();

        prefab.SetActive(true);
    }

    public void SendInternalDB()
    {
        mCube_name = nameField.text;
        const string TABLENAME = "myBurniture";
        Color color;
        string conn;
        IDbConnection dbconn;
        furnitureNum = dropdown.value;
        conn = "URI=file:" + filePath;
        dbconn = new SqliteConnection(conn);                        // db 연결
        dbconn.Open(); //Open connection to the database.
        color = GameObject.Find("ColorPicker").GetComponent<RawImage>().color;
        IDbCommand dbcmd = dbconn.CreateCommand();                  // 명령어 생성
        string sqlQuery = "INSERT INTO "+TABLENAME+"(Type,Name,XLength,YLength,ZLength,RGB) VALUES ('" + furnitureNum + "', '" + mCube_name + "', '" + mCube_xScale.ToString() + "', '" + mCube_yScale + "', '" + mCube_zScale + "', '" + ColorUtility.ToHtmlStringRGBA(color) + "');";        // 쿼리문 만들기
        Debug.Log("User_Save Color : " + ColorUtility.ToHtmlStringRGBA(color));
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
        prefab.SetActive(false);
    }
    public void SendInternalDB(Cube cube,bool isRoom)
    {
        string conn;
        string TABLENAME;
        if (isRoom)
        {
            TABLENAME = "myRoomFurniture";
        }
        else
        {
            TABLENAME = "myBurniture";
        }
        IDbConnection dbconn;

        conn = "URI=file:" + Application.dataPath + "/" + dbName;                           // db 경로
        dbconn = new SqliteConnection(conn);                        // db 연결
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();                  // 명령어 생성
        
        string sqlQuery = "INSERT INTO "+TABLENAME+"(Type,Name,XLength,YLength,ZLength,XAxis,YAxis,ZAxis,RGB) VALUES ('" + cube.type + "', '" + cube.name + "', '" + cube.x.ToString() +
                     "', '" + cube.y.ToString() + "', '" + cube.z.ToString() + "', '" + cube.xAxis + "','" + cube.yAxis + "','" + cube.zAxis + "','" + cube.color + "');";        // 쿼리문 만들기
        Debug.Log("SendToDatabase sqlQuery:" +sqlQuery);
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
    }
    
    public void SendCube()                                              // 외부 DB 전송 ( 이름 바꿀것)
    {
        string RGB = GameObject.Find("Spuit").GetComponent<RawImage>().color.ToString();                     
        if (RGB.Length <= 0)
        {
            RGB = "0,0,0";
        }
     
        mCube_name = nameField.text;                                    //이름 필드에서 가구 이름 받아옴
        //Debug.Log("name :" + mCube_name);
        if (mCube_name == null && mCube_name.Length == 0) { mCube_name = "MyCube" + Random.Range(1, 10000); }//가구 이름 입력 안 했으면
                                                                                                             //MyCube + 임의의 수로 정함

        tran.WriteCube(mCube_name, mCube_xScale, mCube_yScale, mCube_zScale, 
                        mCube_xPosition, mCube_yPosition, mCube_zPosition, RGB);
        //큐브 저장
        prefab.SetActive(false);                                                                        //Dialog off
    }

    public void Cancel()
    {
        prefab.SetActive(false);                    //Dialog off
    }

}
