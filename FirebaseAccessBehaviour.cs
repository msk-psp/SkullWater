using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

namespace Furniture
{
    public class Cube
    {
        public float X;
        public float Y;
        public float Z;
        public Cube()
        {
        }
        public Cube(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
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
            //FirebaseApp.DefaultInstance.SetEditorP12FileName("unity-d5c83-P12.p12");
            //FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("administrator@unity-d5c83.iam.gserviceaccount.com");
            //FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");

            mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
            Debug.Log(mDatabaseRef + "주소로 로그인");
        }
        // Update is called once per frame
  
        public void writeCube(string name, float X, float Y, float Z)
        {
            Furniture.Cube cube = new Furniture.Cube(X, Y, Z);
            string json = JsonUtility.ToJson(cube);
            
            mDatabaseRef.Child("Cubes").Child(name).SetRawJsonValueAsync(json);
            Debug.Log(json + "현태로 전송됨?");
        }
    }

}