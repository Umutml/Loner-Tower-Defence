using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private Transform startPos;
    private readonly Queue<Arrow> arrows = new();
    private Transform arrowTransformParent;
    public static ArrowPool Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.GameObject());
        CreateNullArrowObject();
        ArrowShot(10);
    }

    private void CreateNullArrowObject()
    {
        var go = new GameObject();
        go.name = "ArrowList";
        go.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
        arrowTransformParent = go.transform;
    }

    public Arrow Get()
    {
        if (arrows.Count == 0) ArrowShot(1);
        var arrow = arrows.Dequeue();
        arrow.transform.position = startPos.position;
        arrow.gameObject.SetActive(true);
        return arrow;
    }


    public void ArrowShot(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var arrowShot = Instantiate(arrowPrefab, arrowTransformParent).GetComponent<Arrow>();
            arrowShot.arrowSpeed = arrowSpeed;
            arrowShot.gameObject.SetActive(false);
            arrows.Enqueue(arrowShot);
        }
    }

    public void ReturnToPool(Arrow arrow)
    {
        arrow.gameObject.SetActive(false);
        arrows.Enqueue(arrow);
    }
}