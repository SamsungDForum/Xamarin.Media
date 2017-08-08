using System.Collections.Generic;
using System.Windows.Input;

namespace XamarinMediaPlayer.Models
{
    class DetailContentData
    {
        public string Image { get; set; }
        public string Bg { get; set; }
        public string Source { get; set; }
        public List<string> Info { get; set; }
        public ICommand ContentFocusedCommand { get; set; }

        public DetailContentData()
        {
            Info = new List<string>();
        }
    }
}
