using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class _MonsterCon : MonoBehaviour, IDamagable
{
     private new Renderer renderer;

    private void Awake() => renderer = GetComponent<Renderer>();

    public void TakeHit(int damage)
    {
        Sequence sequence = DOTween.Sequence();
        sequence
            .Append(renderer.material.DOColor(Color.red, 0.1f))
            .Append(renderer.material.DOColor(Color.white, 0.1f));
    }
}

public interface IDamagable
{
    void TakeHit(int damage);
}