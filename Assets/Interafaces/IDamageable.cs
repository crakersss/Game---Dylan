using System.Numerics;

public interface IDamageable{
    public float Health{set; get; }
    void OnHit(float damage, Vector2 knockback);

}