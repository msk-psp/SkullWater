using UnityEngine;
using System.Collections;

public class RoomGenerate : MonoBehaviour {

    public GameObject Plane;
    public GameObject Quad1, Quad2, Quad3, Quad4;
    public GameObject LF, RF, LB, RB, LFU, LBU, RFU, RBU;
    private float Plane_xScale, Plane_yScale, Plane_zScale;
    private float Plane_xPosition, Plane_yPosition, Plane_zPosition;
    private float Quad_xScale, Quad_yScale, Quad_zScale;
    private float Quad_xPosition, Quad_yPosition, Quad_zPosition;

#if UNITY_ANDROID
    private AndroidJavaObject javaObj = null;

    private AndroidJavaObject GetJavaObject()
    {
        if (javaObj == null)
        {
            javaObj = new AndroidJavaObject("com.kpu.burniture.MainActivity");
        }
        return javaObj;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Retrieve current Android Activity from the Unity Player
        AndroidJavaClass jclass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = jclass.GetStatic<AndroidJavaObject>("currentActivity");

        // Pass reference to the current Activity into the native plugin,
        // using the 'setActivity' method that we defined in the ImageTargetLogger Java class
        GetJavaObject().Call("setActivity", activity);
    }
    public void DestroyRoom()
    {
        if (null != GameObject.FindWithTag("Bottom"))
        {
            Destroy(Plane);
            Destroy(Quad1);
            Destroy(Quad2);
            Destroy(Quad3);
            Destroy(Quad4);
        }
        else
        {
            //생성된 벽이 없습니다.
        }
    }
    public void GenerateRoom()
    {
        BoxCollider rb;

        if (null!=GameObject.FindWithTag("Bottom"))
        {
            //Debug.Log("요고요고");
            GetJavaObject().Call("WallCheckMessage");
        }
        else
        {
            /*바닥*/
            Plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            Plane.tag = "Bottom";
            Plane_xScale = Mathf.Abs((RF.transform.position.x - LF.transform.position.x)) / 10;//Mathf.Abs(LF.transform.position.x - RF.transform.position.x);
            Plane_yScale = 1;
            Plane_zScale = Mathf.Abs((RB.transform.position.z - RF.transform.position.z)) / 10;//Mathf.Abs(RB.transform.position.z - RF.transform.position.z);
            Plane_xPosition = (RF.transform.position.x + LF.transform.position.x) / 2;
            Plane_yPosition = -0.1f;
            Plane_zPosition = (LB.transform.position.z + RF.transform.position.z) / 2;

            Plane.transform.position = new Vector3(Plane_xPosition, Plane_yPosition, Plane_zPosition);
            Plane.transform.localScale = new Vector3(Plane_xScale, Plane_yScale, Plane_zScale);

            rb = Plane.AddComponent<BoxCollider>();
            rb.size = new Vector3(Plane_xScale, 100, Plane_zScale);
            rb.center = new Vector3(0, -rb.size.y / 2, 0);


            /*왼쪽벽*/
            Quad1 = GameObject.CreatePrimitive(PrimitiveType.Quad);
            Quad1.tag = "LeftWall";

            Quad_xScale = Mathf.Abs((LB.transform.position.z - LF.transform.position.z));
            Quad_yScale = Mathf.Abs((LF.transform.position.y - LFU.transform.position.y));
            Quad_zScale = 1;
            Quad_xPosition = (LF.transform.position.x);
            Quad_yPosition = (LF.transform.position.y + LFU.transform.position.y) / 2;
            Quad_zPosition = (LF.transform.position.z + RB.transform.position.z) / 2 - 0.1f;

            Quad1.transform.position = new Vector3(Quad_xPosition, Quad_yPosition, Quad_zPosition);
            Quad1.transform.localScale = new Vector3(Quad_xScale, Quad_yScale, Quad_zScale);
            Quad1.transform.Rotate(new Vector3(0, -90, 0)); // 오른쪽으로 90도회전

            rb = Quad1.AddComponent<BoxCollider>();
            rb.size = new Vector3(3, 3, 100);
            rb.center = new Vector3(0, 0, rb.size.z / 2);

            /*뒷쪽벽*/
            Quad2 = GameObject.CreatePrimitive(PrimitiveType.Quad);
            Quad2.tag = "BackWall";

            Quad_xScale = Mathf.Abs((LB.transform.position.x - RB.transform.position.x));
            Quad_yScale = Mathf.Abs((LB.transform.position.y - LBU.transform.position.y));
            Quad_zScale = 1;
            Quad_xPosition = (LB.transform.position.x + RB.transform.position.x) / 2;
            Quad_yPosition = (LB.transform.position.y + LBU.transform.position.y) / 2;
            Quad_zPosition = (RB.transform.position.z) - 0.1f;

            Quad2.transform.position = new Vector3(Quad_xPosition, Quad_yPosition, Quad_zPosition);
            Quad2.transform.localScale = new Vector3(Quad_xScale, Quad_yScale, Quad_zScale);

            rb = Quad2.AddComponent<BoxCollider>();
            rb.size = new Vector3(3, 3, 100);
            rb.center = new Vector3(0, 0, rb.size.z / 2);

            /*오른쪽벽*/
            Quad3 = GameObject.CreatePrimitive(PrimitiveType.Quad);
            Quad3.tag = "RightWall";

            Quad_xScale = Mathf.Abs((RB.transform.position.z - RF.transform.position.z));
            Quad_yScale = Mathf.Abs((RF.transform.position.y - RFU.transform.position.y));
            Quad_zScale = 1;
            Quad_xPosition = (RF.transform.position.x);
            Quad_yPosition = (RF.transform.position.y + RFU.transform.position.y) / 2;
            Quad_zPosition = (RF.transform.position.z + RB.transform.position.z) / 2 - 0.1f;

            Quad3.transform.position = new Vector3(Quad_xPosition, Quad_yPosition, Quad_zPosition);
            Quad3.transform.localScale = new Vector3(Quad_xScale, Quad_yScale, Quad_zScale);
            Quad3.transform.Rotate(new Vector3(0, 90, 0)); // 왼쪽으로 90도회전

            rb = Quad3.AddComponent<BoxCollider>();
            rb.size = new Vector3(3, 3, 100);
            rb.center = new Vector3(0, 0, rb.size.z / 2);


            //Quad3.AddComponent<BoxCollider>();

            /*앞쪽벽*/
            Quad4 = GameObject.CreatePrimitive(PrimitiveType.Quad);
            Quad4.tag = "FrontWall";

            Quad_xScale = Mathf.Abs((LF.transform.position.x - RF.transform.position.x));
            Quad_yScale = Mathf.Abs((RF.transform.position.y - RFU.transform.position.y));
            Quad_zScale = 1;
            Quad_xPosition = (RF.transform.position.x + LF.transform.position.x) / 2;
            Quad_yPosition = (RF.transform.position.y + RFU.transform.position.y) / 2;
            Quad_zPosition = (RF.transform.position.z);

            Quad4.transform.position = new Vector3(Quad_xPosition, Quad_yPosition, Quad_zPosition);
            Quad4.transform.localScale = new Vector3(Quad_xScale, Quad_yScale, Quad_zScale);
            Quad4.transform.Rotate(new Vector3(0, 180, 0));

            rb = Quad4.AddComponent<BoxCollider>();
            rb.size = new Vector3(3, 3, 100);
            rb.center = new Vector3(0, 0, rb.size.z / 2);

            //Plane.transform.localScale += new Vector3(Plane_xScale/11, Plane_yScale/2, Plane_zScale/11) /*- new Vector3(1, 1, 1)*/;
            //Instantiate(Plane);
            /*Destroy(RB);
            Destroy(LF);
            Destroy(RF);
            Destroy(LB);
            Destroy(LFU);
            Destroy(LBU);
            Destroy(RFU);
            Destroy(RBU);*/
            RB.SetActive(false);
            LF.SetActive(false);
            RF.SetActive(false);
            LB.SetActive(false);
            LFU.SetActive(false);
            LBU.SetActive(false);
            RFU.SetActive(false);
            RBU.SetActive(false);
        }
    }
#else
    private void ShowTargetInfo(string targetName, float targetWidth, float targetHeight) {
        Debug.Log("ShowTargetInfo method placeholder for Play Mode (not running on Android device)");
    }
#endif
}
