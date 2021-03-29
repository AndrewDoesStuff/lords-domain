using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    [SerializeField]
    private int sortingOrderBase = 500;
    [SerializeField]
    private Renderer renderer;

    private void Awake()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        renderer.sortingOrder = (int) (sortingOrderBase - transform.position.y);
    }
}
