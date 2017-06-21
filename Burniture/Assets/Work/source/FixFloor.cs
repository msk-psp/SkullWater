using UnityEngine;
using Vuforia;

public class FixFloor : MonoBehaviour
{

    private GameObject[] Spheres;
    private GameObject[] Lines;
    private const string SPHERE_TAG_NAME = "Sphere";
    private const string LINE_TAG_NAME = "Lines";
    private const int SPHERE_NUMBER = 8;
    private GameObject Ground;

    public void FixedFloor()
    {
        GameObject obj;
        Spheres = new GameObject[SPHERE_NUMBER];
        Lines = new GameObject[GameObject.FindGameObjectsWithTag(LINE_TAG_NAME).Length];
        Lines = GameObject.FindGameObjectsWithTag(LINE_TAG_NAME);

        for (int i = 0; i < SPHERE_NUMBER; i++)
        {
            Spheres[i] = GameObject.FindGameObjectWithTag(SPHERE_TAG_NAME + (i + 1));
            obj = Instantiate(Spheres[i]);
            obj.transform.position = Spheres[i].transform.position;

        }

        for (int i = 0; i < Lines.Length; i++)
        {
            obj = Instantiate(Lines[i]);
            obj.transform.position = Lines[i].transform.position;
        }

        //Ground = GameObject.FindGameObjectWithTag("Grond");
        //obj=Instantiate(Ground);
        //obj.transform.position = Ground.transform.position;
        GameObject.Find("ARCamera").GetComponent<Drag>().SetNewObject(Spheres);
        TrackerManager.Instance.GetTracker<ObjectTracker>().PersistExtendedTracking(true);
    }
}