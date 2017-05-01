using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Furniture;
public class MyMenuClick : MonoBehaviour {
    public RectTransform prefab;
    public GameObject oCube;

    private string mCube_name;

    private float mCube_xScale;
    private float mCube_yScale;
    private float mCube_zScale;

    private float mCube_xPosition;
    private float mCube_yPosition;
    private float mCube_zPosition;

    
    public void CreateCube()
    {
        GameObject plane = GameObject.FindWithTag("Bottom");
        GameObject Furniture;
        mCube_name = prefab.FindChild("TitlePanel").FindChild("CubeName").GetComponent<Text>().text.ToString();
       // if(GameObject.FindWithTag("Furniture")==null){
        //}
        

        if (GameObject.Find(mCube_name)!=null)
        {
            Debug.Log("이미 생성되었습니다");
            GameObject.Find("ScrollPanel").SetActive(false);        ///이미 있는 가구 생성
            return;
        }
        else if(GameObject.FindWithTag("Furniture")!=null){
            Furniture = GameObject.FindWithTag("Furniture");        //다른 가구 생성
        }
        else {
            Furniture = Instantiate(oCube);     //처음 생성
        }
        

        mCube_xPosition = plane.transform.position.x;
        mCube_yPosition = plane.transform.position.y;
        mCube_zPosition = plane.transform.position.z;

        float.TryParse(prefab.FindChild("XText").GetComponent<Text>().text, out mCube_xScale);
        float.TryParse(prefab.FindChild("YText").GetComponent<Text>().text, out mCube_yScale);
        float.TryParse(prefab.FindChild("ZText").GetComponent<Text>().text, out mCube_zScale);

        
        Furniture.transform.localScale = new Vector3(mCube_xScale*10, mCube_yScale*10, mCube_zScale*10);
        Furniture.transform.position = new Vector3(mCube_xPosition, Furniture.transform.localScale.x / 2 + 1, mCube_zPosition);
        Furniture.SetActive(true);
        Furniture.name = mCube_name;
        Furniture.tag = "Furniture";
        GameObject.Find("ScrollPanel").SetActive(false);
    }
}
