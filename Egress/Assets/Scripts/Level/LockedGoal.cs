using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockedGoal : MonoBehaviour
{
    public GameObject door;
    public GameObject key;

    // Start is called before the first frame update
    void Update()
    {
        if (key.GetComponent<Key>().open)
        {
            door.SetActive(false);
        }
        else
        {
            door.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (key.GetComponent<Key>().open)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

}