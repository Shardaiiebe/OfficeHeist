using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MiniGame : MonoBehaviour
{
    public List<Button> correctButtonOrder = new List<Button>();
    public List<Button> playerButtonOrder = new List<Button>();
    public Button[] buttons;
    public Transform[] spots;

    void Start() {
        Shuffle(buttons);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.position = spots[i].position;
        }
    }

    static void Shuffle<T>(IList<T> array)
    {
        int n = array.Count;
        for (int i = 0; i < n; i++)
        {
            int r = i + (int)(Random.value * (n - i));
            (array[r], array[i]) = (array[i], array[r]);
        }
    }

    public void OnButtonClick(Button button)
    {
        playerButtonOrder.Add(button);
        
        int numButtonsToCheck = playerButtonOrder.Count;
        List<Button> subCorrectButtonOrder = correctButtonOrder.Take(numButtonsToCheck).ToList();

        if (subCorrectButtonOrder.SequenceEqual(playerButtonOrder))
        {
            button.image.color = Color.gray;
            if (subCorrectButtonOrder.Count != correctButtonOrder.Count) return;
            correctButtonOrder.ForEach(b => b.image.color = Color.green);
            playerButtonOrder.Clear();
        }
        else
        {
            StartCoroutine(DisableButtonsTemporarily());
            Start();
            playerButtonOrder.Clear();
        }
    }
    private IEnumerator DisableButtonsTemporarily()
    {
        // Disable all buttons
        correctButtonOrder.ForEach(button => button.interactable = false);

        // Wait for 1 second
        yield return new WaitForSeconds(1.0f);

        // Enable all buttons
        correctButtonOrder.ForEach(button => button.interactable = true);
        correctButtonOrder.ForEach(button => button.image.color = button.colors.normalColor);
    }
}