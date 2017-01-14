using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {
    private LineRenderer lineRenderer; // 라인렌더러 선언
    //private float dist;
    public Transform Sphere1;
    public Transform Sphere2;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>(); // lineRenderer가 라인렌더러 컴포넌트임을 알려줌
       // dist = Vector3.Distance(Sphere1.position, Sphere2.position);
    }
	
	// Update is called once per frame
	void Update () {
	    //if(counter<dist)
        {
            /* 선의 시작과 끝 지점을 잡아줌 */
            lineRenderer.SetPosition(0, Sphere1.position);  
            lineRenderer.SetPosition(1, Sphere2.position); 
        }
	}
}
