using UnityEngine;

/// <summary>
/// 弾丸を制御するコンポーネント
/// </summary>
public class BulletControllerStraight_3 : BaseBulletController
{
    [SerializeField, Tooltip("弾が飛ぶ速さ")] float m_speed = 3f;
    [Tooltip("弾丸を生成するオブジェクト")] GameObject _shooterObject;
    [Tooltip("ローカルスケール(反転情報)を入れている")] Vector3 _bulletLoSc;

    public override void MoveBullet()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        string parentObjName = transform.parent.gameObject.name;
        _shooterObject = GameObject.Find(parentObjName + "/Chara");

        _bulletLoSc = _shooterObject.transform.localScale;
        float _xSc = _bulletLoSc.x;

        // 最初にCharaを反転させているため、左右判定も反転させる必要がある  
        // 左向き
        if (_xSc >= -1)
        {
            // 左方向に飛ばす
            rb.velocity = Vector2.left * m_speed;
        }
        // 右向き
        else if (_xSc <= 1)
        {
            // 右方向に飛ばす
            rb.velocity = Vector2.right * m_speed;
            transform.Rotate(0, 0, -180);
        }
    }
}
