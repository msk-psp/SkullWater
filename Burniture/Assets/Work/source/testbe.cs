using UnityEngine;
using UnityEditor;

public class testbe : MonoBehaviour {
    Rect position;
    GUIContent label;
    SerializedProperty property;

    // Use this for initialization
    void Start () {
        Rect htmlField = new Rect(position.x, position.y, position.width - 100, position.height);
        Rect colorField = new Rect(position.x + htmlField.width, position.y, position.width - htmlField.width, position.height);

        string htmlValue = EditorGUI.TextField(htmlField, label, "#" + ColorUtility.ToHtmlStringRGBA(property.colorValue));

        Color newCol;
        if (ColorUtility.TryParseHtmlString(htmlValue, out newCol))
            property.colorValue = newCol;
        Debug.Log("Color Test (htmlValue) : " + htmlValue);
        Debug.Log("Color Test (newCol) : " + newCol);
        property.colorValue = EditorGUI.ColorField(colorField, property.colorValue);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
