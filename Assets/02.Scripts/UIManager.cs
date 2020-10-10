using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void OnClickStart()
    {
        //씬 로딩
        SceneManager.LoadScene("Level01");
        SceneManager.LoadScene("Game" , LoadSceneMode.Additive);
    }
}
