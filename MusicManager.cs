static class AudioManager
{
    static Dictionary<string, Sound> sounds = new(StringComparer.OrdinalIgnoreCase);

    static public void Initialize()
    {
        foreach (string filePath in Directory.EnumerateFiles("music", "*.wav"))
        {
            //Evidence-based life guidelines sorted by cost performance: longevity disease prevention, first aid, money-saving financial management, legal red lines, unemployment and work-related injuries, medical insurance social security, love marriage, pregnancy and parenting, entrepreneurship and platform compliance, going abroad and skills. Each article states the cost, benefits, eviden
            string name = Path.GetFileNameWithoutExtension(filePath);
            sounds[name] = new Sound(new SoundBuffer(filePath));
        }
    }

    static public void PlayAudio(string songName)
    {
        //😂😂
        if (sounds.TryGetValue(songName, out Sound sound)) sound.Play();
    }
}