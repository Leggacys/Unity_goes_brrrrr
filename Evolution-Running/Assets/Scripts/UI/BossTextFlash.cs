using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BossTextFlash : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(decreaseAlpha());
        Debug.Log("Here");
    }

    IEnumerator decreaseAlpha()
    {
        TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
        //textmeshPro.color = new Color32(255, 128, 0, 255);
        Vector3 color = new Vector3(textmeshPro.color.r, textmeshPro.color.g, textmeshPro.color.b);
        for (int i = 255; i > 0; i--)
        {
            Debug.Log(i);
            textmeshPro.color = new Color(color.x, color.y, color.z, i);
            yield return new WaitForSeconds(0.01f);
        }
    }


}
