using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static GameManager instance;
    public GameObject loadingScreen;
    public GameObject progressBar;


    private void Awake()
    {
        instance = this;
        SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void LoadGame()
    {
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MAP, LoadSceneMode.Additive));

    }

    float totalSceneProgress;
   public IEnumerator GetSceneLoadProgres()
    {
        for(int i = 0;  i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;
                
                foreach(AsyncOperation operation in scenesLoading)
                {

                    totalSceneProgress += operation.progress;

                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;

                progressBar.GetComponent<Slider>().value = Mathf.RoundToInt(totalSceneProgress);

                yield return null;
            }

 
        }

        loadingScreen.SetActive(false);
    }
}
