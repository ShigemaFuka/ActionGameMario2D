using UnityEngine;


public class ImpactDeathEnemy : MonoBehaviour
{
    Rigidbody2D _rb= default;
    Vector2 _position = Vector2.zero;
    Vector2 _targetPosition = Vector2.zero;
    Vector2 _distance = Vector2.zero; 
    Vector2 _direction = new Vector2(1, 1);
    [SerializeField] float _power = 10f;
    Animator _animator = default;
    int _num = 0;

    void OnEnable()
    {
        _animator = GetComponent<Animator>(); 
        _rb = GetComponent<Rigidbody2D>();
        _position = this.transform.position;
        _targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        _distance = (_targetPosition - _position).normalized;
        // Player‚ª“G‚æ‚è‰E‚É‹‚é‚Æ‚« 
        if (_distance.x >= 0)
        {
            _direction.x = -_direction.x;
            Vector2 _localScale = this.transform.localScale;
            this.transform.localScale = new Vector3(-_localScale.x, this.transform.localScale.y);
        }
        //_num = Random.Range(1, 4);
        _num = 3;
        if (_num == 3) _power = _power / 2;
        _animator.SetTrigger($"isDead_{_num}"); 
        _rb.AddForce(_direction * _power, ForceMode2D.Impulse);
        Destroy(this.gameObject, 2);
    }
}
