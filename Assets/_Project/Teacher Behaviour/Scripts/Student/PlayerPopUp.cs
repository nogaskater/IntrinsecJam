using UnityEngine;

public class PlayerPopUp : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _speed;

    private float _counter = 0;
    
    private void Update()
    {
        _counter += Time.deltaTime;

        if (_counter > _duration)
            Destroy(gameObject);

        transform.Translate(Vector3.up * _speed * Time.deltaTime);

    }
}