using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] Image transitionImage = null;

    void Start()
    {
        StartCoroutine(FadeTextToFullAlpha(0.5f, transitionImage));
    }

    void Update()
    {
    }

    public IEnumerator FadeTextToFullAlpha(float t, Image image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        yield return new WaitForSeconds(1);
        
        while (image.color.a > 0.0f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
