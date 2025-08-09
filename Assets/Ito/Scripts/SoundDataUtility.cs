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
            public static readonly string Turnaround = "Turnaround";
            public static readonly string hide = "hide";
            public static readonly string foot = "foot";
            public static readonly string finish = "finish";
        }

        public static class Bgm
        {
            public static readonly string InGame = "InGame";
            public static readonly string InTitle = "InTitle";
            public static readonly string InResult = "InResult";
            public static readonly string foot = "foot";
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
        source.volume = soundData.Volume;
    }
}
