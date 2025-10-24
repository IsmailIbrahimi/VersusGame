using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.L))
        // {
        //     LoadNextLevel();
        // }
    }

    public void LoadNextLevel()
    {
        Debug.Log("Loading next level...");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    }

    public IEnumerator PlayTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        transition.Play("Entry", 0, 0); // Force retour à l'état initial
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return StartCoroutine(PlayTransition());
        SceneManager.LoadScene(levelIndex);
    }
}
