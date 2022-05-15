using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyBase
{
    // Start is called before the first frame update
    void onDestruction(float slamForce, float radius, Vector3 position);
}
