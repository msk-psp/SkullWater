using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FirebaseAccess;
using Furniture;
public class ReceiveFromDatabase : MonoBehaviour {

    private Transaction mTran;   //Database Transaction Class
    public string mCubeName;
    public string mUsrName;
	
    
    public void ReceiveCubeByName(){                
        mTran = new Transaction();
      //  mTran.RetrieveCubesByName(mCubeName); 
    }

    public void ReceiveCubesByUserID(){
        mTran = new Transaction();
        mTran.RetrieveCubesByUserid(mUsrName);
    }



}
