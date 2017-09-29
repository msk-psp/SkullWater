using UnityEngine;
using System.Collections;

public class Out_check : MonoBehaviour {

    public RectTransform myCanvas;

    // Use this for initialization
    void Start () {
	
	}
	
    public void Out_Che()
    {
        myCanvas.Find("ContextMenu").gameObject.SetActive(false);
        myCanvas.Find("Outcheck").gameObject.SetActive(false);
    }
}
