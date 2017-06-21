using UnityEngine;
using System.Collections;
using Vuforia;
public class FixFloor : MonoBehaviour  {
    
    private GameObject[] Spheres;
    private GameObject[] Lines;
    private const string SPHERE_TAG_NAME = "Sphere";
    private const string LINE_TAG_NAME = "Lines";
    private const int SPHERE_NUMBER = 8;
    private const string IMAGETARGET_NAME = "ImageTarget";
    private const string ARCAMERA_NAME = "ARCamera";
	public void fixfloor()
    {
        GameObject obj;
        
        Spheres = new GameObject[SPHERE_NUMBER];
        Lines = new GameObject[GameObject.FindGameObjectsWithTag(LINE_TAG_NAME).Length];
        Lines = GameObject.FindGameObjectsWithTag(LINE_TAG_NAME);
        

        for (int i = 0; i < SPHERE_NUMBER; i++)
        {
            Spheres[i] = GameObject.FindGameObjectWithTag(SPHERE_TAG_NAME + (i+1));
            obj = Instantiate(Spheres[i]);
            obj.transform.position = Spheres[i].transform.position;
            Spheres[i].SetActive(false);
        }
        string[] name = new string[Lines.Length]; ;
        for (int i = 0; i < Lines.Length; i++)
        {
            obj = Instantiate(Lines[i]);
            obj.transform.position = Lines[i].transform.position;
            name[i] = Lines[i].GetComponent<Line>().GetNames();
            Lines[i].SetActive(false);
        }

        GameObject.Find(IMAGETARGET_NAME).GetComponent<DefaultTrackableEventHandler>().ChangeObjects();
        GameObject.Find(ARCAMERA_NAME).GetComponent<Drag>().ChangeObjects();
        Lines = GameObject.FindGameObjectsWithTag(LINE_TAG_NAME);   //find line without deactivated gameobject
        
        for (int i = 0; i < Lines.Length; i++)
        {
            string[]  splitNames = name[i].Split(',');
            Lines[i].GetComponent<Line>().ChangeObjects(splitNames[0],splitNames[1]);
            if (Lines[i].GetComponent<Distance>())
            {
                Lines[i].GetComponent<Distance>().ChangeObjects(splitNames[0], splitNames[1]);
            }
        }
            TrackerManager.Instance.GetTracker<ObjectTracker>().PersistExtendedTracking(true);
        //TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
        
    }
}
