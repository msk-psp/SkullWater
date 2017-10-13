using UnityEngine;
using System.Collections;

public class Fix : MonoBehaviour
{
    public GameObject Sphere1, Sphere2, Sphere3, Sphere4, Sphere5, Sphere6, Sphere7, Sphere8;
    private Vector3 v;

    public void Fixed()
    {
        FindSphere();

        //바닥
        v.x = Sphere1.transform.position.x;
        v.y = Sphere2.transform.position.y;
        v.z = Sphere2.transform.position.z;
        transform.position = v;
        Sphere1.transform.position = v;

        v.x = Sphere1.transform.position.x;
        v.y = Sphere7.transform.position.y;
        v.z = Sphere7.transform.position.z;
        transform.position = v;
        Sphere8.transform.position = v;

        v.x = Sphere2.transform.position.x;
        v.y = Sphere7.transform.position.y;
        v.z = Sphere7.transform.position.z;
        transform.position = v;
        Sphere7.transform.position = v;

        //천장
        v.x = Sphere2.transform.position.x;
        v.y = Sphere3.transform.position.y;
        v.z = Sphere3.transform.position.z;
        transform.position = v;
        Sphere3.transform.position = v;

        v.x = Sphere1.transform.position.x;
        v.y = Sphere3.transform.position.y;
        v.z = Sphere3.transform.position.z;
        transform.position = v;
        Sphere4.transform.position = v;

        v.x = Sphere8.transform.position.x;
        v.y = Sphere3.transform.position.y;
        v.z = Sphere8.transform.position.z;
        transform.position = v;
        Sphere5.transform.position = v;

        v.x = Sphere7.transform.position.x;
        v.y = Sphere3.transform.position.y;
        v.z = Sphere7.transform.position.z;
        transform.position = v;
        Sphere6.transform.position = v;

        /*버튼모션 제어*/
        
        ButtonMotion.State = 4;
        ButtonMotion.ChangeState = 0;
    }

    void FindSphere()
    {
        Sphere1 = GameObject.FindGameObjectWithTag("Sphere1");
        Sphere2 = GameObject.FindGameObjectWithTag("Sphere2");
        Sphere3 = GameObject.FindGameObjectWithTag("Sphere3");
        Sphere4 = GameObject.FindGameObjectWithTag("Sphere4");
        Sphere5 = GameObject.FindGameObjectWithTag("Sphere5");
        Sphere6 = GameObject.FindGameObjectWithTag("Sphere6");
        Sphere7 = GameObject.FindGameObjectWithTag("Sphere7");
        Sphere8 = GameObject.FindGameObjectWithTag("Sphere8");
    }
}