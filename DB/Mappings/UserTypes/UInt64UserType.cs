namespace DiscordMusicBot.DB.Mappings.UserTypes
{
    using NHibernate;
    using NHibernate.Engine;
    using NHibernate.SqlTypes;
    using NHibernate.UserTypes;
    using System;
    using System.Data.Common;

    public class UInt64UserType : IUserType
    {
        public SqlType[] SqlTypes => new SqlType[] { SqlTypeFactory.Int64 };

        public Type ReturnedType => typeof(Nullable<UInt64>);

        public bool IsMutable => false;

        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public new bool Equals(object x, object y)
        {
            if (Object.ReferenceEquals(x, y))
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            if (!(x is UInt64 && y is UInt64))
            {
                return false;
            }

            UInt64 a = (UInt64)x;
            UInt64 b = (UInt64)y;

            bool result = a == b;

            return result;
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            Int64? i = (Int64?)NHibernateUtil.Int64.NullSafeGet(rs, names[0], session);
            return (UInt64?)i;
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            UInt64? u = (UInt64?)value;
            Int64? i = (Int64?)u;
            NHibernateUtil.Int64.NullSafeSet(cmd, i, index, session);
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }
    }
}
