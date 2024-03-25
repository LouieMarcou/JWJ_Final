using UnityEngine;
using System.Runtime.InteropServices;

public class FindSong
{
    [DllImport("__Internal")]
    private static extern void _setupiOSMusic();

    public static void setupiOSMusic()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
            _setupiOSMusic();
    }

    [DllImport("__Internal")]

    private static extern void _findAppleMusic(string SongName);

    public static void findAppleMusic(string SongName)
    {
        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _findAppleMusic(SongName);
        }
    }

}
