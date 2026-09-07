using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private List<BuffBase> buffList = new List<BuffBase>();

    public Transform buffUiRoot;
    public BuffImageUI buffImagePrefab;

    private PlayerHealth playerHealth;
    private PlayerMove playerMove;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMove = GetComponent<PlayerMove>();
    }
    public void AddBuff(BuffBase buff)
    {
        BuffBase oldSameTypeBuff = null;
        foreach (var oldbuff in buffList)
        {
            if (oldbuff.buffType == buff.buffType)
            {
                oldSameTypeBuff = oldbuff;
                break;
            }
        }
        if (oldSameTypeBuff != null)
        {
            oldSameTypeBuff.OnRemove(playerHealth, playerMove);
            if (oldSameTypeBuff.buffImageUI != null)
            {
                Destroy(oldSameTypeBuff.buffImageUI.gameObject);
            }
            buffList.Remove(oldSameTypeBuff);
        }
        BuffImageUI ui = Instantiate(buffImagePrefab, buffUiRoot);
        ui.Setup(buff.buffSprite);
        buff.buffImageUI = ui;
        buff.OnApply(playerHealth, playerMove);
        buffList.Add(buff);
    }

    void Update()
    {
        for (int i = buffList.Count - 1; i >= 0; i--)
        {
            var buff = buffList[i];
            buff.OnUpdate(Time.deltaTime, playerHealth, playerMove);

            float ratio = buff.currentTimer / buff.totalDuration;
            buff.buffImageUI.UpdateMask(ratio);

            if (buff.isFinished)
            {
                buff.OnRemove(playerHealth, playerMove);
                if (buff.buffImageUI != null)
                {
                    Destroy(buff.buffImageUI.gameObject);
                }
                buffList.RemoveAt(i);
            }
        }
    }
}