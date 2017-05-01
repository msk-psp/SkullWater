using UnityEngine;
<<<<<<< HEAD
using System.Collections;
using FirebaseAccess;
using Furniture;

public class ReceiveFromDatabaseByName : MonoBehaviour {

    private Transaction mTran;   //Database Transaction Class

	public void ReceiveCube () {
        mTran = new Transaction();

=======
using System.Collections;
using FirebaseAccess;
using Furniture;

public class ReceiveFromDatabaseByName : MonoBehaviour {

    private Transaction mTran;   //Database Transaction Class

	public void ReceiveCube () {
        mTran = new Transaction();

>>>>>>> newStopsBranch
        mTran.RetrieveCube("Cube");
	}
}
