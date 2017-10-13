using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour
{
    public Transform Sphere1;
    public Transform Sphere2;
    public Transform ThisLine;
    public LineRenderer LineRend;
    private const string CLONE = "(Clone)";
    private Material LineMat;
    private Vector2 Tiling;
    private float Dist,FirstDis;
    private int state;
    //public float Spare = 0;


    // Use this for initialization
    void Start()
    {
        ThisLine = this.gameObject.transform;
        LineRend = ThisLine.GetComponent<LineRenderer>();
        LineMat = ThisLine.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
        /* 선의 시작과 끝 지점을 잡아줌 */
        if (ThisLine.name.Contains("Line1-2") || ThisLine.name.Contains("Line5-6")
            || ThisLine.name.Contains("Line3-4") || ThisLine.name.Contains("Line7-8"))
        {
            LineRend.SetPosition(0, Sphere1.position + new Vector3(0, 0, 0));
            LineRend.SetPosition(1, Sphere2.position - new Vector3(0, 0, 0));
        } // 23 14 67 58
        else if (ThisLine.name.Contains("Line2-3") || ThisLine.name.Contains("Line1-4")
            || ThisLine.name.Contains("Line6-7") || ThisLine.name.Contains("Line5-8"))
        {
            LineRend.SetPosition(0, Sphere1.position + new Vector3(0, 0, 0));
            LineRend.SetPosition(1, Sphere2.position - new Vector3(0, 0, 0));
        }
        else
        {
            LineRend.SetPosition(0, Sphere1.position + new Vector3(0, 0, 0));
            LineRend.SetPosition(1, Sphere2.position - new Vector3(0, 0, 0));
        }
        Dist= Vector3.Distance(Sphere1.position, Sphere2.position);
        if (state == 0)
        {
            FirstDis = Dist;
            state = 1;
        }
        Tiling = new Vector2(Dist * (2 / FirstDis), 0);
        LineMat.mainTextureScale = Tiling;
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