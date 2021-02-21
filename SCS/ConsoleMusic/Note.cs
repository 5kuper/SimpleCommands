using System;

namespace SCS.ConsoleMusic
{
    /// <summary>Frequencies of notes of different octaves, as well as silence (rest).</summary>
    public enum Tone 
    {
        Rest = 0,

        A4 = 440, ASharp4 = 466, B4 = 494, C4 = 262, CSharp4 = 277, D4 = 293,
        DSharp4 = 311, E4 = 330, F4 = 349, FSharp4 = 370, G4 = 392, GSharp4 = 415,

        A5 = 880, ASharp5 = 932, B5 = 988, C5 = 523, CSharp5 = 554, D5 = 587,
        DSharp5 = 622, E5 = 659, F5 = 698, FSharp5 = 740, G5 = 784, GSharp5 = 831
    }

    /// <summary>Duration of a note in units of milliseconds.</summary>
    public enum Duration
    {
        Whole = 1400,
        Half = Whole / 2,
        Quarter = Half / 2,
        Eighth = Quarter / 2,
        Sixteenth = Eighth / 2
    }

    [Serializable]
    public struct Note
    {
        public Tone Tone { get; set; }
        public Duration Duration { get; set; }

        public Note(Tone frequency, Duration time)
        {
            Tone = frequency;
            Duration = time;
        }
    }
}
