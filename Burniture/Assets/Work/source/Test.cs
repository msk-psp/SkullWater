using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    public GameObject cube;
    public float FirstDistance;
    public float Speed = 15f;
    public int Cube_num = 0;
    public GameObject MoveCube;
    //private RoomGenerate Room;
    public Vector3 v;
 
    public void Generate()
    {
        GameObject plane = GameObject.FindWithTag("Bottom");
        //Instantiate(cube);
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        v.x = plane.transform.position.x;
        v.y = plane.transform.position.y;
        v.z = plane.transform.position.z;
        transform.position = v;
        cube.transform.localScale += new Vector3(50,50,50);
        cube.transform.position = new Vector3(v.x, cube.transform.localScale.x/2+1, v.z);
    }
    //자이로센서
    void Start()
    {
        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 0.01f;
        FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
    }


    void Update()
    {
        transform.Rotate(-Input.gyro.rotationRateUnbiased.x,
                          -Input.gyro.rotationRateUnbiased.y,
                          0);
        //Drag();
    }
    void Drag()
    {
        if (Input.touchCount == 0)              // 터치가 없으면
        {
            Cube_num = 0;                     // 선택된 것도 없음
            MoveCube = null;
        }
        if (Input.touchCount == 1)              // 화면에 터치한 손가락의 갯수가 한개일때
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); // 손가락에서 화면안으로 레이저를쏨
            RaycastHit hit = new RaycastHit(); // 레이저가 맞을때를 hit라고 선언

            if (Physics.Raycast(ray, out hit) && Cube_num == 0) // 레이저가 오브젝트에 맞고, 아직 선택된 것이 없을때
            {
                if (hit.collider.gameObject.tag == "Cube")
                {
                    Cube_num = 1;
                    MoveCube = cube;
                }
            }
            else
            {
                var touchDeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                MoveCube.transform.Translate(touchDeltaPosition.x * Time.deltaTime * 20f, 0, touchDeltaPosition.y * Time.deltaTime * 20f); //드래그
            }
        }
        else if (Input.touchCount > 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                //Debug.Log("움직인다.");

                //var touchDeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                if (Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) > FirstDistance) // 위아래
                {
                    //Debug.Log("움직여");
                    FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                    cube.transform.Translate(0, cube.transform.position.y * Time.deltaTime,0);
                }
                else
                {
                    FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                    cube.transform.Translate(0, -(cube.transform.position.y * Time.deltaTime),0);
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(1).phase == TouchPhase.Began)
            {
                FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended && Input.GetTouch(1).phase == TouchPhase.Ended)
            {
                FirstDistance = 0;
            }
        }
    }
}
