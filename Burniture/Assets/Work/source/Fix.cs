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
    public void Fixed()
    {
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
    }
}