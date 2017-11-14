using Model;
using System;
using System.Linq;
using DataBase;

namespace Logic
{
    public class AuthSessions
    {
        private readonly AlphaBankEntities _db = new AlphaBankEntities();
        private readonly Serializers _srz = new Serializers();

        public bool AuthorizeAccess(string[] svcCredentials, string timestamp, string publicKey, string hash)
        {
            try
            {
                var user = new
                {
                    Name = svcCredentials[0],
                    Password = svcCredentials[1]
                };
                var query = @"select entity_id, entity_name, entity_description, entity_PublicKey, 
                                entity_PrivateKey, entity_user, entity_password
                                from dbo.Entity
                                where entity_user = '" + user.Name + "'" +
                                @"and entity_password = '" + user.Password + "'" +
                                @"and entity_PublicKey = '" + publicKey + "'";
                var entity = _db.Database.SqlQuery<GenericBusAuth>(query).FirstOrDefault();
                if (entity == null) return false;
                var recycledHash = _srz.CalculateMD5Hash(string.Concat(timestamp, entity.entity_publicKey, entity.entity_PrivateKey));
                return (recycledHash.Equals(hash));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AuthorizeAccess(string[] svcCredentials, string timestamp, string publicKey, string hash, string adminKey)
        {
            try
            {
                var user = new
                {
                    Name = svcCredentials[0],
                    Password = svcCredentials[1]
                };
                var query = @"select entity_id, entity_name, entity_description, entity_PublicKey, 
                                entity_PrivateKey, entity_user, entity_password
                                from dbo.Entity
                                where entity_user = '" + user.Name + "'" +
                                @"and entity_password = '" + user.Password + "'" +
                                @"and entity_PublicKey = '" + publicKey + "'" +
                                @"and entity_adminKey = '" + adminKey + "'";
                var entity = _db.Database.SqlQuery<GenericBusAuth>(query).FirstOrDefault();
                if (entity == null) return false;
                var recycledHash = _srz.CalculateMD5Hash(string.Concat(timestamp, entity.entity_publicKey, entity.entity_PrivateKey));
                return (recycledHash.Equals(hash));
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
