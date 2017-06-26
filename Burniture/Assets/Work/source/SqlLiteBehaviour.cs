using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;


public class SqlLiteBehaviour : MonoBehaviour
{
    public void initDB()
    {
        string dbName = "burnitureDatabase.db";
        string conn = "URI=file:" + Application.dataPath + "/" + dbName; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.

        IDbCommand dbcmd = dbconn.CreateCommand();                 // 명령어 생성
        string sqlQuery = "SELECT * " + "FROM myBurniture";        // 쿼리문 만들기
        dbcmd.CommandText = sqlQuery;                              // 명령어 설정
        IDataReader reader = dbcmd.ExecuteReader();                // 명령어 실행하여 데이터 리더기에 저장
        while (reader.Read())                                      // 해당 커서의 값이 존재 하지 않을때 까지 반복하여 꺼낸다.
        {
            /// myBurniture table schema//
            //0 : index, 1 : type(integer), 2 : name(Text),
            // 3 : XLength(integer), 4: Ylength(integer), 
            //5: Zlength(integer), 6, RGB(Text)
            ////

            int type = reader.GetInt32(1);                         // getIndex(attribute index)에서 꺼내옴
            string name = reader.GetString(2);                     // 



         //   Debug.Log("type= " + type + "  name =" + name);
        }
        reader.Close();                                             // 리더(커서 비슷한거) 닫아주기
        reader = null;
        dbcmd.Dispose();                                            // 커멘드 닫아주기
        dbcmd = null;
        dbconn.Close();                                             // 트랜잭션 닫아주기
        dbconn = null;
    }



    // Use this for initialization
    void Start()
    {
        initDB();
    }

    // Update is called once per frame
    void Update()
    {

    }
}