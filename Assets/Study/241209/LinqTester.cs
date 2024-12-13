using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinqTester : MonoBehaviour
{
    private List<int> dataBase = new List<int>() { 3, 4, 1, 2, 7, 9, 5, 8, 6 };
    private List<MonsterData> monsters = new List<MonsterData>()
    {
        new MonsterData(MonsterType.Normal, 10, "����"),
        new MonsterData(MonsterType.Fire, 7, "���̸�"),
        new MonsterData(MonsterType.Water, 12, "���α�"),
        new MonsterData(MonsterType.Grass, 5, "�̻��ؾ�"),
        new MonsterData(MonsterType.Normal, 1, "������")
    };


    private void Start()
    {
        var quary = from element in dataBase  // = foreach (int element in dataBase)
                    where element > 5         // = if (element > 5)
                    orderby element ascending // = ������������ ���� ����, �ݴ��� ��쿡�� descending
                    select element;           // = result.Add(element);

        // �� ����� ����Ʈ�� �����Ѵ�.
        List<int> list = quary.ToList();
        // �ش� ����Ʈ�� ����� ���� �ܼ�â�� ����Ѵ�.
        for (int i = 0; i < list.Count; i++)
            Debug.Log(list[i]);

        // =================================================================================================

        var _quary = from monster in monsters                 // = foreach (MonsterData monster in monsters)
                     where monster.type == MonsterType.Normal // = if (monster.type == MonsterType.Normal)
                     orderby monster.level ascending          // = ���� ������������ ���� ����
                     select monster;                          // = result.Add(monster);

        List<MonsterData> _list = _quary.ToList();
        for (int i = 0; i < _list.Count; i++)
            Debug.Log($"{_list[i].name} / {_list[i].level} /{_list[i].type}");

        // =================================================================================================

        var autoLockOn = from target in Physics.OverlapSphere(transform.position, 3f)
                         where target.gameObject.layer == LayerMask.NameToLayer("Monster")
                         where Vector3.Distance(transform.position,target.transform.position) < 3f
                         orderby target ascending
                         select target;
    }

    #region ������ ����Ͽ��� ���ϴ� ���� ã�� �Լ� ����
    public List<int> Search()
    {
        List<int> result = new List<int>();

        foreach (int element in dataBase)
        {
            if (element > 5) result.Add(element);
        }

        return result;
    }
    #endregion

    #region ������ ����Ͽ��� ���ϴ� ������ ���� ã�� �Լ� ����
    public List<MonsterData> DataSearch()
    {
        List<MonsterData> result = new List<MonsterData>();

        foreach (MonsterData monster in monsters)
        {
            if (monster.type == MonsterType.Normal) result.Add(monster);
        }

        return result;
    }
    #endregion
}

public enum MonsterType { Normal, Fire, Water, Grass }
public class MonsterData
{
    public MonsterType type;
    public int level;
    public string name;

    public MonsterData(MonsterType type, int level, string name)
    {
        this.type = type;
        this.level = level;
        this.name = name;
    }
}
