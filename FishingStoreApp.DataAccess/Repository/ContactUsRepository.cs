using FishingStoreApp.DataAccess.Data;
using FishingStoreApp.DataAccess.Repository.IRepository;
using FishingStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FishingStoreApp.DataAccess.Repository
{
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        private ApplicationDbContext _db;
        public ContactUsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ContactUs obj)
        {
            _db.ContactUs.Update(obj);
        }
    }
}
