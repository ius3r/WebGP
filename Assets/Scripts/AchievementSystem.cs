using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    [SerializeField] private VoidEventChannel voidChannel;
    [SerializeField] private FloatEventChannel floatChannel;
    [SerializeField] private GameDataEventChannel gameDataChannel;

    private int achievementJumps = 10;
    private int currentJumps = 0;
    private int achievementShots = 10;
    private int currentShots = 0;
    private int achievementEnemyKills = 10;
    private int currentEnemyKills = 0;

    private const float ShotEventCode = 1f;
    private const float EnemyKillEventCode = 2f;

    private void OnEnable()
    {
        if (floatChannel == null)
        {
            floatChannel = EventChannelManager.Instance.floatEvent;
        }

        voidChannel.OnEventRaised += EventCalled;
        floatChannel.OnEventRaised += FloatEventCalled;
        gameDataChannel.OnEventRaised += GameDataEventCalled;
    }
    private void OnDisable()
    {
        voidChannel.OnEventRaised -= EventCalled;
        floatChannel.OnEventRaised -= FloatEventCalled;
        gameDataChannel.OnEventRaised -= GameDataEventCalled;
    }

    private void EventCalled()
    {
        Debug.Log("Event Called by listening to the Event Channel of Void type");
        currentJumps++;
        if (currentJumps == achievementJumps)
        {
            Debug.Log("Achievement Completed. Jumped 10 times");
        }
    }
    private void GameDataEventCalled(GameData data)
    {
        Debug.Log("Event with GameData data passed on with filename as "+data.fileName);
    }

    private void FloatEventCalled(float value)
    {
        if (value == ShotEventCode)
        {
            currentShots++;
            if (currentShots == achievementShots)
            {
                Debug.Log("Achievement Completed. Shot 10 times");
            }
        }
        else if (value == EnemyKillEventCode)
        {
            currentEnemyKills++;
            if (currentEnemyKills == achievementEnemyKills)
            {
                Debug.Log("Achievement Completed. Killed 10 enemies");
            }
        }
    }
}