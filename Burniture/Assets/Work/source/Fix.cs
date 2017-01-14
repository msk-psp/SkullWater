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
    Vector3 v1,v2,v3,v4;
    /*void start()
    {
        Sphere1 = GameObject.Find("Sphere1");
        Sphere2 = GameObject.Find("Sphere2");
        Sphere3 = GameObject.Find("Sphere3");
        Sphere4 = GameObject.Find("Sphere4");
        Sphere5 = GameObject.Find("Sphere5");
        Sphere6 = GameObject.Find("Sphere6");
        Sphere7 = GameObject.Find("Sphere7");
        Sphere8 = GameObject.Find("Sphere8");
    }*/
    public void asd()
    {
        Debug.Log("ad");
    }
    public void onClick()
    {
        Debug.Log("Akkkkkkkkkkkkkkkkk");
        //Shere1,2,3,7은 안해도됨
        v1 = transform.position;
        v1.x = Sphere1.transform.position.x;
        v1.y = Sphere3.transform.position.y;
        v1.z = Sphere1.transform.position.z;
        transform.position = v1;
        Sphere4.transform.position = v1;
        v2 = transform.position;
        v2.x = Sphere1.transform.position.x;
        v2.y = Sphere3.transform.position.y;
        v2.z = Sphere7.transform.position.z;
        transform.position = v2;
        Sphere5.transform.position = v2;
        v3 = transform.position;
        v3.x = Sphere3.transform.position.x;
        v3.y = Sphere3.transform.position.y;
        v3.z = Sphere7.transform.position.z;
        Sphere6.transform.position = v3;
        v4 = transform.position;
        v4.x = Sphere1.transform.position.x;
        v4.y = Sphere1.transform.position.y;
        v4.z = Sphere7.transform.position.z;
        Sphere8.transform.position = v4;
        Debug.Log("Akkkkkkkkkkkkkkkkk");
    }
}
