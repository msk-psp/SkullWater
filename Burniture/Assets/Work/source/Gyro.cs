using UnityEngine;
using System.Collections;

public class Gyro : MonoBehaviour
{
    private int StartX;
    private int StartY;
    private int StartZ;

    void Start()
    {

        Input.gyro.enabled = true; // 자이로 센서 작동
        Input.gyro.updateInterval = 0.01f; // 자이로 센서 업데이트 시간

        /*처음 자이로 센서의 회전 각도를 기억*/
        StartX = (int)Input.gyro.rotationRateUnbiased.x;
        StartY = (int)Input.gyro.rotationRateUnbiased.y;
    }


    void Update()
    {
        /*처음 회전에서 프레임마다 회전각을 계산하여 객제를 회전시킴*/
        transform.Rotate(StartX - Input.gyro.rotationRateUnbiased.x,
                        StartY - Input.gyro.rotationRateUnbiased.y,
                        StartZ + Input.gyro.rotationRateUnbiased.z);
    }
}