using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTweenTester : MonoBehaviour
{
    private void Update()
    {
        // ���� ���콺 ��ư�� Ŭ������ ��,
        if (!Input.GetMouseButtonDown(0)) return;

        // ȭ����� ���콺 ��ġ�� �������� ����ĳ��Ʈ�� �߻�
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // ������ ���� ���� ��ġ�� �̵��Ѵ�.
        if (Physics.Raycast(ray, out RaycastHit hitInfo)) MoveTo(hitInfo.point);
    }


    public void MoveTo(Vector3 des)
    {
        transform.DOMove(des, 1f)
            .SetEase(Ease.InOutCirc)
            .SetDelay(0.5f);

        Material material = GetComponent<Material>();
        material.DOColor(Random.ColorHSV(), 0.5f);
    }
}
