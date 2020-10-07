using System;
using System.Collections;
using System.Collections.Generic;

public class GameData
{
    public int Version { get; set; } = 1;

    public int Stars { get; set; } = 0;

    public bool TutorialPassed { get; set; } = false;

    public List<UpgradeCard> UpgradeCards { get; set; } = new List<UpgradeCard>();
}
