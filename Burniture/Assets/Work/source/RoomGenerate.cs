using UnityEngine;
using System.Collections;
using Vuforia;
public class RoomGenerate : MonoBehaviour
{
    public static GameObject Plane; // 바닥
    public static GameObject Quad1, Quad2, Quad3, Quad4; // 벽
    private GameObject LF, RF, LB, RB, LFU, LBU, RFU, RBU; // 꼭지점 객체
    private float Plane_xScale, Plane_yScale, Plane_zScale; // 바닥 사이즈
    private float Plane_xPosition, Plane_yPosition, Plane_zPosition; // 바닥 위치
    private float Quad_xScale, Quad_yScale, Quad_zScale; // 벽 사이즈
    private float Quad_xPosition, Quad_yPosition, Quad_zPosition; // 벽 위치
    public Material Fmat, Wmat; // 벽과 바닥의 material

    private void Start()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ignore Raycast"), LayerMask.NameToLayer("Ignore Raycast"), true); // 벽과 벽의 충돌 무시
    }
    public void GenerateRoom()
    {
        BoxCollider rb;
        //Rigidbody rigid;

        FindSphere();

        /*바닥*/
        Plane = GameObject.CreatePrimitive(PrimitiveType.Plane); // 바닥 Plane을 동적으로 생성
        Plane.tag = "Bottom"; // 태그를 부여
                              /*크기를 각 꼭지점 사이 거리에 반비례하여 부여*/
        Plane_xScale = Mathf.Abs((RF.transform.position.x - LF.transform.position.x)) / 10;
        Plane_yScale = 1;
        Plane_zScale = Mathf.Abs((RB.transform.position.z - RF.transform.position.z)) / 10;

        /*위치*/
        Plane_xPosition = (RF.transform.position.x + LF.transform.position.x) / 2;
        Plane_yPosition = -0.1f; // 가구를 생성해야 하므로 실제 바닥보다 0.1 낮게 설정
        Plane_zPosition = (LB.transform.position.z + RF.transform.position.z) / 2;

        Plane.transform.position = new Vector3(Plane_xPosition, Plane_yPosition, Plane_zPosition);
        Plane.transform.localScale = new Vector3(Plane_xScale, Plane_yScale, Plane_zScale);
        Plane.transform.Rotate(new Vector3(0, 180, 0)); // 한면만 표시되므로 회전이 필요

        Plane.layer = 2; // 레이캐스트를 무시 하여 드래그가 되지 않게 설정

        /*가구와의 충돌 구현을 위하여 충돌 박스를 객체보다 크게 설정*/
        rb = Plane.AddComponent<BoxCollider>();
        rb.size = new Vector3(Plane_xScale, 100, Plane_zScale);
        rb.center = new Vector3(0, -rb.size.y / 2, 0);

        /*rigid = Plane.AddComponent<Rigidbody>();  // RigidBody 컴포넌트 추가
        rigid.useGravity = false;
        rigid.interpolation = RigidbodyInterpolation.Interpolate;
        rigid.constraints = RigidbodyConstraints.FreezeRotation;

        Destroy(Plane.GetComponent<MeshCollider>()); // MeshCollider컴포넌트 제거
        */
        Plane.GetComponent<Renderer>().material = Fmat; // 바닥 material을 넣어줌

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

        /*rigid = Quad1.AddComponent<Rigidbody>();  // RigidBody 컴포넌트 추가
        rigid.useGravity = false;
        rigid.interpolation = RigidbodyInterpolation.Interpolate;
        rigid.constraints = RigidbodyConstraints.FreezeRotation;

        Destroy(Quad1.GetComponent<MeshCollider>());
        */
        Quad1.GetComponent<Renderer>().material = Wmat;
        Quad1.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f); // 벽의 스위치 위치를 고려하여 벽의 투명도를 0.5로 설정

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
        /*
        rigid = Quad2.AddComponent<Rigidbody>();  // RigidBody 컴포넌트 추가
        rigid.useGravity = false;
        rigid.interpolation = RigidbodyInterpolation.Interpolate;
        rigid.constraints = RigidbodyConstraints.FreezeRotation;

        Destroy(Quad2.GetComponent<MeshCollider>());
        */
        Quad2.GetComponent<Renderer>().material = Wmat;
        Quad2.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);

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
        /*
        rigid = Quad3.AddComponent<Rigidbody>();  // RigidBody 컴포넌트 추가
        rigid.useGravity = false;
        rigid.interpolation = RigidbodyInterpolation.Interpolate;
        rigid.constraints = RigidbodyConstraints.FreezeRotation;

        Destroy(Quad3.GetComponent<MeshCollider>());
        */
        Quad3.GetComponent<Renderer>().material = Wmat;
        Quad3.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);

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
        /*
        rigid = Quad4.AddComponent<Rigidbody>();  // RigidBody 컴포넌트 추가
        rigid.useGravity = false;
        rigid.interpolation = RigidbodyInterpolation.Interpolate;
        rigid.constraints = RigidbodyConstraints.FreezeRotation;

        Destroy(Quad4.GetComponent<MeshCollider>());
        */
        Quad4.GetComponent<Renderer>().material = Wmat;
        Quad4.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);

        RB.SetActive(false);
        LF.SetActive(false);
        RF.SetActive(false);
        LB.SetActive(false);
        LFU.SetActive(false);
        LBU.SetActive(false);
        RFU.SetActive(false);
        RBU.SetActive(false);

        /*버튼모션 제어*/
        ButtonMotion.State = 5;
        ButtonMotion.ChangeState = 0;

        gameObject.GetComponent<ControlFurniture>().enabled = true;
        gameObject.GetComponent<Drag>().enabled = false;
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