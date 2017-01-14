using UnityEngine;
using System.Collections;
using FirebaseAccess;
using Furniture;
public class SendToDatabase : MonoBehaviour {
    public GameObject target;          //Clicked Object
    private Camera _mainCam = null; //Main Camera
    private FirebaseAccess.Transaction mTran;   //Database Transaction Class
    private Cube cube;
    private bool _mouseState;
    Vector3 v;
	// Use this for initialization
	void Start () {
        cube = new Cube();
        mTran = new FirebaseAccess.Transaction();
        _mainCam = Camera.main;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo;
           // target = GetClickedObject(out hitInfo);
            v = target.transform.localScale;
            
            mTran.writeCube(target.name, v.x, v.y, v.z);
            Debug.Log("cube name: " + target.name + "x : " + v.x + " y : " + v.y + "z : " + v.z);
        } 
	}

    GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }

        return target;
    } 
}
