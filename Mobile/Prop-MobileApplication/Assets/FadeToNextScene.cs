using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToNextScene : MonoBehaviour {

	public void fade_to_next()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }
}
