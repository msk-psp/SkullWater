using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour
{

    float FirstDistance;
    public float Speed = 15f;
    public int Cube_num = 0;
    public GameObject MoveCube;
    private GameObject Furn;
    private GameObject Arrow;
    
    public Material Mat;

    void Start()
    {
        FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
    }
    void Update()
    {
        if(MoveCube.gameObject.name==this.gameObject.name) // 터치된 오브젝트의
        {
            Arrow = this.transform.transform.FindChild("Arrow").gameObject; // 터치된 오브젝트의 자식 오브젝트 중 Arrow이름을 가진 오브젝트를 Arrow로한다.
            Arrow.SetActive(true); // 화살표를 활성화
        }
        if (Input.touchCount == 0)              // 터치가 없으면
        {
            Cube_num = 0;                     // 선택된 것도 없음

            //this.GetComponent<MeshRenderer>().material = Mat;// 원래색으로 돌려줌
            Furn.GetComponent<MeshRenderer>().material = Mat;
        }
        if (Input.touchCount == 1)              // 화면에 터치한 손가락의 갯수가 한개일때
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); // 손가락에서 화면안으로 레이저를쏨
            RaycastHit hit = new RaycastHit(); // 레이저가 맞을때를 hit라고 선언

            if (Physics.Raycast(ray, out hit) && Cube_num == 0) // 레이저가 오브젝트에 맞고, 아직 선택된 것이 없을때
            {
                if (hit.collider.gameObject.name == this.gameObject.name)//터치된것과 안된것을 이름으로 구별
                {
                    Cube_num = 1;
                    MoveCube = hit.collider.gameObject;
                    Furn = this.transform.transform.FindChild("chair").gameObject;//자식 오브젝트를 찾음
                    Furn.transform.localScale = this.transform.localScale;//크기를 같게 맞추어준다
                    Furn.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f); // 선택된 객체 투명하게
                    //회전해야함
                }
                else
                    Furn.GetComponent<MeshRenderer>().material = Mat;
                //this.GetComponent<MeshRenderer>().material = Mat;
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
                if (Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) > FirstDistance) // 위아래
                {
                    FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                    MoveCube.transform.Translate(0, MoveCube.transform.position.y * Time.deltaTime, 0);
                }
                else
                {
                    FirstDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                    MoveCube.transform.Translate(0, -(MoveCube.transform.position.y * Time.deltaTime), 0);
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