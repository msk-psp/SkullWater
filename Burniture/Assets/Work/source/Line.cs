using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {
    private LineRenderer lineRenderer; // 라인렌더러 선언
    //private float dist;
    public Transform Sphere1;
    public Transform Sphere2;
    private string Sphere1Name,Sphere2Name;
    private const string CLONE = "(Clone)";
	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>(); // linRenderer에 LineRenderer컴포넌트를 가져옴
        Sphere1Name = Sphere1.name;
        Sphere2Name = Sphere2.name;

       // dist = Vector3.Distance(Sphere1.position, Sphere2.position);
    }

    // Update is called once per frame
    void Update()
    {
        /* 선의 시작과 끝 지점을 잡아줌 */
        lineRenderer.SetPosition(0, Sphere1.position);
        lineRenderer.SetPosition(1, Sphere2.position);
    }

    public void ChangeObjects(string name1, string name2)
    {
        if (this.name.Contains(CLONE))
        {
            Sphere1 = GameObject.Find(name1 + CLONE).transform;
            Sphere2 = GameObject.Find(name2 + CLONE).transform;
        }
    }
    public string GetNames()
    {
        return Sphere1.name +','+ Sphere2.name;
    }
}
