using PlaylistInterface;
using System;

namespace PlaylistImplementation
{
    public class Playlist : IPlaylist
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool Selected { get; set; }
    }
}
