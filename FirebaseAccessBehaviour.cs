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
        DatabaseReference mDatabaseRef;
        public List<Cube> cubes;
        public bool isFailed= false;
        public bool isWaiting = true;
        public bool isSuccess = false;
        // Use this for initialization
        public Transaction(){
            cubes = new List<Cube>();
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-d5c83.firebaseio.com/");
            //Test용 계정
            FirebaseApp.DefaultInstance.SetEditorP12FileName("Unity-e7e350bf9dcc.p12");        
            FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("administrator@unity-d5c83.iam.gserviceaccount.com");
            FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
            
            mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
            Debug.Log(mDatabaseRef + "주소로 로그인");
        }
        // Update is called once per frame
  
        public void WriteCube(string name, float x, float y, float z, float xAxis, float yAxis, float zAxis){
            Cube cube = new Cube(name, x, y, z,xAxis, yAxis, zAxis);
            string json = JsonUtility.ToJson(cube);                                 //
            mDatabaseRef.Child("Cubes").Child(name).SetRawJsonValueAsync(json);//json 형태로 바꿔 데이터베이스에 전송
            Debug.Log(json + "형태로 전송됨?");
        }

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
                  .GetReference("Cubes")
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
                  .GetReference("Users").Child(uid)
                  .GetValueAsync().ContinueWith(task => {
                      if (task.IsFaulted)
                      {
                          Debug.Log("Task Fault");
                          //failed to load database snapshot
                          isFailed = true;
                      }
                      else if (task.IsCompleted)
                      {
                          Debug.Log("Task Completed");
                          DataSnapshot snapshot = task.Result;
                          if(snapshot == null){
                              Debug.LogError("해당 유저를 찾을 수 없습니다.");
                              isFailed = true;
                              return ;
                          }
                          Debug.Log("key : "+snapshot.Key);
                          isSuccess = true;
                          isWaiting = false;
                        
                         foreach (DataSnapshot cubeSnap in snapshot.Child("Cubes").Children){
                              cubes.Add(JsonUtility.FromJson<Cube>(cubeSnap.GetRawJsonValue())); //Cube List에 cubeSnap안에서 cube를 꺼내서 넣음
                             
                              Debug.Log("foreach " + cubeSnap.Key + cubeSnap.GetRawJsonValue());
                          }
                          /*foreach(Cube cu1 in cubes){
                              Debug.Log("cube foreach" + JsonUtility.ToJson(cu1));
                          }*/
                         
                      }
                  });
            return cubes;
        }
    }
}