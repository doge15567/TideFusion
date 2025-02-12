﻿using LabFusion.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace LabFusion.SDK.Points {
    public class NESGuitar : AccessoryItem {
        public override string Title => "Retro Guitar";

        public override string Author => BitEconomy.BaBaAuthor;

        public override string Description => "SO RETRO!!!!";

        public override int Price => 1983;

        public override RarityLevel Rarity => RarityLevel.Pink;

        public override Texture2D PreviewImage => FusionPointItemLoader.GetPair(nameof(NESGuitar)).Preview;

        public override GameObject AccessoryPrefab => FusionPointItemLoader.GetPair(nameof(NESGuitar)).GameObject;

        public override AccessoryPoint ItemPoint => AccessoryPoint.CHEST_BACK;

        public override AccessoryScaleMode ScaleMode => AccessoryScaleMode.HEIGHT;

        public override bool IsHiddenInView => true;

        public override string[] Tags => new string[3] {
            "Backpack",
            "Cosmetic",
            "Retro",
        };
    }
}