using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FurnitureGenerate : MonoBehaviour {

    public GameObject cube;
    public Vector3 v;

    public void Generate()
    {
        GameObject Furniture;
        GameObject plane = GameObject.FindWithTag("Bottom");

        Furniture = Instantiate(cube);

        v.x = plane.transform.position.x;
        v.y = plane.transform.position.y;
        v.z = plane.transform.position.z;
        transform.position = v;
        Furniture.transform.localScale = new Vector3(50, 50, 50);
        Furniture.transform.position = new Vector3(v.x, Furniture.transform.localScale.x / 2 + 1, v.z);
        Furniture.SetActive(true);
    }
}
