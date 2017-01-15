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
    public Vector3 v;

    void Update()
    {
        v = transform.position;

        //윗면 Sphere3을 기준으로
        //윗면 Sphere4
        v.y = Sphere3.transform.position.y;
        v.z = Sphere3.transform.position.z;
        transform.position = v;
        Sphere4.transform.position = v;

        //윗면 Sphere5
        v.x = Sphere4.transform.position.x;
        v.y = Sphere3.transform.position.y;
        transform.position = v;
        Sphere5.transform.position = v;

        //윗면 Sphere6
        v.x = Sphere3.transform.position.x;
        v.y = Sphere3.transform.position.y;
        transform.position = v;
        Sphere6.transform.position = v;

        //아랫면 Sphere2를 기준으로
        //아랫면 Sphere1
        v.y = Sphere2.transform.position.y;
        v.z = Sphere2.transform.position.z;
        transform.position = v;
        Sphere1.transform.position = v;

        //아랫면 Sphere7
        v.x = Sphere2.transform.position.x;
        v.y = Sphere2.transform.position.y;
        transform.position = v;
        Sphere7.transform.position = v;

        //아랫면 Sphere8
        v.x = Sphere1.transform.position.x;
        v.y = Sphere2.transform.position.y;
        transform.position = v;
        Sphere8.transform.position = v;
        /*v.x = Sphere1.transform.position.x;
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
    }
}