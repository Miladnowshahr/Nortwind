using Core.Concrete;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal: EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context=new NorthwindContext())
            {
                var result = (from oc in context.OperationClaim
                              join uoc in context.UserOperationClaim
                              on oc.Id equals uoc.OperationClaimId
                              where uoc.UserId == user.Id
                              select new OperationClaim { Id = oc.Id, Name = oc.Name }
                             );
                return result.ToList();
            }
        }
    }
}
