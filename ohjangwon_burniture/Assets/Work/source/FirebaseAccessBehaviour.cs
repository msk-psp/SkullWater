﻿using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;
using Furniture;
using System.Collections.Generic;

namespace Furniture
{
    public class Cube
    {
        public float x;
        public float y;
        public float z;
        public float xAxis;
        public float yAxis;
        public float zAxis;
        public string name;
        public Cube()
        {}
        public Cube(string name, float x, float y, float z, float xAxis, float yAxis, float zAxis)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.z = z;
            this.xAxis = xAxis;
            this.yAxis = yAxis;
            this.zAxis = zAxis;
        }
    }
}


namespace FirebaseAccess
{
    public class Transaction : MonoBehaviour{
     //접속 테스트용
        const string FIREBASE_URL = "https://unity-d5c83.firebaseio.com/";
        const string CHILD_USERS = "Users";
        const string CHILD_CUBES = "Cubes";                                                         //DB Child
        const string PLAYERID = "USER_ID_KEY";                                                      //
        DatabaseReference mDatabaseRef;                                                             //Firebase 참조 객체
        public List<Cube> cubes;                                                                    //가구 리스트 받아올 리스트
        public bool isFailed= false;                                                                // 접속 실패
        public bool isWaiting = true;                                                               // 대기중
        public bool isSuccess = false;                                                              // 성공 변수
        //public FirebaseUser mUser=null;
      
        public Transaction(){
            
            cubes = new List<Cube>();                                                               //큐브 초기화
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(FIREBASE_URL);//Firebase 주소 설정
            //Test용 계정
            FirebaseApp.DefaultInstance.SetEditorP12FileName("Unity-e7e350bf9dcc.p12");        //Editor용 파일 이름 설정
            FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("administrator@unity-d5c83.iam.gserviceaccount.com");
            FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
            
            mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;                      //Root 주소 받아옴
            Debug.Log(mDatabaseRef + "주소");
           
        }
        public void WriteCube(string cubeName, float x, float y, float z, float xAxis, float yAxis, float zAxis){
            Cube cube = new Cube(cubeName, x, y, z,xAxis, yAxis, zAxis);                              //받아온 변수로 객체 생성
            string json = JsonUtility.ToJson(cube);                                                 // cube객체->Json 형태로
            string userID = PlayerPrefs.GetString(PLAYERID);
            if (userID != null) {
                mDatabaseRef.Child(CHILD_USERS).Child(userID).Child(CHILD_CUBES).Child(cubeName).SetRawJsonValueAsync(json); //유저 익명 아이디로 저장
            }
            else
            {
                Debug.Log("비로그인 상태입니다.");
            }
            mDatabaseRef.Child(CHILD_CUBES).Child(cubeName).SetRawJsonValueAsync(json);//가구 목록에 저장
            Debug.Log(json + "형태로 전송됨?");
        }
        //현재 안 쓰임//
        public Cube RetrieveCubesByName(string name){
            Cube cube=null;
            
            GameObject BotLDSphere; //2
            GameObject BotLUSphere; //7
            GameObject BotRDSphere; //1
            GameObject TopLDSphere; //3
            GameObject oCube;
            float mCube_xScale;
            float mCube_yScale;
            float mCube_zScale;

            float mCube_xPosition;
            float mCube_yPosition;
            float mCube_zPosition;

            FirebaseDatabase.DefaultInstance
                  .GetReference(CHILD_CUBES)
                  .GetValueAsync().ContinueWith(task =>{
                      if (task.IsFaulted){
                          //failed to load database snapshot

                      }
                      else if (task.IsCompleted) {
                          DataSnapshot snapshot = task.Result;
                          if (snapshot == null) {
                              Debug.Log("RetrieveCubes By Name : 해당 큐브를 찾을 수 없습니다.");
                              return;
                          }
                          string json = snapshot.Child(name).GetRawJsonValue();
                          cube = JsonUtility.FromJson<Cube>(json);
                          oCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                          BotLDSphere = GameObject.FindWithTag("Sphere2");
                          BotLUSphere = GameObject.FindWithTag("Sphere7");
                          BotRDSphere = GameObject.FindWithTag("Sphere1");
                          TopLDSphere = GameObject.FindWithTag("Sphere3");

                          mCube_xScale = cube.x;
                          mCube_yScale = cube.y;
                          mCube_zScale = cube.z;

                          mCube_xPosition = BotRDSphere.transform.position.x;
                          mCube_yPosition = TopLDSphere.transform.position.y;
                          mCube_zPosition = BotLUSphere.transform.position.z;

                          oCube.transform.localScale += new Vector3(mCube_xScale, mCube_yScale, mCube_zScale);
                          oCube.transform.position = new Vector3(mCube_xPosition, mCube_yPosition, mCube_zPosition);

                          Debug.Log("x : " + mCube_xScale + "y : " + mCube_yScale + "z : " + mCube_zScale);
                          Instantiate(oCube);
                          
                          Debug.Log(json +" Cube Receive");
                            
                      }
                  });
                
            return cube;
        }
        public List<Cube> RetrieveCubesByUserid(string uid){
            isFailed = false;
            isWaiting = true;
            isSuccess = false;

            FirebaseDatabase.DefaultInstance
                  .GetReference(CHILD_USERS).Child(uid)
                  .GetValueAsync().ContinueWith(task => {                           //비동기적으로 기본주소/Users/%uid% 아래 있는 것들을 받아옴
                      if (task.IsFaulted)
                      {
                          Debug.Log("Task Fault");
                          //failed to load database snapshot
                          isFailed = true;                                          //받기 실패
                      }
                      else if (task.IsCompleted)
                      {
                          Debug.Log("Task Completed");
                          DataSnapshot snapshot = task.Result;                          //받아온 걸 스냅샷에 넣음
                          if(snapshot == null){                                         //받아온 게 null이면
                              Debug.LogError("해당 유저를 찾을 수 없습니다.");
                              isFailed = true;                                                  //실패
                              return ;
                          }
                          Debug.Log("key : "+snapshot.Key);
                          isSuccess = true;                                             //상태 바꿔주고
                          isWaiting = false;
                        
                         foreach (DataSnapshot cubeSnap in snapshot.Child("Cubes").Children){           
                              cubes.Add(JsonUtility.FromJson<Cube>(cubeSnap.GetRawJsonValue())); //Cube List에 cubeSnap안에서 cube를 꺼내서 넣음
                             
                              Debug.Log("foreach " + cubeSnap.Key + cubeSnap.GetRawJsonValue());
                          }
                         
                      }
                  });
            return cubes;
        }
    }
}