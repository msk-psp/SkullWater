using UnityEngine;
using System.Collections;

public class MeasureDrag : MonoBehaviour
{

    public GameObject Selected;
    RaycastHit hit;
    private Vector2 Finger1Pre, Finger2Pre;
    public GameObject[] Spheres = new GameObject[4];

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); // 손가락에서 화면안으로 레이저를쏨
                hit = new RaycastHit();
                Physics.Raycast(ray, out hit); // 레이저에 맞은 객체를 hit에 저장3

                if (hit.collider != null)
                {
                    if (hit.collider.name.Contains("Sphere")) // 레이가 Sphere란 단어가 이름에 속해 있는 오브젝트에 맞은 경우
                    {
                        Selected = hit.collider.gameObject;
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (Selected != null)
                {
                    Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, 0, Input.GetTouch(0).position.y)); // 스크린 좌표를 월드좌표계로 변환
                    transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime);
                    Selected.transform.position = new Vector3(Selected.transform.position.x + Input.GetTouch(0).deltaPosition.x *1.1f, Selected.transform.position.y, Selected.transform.position.z + Input.GetTouch(0).deltaPosition.y * 2f);
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (Selected != null)
                {
                    Selected = null;
                }
            }

        }
        else if (Input.touchCount > 1) // 높이 설정
        {
            Finger1Pre = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
            Finger2Pre = Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition;

            float prevTouchDeltaMag = (Finger1Pre - Finger2Pre).magnitude;
            float touchDeltaMag = (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Spheres[0].transform.position = new Vector3(Spheres[0].transform.position.x, Spheres[0].transform.position.y - deltaMagnitudeDiff, Spheres[0].transform.position.z);
            Spheres[1].transform.position = new Vector3(Spheres[1].transform.position.x, Spheres[1].transform.position.y - deltaMagnitudeDiff, Spheres[1].transform.position.z);
            Spheres[2].transform.position = new Vector3(Spheres[2].transform.position.x, Spheres[2].transform.position.y - deltaMagnitudeDiff, Spheres[2].transform.position.z);
            Spheres[3].transform.position = new Vector3(Spheres[3].transform.position.x, Spheres[3].transform.position.y - deltaMagnitudeDiff, Spheres[3].transform.position.z);
        }
    }
}
