using UnityEngine;

public static class InstrumentTypeNameMapper
{
    public static string InstrumentTypeFriendlyName(InstrumentType type)
    {
        switch (type)
        {
            case InstrumentType.BASS_GUITAR:
                return "Bass";
            case InstrumentType.DRUM_KIT:
                return "Drums";
            case InstrumentType.LEAD_GUITAR:
                return "Guitar";
            default:
                Debug.LogError("Tried to get friendly name for unknown instrument type: " + type.ToString());
                return "???";
        }
    }
}
