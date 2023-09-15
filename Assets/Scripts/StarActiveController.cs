using UnityEngine;
/// <summary>
/// ���̃I�u�W�F�N�g���A�N�e�B�u�E��A�N�e�B�u�ɐ؂�ւ���
/// �U�����邽�тɃX�R�A���Z
/// ���Ԑ��Ŕ�A�N�e�B�u�ɂ���
/// </summary>
public class StarActiveController : MonoBehaviour
{
    [SerializeField, Tooltip("���̃I�u�W�F�N�g")] GameObject _star = default;
    [SerializeField] float _interval = 10f; 
    float _timer = 0f; 
    void Start()
    {
        _star.SetActive(false); 
    }
    void Update()
    {
        _timer += Time.deltaTime; 
        
        if (_star.activeSelf)
        {
            // �\���I��  
            if (_timer > _interval)
            {
                _star.SetActive(false);
                _timer = 0f;
            }
        }
        // �\���J�n 
        else if (_timer > _interval)
        {
            _star.SetActive(true); 
            _timer = 0f;
        }
    }
}
