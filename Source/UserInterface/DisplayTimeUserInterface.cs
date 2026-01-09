using Watches.Components;

namespace Watches.UserInterface;

internal static class DisplayTimeUserInterface
{
    private static void AddHourMarkers(GameObject clockObject)
    {
        const float radius = 35f;
        for (var i = 1; i <= 12; i++)
        {
            GameObject hourMarker = new($"HourMarker{i}");
            hourMarker.transform.SetParent(clockObject.transform, false);

            var label = hourMarker.AddComponent<UILabel>();
            SetupUiLabel(
                label,
                i.ToString(),
                FontStyle.Normal,
                UILabel.Crispness.Always,
                NGUIText.Alignment.Center,
                UILabel.Overflow.ResizeFreely,
                false,
                1,
                14,
                Color.white,
                false
            );
            
            var angle = (90 - i * 30) * Mathf.Deg2Rad;
            var x = Mathf.Cos(angle) * radius;
            var y = Mathf.Sin(angle) * radius - 2f;
            hourMarker.transform.localPosition = new Vector3(x, y, 0);
        }
    }
    
    internal static GameObject SetupAnalogTime(Transform parent, bool worldPositionStays)
    {
        GameObject gameObject = new("AnalogTime");
        gameObject.transform.SetParent(parent, worldPositionStays);
        gameObject.SetActive(false);
        
        GameObject clockCircle = new("ClockCircle");
        clockCircle.transform.SetParent(gameObject.transform);
        clockCircle.transform.localScale = Vector3.one;
        
        var clockSprite = clockCircle.AddComponent<UISprite>();
        SetupUiSprite(
            clockSprite,
            "featSlot"
        );

        AddHourMarkers(gameObject);
        
        GameObject hourHand = new("HourHand");
        hourHand.transform.SetParent(gameObject.transform);
        hourHand.transform.localScale = Vector3.one;
        
        var hourHandSprite = hourHand.AddComponent<UISprite>();
        hourHandSprite.pivot = UIWidget.Pivot.Left;
        hourHandSprite.width = 18;
        hourHandSprite.height = 10;
        SetupUiSprite(
            hourHandSprite,
            "ico_almanac_arrow"
        );
        
        GameObject minuteHand = new("MinuteHand");
        minuteHand.transform.SetParent(gameObject.transform);
        minuteHand.transform.localScale = Vector3.one;
        
        var minuteHandSprite = minuteHand.AddComponent<UISprite>();
        minuteHandSprite.pivot = UIWidget.Pivot.Left;
        minuteHandSprite.width = 25;
        minuteHandSprite.height = 10;
        SetupUiSprite(
            minuteHandSprite,
            "ico_almanac_arrow"
        );
        
        return gameObject;
    }
    
    internal static UILabel SetupDigitalTime(Transform parent, bool worldPositionStays)
    {
        GameObject gameObject = new("DigitalTime");
        gameObject.transform.SetParent(parent, worldPositionStays);
        gameObject.SetActive(false);
        
        var uiLabel = gameObject.AddComponent<UILabel>();
        SetupUiLabel(
            uiLabel,
            string.Empty,
            FontStyle.Normal,
            UILabel.Crispness.Always,
            NGUIText.Alignment.Center,
            UILabel.Overflow.ResizeFreely,
            false,
            0,
            18,
            Color.white,
            true
        );

        GameObject batteryIcon = new("BatterySprite");
        batteryIcon.transform.SetParent(gameObject.transform, worldPositionStays);
        batteryIcon.transform.localScale = Vector3.one;
        batteryIcon.transform.localPosition = new Vector3(85f, -20f, 0f);
        
        var batterySprite = batteryIcon.AddComponent<UISprite>();
        batterySprite.width = 42;
        batterySprite.height = 42;
        SetupUiSprite(
            batterySprite,
            "ico_lightSource_flashlight"
        );
        
        return uiLabel;
    }
    
    internal static GameObject SetupDisplayTimesGameObject(Transform parent, bool worldPositionStays)
    {
        GameObject gameObject = new("DisplayTimes");
        gameObject.AddComponent<DisplayTime>();
        gameObject.transform.SetParent(parent, worldPositionStays);
        
        return gameObject;
    }

    private static void SetupUiLabel(
            UILabel label,
            string text,
            FontStyle fontStyle,
            UILabel.Crispness crispness,
            NGUIText.Alignment alignment,
            UILabel.Overflow overflow,
            bool multiLine,
            int depth,
            int fontSize,
            Color color,
            bool capsLock)
    {
        label.text = text;
        label.ambigiousFont = InterfaceManager.GetPanel<Panel_Subtitles>().m_Label_Subtitles.ambigiousFont;
        label.bitmapFont = InterfaceManager.GetPanel<Panel_Subtitles>().m_Label_Subtitles.bitmapFont;
        label.font = InterfaceManager.GetPanel<Panel_Subtitles>().m_Label_Subtitles.font;
        label.fontStyle = fontStyle;
        label.keepCrispWhenShrunk = crispness;
        label.alignment = alignment;
        label.overflowMethod = overflow;
        label.multiLine = multiLine;
        label.depth = depth;
        label.fontSize = fontSize;
        label.color = color;
        label.capsLock = capsLock;
    }
    
    private static void SetupUiSprite(UISprite sprite, string spriteName)
    {
        var baseAtlas = InterfaceManager.GetInstance().m_ScalableAtlases[0] ?? throw new NullReferenceException("The index of 0 in m_ScalableAtlases is null.");
        var spriteData = baseAtlas.GetSprite(spriteName);

        sprite.atlas = baseAtlas;
        sprite.spriteName = spriteName;
        sprite.mSprite = spriteData;
        sprite.mSpriteSet = true;
    }
}