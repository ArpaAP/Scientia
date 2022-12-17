using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] Image image = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeTextToFullAlpha(1f, image));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeTextToFullAlpha(float t, Image image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        yield return new WaitForSeconds(1);

        while (image.color.a < 1.0f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (Time.deltaTime / t));
            yield return null;
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        
        yield return new WaitForSeconds(3);

        while (image.color.a > 0.0f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
