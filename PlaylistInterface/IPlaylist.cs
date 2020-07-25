using System;
using System.Collections.Generic;
using System.Text;

namespace PlaylistInterface
{
    public interface IPlaylist
    {
        string Name { get; set; }
        string Path { get; set; }
        bool Selected { get; set; }
    }
}
