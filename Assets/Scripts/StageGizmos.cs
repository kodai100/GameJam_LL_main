using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGizmos : MonoBehaviour {

    public Vector3 stageSize = new Vector3(100, 100, 100);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(Vector3.zero, stageSize);
    }
}
