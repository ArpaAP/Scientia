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
        StartCoroutine(FadeTextToFullAlpha(image));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeTextToFullAlpha(Image image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        for (float f = 0f; f < 1; f += 0.005f)
        {
            Color c = image.GetComponent<Image>().color;
            c.a = f;
            image.GetComponent<Image>().color = c;
            yield return null;
        }
        
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        yield return new WaitForSeconds(3);


        for (float f = 1f; f > 0; f -= 0.005f)
        {
            Color c = image.GetComponent<Image>().color;
            c.a = f;
            image.GetComponent<Image>().color = c;
            yield return null;
        }
    }
}
