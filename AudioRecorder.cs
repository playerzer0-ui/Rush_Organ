using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Rush_Organ
{
    public class AudioRecorder
    {
        public static List<SoundEffect> sounds = new List<SoundEffect>();
        public static bool isRecording = false;
        public bool isPlaying = false;
        private float recTime = 0.01f;
        private float maxRecTime = 0.01f;
        private float playTime = 0.01f; // New variable for playing
        private float maxPlayTime = 0.01f; // New variable for playing
        public int count = 0;
        public int index = 0;


        public void StartRecording(GameTime gt)
        {
            float timer = (float)gt.ElapsedGameTime.TotalSeconds;

            recTime -= timer;
            if(recTime < 0)
            {
                sounds.Add(null);

                recTime = maxRecTime;
            }
        }

        public void StopRecording() 
        {
            isRecording = false;
            count = sounds.Count - 1;
        }

        public void PlayRecording(GameTime gt)
        {
            float timer = (float)gt.ElapsedGameTime.TotalSeconds;

            playTime -= timer;
            if (playTime < 0)
            {
                if(index == count)
                {
                    isPlaying = false;
                    index = 0;
                }
                else if (sounds[index] == null)
                {
                    index++;
                }
                else if (index < count && sounds[index] != null)
                {
                    sounds[index].Play();
                    index++;
                }

                playTime = maxPlayTime;
            }

        }

        public void StopPlayBack()
        {
            isPlaying = false;
            count = 0;
            index = 0;
        }

        public void ClearRecording()
        {
            isPlaying = false;
            isRecording = false;
            count = 0;
            index = 0;
            sounds.Clear();
        }
    }
}
