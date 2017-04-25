using UnityEngine;
using System.Collections;
using FirebaseAccess;
using Furniture;

public class ReceiveFromDatabaseByName : MonoBehaviour {

    private Transaction mTran;   //Database Transaction Class

	public void ReceiveCube () {
        mTran = new Transaction();

        mTran.RetrieveCube("Cube");
	}
}
