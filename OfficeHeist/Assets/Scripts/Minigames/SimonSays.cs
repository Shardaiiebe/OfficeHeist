using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SimonSays : MonoBehaviour
{
    List<Button> correctButtonOrder = new List<Button>();
    List<Button> playerButtonOrder = new List<Button>();
    public List<Button> buttons;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(LightUpButtons());
    }

    IEnumerator LightUpButtons() {
        List<Button> buttonsLeft = new List<Button>(buttons);
        correctButtonOrder.Clear();
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 5; i++) {
            int number = (int)(Random.value * buttonsLeft.Count);
            correctButtonOrder.Add(buttonsLeft[number]);
            Color currentColor = buttonsLeft[number].image.color;
            buttonsLeft[number].image.color = new Color(0.992f, 0.992f, 0.5859f, 1f);
            yield return new WaitForSeconds(1.0f);
            buttonsLeft[number].image.color = currentColor;
            buttonsLeft.RemoveAt(number);
            yield return new WaitForSeconds(0.5f);
        }
        buttons.ForEach(button => button.interactable = true);
    }
    
    public void OnButtonClick(Button button)
    {
        playerButtonOrder.Add(button);
        
        int numButtonsToCheck = playerButtonOrder.Count;
        List<Button> subCorrectButtonOrder = correctButtonOrder.Take(numButtonsToCheck).ToList();
        
        if (subCorrectButtonOrder.SequenceEqual(playerButtonOrder))
        {
            button.image.color = new Color(0.3f, 0.3f, 0.3f, 1f);
            if (subCorrectButtonOrder.Count != correctButtonOrder.Count) return;
            buttons.ForEach(b => b.image.color = Color.green);
            playerButtonOrder.Clear();
        }
        else
        {
            StartCoroutine(DisableButtonsTemporarily());
            playerButtonOrder.Clear();
        }
    }
    
    private IEnumerator DisableButtonsTemporarily()
    {
        buttons.ForEach(button => {
            button.interactable = false;
            button.image.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        });

        yield return new WaitForSeconds(1.0f);

        buttons.ForEach(button => {
            button.interactable = true;
            button.image.color = button.colors.normalColor;
        });
        
        yield return new WaitForSeconds(0.5f);
        
        Start();
    }
}
