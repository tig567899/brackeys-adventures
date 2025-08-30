using TMPro;
using UnityEngine;

public class BattleManager: MonoBehaviour
{
    // TODO: Should vary depending on player state and enemy stats
    public int playerHealth = 100;
    public int enemyHealth = 100;
    public int enemyDifficulty = 12;

    public int[] dice = { 6, 6 };
    public GameObject diceParent;

    public void RollDice()
    {
        int[] rolls = new int[dice.Length];
        var rand = new System.Random();
        for (int i = 0; i < dice.Length; i++)
        {
            rolls[i] = rand.Next(1, 6);
            diceParent.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = rolls[i].ToString();
        }

        // TODO: Do we want any enemy randomization? If not, just use enemyDifficulty as the number to beat
        int enemyRoll = rand.Next(1, enemyDifficulty);

        // TODO: Handle battle state
        // i.e. if sum of roll > enemy difficulty, do xyz
    }
}
