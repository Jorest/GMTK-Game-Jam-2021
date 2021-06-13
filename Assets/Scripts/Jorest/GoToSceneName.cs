using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneName : MonoBehaviour
{
  
    public void goToSceneName(string name)
    {
        SceneManager.LoadScene(name);
    }

}
