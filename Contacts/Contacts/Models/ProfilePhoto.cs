using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Models
{
    public class ProfilePhoto
    {
        public int ProfilePhotoId { get; set; }
        public byte[] Content { get; set; }
    }
}
