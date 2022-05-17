using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomColour : MonoBehaviour
{
    [SerializeField] private Tilemap _tl;

     Color32 _colour2;

    #region colours
    Color32 _black = Pico8Colours.Black;
    Color32 _darkBlue = Pico8Colours.DarkBlue;
    Color32 _darkPurple = Pico8Colours.DarkPurple;
    Color32 _darkGreen = Pico8Colours.DarkGreen;
    Color32 _brown = Pico8Colours.Brown;
    Color32 _darkGrey = Pico8Colours.DarkGrey;
    Color32 _lightGrey = Pico8Colours.LightGrey;
    Color32 _white = Pico8Colours.White;
    Color32 _red = Pico8Colours.Red;
    Color32 _orange = Pico8Colours.Orange;
    Color32 _yellow = Pico8Colours.Yellow;
    Color32 _green = Pico8Colours.Green;
    Color32 _blue = Pico8Colours.Blue;
    Color32 _lavander = Pico8Colours.Lavander;
    Color32 _pink = Pico8Colours.Pink;
    Color32 _lighpeach = Pico8Colours.LightPeach;
    #endregion

    private void Start()
    {
        StartCoroutine(ColourChanger());
    }

    IEnumerator ColourChanger()
    {
        yield return new WaitForSeconds(1f);
        RandomColour1(_colour2);
        _tl.color = _colour2;
        StartCoroutine(ColourChanger());
    }

    
    private void RandomColour1(Color32 colour)
    {
        int random = Random.Range(0, 15);
        switch (random)
        {
            case 0:
                colour = _black;
                break;
            case 1:
                colour = _darkBlue;
                break;
            case 2:
                colour = _darkPurple;
                break;
            case 3:
                colour = _darkGreen;
                break;
            case 4:
                colour = _brown;
                break;
            case 5:
                colour = _darkGrey;
                break;
            case 6:
                colour = _lightGrey;
                break;
            case 7:
                colour = _white;
                break;
            case 8:
                colour = _red;
                break;
            case 9:
                colour = _orange;
                break;
            case 10:
                colour = _yellow;
                break;
            case 11:
                colour = _green;
                break;
            case 12:
                colour = _blue;
                break;
            case 13:
                colour = _lavander;
                break;
            case 14:
                colour = _pink;
                break;
            case 15:
                colour = _lighpeach;
                break;
        }
        _colour2 = colour;
    }
}
