using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    string currentScene;
    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) || dead)
        {
            SceneManager.LoadScene(currentScene);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Death")
        {
            dead = true;
        }
    }
}
