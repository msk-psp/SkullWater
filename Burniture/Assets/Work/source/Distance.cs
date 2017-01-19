using UnityEngine;
using System.Collections;

public class Distance : MonoBehaviour {
    public GameObject text;
    public int dist;
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
        Linepos.y = Sphere1.transform.position.y;
        Linepos.z = Sphere1.transform.position.z;
        transform.position = Linepos;
        dist = (int)Vector3.Distance(Sphere1.position, Sphere2.position); // Sphere1과 Sphere2의 사이 거리계산
        text = GameObject.FindWithTag("distance");  // ??(text가 어떤 게임오브젝트인지 찾는것 같음)
        distance = dist.ToString(); // folat인 dist를 문자열로 변환
        text.GetComponent<TextMesh>().text = distance; // text에 문자열을 출력시킨다.
        text.transform.position = Linepos;
    }
}
