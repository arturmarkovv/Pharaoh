using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharaoh
{
    public interface IPlayer
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool IsAi { get; set; }
        public List<ICard> HandCards { get; set; }
        public void GetCard(ICard card);
        public void GetCard(ICard[] cards);
        public ICard MoveCard(int index);
        public Suit ChooseSuit(int index);
    }
}
