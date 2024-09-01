using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Domain
{
    // IEntity interface representing an entity with a key.
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }

}
