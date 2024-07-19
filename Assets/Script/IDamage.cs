using UnityEngine;
public interface IDamage
{
    public float Health { set; get; }

    public bool Targetable {  set; get; }
    public bool Invincible{set; get; }

    public void OnHit(float damage);
    public void OnObjectDestroyed();

}
