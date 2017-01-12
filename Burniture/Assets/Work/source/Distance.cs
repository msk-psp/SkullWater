using UnityEngine;
using System.Collections;

public class Distance : MonoBehaviour {
    public GameObject text;
    public float dist;
    public string distance;
    public Transform Sphere1;
    public Transform Sphere2;
    public Transform Line;

    // Use this for initialization


    // Update is called once per frame
    void Update () {
        dist = Vector3.Distance(Sphere1.position, Sphere2.position); // Sphere1과 Sphere2의 사이 거리계산
        text = GameObject.Find("distance") as GameObject;  // ??(text가 어떤 게임오브젝트인지 찾는것 같음)
        distance = dist.ToString(); // folat인 dist를 문자열로 변환
        text.GetComponent<TextMesh>().text = distance; // text에 문자열을 출력시킨다.
        transform.position = new Vector3(Line.position.x, Line.position.y, Line.position.z); // text를 선의 위치에 따라 위치를 변환 시키려고 했지만 Fail...
    }
}
