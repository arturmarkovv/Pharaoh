using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharaoh
{
    public interface ICard
    {
        public Quality Quality { get;}
        public Suit Suit { get;}
        public Guid? Owner { get;}

        public bool Equals(ICard second);
        public void ChangeOwner(Guid newOwnerGuid);
    }
}
