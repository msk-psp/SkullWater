<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using FirebaseAccess;
using Furniture;

public class MyScrollViewAdapter : MonoBehaviour {
    public Texture2D[] availableIcons;                  // 아이콘 이미지들
    public RectTransform prefab;                        // 아이템 prefab
    //public Text countText;                              // 개수 텍스트 여기에 큐브 개수 넣으면 됨.
    public ScrollRect scrollView;                       // 스크롤 뷰 객체
    public RectTransform content;                       // content 객체
    public string userID;                               //user ID;
    private Transaction tran;                           //파이어 베이스에서 데이터 가져올 때 사용할 거임.
       
    List<CubeItemView> views = new List<CubeItemView>();       // Content에 넣을 아이템들 
	// Use this for initialization
	void Start () {
                                       // 파이어베이스 접속 객체 생성
	}
	public void UpdateItems()                                       //아이템 업데이트
    {
        tran = new Transaction();

        tran.RetrieveCubesByUserid(userID);
      //  int newCount;                                                   //아이템 개수
      //  int.TryParse(countText.text, out newCount);                     // Input field 에서 Text 의 값을 int로 parse 하는 기능
        StartCoroutine(FetchItemModelsFromServer(results => OnReceivedNewModels(results)));       // newCount 개수 만큼, results 에 반환 된 cube Item들을 가지고


    }                                                                                                   //OnreceivedNewModels에 넘겨줘 각 게임 오브젝트를 생성하고 view List에 
                                                                                                        //
	// Update is called once per frame
    void OnReceivedNewModels(CubeItem[] models)
    {
        foreach (Transform child in content.transform)                              // content 에 있는 아이템들 다 지움
        {
            Destroy(child.gameObject);
        }

        views.Clear();                                                              // views clear

        int i = 0;
        foreach (var model in models)                                               // Fetch 에서 받아온 걸로 views에 추가함 
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;     //
            instance.transform.SetParent(content, false);                                   // parent가 content가 되므로 자동 추가 됨
            var view = InitializeItemView(instance, model);                         
            views.Add(view);
            ++i;
        }
    }
    CubeItemView InitializeItemView(GameObject viewGameObject, CubeItem model)          // CubeItem View를 초기화하여 넘겨줌,
    {                                                                                       
        CubeItemView view = new CubeItemView(viewGameObject.transform);

        view.titleText.text = model.name;
       // view.iconImage.texture = availableIcons[model.iconIndex];                           //각 index들은 fetch에서 랜덤으로 받아옴'
        view.x.text = model.x.ToString();
        view.y.text = model.y.ToString();
        view.z.text = model.z.ToString();

        view.xAxis.text = model.xAxis.ToString();
        view.yAxis.text = model.yAxis.ToString();
        view.zAxis.text = model.zAxis.ToString();

        //view.icon2Image.texture = availableIcons[model.icon2Index];
        //view.icon3Image.texture = availableIcons[model.icon3Index];

        return view;
    }
    IEnumerator FetchItemModelsFromServer( Action<CubeItem[]> onDone)
    {
        int count=0;
        int delay = 0;

        Debug.Log("FetchItemModels " );
        while(true)                                                 //  서버에서 값을 받아 올 때 까지 기다림 
        {
            if (tran.isFailed || 20<=delay++) { Debug.Log("is failed in FetchItem while"); count = 0; break; }
            else if (tran.isWaiting) { Debug.Log("is waiting in FetchItem while"); yield return new WaitForSeconds(0.5f); }
            else if (tran.isSuccess) { count = tran.cubes.Count;  Debug.Log("is success in FetchItem while"); break; }   
        }

        Debug.Log("after FetchItem while");
        //foreach (Cube cubes in tran.cubes){                             
        //        Debug.Log("foreach " + JsonUtility.ToJson(cubes));
        //}


        count = tran.cubes.Count;

        var results = new CubeItem[count];
       
        for (int i = 0; i < count; ++i)                                 // count 만큼 results[i] 에 값을 넣어줌 그걸 리턴 함
        {                                                                  //여기서 큐브의 정보를 넣어주면 됨 
            results[i] = new CubeItem();                                        // X,Y,Z , 위치좌표, 큐브이름 
            results[i].name = tran.cubes[i].name;
            //results[i].iconIndex = UnityEngine.Random.Range(0, availableIcons.Length);
            results[i].x = tran.cubes[i].x.ToString();
            results[i].y = tran.cubes[i].y.ToString();
            results[i].z = tran.cubes[i].z.ToString();
            results[i].xAxis = tran.cubes[i].xAxis.ToString();
            results[i].yAxis = tran.cubes[i].yAxis.ToString();
            results[i].zAxis = tran.cubes[i].zAxis.ToString();

            //results[i].icon2Index = UnityEngine.Random.Range(0, availableIcons.Length);
            //results[i].icon3Index = UnityEngine.Random.Range(0, availableIcons.Length);
        }
        onDone(results);
    }
    public class CubeItemView
    {
        public Text titleText;
        public Text x, y, z;
        public Text xAxis, yAxis, zAxis;
      //  public RawImage iconImage;//, icon2Image,icon3Image;
     
        public CubeItemView(Transform rootView)
        {
            titleText = rootView.Find("TitlePanel/CubeName").GetComponent<Text>();
            //iconImage = rootView.Find("IconImage").GetComponent<RawImage>();
            x = rootView.Find("XText").GetComponent<Text>();
            y = rootView.Find("YText").GetComponent<Text>();
            z = rootView.Find("ZText").GetComponent<Text>();
            xAxis = rootView.Find("XAxisText").GetComponent<Text>();
            yAxis = rootView.Find("YAxisText").GetComponent<Text>();
            zAxis = rootView.Find("ZAxisText").GetComponent<Text>();
          //  icon2Image = rootView.Find("Icon2Image").GetComponent<RawImage>();
           // icon3Image = rootView.Find("TitlePanel/Icon3Image").GetComponent<RawImage>();
        }

    }
    public class CubeItem
    {
        public string name;
        public string x,y,z;
        public string xAxis, yAxis, zAxis;
       // public int iconIndex;//, icon2Index, icon3Index;
    }
}
=======
﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using FirebaseAccess;
using Furniture;

public class MyScrollViewAdapter : MonoBehaviour {
    public Texture2D[] availableIcons;                  // 아이콘 이미지들
    public RectTransform prefab;                        // 아이템 prefab
    //public Text countText;                              // 개수 텍스트 여기에 큐브 개수 넣으면 됨.
    public ScrollRect scrollView;                       // 스크롤 뷰 객체
    public RectTransform content;                       // content 객체
    public string userID;                               //user ID;
    private Transaction tran;                           //파이어 베이스에서 데이터 가져올 때 사용할 거임.
       
    List<CubeItemView> views = new List<CubeItemView>();       // Content에 넣을 아이템들 
	// Use this for initialization
	void Start () {
                                       // 파이어베이스 접속 객체 생성
	}
	public void UpdateItems()                                       //아이템 업데이트
    {
        tran = new Transaction();

        tran.RetrieveCubesByUserid(userID);
      //  int newCount;                                                   //아이템 개수
      //  int.TryParse(countText.text, out newCount);                     // Input field 에서 Text 의 값을 int로 parse 하는 기능
        StartCoroutine(FetchItemModelsFromServer(results => OnReceivedNewModels(results)));       // newCount 개수 만큼, results 에 반환 된 cube Item들을 가지고


    }                                                                                                   //OnreceivedNewModels에 넘겨줘 각 게임 오브젝트를 생성하고 view List에 
                                                                                                        //
	// Update is called once per frame
    void OnReceivedNewModels(CubeItem[] models)
    {
        foreach (Transform child in content.transform)                              // content 에 있는 아이템들 다 지움
        {
            Destroy(child.gameObject);
        }

        views.Clear();                                                              // views clear

        int i = 0;
        foreach (var model in models)                                               // Fetch 에서 받아온 걸로 views에 추가함 
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;     //
            instance.transform.SetParent(content, false);                                   // parent가 content가 되므로 자동 추가 됨
            var view = InitializeItemView(instance, model);                         
            views.Add(view);
            ++i;
        }
    }
    CubeItemView InitializeItemView(GameObject viewGameObject, CubeItem model)          // CubeItem View를 초기화하여 넘겨줌,
    {                                                                                       
        CubeItemView view = new CubeItemView(viewGameObject.transform);

        view.titleText.text = model.name;
       // view.iconImage.texture = availableIcons[model.iconIndex];                           //각 index들은 fetch에서 랜덤으로 받아옴'
        view.x.text = model.x.ToString();
        view.y.text = model.y.ToString();
        view.z.text = model.z.ToString();

        view.xAxis.text = model.xAxis.ToString();
        view.yAxis.text = model.yAxis.ToString();
        view.zAxis.text = model.zAxis.ToString();

        //view.icon2Image.texture = availableIcons[model.icon2Index];
        //view.icon3Image.texture = availableIcons[model.icon3Index];

        return view;
    }
    IEnumerator FetchItemModelsFromServer( Action<CubeItem[]> onDone)
    {
        int count=0;
        int delay = 0;

        Debug.Log("FetchItemModels " );
        while(true)                                                 //  서버에서 값을 받아 올 때 까지 기다림 
        {
            if (tran.isFailed || 20<=delay++) { Debug.Log("is failed in FetchItem while"); count = 0; break; }
            else if (tran.isWaiting) { Debug.Log("is waiting in FetchItem while"); yield return new WaitForSeconds(0.5f); }
            else if (tran.isSuccess) { count = tran.cubes.Count;  Debug.Log("is success in FetchItem while"); break; }   
        }

        Debug.Log("after FetchItem while");
        //foreach (Cube cubes in tran.cubes){                             
        //        Debug.Log("foreach " + JsonUtility.ToJson(cubes));
        //}


        count = tran.cubes.Count;

        var results = new CubeItem[count];
       
        for (int i = 0; i < count; ++i)                                 // count 만큼 results[i] 에 값을 넣어줌 그걸 리턴 함
        {                                                                  //여기서 큐브의 정보를 넣어주면 됨 
            results[i] = new CubeItem();                                        // X,Y,Z , 위치좌표, 큐브이름 
            results[i].name = tran.cubes[i].name;
            //results[i].iconIndex = UnityEngine.Random.Range(0, availableIcons.Length);
            results[i].x = tran.cubes[i].x.ToString();
            results[i].y = tran.cubes[i].y.ToString();
            results[i].z = tran.cubes[i].z.ToString();
            results[i].xAxis = tran.cubes[i].xAxis.ToString();
            results[i].yAxis = tran.cubes[i].yAxis.ToString();
            results[i].zAxis = tran.cubes[i].zAxis.ToString();

            //results[i].icon2Index = UnityEngine.Random.Range(0, availableIcons.Length);
            //results[i].icon3Index = UnityEngine.Random.Range(0, availableIcons.Length);
        }
        onDone(results);
    }
    public class CubeItemView
    {
        public Text titleText;
        public Text x, y, z;
        public Text xAxis, yAxis, zAxis;
      //  public RawImage iconImage;//, icon2Image,icon3Image;
     
        public CubeItemView(Transform rootView)
        {
            titleText = rootView.Find("TitlePanel/CubeName").GetComponent<Text>();
            //iconImage = rootView.Find("IconImage").GetComponent<RawImage>();
            x = rootView.Find("XText").GetComponent<Text>();
            y = rootView.Find("YText").GetComponent<Text>();
            z = rootView.Find("ZText").GetComponent<Text>();
            xAxis = rootView.Find("XAxisText").GetComponent<Text>();
            yAxis = rootView.Find("YAxisText").GetComponent<Text>();
            zAxis = rootView.Find("ZAxisText").GetComponent<Text>();
          //  icon2Image = rootView.Find("Icon2Image").GetComponent<RawImage>();
           // icon3Image = rootView.Find("TitlePanel/Icon3Image").GetComponent<RawImage>();
        }

    }
    public class CubeItem
    {
        public string name;
        public string x,y,z;
        public string xAxis, yAxis, zAxis;
       // public int iconIndex;//, icon2Index, icon3Index;
    }
}
>>>>>>> newStopsBranch
