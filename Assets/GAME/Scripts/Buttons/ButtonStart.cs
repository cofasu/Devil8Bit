
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonStart : MonoBehaviour
{
    Button myButton;
    private bool _Active = true;
    private bool _LoadScene = false;

    [SerializeField]
    private int scene;
    [SerializeField]
    private Text loadingText;

    void Awake()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(() => { _startAction(); });
    }

    private void _startAction()
    {
        if (_Active)
        {
            _Active = false;
            _LoadScene = true;
            loadingText.text = "Loading...";
            
            StartCoroutine(LoadNewScene());            
        }
    }

    void Update()
    {
        if (_LoadScene)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));            
        }
    }

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(2.5f);
        
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        while (!async.isDone)
        {
            yield return null;
        }		
    }
}

