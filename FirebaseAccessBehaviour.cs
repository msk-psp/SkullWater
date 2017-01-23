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
        public string name;
        public Cube()
        {}
        public Cube(string name, float x, float y, float z)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
namespace FirebaseAccess
{
    public class Transaction {
     //접속 테스트용
        DatabaseReference mDatabaseRef;
        // Use this for initialization
        public Transaction(){
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-d5c83.firebaseio.com/");
            //Test용 계정
            FirebaseApp.DefaultInstance.SetEditorP12FileName("Unity-e7e350bf9dcc.p12");        
            FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("administrator@unity-d5c83.iam.gserviceaccount.com");
            FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
            
            mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
            Debug.Log(mDatabaseRef + "주소로 로그인");
        }
        // Update is called once per frame
  
        public void WriteCube(string name, float x, float y, float z){
            Cube cube = new Cube(name, x, y, z);
            string json = JsonUtility.ToJson(cube);                                 //
            mDatabaseRef.Child("Cubes").Child(name).SetRawJsonValueAsync(json);//json 형태로 바꿔 데이터베이스에 전송
            Debug.Log(json + "형태로 전송됨?");
        }

        public Cube RetrieveCubesByName(string name){
            Cube cube=null;
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

                          Debug.Log(json +" Cube Receive");
                      }
                  });
            return cube;
        }
        public List<Cube> RetrieveCubesByUserid(string uid){
            List<Cube> cubes = new List<Cube>();
            FirebaseDatabase.DefaultInstance
                  .GetReference("UserID")
                  .GetValueAsync().ContinueWith(task => {
                      if (task.IsFaulted)
                      {
                          Debug.Log("Task Fault");
                          //failed to load database snapshot

                      }
                      else if (task.IsCompleted)
                      {
                          Debug.Log("Task Completed");
                          DataSnapshot snapshot = task.Result;
                          if(snapshot == null){
                              Debug.LogError("해당 유저를 찾을 수 없습니다.");
                              cubes = null;//유저를 찾을 수 없을 때 null List를 반환
                              return ;
                          }
                          IEnumerator s = snapshot.Child("Minsoo").Children.GetEnumerator();

                          Debug.Log(snapshot.Child("Minsoo").GetRawJsonValue());
                          //Debug.Log(snapshot.Child("Minsoo").Children.GetEnumerator().Current.Reference);
                          //Debug.Log(snapshot.Child("Minsoo").Children.GetEnumerator().Current.GetRawJsonValue());
                          Debug.Log(snapshot.Child("Minsoo").GetRawJsonValue().Length);
                          Debug.Log(snapshot.Child("Minsoo").GetRawJsonValue());
                          
                          
                         
                      }
                  });
            return cubes;
        }
    }
}