using UnityEngine;
using UnityEngine.EventSystems;

public class DestinationSelection : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }
    
    public void OnPointerDown( PointerEventData eventData )
    {
    }

    public void OnPointerUp( PointerEventData eventData )
    {
    }
}
