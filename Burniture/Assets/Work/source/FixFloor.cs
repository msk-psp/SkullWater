using UnityEngine;
using Vuforia;

public class FixFloor : MonoBehaviour
{

    //private GameObject[] Spheres; // 구들을 저장할 객체의 배열
    private GameObject[] Lines; // 라인들을 저장할 객체의 배열
    private const string SPHERE_TAG_NAME = "Sphere";
    private const string LINE_TAG_NAME = "Lines";
    private const int SPHERE_NUMBER = 8; // 구의 갯수
    private const string IMAGETARGET_NAME = "ImageTarget";
    private const string ARCAMERA_NAME = "ARCamera";
    private GameObject NParent; // 이미지 타겟 밑 자식들의 새로운 부모가 될 비어있는 객체
    private GameObject obj; // 이미지 타겟 밑 자식들이 저장될 비어있는 객체


    public void fixfloor()
    {
        NParent = GameObject.Find("Copy"); // 새로운 부모를 이름으로 찾아서 초기화

        /* 배열의 갯수 초기화 */
        //Spheres = new GameObject[SPHERE_NUMBER];
        Lines = new GameObject[GameObject.FindGameObjectsWithTag(LINE_TAG_NAME).Length];

        Lines = GameObject.FindGameObjectsWithTag(LINE_TAG_NAME); // 태그로 객체를 찾아 Lines 배열에 넣어줌

        NParent.transform.position = GameObject.Find(IMAGETARGET_NAME).transform.position; // 새로운 부모의 좌표를 이미지 타겟의 좌표로 옮겨줌
        for (int index = 0; index < SPHERE_NUMBER; index++)
        {
            //Spheres[index] = GameObject.FindGameObjectWithTag(SPHERE_TAG_NAME + (index + 1));
            //obj = Spheres[index];
            obj = GameObject.FindGameObjectWithTag(SPHERE_TAG_NAME + (index + 1));
            obj.transform.parent = NParent.transform; // 부모바꾸기
        }
        string[] name = new string[Lines.Length]; ;
        for (int index = 0; index < Lines.Length; index++)
        {
            obj = Lines[index];
            //name[index] = Lines[index].GetComponent<Line>().GetNames();
            obj.transform.parent = NParent.transform; // 부모바꾸기
        }
        GameObject ground = GameObject.Find("Ground");

        obj = ground;
        obj.transform.parent = NParent.transform; // 부모바꾸기

        Lines = GameObject.FindGameObjectsWithTag(LINE_TAG_NAME);   //find line without deactivated gameobject

        /*for (int index = 0; index < Lines.Length; index++)
        {
            string[] splitNames = name[index].Split(',');
            Lines[index].GetComponent<Line>().ChangeObjects(splitNames[0], splitNames[1]);
            if (Lines[index].GetComponent<Distance>())
            {
                Lines[index].GetComponent<Distance>().ChangeObjects(splitNames[0], splitNames[1]);
            }
        }*/

        TrackerManager.Instance.GetTracker<ObjectTracker>().Stop(); // 객체가 마커를 트래킹하는 기능을 정지 시켜 화면에 고정
        GameObject.Find(ARCAMERA_NAME).GetComponent<Gyro>().enabled = true; // 자이로 센서 스크립트를 작동시킴

        /*버튼모션 제어*/
        ButtonMotion.State = 3;
        ButtonMotion.ChangeState = 0;
    }
}