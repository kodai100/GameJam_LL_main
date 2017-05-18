/*
 タイトルからメインへの遷移
*/
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
			FadeManager.Instance.fadeColor = Color.white;
			FadeManager.Instance.LoadLevel("Main", 0.5f);
        }
    }

}