using UnityEngine;

public static class SoundDataUtility
{
    public static class KeyConfig
    {
        public static class Se
        {
            public static readonly string Button = "Button";
            public static readonly string Success = "Success";
            public static readonly string Miss = "Miss";
        }

        public static class Bgm
        {
            public static readonly string InGame = "InGame";
            public static readonly string InTitle = "InTitle";
            public static readonly string InResult = "Inresult";
        }
    }

    public enum SoundType
    {
        Bgm = 0,
        Se = 1
    }

    public static void PrepareAudioSource(this AudioSource source, SoundData soundData)
    {
        source.playOnAwake = soundData.PlayOnAwake;
        source.loop = soundData.IsLoop;
        source.clip = soundData.Clip;
    }
}
