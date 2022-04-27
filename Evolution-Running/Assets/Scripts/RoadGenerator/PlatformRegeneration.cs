using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRegeneration : MonoBehaviour
{
    private PieceGenerator generator;
    public bool isBroken;

    private void Start()
    {
        generator = PieceGenerator.instance;
    }

    // Start is called before the first frame update
    public void onDestruction()
    {
        isBroken = true;
        StartCoroutine(cleanDebris());
    }

    IEnumerator cleanDebris()
    {
        yield return new WaitForSeconds(5f);
        generator.regeneratePlatform();
        generator.inPlatform.Remove(gameObject);
        Destroy(gameObject);
    }
}
