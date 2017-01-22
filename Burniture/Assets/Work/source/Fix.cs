using UnityEngine;
using System.Collections;

public class Fix : MonoBehaviour
{
    public GameObject Sphere1;
    public GameObject Sphere2;
    public GameObject Sphere3;
    public GameObject Sphere4;
    public GameObject Sphere5;
    public GameObject Sphere6;
    public GameObject Sphere7;
    public GameObject Sphere8;
    public GameObject Selected;
    public Vector3 v;

    public void FixCube()
    {
        //1,2,3,7을 기준으로한다. 1과7은 2와3을 기준으로 2는 3을 기준으로한다.
        v.x = Sphere1.transform.position.x;
        v.y = Sphere3.transform.position.y;
        v.z = Sphere3.transform.position.z;
        transform.position = v;
        Sphere4.transform.position = v;

        v.x = Sphere1.transform.position.x;
        v.y = Sphere3.transform.position.y;
        v.z = Sphere7.transform.position.z;
        transform.position = v;
        Sphere5.transform.position = v;

        v.x = Sphere3.transform.position.x;
        v.y = Sphere3.transform.position.y;
        v.z = Sphere7.transform.position.z;
        transform.position = v;
        Sphere6.transform.position = v;

        v.x = Sphere3.transform.position.x;
        v.y = Sphere2.transform.position.y;
        v.z = Sphere3.transform.position.z;
        transform.position = v;
        Sphere2.transform.position = v;

        v.x = Sphere1.transform.position.x;
        v.y = Sphere2.transform.position.y;
        v.z = Sphere2.transform.position.z;
        transform.position = v;
        Sphere1.transform.position = v;

        v.x = Sphere1.transform.position.x;
        v.y = Sphere2.transform.position.y;
        v.z = Sphere7.transform.position.z;
        transform.position = v;
        Sphere8.transform.position = v;

        v.x = Sphere2.transform.position.x;
        v.y = Sphere2.transform.position.y;
        v.z = Sphere7.transform.position.z;
        transform.position = v;
        Sphere7.transform.position = v;
    }
    /*void Update()
    {
        if(Input.touchCount==1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); // 손가락에서 화면안으로 레이저를쏨
            RaycastHit hit = new RaycastHit(); // 레이저가 맞을때를 hit라고 선언
            v = transform.position;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.tag == "Sphere1")
                {
                    Selected = Sphere1;
                }
                else if(hit.collider.gameObject.tag == "Sphere2")
                {
                    Selected = Sphere2;
                }
                else if(hit.collider.gameObject.tag == "Sphere3")
                {
                    Selected = Sphere3;
                }
                else if(hit.collider.gameObject.tag == "Sphere4")
                {
                    Selected = Sphere4;
                }
                else if(hit.collider.gameObject.tag == "Sphere5")
                {
                    Selected = Sphere5;
                }
                else if(hit.collider.gameObject.tag == "Sphere6")
                {
                    Selected = Sphere6;
                }
                else if(hit.collider.gameObject.tag == "Sphere7")
                {
                    Selected = Sphere7;
                }
                else if(hit.collider.gameObject.tag == "Sphere8")
                {
                    Selected = Sphere8;
                }
            }
            else
            {
                if(Selected==Sphere1)
                {
                    v.x = Sphere1.transform.position.x;
                    v.y = Sphere8.transform.position.y;
                    v.z = Sphere8.transform.position.z;
                    transform.position = v;
                    Sphere8.transform.position = v;

                    v.x = Sphere1.transform.position.x;
                    v.y = Sphere4.transform.position.y;
                    v.z = Sphere1.transform.position.z;
                    transform.position = v;
                    Sphere4.transform.position = v;

                    v.x = Sphere1.transform.position.x;
                    v.y = Sphere5.transform.position.y;
                    v.z = Sphere5.transform.position.z;
                    transform.position = v;
                    Sphere5.transform.position = v;

                    v.x = Sphere2.transform.position.x;
                    v.y = Sphere2.transform.position.y;
                    v.z = Sphere1.transform.position.z;
                    transform.position = v;
                    Sphere2.transform.position = v;

                    v.x = Sphere3.transform.position.x;
                    v.y = Sphere3.transform.position.y;
                    v.z = Sphere1.transform.position.z;
                    transform.position = v;
                    Sphere3.transform.position = v;
                }
                else if(Selected==Sphere2)
                {
                    v.x = Sphere1.transform.position.x;
                    v.y = Sphere1.transform.position.y;
                    v.z = Sphere2.transform.position.z;
                    transform.position = v;
                    Sphere1.transform.position = v;

                    v.x = Sphere2.transform.position.x;
                    v.y = Sphere7.transform.position.y;
                    v.z = Sphere7.transform.position.z;
                    transform.position = v;
                    Sphere7.transform.position = v;

                    v.x = Sphere2.transform.position.x;
                    v.y = Sphere3.transform.position.y;
                    v.z = Sphere2.transform.position.z;
                    transform.position = v;
                    Sphere3.transform.position = v;

                    v.x = Sphere4.transform.position.x;
                    v.y = Sphere4.transform.position.y;
                    v.z = Sphere2.transform.position.z;
                    transform.position = v;
                    Sphere4.transform.position = v;

                    v.x = Sphere2.transform.position.x;
                    v.y = Sphere6.transform.position.y;
                    v.z = Sphere6.transform.position.z;
                    transform.position = v;
                    Sphere6.transform.position = v;
                }
                else if(Selected==Sphere3)
                {
                    v.x = Sphere4.transform.position.x;
                    v.y = Sphere4.transform.position.y;
                    v.z = Sphere3.transform.position.z;
                    transform.position = v;
                    Sphere4.transform.position = v;

                    v.x = Sphere3.transform.position.x;
                    v.y = Sphere6.transform.position.y;
                    v.z = Sphere6.transform.position.z;
                    transform.position = v;
                    Sphere6.transform.position = v;

                    v.x = Sphere1.transform.position.x;
                    v.y = Sphere1.transform.position.y;
                    v.z = Sphere3.transform.position.z;
                    transform.position = v;
                    Sphere1.transform.position = v;

                    v.x = Sphere3.transform.position.x;
                    v.y = Sphere7.transform.position.y;
                    v.z = Sphere7.transform.position.z;
                    transform.position = v;
                    Sphere7.transform.position = v;

                    v.x = Sphere3.transform.position.x;
                    v.y = Sphere2.transform.position.y;
                    v.z = Sphere3.transform.position.z;
                    transform.position = v;
                    Sphere2.transform.position = v; 
                }
                else if(Selected==Sphere4)
                {
                    v.x = Sphere4.transform.position.x;
                    v.y = Sphere1.transform.position.y;
                    v.z = Sphere4.transform.position.z;
                    transform.position = v;
                    Sphere1.transform.position = v;

                    v.x = Sphere4.transform.position.x;
                    v.y = Sphere8.transform.position.y;
                    v.z = Sphere8.transform.position.z;
                    transform.position = v;
                    Sphere8.transform.position = v;

                    v.x = Sphere2.transform.position.x;
                    v.y = Sphere2.transform.position.y;
                    v.z = Sphere4.transform.position.z;
                    transform.position = v;
                    Sphere2.transform.position = v;

                    v.x = Sphere3.transform.position.x;
                    v.y = Sphere3.transform.position.y;
                    v.z = Sphere4.transform.position.z;
                    transform.position = v;
                    Sphere3.transform.position = v;

                    v.x = Sphere4.transform.position.x;
                    v.y = Sphere5.transform.position.y;
                    v.z = Sphere5.transform.position.z;
                    transform.position = v;
                    Sphere5.transform.position = v;
                }
                else if(Selected==Sphere5)
                {
                    v.x = Sphere6.transform.position.x;
                    v.y = Sphere6.transform.position.y;
                    v.z = Sphere5.transform.position.z;
                    transform.position = v;
                    Sphere6.transform.position = v;

                    v.x = Sphere5.transform.position.x;
                    v.y = Sphere4.transform.position.y;
                    v.z = Sphere4.transform.position.z;
                    transform.position = v;
                    Sphere4.transform.position = v;

                    v.x = Sphere5.transform.position.x;
                    v.y = Sphere1.transform.position.y;
                    v.z = Sphere1.transform.position.z;
                    transform.position = v;
                    Sphere1.transform.position = v;

                    v.x = Sphere5.transform.position.x;
                    v.y = Sphere8.transform.position.y;
                    v.z = Sphere5.transform.position.z;
                    transform.position = v;
                    Sphere8.transform.position = v;

                    v.x = Sphere7.transform.position.x;
                    v.y = Sphere7.transform.position.y;
                    v.z = Sphere5.transform.position.z;
                    transform.position = v;
                    Sphere7.transform.position = v;
                }
                else if(Selected==Sphere6)
                {
                    v.x = Sphere6.transform.position.x;
                    v.y = Sphere2.transform.position.y;
                    v.z = Sphere2.transform.position.z;
                    transform.position = v;
                    Sphere2.transform.position = v;

                    v.x = Sphere6.transform.position.x;
                    v.y = Sphere3.transform.position.y;
                    v.z = Sphere3.transform.position.z;
                    transform.position = v;
                    Sphere3.transform.position = v;

                    v.x = Sphere5.transform.position.x;
                    v.y = Sphere5.transform.position.y;
                    v.z = Sphere6.transform.position.z;
                    transform.position = v;
                    Sphere5.transform.position = v;

                    v.x = Sphere6.transform.position.x;
                    v.y = Sphere7.transform.position.y;
                    v.z = Sphere6.transform.position.z;
                    transform.position = v;
                    Sphere7.transform.position = v;

                    v.x = Sphere8.transform.position.x;
                    v.y = Sphere8.transform.position.y;
                    v.z = Sphere6.transform.position.z;
                    transform.position = v;
                    Sphere8.transform.position = v;
                }
                else if(Selected==Sphere7)
                {
                    v.x = Sphere8.transform.position.x;
                    v.y = Sphere8.transform.position.y;
                    v.z = Sphere7.transform.position.z;
                    transform.position = v;
                    Sphere8.transform.position = v;

                    v.x = Sphere7.transform.position.x;
                    v.y = Sphere2.transform.position.y;
                    v.z = Sphere2.transform.position.z;
                    transform.position = v;
                    Sphere2.transform.position = v;

                    v.x = Sphere7.transform.position.x;
                    v.y = Sphere3.transform.position.y;
                    v.z = Sphere3.transform.position.z;
                    transform.position = v;
                    Sphere3.transform.position = v;

                    v.x = Sphere7.transform.position.x;
                    v.y = Sphere6.transform.position.y;
                    v.z = Sphere7.transform.position.z;
                    transform.position = v;
                    Sphere6.transform.position = v;

                    v.x = Sphere5.transform.position.x;
                    v.y = Sphere5.transform.position.y;
                    v.z = Sphere7.transform.position.z;
                    transform.position = v;
                    Sphere5.transform.position = v;
                }
                else if(Selected==Sphere8)
                {
                    v.x = Sphere8.transform.position.x;
                    v.y = Sphere1.transform.position.y;
                    v.z = Sphere1.transform.position.z;
                    transform.position = v;
                    Sphere1.transform.position = v;

                    v.x = Sphere7.transform.position.x;
                    v.y = Sphere7.transform.position.y;
                    v.z = Sphere8.transform.position.z;
                    transform.position = v;
                    Sphere7.transform.position = v;

                    v.x = Sphere8.transform.position.x;
                    v.y = Sphere4.transform.position.y;
                    v.z = Sphere4.transform.position.z;
                    transform.position = v;
                    Sphere4.transform.position = v;

                    v.x = Sphere8.transform.position.x;
                    v.y = Sphere5.transform.position.y;
                    v.z = Sphere8.transform.position.z;
                    transform.position = v;
                    Sphere5.transform.position = v;

                    v.x = Sphere6.transform.position.x;
                    v.y = Sphere6.transform.position.y;
                    v.z = Sphere8.transform.position.z;
                    transform.position = v;
                    Sphere6.transform.position = v;
                }
            }

            /* v.x = Sphere1.transform.position.x;
             v.y = Sphere3.transform.position.y;
             v.z = Sphere3.transform.position.z;
             transform.position = v;
             Sphere4.transform.position = v;

             v.x = Sphere1.transform.position.x;
             v.y = Sphere3.transform.position.y;
             v.z = Sphere7.transform.position.z;
             transform.position = v;
             Sphere5.transform.position = v;

             v.x = Sphere3.transform.position.x;
             v.y = Sphere3.transform.position.y;
             v.z = Sphere7.transform.position.z;
             transform.position = v;
             Sphere6.transform.position = v;

             v.x = Sphere1.transform.position.x;
             v.y = Sphere1.transform.position.y;
             v.z = Sphere7.transform.position.z;
             Sphere8.transform.position = v;*/
        //}
        //}
}