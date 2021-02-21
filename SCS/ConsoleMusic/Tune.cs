using System;
using System.Threading;
using System.Xml.Serialization;

namespace SCS.ConsoleMusic
{
    [Serializable] 
    public class Tune
    {
        public Note[] Notes { get; set; }

        public Note this[int index] => Notes[index];

        [XmlIgnore] public int PlaybackProgress { get; private set; }

        public Tune() { }

        public Tune(Note[] notes) => Notes = notes;

        public void Play(Action beepCallback = null)
        {
            PlaybackProgress = 0;

            for (int i = 0; i < Notes.Length; i++)
            {
                Note note = Notes[i];

                if (note.Tone == Tone.Rest)
                {
                    Thread.Sleep((int)note.Duration);
                }
                else
                {
                    Console.Beep((int)note.Tone, (int)note.Duration);
                    beepCallback?.Invoke();
                }

                PlaybackProgress = (int)Math.Round(((float)i/Notes.Length) * 100);
            }
        }
    }
}
