using UnityEngine;
using System.Collections;

public class TemporaryGenerate : MonoBehaviour {

    public GameObject TempFurn;
    GameObject Furn;
    private Vector3 vector;
    private GameObject plane;
    private int i;

	public void Generate()
    {
        plane = GameObject.FindWithTag("Bottom");

        Furn = Instantiate(TempFurn);                           // 가구의 모델 객체 가져옴(복사)

        Furn.name=Furn.name+i;
        i++;

        vector.x = plane.transform.position.x;                          // vector.x에 바닥의 x포지션 저장
        vector.y = plane.transform.position.y;
        vector.z = plane.transform.position.z;

        Furn.transform.position = new Vector3(vector.x, Furn.transform.localScale.y / 2, vector.z); // (!)바닥 위에 생성
        Furn.SetActive(true);                                  // 객체 활성화
        Furn.transform.localScale *= 100;
    }
    public float ChoiceMax(float x, float y, float z)
    {
        float[] a = new float[3];
        a[0] = x;
        a[1] = y;
        a[2] = z;
        return Mathf.Max(a);
    }
}
