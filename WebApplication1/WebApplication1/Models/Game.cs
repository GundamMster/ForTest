using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Game
    {
        public string GameName { get; set; }

        public Guid Id { get; set; }

        public string Player1Id { get; set; }

        public string Player2Id { get; set; }

        public string Grid { get; set; }

        public int Steps
        {
            get
            {
                return 9 - Grid.Count(e => e == '_');
            }
        }
    }
}