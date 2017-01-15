using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRandomColoring : MonoBehaviour {

    Material material;

    public float saturation = 0.7f;
    public float value = 1f;

	// Use this for initialization
	void Start () {
        material = GetComponent<Renderer>().material;
        material.SetColor("_Color", Color.HSVToRGB(Random.Range(0f, 1f), saturation, value));
    }
	
}
