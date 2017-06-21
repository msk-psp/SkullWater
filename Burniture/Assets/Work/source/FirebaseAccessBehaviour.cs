using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Furniture;
using System.Collections.Generic;

namespace Furniture
{
    public class Cube
    {
        public int type;
        public int index;
        public float x;
        public float y;
        public float z;
        public float xAxis;
        public float yAxis;
        public float zAxis;
        public string name;
        public string color;
        public Cube()
        { }
        public Cube(string name, float x, float y, float z, float xAxis, float yAxis, float zAxis, string RGB)
        {
            this.index = -1;
            this.name = name;
            this.x = x;
            this.y = y;
            this.z = z;
            this.xAxis = xAxis;
            this.yAxis = yAxis;
            this.zAxis = zAxis;
            this.color = RGB;
        }
        public Cube(int type, string name, float x, float y, float z, float xAxis, float yAxis, float zAxis, string RGB)
        {
            this.index = -1;
            this.type = type;
            this.name = name;
            this.x = x;
            this.y = y;
            this.z = z;
            this.xAxis = xAxis;
            this.yAxis = yAxis;
            this.zAxis = zAxis;
            this.color = RGB;
        }
        public Cube(int index, int type, string name, float x, float y, float z, float xAxis, float yAxis, float zAxis, string RGB)
        {
            this.index = index;
            this.type = type;
            this.name = name;
            this.x = x;
            this.y = y;
            this.z = z;
            this.xAxis = xAxis;
            this.yAxis = yAxis;
            this.zAxis = zAxis;
            this.color = RGB;
        }
    }
}


namespace FirebaseAccess
{
    public class Transaction
    {
        //접속 테스트용
        const string FIREBASE_URL = "https://unity-d5c83.firebaseio.com/";                          //DB URL
        const string CHILD_USERS = "Users";
        const string CHILD_CUBES = "Cubes";                                                         //DB Child
        const string PLAYERID = "USER_ID_KEY";                                                      //Player ref key
        DatabaseReference mDatabaseRef;                                                             //Firebase 참조 객체
        public List<Cube> cubes;
        public Cube cube; //가구 리스트 받아올 리스트
        public bool isFailed = false;                                                                // 접속 실패
        public bool isWaiting = true;                                                               // 대기중
        public bool isSuccess = false;                                                              // 성공 변수
        //public FirebaseUser mUser=null;


        public Transaction()
        {

            cubes = new List<Cube>();                                                               //큐브 초기화
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(FIREBASE_URL);
            cube = new Cube();
            mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;                      //Root 주소 받아옴
            Debug.Log(mDatabaseRef + "주소");

        }
        //type 있는 writeCube
        public void WriteCube(int type, string cubeName, float x, float y, float z, float xAxis, float yAxis, float zAxis, string RGB)
        {

            Cube cube = new Cube(type, cubeName, x, y, z, xAxis, yAxis, zAxis, RGB);                              //받아온 변수로 객체 생성
            string json = JsonUtility.ToJson(cube);                                                 // cube객체->Json 형태로
            string userID = PlayerPrefs.GetString(PLAYERID);
            Debug.Log(FirebaseDatabase.DefaultInstance
                      .GetReference(CHILD_USERS).Child(userID).Child(CHILD_CUBES).Child(cubeName));
            if (userID != null)
            {

                FirebaseDatabase.DefaultInstance
                      .GetReference(CHILD_USERS).Child(userID).Child(CHILD_CUBES).Child(cubeName)
                      .SetRawJsonValueAsync(json).ContinueWith(task =>
                      {                           //비동기적으로 기본주소/Users/%uid% 아래 있는 것들을 받아옴
                          if (task.IsFaulted)
                          {
                              Debug.Log("Retrieve Task Fault");
                              //failed to load database snapshot
                              isFailed = true;                                          //받기 실패
                          }
                          else if (task.IsCompleted)
                          {
                              Debug.Log("Task Completed");
                              isFailed = true;                                                  //실패
                              return;
                          }
                          //  Debug.Log("key : "+snapshot.Key);
                          isSuccess = true;                                             //상태 바꿔주고
                          isWaiting = false;
                      });

            }
            else
            {
                Debug.Log("비로그인 상태입니다.");
            }
            mDatabaseRef.Child(CHILD_CUBES).Child(cubeName).SetRawJsonValueAsync(json);//가구 목록에 저장
            Debug.Log(json + "형태로 전송됨?");

        }
        //type 없는 writeCube
        public void WriteCube(string cubeName, float x, float y, float z, float xAxis, float yAxis, float zAxis, string RGB)
        {
            if (cubeName == null && cubeName.Length == 0) { cubeName = "MyCube" + Random.Range(1, 10000); }
            Cube cube = new Cube(cubeName, x, y, z, xAxis, yAxis, zAxis, RGB);                              //받아온 변수로 객체 생성

            string json = JsonUtility.ToJson(cube);                                                 // cube객체->Json 형태로
            string userID = PlayerPrefs.GetString(PLAYERID);

            if (userID != null)
            {

                FirebaseDatabase.DefaultInstance
                      .GetReference(CHILD_USERS).Child(userID).Child(CHILD_CUBES).Child(cubeName)
                      .SetRawJsonValueAsync(json).ContinueWith(task =>
                      {                           //비동기적으로 기본주소/Users/%uid% 아래 있는 것들을 받아옴
                          if (task.IsFaulted)
                          {
                              Debug.Log("Retrieve Task Fault");
                              //failed to load database snapshot
                              isFailed = true;                                          //받기 실패
                          }
                          else if (task.IsCompleted)
                          {
                              Debug.Log("Task Completed");
                              isFailed = true;                                                  //실패
                              return;
                          }
                          //  Debug.Log("key : "+snapshot.Key);
                          isSuccess = true;                                             //상태 바꿔주고
                          isWaiting = false;
                      });

            }
            else
            {
                Debug.Log("비로그인 상태입니다.");
            }
            mDatabaseRef.Child(CHILD_CUBES).Child(cubeName).SetRawJsonValueAsync(json);//가구 목록에 저장
            Debug.Log(json + "형태로 전송됨?");
        }

        public List<Cube> RetrieveCubesByUserId(string uid)
        {
            isFailed = false;
            isWaiting = true;
            isSuccess = false;
            if (uid != "" || uid != null || uid.Length <= 0)
            {
                uid = PlayerPrefs.GetString(PLAYERID);
            }
            FirebaseDatabase.DefaultInstance
                  .GetReference(CHILD_USERS).Child(uid)
                  .GetValueAsync().ContinueWith(task =>
                  {                           //비동기적으로 기본주소/Users/%uid% 아래 있는 것들을 받아옴
                      if (task.IsFaulted)
                      {
                          Debug.Log("Retrieve Task Fault");
                          //failed to load database snapshot
                          isFailed = true;                                          //받기 실패
                      }
                      else if (task.IsCompleted)
                      {
                          Debug.Log("Task Completed");
                          DataSnapshot snapshot = task.Result;                          //받아온 걸 스냅샷에 넣음
                          if (snapshot == null)
                          {                                         //받아온 게 null이면
                              Debug.LogError("해당 유저를 찾을 수 없습니다.");
                              isFailed = true;                                                  //실패
                              return;
                          }
                          //  Debug.Log("key : "+snapshot.Key);
                          isSuccess = true;                                             //상태 바꿔주고
                          isWaiting = false;

                          foreach (DataSnapshot cubeSnap in snapshot.Child("Cubes").Children)
                          {
                              cubes.Add(JsonUtility.FromJson<Cube>(cubeSnap.GetRawJsonValue())); //Cube List에 cubeSnap안에서 cube를 꺼내서 넣음

                              //Debug.Log("foreach " + cubeSnap.Key + cubeSnap.GetRawJsonValue());
                          }

                      }
                  });

            return cubes;
        }
        /*
        public List<Cube> RetrieveCubesAll()
        {
            isFailed = false;
            isWaiting = true;
            isSuccess = false;
            
            FirebaseDatabase.DefaultInstance
                  .GetReference(CHILD_CUBES)
                  .GetValueAsync().ContinueWith(task =>
                  {                           //비동기적으로 기본주소/Users/%uid% 아래 있는 것들을 받아옴
                      if (task.IsFaulted)
                      {
                          Debug.Log("Retrieve Task Fault");
                          //failed to load database snapshot
                          isFailed = true;                                          //받기 실패
                      }
                      else if (task.IsCompleted)
                      {
                          Debug.Log("Task Completed");
                          DataSnapshot snapshot = task.Result;                          //받아온 걸 스냅샷에 넣음
                          if (snapshot == null)
                          {                                         //받아온 게 null이면
                              Debug.LogError("해당 큐브들을 찾을 수 없습니다.");
                              isFailed = true;                                                  //실패
                              return;
                          }
                          //  Debug.Log("key : "+snapshot.Key);
                          isSuccess = true;                                             //상태 바꿔주고
                          isWaiting = false;
                          
                          foreach (DataSnapshot cubeSnap in snapshot)
                          {
                              cubes.Add(JsonUtility.FromJson<Cube>(cubeSnap.GetRawJsonValue())); //Cube List에 cubeSnap안에서 cube를 꺼내서 넣음

                              //Debug.Log("foreach " + cubeSnap.Key + cubeSnap.GetRawJsonValue());
                          }

                      }
                  });

            return cubes;
        }
        */
        public Cube RetrieveCubeByUserIDAndCubeName(string userId, string cubeName)
        {
            isFailed = false;
            isWaiting = true;
            isSuccess = false;
            if (userId != "" || userId != null || userId.Length <= 0)
            {
                userId = PlayerPrefs.GetString(PLAYERID);
            }
            Debug.Log("RetrieveCubeByUserIDAndCubeName Reference : " + FirebaseDatabase.DefaultInstance
                  .GetReference(CHILD_USERS).Child(userId).Child(CHILD_CUBES).Child(cubeName)
            );
            FirebaseDatabase.DefaultInstance
                  .GetReference(CHILD_USERS).Child(userId).Child(CHILD_CUBES).Child(cubeName)
                  .GetValueAsync().ContinueWith(task =>
                  {                           //비동기적으로 기본주소/Users/%uid% 아래 있는 것들을 받아옴
                      if (task.IsFaulted)
                      {
                          Debug.Log("Retrieve Task Fault");
                          //failed to load database snapshot
                          isFailed = true;                                          //받기 실패
                      }
                      else if (task.IsCompleted)
                      {
                          Debug.Log("Task Completed");
                          DataSnapshot snapshot = task.Result;                          //받아온 걸 스냅샷에 넣음
                          if (snapshot == null)
                          {                                         //받아온 게 null이면
                              Debug.LogError("해당 유저를 찾을 수 없습니다.");
                              isFailed = true;                                                  //실패
                              return;
                          }
                          //  Debug.Log("key : "+snapshot.Key);
                          isSuccess = true;                                             //상태 바꿔주고
                          isWaiting = false;
                          cube = JsonUtility.FromJson<Cube>(snapshot.GetRawJsonValue());
                          Debug.Log("Retrieved cube information :" + snapshot.GetRawJsonValue());
                      }
                  });

            return cube;
        }
        public void DeleteCubeByCubeName(string cubeName)
        {
            isFailed = false;
            isWaiting = true;
            isSuccess = false;
            string userID = PlayerPrefs.GetString(PLAYERID);
            Debug.Log("ID in DeleteCube:" + userID);
            Debug.Log("reference :" + FirebaseDatabase.DefaultInstance
                        .GetReference(CHILD_USERS).Child(userID));

            FirebaseDatabase.DefaultInstance
                 .GetReference(CHILD_USERS).Child(userID).Child(CHILD_CUBES).Child(cubeName).RemoveValueAsync().ContinueWith(task =>
                 {
                     if (task.IsFaulted)
                     {
                         Debug.Log("Save Task Fault");
                         //failed to load database snapshot
                         isFailed = true;
                         isWaiting = false;
                     }
                     else if (task.IsCompleted)
                     {
                         Debug.Log("Task Completed");
                         isSuccess = true;
                     }
                 });
        }
    }

}