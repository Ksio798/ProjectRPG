using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelUpdate : MonoBehaviour
{
    //int EgorIndex=0;
    //int AlexIndex = 0;
    //int DimaIndex = 0;
    //int MaximIndex = 0;
    public Color FullColour;
    public Color EmptyColour;
   public List<Image> images = new List<Image>();
   
    public void StartUpdateImages(int count)
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].color = EmptyColour;
        }
        for (int i = 0; i < count; i++)
        {
            images[i].color = FullColour;
        }
    }
    public void UpdateImages(int count)
    {
        images[count].color = FullColour;
    }



   //public int GetPlayerIndex(PlayerType playerType)
   // {
   //     int p = 0;
   //     if (playerType == PlayerType.Alex)
   //     {
   //         p = AlexIndex;
   //     }
   //     else if(playerType == PlayerType.Dimitry)
   //     {
   //         p = DimaIndex;
   //     }
   //     else if (playerType == PlayerType.Egor)
   //     {
   //         p = EgorIndex;
   //     }
   //     else
   //     {
   //         p = MaximIndex;
   //     }
   //     return p;
   // }
    //int GetPlayerIndexToUpdate(PlayerType playerType)
    //{
    //    int p = 0;
    //    if (playerType == PlayerType.Alex)
    //    {
    //        AlexIndex++;
    //        p = AlexIndex;

    //    }
    //    else if (playerType == PlayerType.Dimitry)
    //    {
    //        DimaIndex++; 
    //        p = DimaIndex;
    //    }
    //    else if (playerType == PlayerType.Egor)
    //    {
    //        EgorIndex++;
    //        p = EgorIndex;
    //    }
    //    else
    //    {
    //        MaximIndex++;
    //        p = MaximIndex;
    //    }
    //    return p;
    //}

}
