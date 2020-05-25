using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    GameObject MainPage;
    // Start is called before the first frame update

    LobbyManager[] pages;
    void Start()
    {
        pages = FindObjectsOfType<LobbyManager>();
        MainPage.SetActive(true);
        foreach (LobbyManager item in pages)
        {
            if (item.gameObject != MainPage && item!=this)
                item.gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePage(GameObject newPage)
    {
        newPage.SetActive(true);
        gameObject.SetActive(false);
    }

    public void LoadGame(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
