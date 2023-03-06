using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ButlerIsland
{
    //holds message for a dialogue box
    class Speaker
    {
        //public int AvatarIndex;
        public string Message;
        public Speaker(int avatar, string msg)
        {
            //AvatarIndex = avatar;
            Message = msg;
        }
        public Speaker(string msg)
        {
            Message = msg;
        }
    }
}
