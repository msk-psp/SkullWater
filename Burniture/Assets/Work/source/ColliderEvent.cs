using UnityEngine;
using System.Collections;

public class ColliderEvent : MonoBehaviour {

    private Vector3 FurnPos;
    private GameObject Bottom, Left, Right, Back, Front;
    public GameObject ThisFurn;

    private void Start()
    {
        /*Bottom = RoomGenerate.Plane;
        Left = RoomGenerate.Quad1;
        Back = RoomGenerate.Quad2;
        Right = RoomGenerate.Quad3;
        Front = RoomGenerate.Quad4;
        ThisFurn = this.gameObject;*/
        ThisFurn = this.gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Furniture") // 가구끼리의 충돌
        {
            FurnPos = ThisFurn.transform.position;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Bottom") //  가구의 높낮이 조정중 바닥과의 충돌
        {
            ThisFurn.transform.position = new Vector3(ThisFurn.transform.position.x, 0.1f, ThisFurn.transform.position.z);
        }
        else if(collision.gameObject.tag=="Furniture")
        {
            ThisFurn.transform.position = FurnPos;
        }
    }
}
