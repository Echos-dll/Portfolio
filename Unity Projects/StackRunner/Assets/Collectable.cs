using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private bool _isUsed;

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isUsed)
        {
            collision.gameObject.GetComponent<CollectableStack>().AddItem();
            _isUsed = true;
            Destroy(gameObject);
        }
    }
}
