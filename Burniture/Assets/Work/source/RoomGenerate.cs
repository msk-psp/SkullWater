using UnityEngine;
using System.Collections;

public class RoomGenerate : MonoBehaviour {

    private GameObject Plane;
    private GameObject Quad1, Quad2, Quad3, Quad4;
    private GameObject LF, RF, LB, RB, LFU, LBU, RFU, RBU;
    private float Plane_xScale, Plane_yScale, Plane_zScale;
    private float Plane_xPosition, Plane_yPosition, Plane_zPosition;
    private float Quad_xScale, Quad_yScale, Quad_zScale;
    private float Quad_xPosition, Quad_yPosition, Quad_zPosition;
    public Material Wmat;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

        FindSphere();

        if (null!=GameObject.FindWithTag("Bottom"))
        {
           //토스트 방이 이미 생성되었습니다.
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
            Plane.transform.Rotate(new Vector3(0, 180, 0));

            Plane.layer = 2; // 레이캐스트 무시

            rb = Plane.AddComponent<BoxCollider>();
            rb.size = new Vector3(Plane_xScale, 100, Plane_zScale);
            rb.center = new Vector3(0, -rb.size.y / 2, 0);

            Plane.GetComponent<Renderer>().material = Wmat;
            Plane.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.3f);

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

            Quad1.layer = 2;

            rb = Quad1.AddComponent<BoxCollider>();
            rb.size = new Vector3(3, 3, 100);
            rb.center = new Vector3(0, 0, rb.size.z / 2);

            Quad1.GetComponent<Renderer>().material = Wmat;
            Quad1.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.3f);

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

            Quad2.layer = 2;

            rb = Quad2.AddComponent<BoxCollider>();
            rb.size = new Vector3(3, 3, 100);
            rb.center = new Vector3(0, 0, rb.size.z / 2);

            Quad2.GetComponent<Renderer>().material = Wmat;
            Quad2.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.3f);

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

            Quad3.layer = 2;

            rb = Quad3.AddComponent<BoxCollider>();
            rb.size = new Vector3(3, 3, 100);
            rb.center = new Vector3(0, 0, rb.size.z / 2);

            Quad3.GetComponent<Renderer>().material = Wmat;
            Quad3.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.3f);

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

            Quad4.layer = 2;

            rb = Quad4.AddComponent<BoxCollider>();
            rb.size = new Vector3(3, 3, 100);
            rb.center = new Vector3(0, 0, rb.size.z / 2);

            Quad4.GetComponent<Renderer>().material = Wmat;
            Quad4.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.3f);

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
    void FindSphere()
    {
        //LF, RF, LB, RB, LFU, LBU, RFU, RBU;
        RF = GameObject.FindGameObjectWithTag("Sphere1");
        LF = GameObject.FindGameObjectWithTag("Sphere2");
        LFU = GameObject.FindGameObjectWithTag("Sphere3");
        RFU = GameObject.FindGameObjectWithTag("Sphere4");
        RBU = GameObject.FindGameObjectWithTag("Sphere5");
        LBU = GameObject.FindGameObjectWithTag("Sphere6");
        LB = GameObject.FindGameObjectWithTag("Sphere7");
        RB = GameObject.FindGameObjectWithTag("Sphere8");
    }
}
