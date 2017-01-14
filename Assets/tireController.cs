/*
 タイトルからリザルトへの遷移
*/
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class tireController : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main");
        }
    }

}