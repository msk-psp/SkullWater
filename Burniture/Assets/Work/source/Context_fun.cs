using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mono.Data.Sqlite;
using System.Data;
using FirebaseAccess;

public class Context_fun : MonoBehaviour
{
    public RectTransform contextMenu;

    public RectTransform panel;
    public RectTransform myCanvas;

    private RectTransform prefab;
    private string mCube_name;
    private Text selectedIndex;

    private int type;
    private int index;

    private float mCube_xScale;
    private float mCube_yScale;
    private float mCube_zScale;

    private float mCube_xPosition;
    private float mCube_yPosition;
    private float mCube_zPosition;


    public GameObject[] mFurniture = new GameObject[10];

    public GameObject cube;
    public GameObject chair, table;//가구 이름
    public Vector3 vector;
    private Transform Fv;
    private int num;
    private  Color fcolor;
    public Color tcolor;
    // Use this for initialization
    void Start()
    {

    }

    public void Touched()
    {
        prefab = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
        Debug.Log("Touched : " + prefab.name);
        myCanvas.Find("Outcheck").gameObject.SetActive(true);
        myCanvas.Find("ContextMenu").gameObject.SetActive(true);

        if (prefab != null)
        {
            index = int.Parse(prefab.Find("Index").GetComponent<Text>().text.ToString());
            type = Furniture_Choose(prefab.Find("TitlePanel/Type").GetComponent<Text>().text.ToString());
            mCube_name = prefab.Find("TitlePanel").Find("CubeName").GetComponent<Text>().text.ToString();
            if (!ColorUtility.TryParseHtmlString("#" + prefab.Find("Color").GetComponent<Text>().text.ToString(), out fcolor))
                ColorUtility.TryParseHtmlString("#FFFFFF",out fcolor);
            float.TryParse(prefab.Find("TitlePanel/XText").GetComponent<Text>().text, out mCube_xScale);
            float.TryParse(prefab.Find("TitlePanel/YText").GetComponent<Text>().text, out mCube_yScale);
            float.TryParse(prefab.Find("TitlePanel/ZText").GetComponent<Text>().text, out mCube_zScale);
            float.TryParse(prefab.Find("XAxisText").GetComponent<Text>().text, out mCube_xPosition);
            float.TryParse(prefab.Find("YAxisText").GetComponent<Text>().text, out mCube_yPosition);
            float.TryParse(prefab.Find("ZAxisText").GetComponent<Text>().text, out mCube_zPosition);


        }
        else
            Debug.Log("prefab is null");
    }
    public void DeleteItem()
    {
        Debug.Log("Delete:" + index);
        if (index > 0)
        {
            this.GetComponent<MyScrollViewAdapter>().Internal_Delete_Item(index);
            index = -1;
        }
        /*else///안쓰임
        {
            this.GetComponent<MyScrollViewAdapter>().External_Delete_Item(mCube_name);
        }*/
        ContextMenuHiding();
    }
    public void DownloadItem()
    {
        Debug.Log("DownloadItem : index " + index);
        if (index <= 0)
        {
            this.GetComponent<MyScrollViewAdapter>().External_Download_Item(index);
        }
        ContextMenuHiding();
    }
    public void UploadItem()
    {
        if (index > 0)
        {
            int delay = 0;
            Transaction tran = new Transaction();
            while (true)                                                 //  서버에서 값을 받아 올 때 까지 기다림 
            {
                if (tran.isFailed || 100 <= delay++) { Debug.Log("is failed in delete while"); break; }
                else if (tran.isWaiting) { Debug.Log("is waiting in delete while"); new WaitForSeconds(0.1f); }
                else if (tran.isSuccess) { Debug.Log("is success in delete while"); break; }
            }
            tran.WriteCube(type, mCube_name, mCube_xScale, mCube_yScale, mCube_zScale, mCube_xPosition, mCube_yPosition, mCube_zScale, "0");
        }
        else
            Debug.Log("이미 업로드 되어있습니다.");

        ContextMenuHiding();

    }
    public void NewItem()
    {
        GameObject FurnitureCube, Furn;
        GameObject plane = GameObject.FindWithTag("Bottom");                // 바닥 객체를 가져옴.
        Vector3 mCube_Scale = new Vector3(mCube_xScale, mCube_yScale, mCube_zScale);

        if (plane != null)
        {
            //  Debug.Log("Button clicked!!");
            FurnitureCube = Instantiate(cube);                              // 육면체의 테투리 부분 객체 가져옴(복사)
            //if문 넣어서 데이터 베이스에 저장된 가구 객체를 불러온다.
            Furn = Instantiate(mFurniture[type]);                           // 가구의 모델 객체 가져옴(복사)
            Debug.Log("NewItem Furniture : " + type);
            
            vector.x = plane.transform.position.x;                          // vector.x에 바닥의 x포지션 저장
            vector.y = plane.transform.position.y;                          
            vector.z = plane.transform.position.z;

            Debug.Log(mCube_xScale + mCube_yScale + mCube_zScale);
            FurnitureCube.transform.localScale = mCube_Scale; // 데이터베이스 꺼낸온 스케일을 저장함
            FurnitureCube.name = Furn.name + num++;                         // 각 객체를 구분짓기위해 번호를 부여하여 구분
            //num++;
            FurnitureCube.transform.position = new Vector3(vector.x, FurnitureCube.transform.localScale.y / 2 + 1, vector.z); // (!)바닥 위에 생성
            FurnitureCube.SetActive(true);                                  // 객체 활성화
            Fv = FurnitureCube.transform;
            Furn.transform.localScale = new Vector3(
                Furn.transform.localScale.x * ChoiceMax(Fv.localScale.x, Fv.localScale.y, Fv.localScale.z),
                Furn.transform.localScale.y * ChoiceMax(Fv.localScale.x, Fv.localScale.y, Fv.localScale.z),
                Furn.transform.localScale.z * ChoiceMax(Fv.localScale.x, Fv.localScale.y, Fv.localScale.z));             // 가구 크기를 큐브의 테두리 크기로 맞춤
           /* if (Furn.gameObject.tag == "Chair") // 의자 
                Furn.transform.position = new Vector3(Fv.position.x, Fv.position.y - 20, Fv.position.z - (Furn.transform.localScale.z / 2.6f));
            else if (Furn.gameObject.tag == "Air") // 에어컨
                Furn.transform.position = new Vector3(Fv.position.x, Fv.position.y, Fv.position.z - 1.597f);
            else if (Furn.gameObject.tag == "Bed") // 침대
                Furn.transform.position = new Vector3(Fv.position.x, Fv.position.y - 51.39358f, Fv.position.z - 0.00020029f);
            else if (Furn.gameObject.tag == "Book") // 책장 
                Furn.transform.position = new Vector3(Fv.position.x, Fv.position.y - 51.395f, Fv.position.z + 5.640381e-09f);
            else if (Furn.gameObject.tag == "Chest") // 서랍장
                Furn.transform.position = new Vector3(Fv.position.x, 0.47f, Fv.position.z);
            else if (Furn.gameObject.tag == "Closet") // 옷장
                Furn.transform.position = new Vector3(Fv.position.x, Fv.position.y + 0.458f, Fv.position.z - 0.0013838f);
            else if (Furn.gameObject.tag == "Desk") // 책상
                Furn.transform.position = new Vector3(Fv.position.x + 5, Fv.position.y - Furn.transform.localScale.y + 13, Fv.position.z - 1);
            else if (Furn.gameObject.tag == "Dressing") // 화장대
                Furn.transform.position = new Vector3(Fv.position.x, Fv.position.y + Furn.transform.localScale.y + 4, Fv.position.z + Furn.transform.localScale.z / 1.7f);
            else if (Furn.gameObject.tag == "Table") // 식탁
                Furn.transform.position = new Vector3(Fv.position.x, Fv.position.y + (Furn.transform.localScale.y / 2) + 5, Fv.position.z);
                */
            //Furn.GetComponent<Renderer>().material.color = new Color(1, 0.92f, 0.016f, 1);//rgb값 넣어서 색변환
           // Furn.GetComponent<Renderer>().material.color = fcolor;
            Debug.Log("Object Color : " + fcolor);
            Furn.SetActive(true);
            Furn.transform.parent = Fv; // 가구의 부모 객체를 큐브로

            myCanvas.Find("Outcheck").gameObject.SetActive(false);
            myCanvas.Find("ContextMenu").gameObject.SetActive(false);
            panel.gameObject.SetActive(false);
            myCanvas.Find("Outcheck").gameObject.SetActive(false);
        }
        else
            Debug.Log("바닥이 생성이 되어있지 않습니다.");
    }
    private void ContextMenuHiding()
    {
        myCanvas.Find("Outcheck").gameObject.SetActive(false);
        myCanvas.Find("ContextMenu").gameObject.SetActive(false);
    }
    private int Furniture_Choose(string furnitureName)
    {
        switch (furnitureName)
        {
            case "에어컨":
                return 0;
            case "침대":
                return 1;
            case "책장":
                return 2;
            case "옷장":
                return 3;
            case "화장대":
                return 4;
            case "선풍기":
                return 5;
            case "냉장고":
                return 6;
            case "쇼파":
                return 7;
            case "선반":
                return 8;
            case "컴퓨터":
                return 9;
            default:
                return 0;
        }
    }
    public float ChoiceMax(float x, float y, float z)
    {
        float[] a = new float[3];
        a[0] = x;
        a[1] = y;
        a[2] = z;
        return Mathf.Max(a);
    }
}