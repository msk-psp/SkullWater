using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FurnitureGenerate : MonoBehaviour {

    public GameObject cube;
    public GameObject chair,table;//가구 이름
    public Vector3 v;
    private Transform Fv;
    private int num;


    public void Generate()
    {
        GameObject FurnitureCube, Furn;
        GameObject plane = GameObject.FindWithTag("Bottom");

        FurnitureCube = Instantiate(cube);
        //if문 넣어서 데이터 베이스에 저장된 가구 객체를 불러온다.
        Furn = Instantiate(chair);
        
        v.x = plane.transform.position.x;
        v.y = plane.transform.position.y;
        v.z = plane.transform.position.z;
        transform.position = v;
        FurnitureCube.transform.localScale = new Vector3(50, 50, 50); // 측정한 가구의크기
        FurnitureCube.name = Furn.name + num; //컨트롤 하기위하여 이름을 모두 다르게 지정
        num++;
        FurnitureCube.transform.position = new Vector3(v.x, FurnitureCube.transform.localScale.x / 2 + 1, v.z); // 바닥 위에 생성
        FurnitureCube.SetActive(true);
        Fv = FurnitureCube.transform;
        Furn.transform.localScale = new Vector3((float)0.6 * Fv.localScale.x, (float)0.6 * Fv.localScale.y, (float)0.5 * Fv.localScale.z);
        Furn.transform.position = new Vector3(Fv.position.x, Fv.position.y, -Fv.position.z);
        Furn.SetActive(true);
        Furn.transform.parent = Fv; // 가구의 부모 객체를 큐브로
    }
}
