using System;

public class Data
{
    public readonly Settings settings;

    public Data(Settings settings)
    {
        this.settings = settings;
    }

    [Serializable]
    public class Settings
    {
        public float ShipSpeed;
    }
}