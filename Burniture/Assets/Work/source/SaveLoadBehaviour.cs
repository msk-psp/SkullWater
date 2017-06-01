using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveLoadBehaviour : MonoBehaviour {

    private GameObject BotLDSphere; //2
    private GameObject BotLUSphere; //7
    private GameObject BotRDSphere; //1
    private GameObject TopLDSphere; //3

    private GameObject BotRUSphere;
    private GameObject TopRDSphere;
    private GameObject TopLUSphere;
    private GameObject TopRUSphere;

  
    private float mCube_xScale;
    private float mCube_yScale;
    private float mCube_zScale;

    private int type;

    //가구 정보 담을 변수들

    void Start()
    {
        BotLDSphere = GameObject.FindWithTag("Sphere2");
        BotLUSphere = GameObject.FindWithTag("Sphere7");
        BotRDSphere = GameObject.FindWithTag("Sphere1");
        TopLDSphere = GameObject.FindWithTag("Sphere3");

        ///////////////////

        BotRUSphere = GameObject.FindWithTag("Sphere8");
        TopRDSphere = GameObject.FindWithTag("Sphere4");
        TopLUSphere = GameObject.FindWithTag("Sphere6");
        TopRUSphere = GameObject.FindWithTag("Sphere5");




        mCube_xScale = 0;
        mCube_yScale = 0;
        mCube_zScale = 0;
        // 가구 정보 가져올 객체 초기화
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            Load();
        }
        Debug.Log("Init SaveLoadBehaviour");
    }

    // 비정상적인 & 종료시 실행
    void OnApplicationPause(bool pauseStatus)
    {
        Debug.Log("SaveLoadBehaviouor: status = " + pauseStatus);
        Debug.Log("SaveLoadBehaviouor: path = " + Application.persistentDataPath);
        // x크기가 0이 아니라면 
       if (pauseStatus)
            {
                Debug.Log("SaveLoadBehaviouor: PauseStatus");
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
                PlayerData data = new PlayerData();

                data.xLength = mCube_xScale;
                data.yLength = mCube_yScale;
                data.zLength = mCube_zScale;

                bf.Serialize(file, data);       
                file.Close();
        }
    }
    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
        PlayerData data = (PlayerData)bf.Deserialize(file);
        file.Close();

        //( (기본길이(99.8) - 변화된 길이) /2) == -
        // bottom
        BotLDSphere.transform.position += new Vector3(((float)99.8 - data.xLength) / 2, BotLDSphere.transform.position.y, ((float)99.8 - data.zLength) / 2);
        BotRDSphere.transform.position += new Vector3((data.xLength - (float)99.8) / 2, BotRDSphere.transform.position.y, ((float)99.8 - data.zLength) / 2);

        BotLUSphere.transform.position += new Vector3(((float)99.8 - data.xLength) / 2, BotLUSphere.transform.position.y, (data.zLength - (float)99.8) / 2);
        BotRUSphere.transform.position += new Vector3((data.xLength - (float)99.8) / 2, BotRUSphere.transform.position.y, (data.zLength - (float)99.8) / 2);
        ///
        //top
        TopLDSphere.transform.position += new Vector3(((float)99.8 - data.xLength) / 2, (data.xLength - (float)99.8) / 2, ((float)99.8 - data.zLength) / 2);
        TopRDSphere.transform.position += new Vector3((data.xLength - (float)99.8) / 2, (data.xLength - (float)99.8) / 2, ((float)99.8 - data.zLength) / 2);

        TopLUSphere.transform.position += new Vector3(((float)99.8 - data.xLength) / 2, (data.xLength - (float)99.8) / 2, (data.zLength - (float)99.8) / 2);
        TopRUSphere.transform.position += new Vector3((data.xLength - (float)99.8) / 2, (data.xLength - (float)99.8) / 2, (data.zLength - (float)99.8) / 2);
        //
    }
    void Update()
    {
        if (BotLDSphere != null && BotLUSphere != null && BotRDSphere != null && TopLDSphere !=null)
        {
            mCube_xScale = Mathf.Abs(BotLDSphere.transform.position.x - BotRDSphere.transform.position.x);
            mCube_yScale = Mathf.Abs(BotLDSphere.transform.position.y - TopLDSphere.transform.position.y);
            mCube_zScale = Mathf.Abs(BotLDSphere.transform.position.z - BotLUSphere.transform.position.z);

            //type =?
        }
        else
        {
            Start();
        }
    }
}

[Serializable]
class PlayerData
{
    public int type;
    public float xLength;
    public float yLength;
    public float zLength;
}