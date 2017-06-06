using UnityEngine;
using System.Collections;

public class Out_check : MonoBehaviour {

    public RectTransform myCanvas;

    // Use this for initialization
    void Start () {
	
	}
	
    public void Out_Che()
    {
        myCanvas.FindChild("ContextMenu").gameObject.SetActive(false);
        myCanvas.FindChild("Outcheck").gameObject.SetActive(false);
    }
}
