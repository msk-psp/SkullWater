using UnityEngine;
using System.Collections;

public class Distance : MonoBehaviour {
    public GameObject text;
    public float dist;
    public string distance;
    public Transform Sphere1;
    public Transform Sphere2;
    public Transform Line;
    public Vector3 Linepos;

    // Use this for initialization


    // Update is called once per frame
    void Update () {
        Linepos = transform.position;
        Linepos.x = (Sphere1.transform.position.x + Sphere2.transform.position.x) / 2;
        Linepos.y = (Sphere1.transform.position.y + Sphere2.transform.position.y) / 2;
        Linepos.z = (Sphere1.transform.position.z + Sphere2.transform.position.z) / 2;
        transform.position = Linepos;
        dist = Vector3.Distance(Sphere1.position, Sphere2.position); // Sphere1과 Sphere2의 사이 거리계산
        dist = (int)((dist/99)*22);
        distance = dist.ToString(); // dist를 문자열로 변환
        text.GetComponent<TextMesh>().text = distance; // text에 문자열을 출력시킨다.
        text.GetComponent<TextMesh>().characterSize = 20;
        text.transform.position = Linepos;
    }
}
