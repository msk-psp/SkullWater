using UnityEngine;
using System.Collections;

public class ControlFurniture : MonoBehaviour
{

    RaycastHit hit;
    private GameObject MovingFurn; // 움직일 가구 임시 저장
    private GameObject RotateUI; // 회전판
    private GameObject PreOBJ, NowOBJ; // 전에 선택한 가구, 현재 선택한 가구
    private Vector2 Finger1Pre, Finger2Pre;
    private int IsRotate;
    private float DragSpeed = 0.15f;

    // Use this for initialization
    void Start()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ignore Raycast"), LayerMask.NameToLayer("Rotate"), true); // 벽과 회전판의 충돌 무시
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Rotate"), LayerMask.NameToLayer("Furniture"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ground"), LayerMask.NameToLayer("Rotate"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ground"), LayerMask.NameToLayer("Furniture"), true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); // 손가락에서 화면안으로 레이저를쏨
                hit = new RaycastHit();
                Physics.Raycast(ray, out hit); // 레이저에 맞은 객체를 hit에 저장
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Furniture")
                    {
                        MovingFurn = hit.collider.gameObject; // 맞은 객체 저장
                        if (PreOBJ == null) // 이전 선택한 가구가 없을때
                        {
                            PreOBJ = MovingFurn;
                        }
                        NowOBJ = MovingFurn;
                        RotateUI = MovingFurn.transform.GetChild(0).gameObject; // 움직일 가구의 자식 저장
                        RotateUI.SetActive(true);

                        if (NowOBJ.name != PreOBJ.name) // 다른 가구를 선택하면
                        {
                            PreOBJ.transform.GetChild(0).gameObject.SetActive(false);
                            PreOBJ = NowOBJ;
                        }
                        IsRotate = 0;
                    }
                    else if (hit.collider.name == "RotateBoard")
                    {
                        IsRotate = 1;
                    }
                    else
                    {
                        if (MovingFurn != null)
                        {
                            MovingFurn = null;
                        }
                        if (RotateUI != null)
                        {
                            RotateUI.SetActive(false);
                        }
                        IsRotate = 0;
                    }
                }
                else
                {
                    if (MovingFurn != null)
                    {
                        MovingFurn = null;
                    }
                    if (RotateUI != null)
                    {
                        RotateUI.SetActive(false);
                    }
                    IsRotate = 0;
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && MovingFurn != null)
            {
                if (IsRotate == 1)
                { // 가구 회전
                    MovingFurn.transform.Rotate(0, 0, -Input.GetTouch(0).deltaPosition.x); // 수평방향 움직인 값만큼 회전
                }
                else
                { // 가구 드래그
                    Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, 0, Input.GetTouch(0).position.y)); // 스크린 좌표를 월드좌표계로 변환
                    transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime);
                    NowOBJ.transform.position = new Vector3(MovingFurn.transform.position.x + Input.GetTouch(0).deltaPosition.x * DragSpeed, MovingFurn.transform.position.y, MovingFurn.transform.position.z + Input.GetTouch(0).deltaPosition.y * DragSpeed);
                }
            }
        }
        else if (Input.touchCount > 1)
        {
            if (NowOBJ != null)
            {
                Finger1Pre = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
                Finger2Pre = Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition;

                float prevTouchDeltaMag = (Finger1Pre - Finger2Pre).magnitude;
                float touchDeltaMag = (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                NowOBJ.transform.position = new Vector3(NowOBJ.transform.position.x, NowOBJ.transform.position.y - deltaMagnitudeDiff * 0.5f, NowOBJ.transform.position.z);
            }
        }
    }
}
