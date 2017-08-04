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


    private string Sphere1Name, Sphere2Name;
    private const string CLONE = "(Clone)";
    // Use this for initialization
    void Start()
    {    
        Sphere1Name = Sphere1.name;
        Sphere2Name = Sphere2.name;
    }

    // Update is called once per frame
    void Update () {
        /*텍스트를 가운데에 출력하기 위함*/
        Linepos.x = (Sphere1.transform.position.x + Sphere2.transform.position.x) / 2;
        Linepos.y = (Sphere1.transform.position.y + Sphere2.transform.position.y) / 2;
        Linepos.z = (Sphere1.transform.position.z + Sphere2.transform.position.z) / 2;
        transform.position = Linepos;
        dist = Vector3.Distance(Sphere1.position, Sphere2.position); // Sphere1과 Sphere2의 사이 거리계산
        dist = (int)(((dist/99)*22)-3); // 오브젝트 사이 거리를 cm로 변환
        distance = dist.ToString(); // dist를 문자열로 변환
        text.GetComponent<TextMesh>().text = distance; // text에 문자열을 출력시킨다.
        text.GetComponent<TextMesh>().characterSize = 3.3f;
        text.GetComponent<TextMesh>().fontSize = 100;
        text.transform.position = Linepos;
    }

    public void ChangeObjects(string name1, string name2)
    {
        if (this.name.Contains(CLONE))
        {
            Sphere1 = GameObject.Find(name1 + CLONE).transform;
            Sphere2 = GameObject.Find(name2 + CLONE).transform;
        }
    }
}
