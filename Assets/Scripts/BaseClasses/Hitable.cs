using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Hitable : MonoBehaviour
{
    [SerializeField] private UnityEvent OnClick;

    private void OnMouseDown()
    {
        OnClicked();
    }

    protected virtual void OnClicked()
    {
        OnClick?.Invoke();
    }
    

}
