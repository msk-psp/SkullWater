using UnityEngine;
using System.Collections;
using FirebaseAccess;
using Furniture;
using UnityEngine.UI;
public class SendToDatabase : MonoBehaviour{
    const string COLORS = "COLORS";

    private GameObject BotLDSphere; //2
    private GameObject BotLUSphere; //7
    private GameObject BotRDSphere; //1
    private GameObject TopLDSphere; //3
    private GameObject oCube;
    private InputField nameField;
    private string mCube_name = null;
    
    private float mCube_xScale;
    private float mCube_yScale;
    private float mCube_zScale;

    private float mCube_xPosition;
    private float mCube_yPosition;
    private float mCube_zPosition;

    private Transaction tran;
    public GameObject prefab;
//    public RectTransform prefab;
    public RectTransform content;

    void Start()
    {
        tran = new Transaction();
        prefab.SetActive(false);
    }
    public void OpenDialog()  {
  
        BotLDSphere = GameObject.FindWithTag("Sphere2");
        BotLUSphere = GameObject.FindWithTag("Sphere7");
        BotRDSphere = GameObject.FindWithTag("Sphere1");
        TopLDSphere = GameObject.FindWithTag("Sphere3");

        mCube_xScale = Mathf.Abs(BotLDSphere.transform.position.x - BotRDSphere.transform.position.x);
        mCube_yScale = Mathf.Abs(BotLDSphere.transform.position.y - TopLDSphere.transform.position.y);
        mCube_zScale = Mathf.Abs(BotLDSphere.transform.position.z - BotLUSphere.transform.position.z);

        mCube_xPosition = BotRDSphere.transform.position.x;
        mCube_yPosition = TopLDSphere.transform.position.y;
        mCube_zPosition = BotLUSphere.transform.position.z;
        
        nameField = prefab.GetComponentInChildren(typeof(InputField)) as InputField;
        Text[] textfields = prefab.GetComponentsInChildren<Text>();
        textfields[0].text = mCube_xScale.ToString();
        textfields[1].text = mCube_zScale.ToString();
        textfields[2].text = mCube_yScale.ToString();
        prefab.SetActive(true);
    }
    public void SendCube()
    {
        string RGB = PlayerPrefs.GetString(COLORS);
        if (RGB.Length <= 0)
        {
            RGB = "0,0,0";
        }
        PlayerPrefs.DeleteKey(COLORS);
        mCube_name = nameField.text;
            Debug.Log("name :"+mCube_name);
        if (mCube_name == null && mCube_name.Length==0) { mCube_name = "MyCube" + Random.Range(1,10000); }

        tran.WriteCube(mCube_name, mCube_xScale, mCube_yScale, mCube_zScale, mCube_xPosition, mCube_yPosition, mCube_zPosition,RGB);
        
        prefab.SetActive(false);
    }

    public void Cancel()
    {
        
        prefab.SetActive(false);
    }
}
