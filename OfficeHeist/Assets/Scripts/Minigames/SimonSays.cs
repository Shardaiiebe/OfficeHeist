using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSays : MonoBehaviour
{
    List<Button> correctButtonOrder = new List<Button>();
    List<Button> playerButtonOrder = new List<Button>();
    public List<Button> buttons;

    // Start is called before the first frame update
    void Start()
    {
        List<Button> buttonsLeft = new List<Button>(buttons);
        for (int i = 0; i < 5; i++) {
            int number = (int)(Random.value * buttonsLeft.Count);
            correctButtonOrder.Add(buttonsLeft[number]);
            Color currentColor = buttonsLeft[number].image.color;
            buttonsLeft[number].image.color = new Color(253, 253, 150);
            buttonsLeft.RemoveAt(number);
        }
    }

}
