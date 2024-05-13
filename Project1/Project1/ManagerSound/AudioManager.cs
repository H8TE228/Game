using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

public static class AudioManager
{
    private static bool _isSoundEnabled = true;

    public static bool IsSoundEnabled
    {
        get { return _isSoundEnabled; }
        set { _isSoundEnabled = value; }
    }

    public static void PlaySoundEffect(SoundEffect soundEffect)
    {
        if (_isSoundEnabled)
        {
            soundEffect.Play();
        }
    }

    public static void PlayMusic(Song song)
    {
        if (_isSoundEnabled)
        {
            MediaPlayer.Play(song);
        }
    }

    public static void StopMusic()
    {
        MediaPlayer.Stop();
    }
}
