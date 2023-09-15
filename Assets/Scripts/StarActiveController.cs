using UnityEngine;
/// <summary>
/// 星のオブジェクトをアクティブ・非アクティブに切り替える
/// 攻撃するたびにスコア加算
/// 時間制で非アクティブにする
/// </summary>
public class StarActiveController : MonoBehaviour
{
    [SerializeField, Tooltip("星のオブジェクト")] GameObject _star = default;
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
            // 表示終了  
            if (_timer > _interval)
            {
                _star.SetActive(false);
                _timer = 0f;
            }
        }
        // 表示開始 
        else if (_timer > _interval)
        {
            _star.SetActive(true); 
            _timer = 0f;
        }
    }
}
