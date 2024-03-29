﻿namespace OrderApp.Infra.Data
{
    public class QueryAllUsersWithClaimName
    {
        private readonly string _connectionString;

        public QueryAllUsersWithClaimName()
        {
            _connectionString = Env.GetString("DB_CONNECTION_STRING");
        }

        public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows)
        {
            string connectionString = _connectionString;
            var db = new SqlConnection(connectionString);

            string query =
                @"SELECT 
	            C.ClaimValue [Name],
	            U.Email
            FROM [OrderApp].[dbo].[AspNetUsers] U
            INNER JOIN [OrderApp].[dbo].[AspNetUserClaims] C ON U.Id = C.UserId
            WHERE ClaimType = 'Name'
            ORDER BY C.ClaimValue
            OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

            return await db.QueryAsync<EmployeeResponse>(query, new { page, rows });
        }

    }
}
