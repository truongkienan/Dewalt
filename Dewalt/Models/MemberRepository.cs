using Dapper;
using System.Data;

namespace Dewalt.Models
{
    public class MemberRepository : BaseRepository
    {
        public MemberRepository(IDbConnection connection) : base(connection)
        {
        }
        public IEnumerable<Member> GetMembers()
        {
            return connection.Query<Member>("SELECT MemberId, Username, Email, Gender, Fullname, DateOfBirth FROM Member");
        }
        public Member GetMemberById(Guid id)
        {
            string sql = "SELECT MemberId, Username, Email, Gender, Fullname, DateOfBirth FROM Member WHERE MemberId=@Id";
            return connection.QuerySingleOrDefault<Member>(sql, new { Id = id });
        }
        public Member Login(LoginModel obj)
        {
            string sql = "SELECT MemberId, Username, Email, Gender FROM Member WHERE Password=@Password AND (Username =@Username OR Email=@Username)";
            return connection.QuerySingleOrDefault<Member>(sql, new
            {
                Username = obj.Username,
                Password = Helper.Hash(obj.Username + "@!#$" + obj.Password)
            });

        }
        public int Add(Member obj)
        {
            return connection.Execute("AddMember", new
            {
                Username = obj.Username,
                Password = Helper.Hash(obj.Username + "@!#$" + obj.Password),
                Email = obj.Email,
                Gender = obj.Gender,
                Fullname = obj.Fullname,
                DateOfBirth = obj.DateOfBirth
            }, commandType: CommandType.StoredProcedure);
        }
        public int Change(ChangeAccModel obj)
        {
            return connection.Execute("UPDATE Member SET Password=@NewPassword WHERE Username=@Username AND Password=@OldPassword", new
            {
                Username = obj.Username,
                OldPassword = Helper.Hash(obj.Username + "@!?^&#" + obj.OldPassword),
                NewPassword = Helper.Hash(obj.Username + "@!?^&#" + obj.NewPassword),
            });
        }
    }
}
