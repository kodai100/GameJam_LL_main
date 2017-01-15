using UnityEngine;

public class ApplicationController : MonoBehaviour
{
	void Start ()
	{
		if (FindObjectsOfType<ApplicationController>().Length > 1)
		{
			DestroyImmediate(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}
