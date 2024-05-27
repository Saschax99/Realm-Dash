using UnityEngine;

public class PopupDamage : MonoBehaviour
{
    private void Start()
    {
        transform.localPosition += new Vector3(0, 1f, 0);
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingOrder = 100;
        meshRenderer.sortingLayerName = "Background"; // to make visible because of lights
        Destroy(transform.parent.gameObject, .5f);
    }
}
