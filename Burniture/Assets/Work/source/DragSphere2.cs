using UnityEngine;
using System.Collections;

public class DragSphere2 : MonoBehaviour {
    public float MvSpeed = 1f;
    public RaycastHit hit;
    void Update()
    {
        if (Input.touchCount == 1)
        {

            //Debug.Log(Input.mousePosition);

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("collide");
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    var DeltaPosition = (Vector3)Input.GetTouch(0).deltaPosition;
                    transform.Translate(DeltaPosition.x * MvSpeed, 0, DeltaPosition.y * MvSpeed);
                }
            }
        }
    }
}
