using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogWithReindeerAntlers
{
    public static class LiquidTypes
    {
        public static Dictionary<LiquidType, LiquidProperties> liquidTypeDict = new Dictionary<LiquidType, LiquidProperties>()
    {
        {
            LiquidType.Banana,
            new LiquidProperties()
            {
                color = new Color(1, .95f, .75f),
                transparency = 0,
                surfaceTension = -2,
                sweetness = 1f,
                sourness = 0f,
                thickness = .7f,
                creaminess = .8f,
            }
        },
        {
            LiquidType.Blackberry,
            new LiquidProperties()
            {
                color = new Color(.25f, .05f, .15f),
                transparency = .2f,
                surfaceTension = 0,
                sweetness = .4f,
                sourness = .6f,
                thickness = .4f,
                creaminess = 0f,
            }
        },
        {
            LiquidType.Blueberry,
            new LiquidProperties()
            {
                color = new Color(.55f, .05f, .3f),
                transparency = 0,
                surfaceTension = 3,
                sweetness = .7f,
                sourness = .3f,
                thickness = .2f,
                creaminess = .1f,
            }
        },
        {
            LiquidType.Raspberry,
            new LiquidProperties()
            {
                color = new Color(.9f, .0f, .15f),
                transparency = .2f,
                surfaceTension = 3,
                sweetness = .6f,
                sourness = .5f,
                thickness = .3f,
                creaminess = 0f,
            }
        },
        {
            LiquidType.Strawberry,
            new LiquidProperties()
            {
                color = new Color(.75f, .0f, .05f),
                transparency = .1f,
                surfaceTension = -1,
                sweetness = .7f,
                sourness = .3f,
                thickness = .5f,
                creaminess = .0f,
            }
        },
        {
            LiquidType.Kiwi,
            new LiquidProperties()
            {
                color = new Color(.5f, .8f, .25f),
                transparency = .1f,
                surfaceTension = -1,
                sweetness = .7f,
                sourness = .5f,
                thickness = .5f,
                creaminess = .0f,
            }
        },
        {
            LiquidType.Mango,
            new LiquidProperties()
            {
                color = new Color(1f, .7f, .1f),
                transparency = 0f,
                surfaceTension = -1,
                sweetness = .8f,
                sourness = .1f,
                thickness = .5f,
                creaminess = .5f,
            }
        },
    };
    }

    public class LiquidProperties
    {
        public Color color = Color.white;
        public float transparency = 0;
        public float surfaceTension = 5;
        public float sweetness = 0;
        public float sourness = 0;
        public float thickness = 0;
        public float creaminess = 0;
    }

    public enum LiquidType
    {
        Strawberry,
        Banana,
        Blueberry,
        Raspberry,
        Blackberry,
        Kiwi,
        Mango,
        /*
        Milk,
        PeanutButter,
        Water,
        */
    }
}


