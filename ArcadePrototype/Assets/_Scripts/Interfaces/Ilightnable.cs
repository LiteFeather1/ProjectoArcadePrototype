using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Ilightnable
{
    void TurnTrueLightning(Vector3 whereTo);

    IEnumerator Co_LightingReturnFalse(float time);

    bool ReturnLightningFalse();
}
