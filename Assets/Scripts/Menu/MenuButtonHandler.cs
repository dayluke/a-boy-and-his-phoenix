using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonHandler : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData) =>gameObject.GetComponent<Button>().onClick.Invoke();
}
