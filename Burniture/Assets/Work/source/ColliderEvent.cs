using UnityEngine;
using System.Collections;

public class ColliderEvent : MonoBehaviour {

    private Vector3 FurnPos;
    private GameObject Bottom, Left, Right, Back, Front;
    public GameObject ThisFurn;

    private void Start()
    {
        Bottom = RoomGenerate.Plane;
        Left = RoomGenerate.Quad1;
        Back = RoomGenerate.Quad2;
        Right = RoomGenerate.Quad3;
        Front = RoomGenerate.Quad4;
        ThisFurn = this.gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag=="Furniture")
        {
            FurnPos = new Vector3(ThisFurn.transform.position.x, ThisFurn.transform.position.y, ThisFurn.transform.position.z);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Bottom")
        {
            ThisFurn.transform.position = new Vector3(ThisFurn.transform.position.x, Bottom.transform.position.y + (0.5f * ThisFurn.transform.localScale.y) + 0.3f, ThisFurn.transform.position.z);
        }
        else if (collision.gameObject.tag == "LeftWall")
        {
            ThisFurn.transform.position = new Vector3(Left.transform.position.x + ThisFurn.transform.localScale.x + 0.3f, ThisFurn.transform.position.y, ThisFurn.transform.position.z);
        }
        else if (collision.gameObject.tag == "RightWall")
        {
            ThisFurn.transform.position = new Vector3(Right.transform.position.x - ThisFurn.transform.localScale.x - 0.3f, ThisFurn.transform.position.y, ThisFurn.transform.position.z);
        }
        else if (collision.gameObject.tag == "BackWall")
        {
            ThisFurn.transform.position = new Vector3(ThisFurn.transform.position.x, ThisFurn.transform.position.y, Back.transform.position.z - ThisFurn.transform.localScale.z - 0.3f);
        }
        else if (collision.gameObject.tag == "FrontWall")
        {
            ThisFurn.transform.position = new Vector3(ThisFurn.transform.position.x, ThisFurn.transform.position.y, Front.transform.position.z + ThisFurn.transform.localScale.z + 0.3f);
        }
        else if(collision.gameObject.tag=="Furniture")
        {

        }
    }
}
