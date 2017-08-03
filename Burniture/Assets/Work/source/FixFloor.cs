using UnityEngine;
using Vuforia;

public class FixFloor : MonoBehaviour
{

    private GameObject[] Spheres;
    private GameObject[] Lines;
    private const string SPHERE_TAG_NAME = "Sphere";
    private const string LINE_TAG_NAME = "Lines";
    private const int SPHERE_NUMBER = 8;
    private const string IMAGETARGET_NAME = "ImageTarget";
    private const string ARCAMERA_NAME = "ARCamera";
    private GameObject NParent; // ㅇㅇ
    //VuforiaBehaviour qcarBehaviour;
    private GameObject obj;


    public void fixfloor()
    {
        NParent = GameObject.Find("Copy"); // ㅇㅇ

        Spheres = new GameObject[SPHERE_NUMBER];
        Lines = new GameObject[GameObject.FindGameObjectsWithTag(LINE_TAG_NAME).Length];
        Lines = GameObject.FindGameObjectsWithTag(LINE_TAG_NAME);

        NParent.transform.position = GameObject.Find(IMAGETARGET_NAME).transform.position ; // ㅇㅇ

        for (int index = 0; index < SPHERE_NUMBER; index++)
        {
            Spheres[index] = GameObject.FindGameObjectWithTag(SPHERE_TAG_NAME + (index + 1));
            //obj = Instantiate(Spheres[i]);
            obj = Spheres[index];
            //obj.transform.position = Spheres[i].transform.position;
            obj.transform.parent = NParent.transform; // 부모바꾸기
            //Spheres[i].SetActive(false);
        }
        string[] name = new string[Lines.Length]; ;
        for (int index = 0; index < Lines.Length; index++)
        {
            //obj = Instantiate(Lines[i]);
            //obj.transform.position = Lines[i].transform.position;
            obj = Lines[index];
            name[index] = Lines[index].GetComponent<Line>().GetNames();
            obj.transform.parent = NParent.transform; // 부모바꾸기
            //Lines[i].SetActive(false);
        }
        GameObject ground = GameObject.Find("Ground");
        //obj = Instantiate(ground);
        //obj.transform.position = ground.transform.position;
        obj = ground;
        obj.transform.parent = NParent.transform; // 부모바꾸기
        //ground.SetActive(false);

        //GameObject.Find(IMAGETARGET_NAME).GetComponent<DefaultTrackableEventHandler>().ChangeObjects();
        //GameObject.Find(ARCAMERA_NAME).GetComponent<Drag>().ChangeObjects();
        Lines = GameObject.FindGameObjectsWithTag(LINE_TAG_NAME);   //find line without deactivated gameobject

        for (int index = 0; index < Lines.Length; index++)
        {
            string[] splitNames = name[index].Split(',');
            Lines[index].GetComponent<Line>().ChangeObjects(splitNames[0], splitNames[1]);
            if (Lines[index].GetComponent<Distance>())
            {
                Lines[index].GetComponent<Distance>().ChangeObjects(splitNames[0], splitNames[1]);
            }
        }

        //TrackerManager.Instance.GetTracker<ObjectTracker>().PersistExtendedTracking(true);
        //VuforiaBehaviour.Instance.enabled = false;
        TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
        /* qcarBehaviour = (VuforiaBehaviour)UnityEngine.Object.FindObjectOfType(
                 typeof(VuforiaBehaviour));
         qcarBehaviour.enabled = false;*/
        GameObject.Find(ARCAMERA_NAME).GetComponent<Gyro>().enabled = true;
        //GameObject.Find(ARCAMERA_NAME).GetComponent<KeepScale>().enabled = true;

        /*버튼모션 제어*/
        ButtonMotion.State = 3;
        ButtonMotion.ChangeState = 0;
    }
}
