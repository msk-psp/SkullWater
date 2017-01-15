using UnityEngine;
using System.Collections;

public class Fix : MonoBehaviour {
    public GameObject Sphere1;
    public GameObject Sphere2;
    public GameObject Sphere3;
    public GameObject Sphere4;
    public GameObject Sphere5;
    public GameObject Sphere6;
    public GameObject Sphere7;
    public GameObject Sphere8;
    Vector3 v;

    public void FixCube()
    {
        //Shere1,2,3,7은 안해도됨
        v = transform.position;

        v.x = Sphere1.transform.position.x;
        v.y = Sphere3.transform.position.y;
        v.z = Sphere3.transform.position.z;
        transform.position = v;
        Sphere4.transform.position = v;

        //v2 = transform.position;
        v.x = Sphere1.transform.position.x;
        v.y = Sphere3.transform.position.y;
        v.z = Sphere7.transform.position.z;
        transform.position = v;
        Sphere5.transform.position = v;

        //v3 = transform.position;
        v.x = Sphere3.transform.position.x;
        v.y = Sphere3.transform.position.y;
        v.z = Sphere7.transform.position.z;
        transform.position = v;
        Sphere6.transform.position = v;

        //v4 = transform.position;
        v.x = Sphere1.transform.position.x;
        v.y = Sphere1.transform.position.y;
        v.z = Sphere7.transform.position.z;
        Sphere8.transform.position = v;
    }
}
