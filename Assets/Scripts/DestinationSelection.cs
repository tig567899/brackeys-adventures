using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DestinationSelection : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool isActive = false;
    private List<GameObject> childNodes = new List<GameObject>();

    private List<LineRenderer> lines = new List<LineRenderer>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        //Debug.Log("Here");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        // Draw lines to child nodes here
        foreach (var item in childNodes)
        {
            this.addLineToChild(item);
            item.GetComponent<DestinationSelection>().SetActive(true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void SetActive(bool active)
    {
        isActive = active;
        gameObject.SetActive(active);
    }

    public void AddChildNode(GameObject child)
    {
        childNodes.Add(child);
    }

    private void addLineToChild(GameObject child)
    {
        LineRenderer newLine = new GameObject().AddComponent<LineRenderer>();
        newLine.material.color = Color.black;
        newLine.startWidth = 0.1f;
        newLine.endWidth = 0.1f;
        newLine.SetPosition(0, gameObject.transform.position);
        newLine.SetPosition(1, child.transform.position);

        lines.Add(newLine);
    }
}
