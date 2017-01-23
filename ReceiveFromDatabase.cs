using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using FirebaseAccess;
using Furniture;

public class ReceiveFromDatabase : MonoBehaviour {

    private Transaction mTran;   //Database Transaction Class
    
	public void ReceiveCubeByName (string name) {
        mTran = new Transaction();
        Cube cube = null;
        //cube = mTran.RetrieveCubesByName(name);
        cube = mTran.RetrieveCubesByName("Cube"); //for test
        if (cube == null){
            //Debug.Log("큐브를 찾을 수 없습니다.");
            return;
        }
	}
    public void ReceiveCubesByUserID(string uid){
        mTran = new Transaction();
        List<Cube> cubes = new List<Cube>();
        //mTran.RetrieveCubesByUserid(uid);
        cubes = mTran.RetrieveCubesByUserid("Minsoo");
        if (cubes.Count == 0){
            //Debug.Log("큐브가 없습니다.");
        }
        else if (cubes == null){
            //Debug.Log("유저가 존재하지 않습니다.");
        }
        
    }
}
