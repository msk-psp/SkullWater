using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour
{
    public Transform Sphere1;
    public Transform Sphere2;
    public Transform ThisLine;
    public LineRenderer LineRend;
    private const string CLONE = "(Clone)";


    // Use this for initialization
    void Start()
    {
        ThisLine = this.gameObject.transform;
        LineRend = ThisLine.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /* 선의 시작과 끝 지점을 잡아줌 */
        LineRend.SetPosition(0, Sphere1.position);
        LineRend.SetPosition(1, Sphere2.position);
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
        return Sphere1.name + ',' + Sphere2.name;
    }
}
