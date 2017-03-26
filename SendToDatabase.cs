using UnityEngine;
using System.Collections;
using FirebaseAccess;
using Furniture;
public class SendToDatabase : MonoBehaviour{
    private GameObject BotLDSphere; //2
    private GameObject BotLUSphere; //7
    private GameObject BotRDSphere; //1
    private GameObject TopLDSphere; //3
    private GameObject oCube;
    private float mCube_xScale;
    private float mCube_yScale;
    private float mCube_zScale;

    private float mCube_xPosition;
    private float mCube_yPosition;
    private float mCube_zPosition;

    private Transaction tran;
    public string cube_name=null;
    public void SendCube()  {
       
        tran = new Transaction();
        BotLDSphere = GameObject.FindWithTag("Sphere2");
        BotLUSphere = GameObject.FindWithTag("Sphere7");
        BotRDSphere = GameObject.FindWithTag("Sphere1");
        TopLDSphere = GameObject.FindWithTag("Sphere3");

        
        mCube_xScale = Mathf.Abs(BotLDSphere.transform.position.x - BotRDSphere.transform.position.x);
        mCube_yScale = Mathf.Abs(BotLDSphere.transform.position.y - TopLDSphere.transform.position.y);
        mCube_zScale = Mathf.Abs(BotLDSphere.transform.position.z - BotLUSphere.transform.position.z);
        Debug.Log("1 x : " + BotLDSphere.transform.position.x + "y : " + BotRDSphere.transform.position.x  );

        mCube_xPosition = BotRDSphere.transform.position.x;
        mCube_yPosition = TopLDSphere.transform.position.y;
        mCube_zPosition = BotLUSphere.transform.position.z;
        if (cube_name == null) { cube_name = "MyCube" + Random.Range(1,10000); }
        tran.WriteCube(cube_name, mCube_xScale, mCube_yScale, mCube_zScale, mCube_xPosition, mCube_yPosition, mCube_zPosition);
        Debug.Log("2 x : " + mCube_xScale + "y : " + mCube_yScale + "z : " + mCube_zScale);
    }
}
